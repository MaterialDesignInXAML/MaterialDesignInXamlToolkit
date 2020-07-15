using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using Google.Protobuf;
using Grpc.Core;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;

namespace VTTests
{
    internal class VisualTreeService : Protocol.ProtocolBase
    {
        private static Guid Initialized { get; } = Guid.NewGuid();

        private List<Assembly> LoadedAssemblies { get; } = new List<Assembly>();

        private Application Application { get; }

        private IDictionary<string, WeakReference<DependencyObject>> KnownElements { get; }
            = new Dictionary<string, WeakReference<DependencyObject>>();

        public VisualTreeService(Application application)
            => Application = application ?? throw new ArgumentNullException(nameof(application));

        public override async Task<GetWindowsResult> GetWindows(GetWindowsQuery request, ServerCallContext context)
        {
            var ids = await Application.Dispatcher.InvokeAsync(() =>
            {
                return Application.Windows
                    .Cast<Window>()
                    .Select(window => VTTest.GetOrSetId(window, KnownElements))
                    .ToList();
            });

            var reply = new GetWindowsResult();
            reply.WindowIds.AddRange(ids);
            return reply;
        }

        public override async Task<GetWindowsResult> GetMainWindow(GetWindowsQuery request, ServerCallContext context)
        {
            string id = await Application.Dispatcher.InvokeAsync(() =>
            {
                return VTTest.GetOrSetId(Application.MainWindow, KnownElements);
            });

            var reply = new GetWindowsResult();
            reply.WindowIds.Add(id);
            return reply;
        }

        public override async Task<ElementResult> GetElement(ElementQuery request, ServerCallContext context)
        {
            var reply = new ElementResult();

            await Application.Dispatcher.InvokeAsync(() =>
            {
                FrameworkElement? searchRoot = GetParentElement();

                if (searchRoot is null) return;

                if (!string.IsNullOrWhiteSpace(request.Query))
                {
                    if (!(EvaluateQuery(searchRoot, request.Query) is DependencyObject element))
                    {
                        reply.ErrorMessages.Add($"Failed to find element by query '{request.Query}' in '{searchRoot.GetType().FullName}'");
                        return;
                    }

                    string id = VTTest.GetOrSetId(element, KnownElements);
                    reply.ElementIds.Add(id);
                    return;
                }

                reply.ErrorMessages.Add($"{nameof(ElementQuery)} did not specify a query");
            });
            return reply;

            FrameworkElement? GetParentElement()
            {
                if (!string.IsNullOrWhiteSpace(request.WindowId))
                {
                    Window? window = GetCachedElement<Window>(request.WindowId);
                    if (window is null)
                    {
                        reply!.ErrorMessages.Add("Failed to find parent window");
                    }
                    return window;
                }
                if (!string.IsNullOrWhiteSpace(request.ParentId))
                {
                    FrameworkElement? element = GetCachedElement<FrameworkElement>(request.ParentId);
                    if (element is null)
                    {
                        reply!.ErrorMessages.Add("Failed to find parent element");
                    }
                    return element;
                }
                reply!.ErrorMessages.Add("No parent element specified as part of the query");
                return null;
            }
        }

        public override async Task<PropertyResult> GetProperty(PropertyQuery request, ServerCallContext context)
        {
            var reply = new PropertyResult();
            await Application.Dispatcher.InvokeAsync(() =>
            {
                DependencyObject? element = GetCachedElement<DependencyObject>(request.ElementId);
                if (element is null)
                {
                    reply.ErrorMessages.Add("Could not find element");
                    return;
                }

                var properties = TypeDescriptor.GetProperties(element);
                PropertyDescriptor? foundProperty = properties.Find(request.Name, false);
                if (foundProperty is null)
                {
                    reply.ErrorMessages.Add($"Could not find property with name '{request.Name}'");
                    return;
                }

                object value = foundProperty.GetValue(element);
                reply.PropertyType = foundProperty.PropertyType.AssemblyQualifiedName;
                reply.ValueType = value?.GetType().AssemblyQualifiedName;
                reply.Value = value?.ToString();
            });
            return reply;
        }

        public override async Task<EffectiveBackgroundResult> GetEffectiveBackground(EffectiveBackgroundQuery request, ServerCallContext context)
        {
            var reply = new EffectiveBackgroundResult();
            await Application.Dispatcher.InvokeAsync(() =>
            {
                DependencyObject? element = GetCachedElement<DependencyObject>(request.ElementId);
                if (element is null)
                {
                    reply.ErrorMessages.Add("Could not find element");
                    return;
                }

                Color backgroundColor = Colors.Transparent;
                foreach (var ancestor in Ancestors<FrameworkElement>(element))
                {
                    Brush background;
                    switch (ancestor)
                    {
                        case Border border:
                            background = border.Background;
                            break;
                        case Control control:
                            background = control.Background;
                            break;
                        default: continue;
                    }

                    if (background is SolidColorBrush brush)
                    {
                        Color foreground = brush.Color.WithOpacity(ancestor.Opacity);
                        backgroundColor = foreground.FlattenOnto(backgroundColor);
                        if (backgroundColor.A == byte.MaxValue)
                        {
                            break;
                        }
                    }
                    else if (background != null)
                    {
                        reply.ErrorMessages.Add($"Could not evaluate background brush of type '{background.GetType().Name}' on '{ancestor.GetType().FullName}'");
                        break;
                    }
                }
                reply.Alpha = backgroundColor.A;
                reply.Red = backgroundColor.R;
                reply.Green = backgroundColor.G;
                reply.Blue = backgroundColor.B;
            });
            return reply;
        }

        public override async Task<PropertyResult> SetProperty(SetPropertyRequest request, ServerCallContext context)
        {
            var reply = new PropertyResult();
            await Application.Dispatcher.InvokeAsync(() =>
            {
                DependencyObject? element = GetCachedElement<DependencyObject>(request.ElementId);
                if (element is null)
                {
                    reply.ErrorMessages.Add("Could not find element");
                    return;
                }

                var properties = TypeDescriptor.GetProperties(element);
                PropertyDescriptor foundProperty = properties.Find(request.Name, false);
                if (foundProperty is null)
                {
                    reply.ErrorMessages.Add($"Could not find property with name '{request.Name}'");
                    return;
                }

                object? value = null;
                switch (request.ValueType)
                {
                    case Types.XamlString:
                        try
                        {
                            value = LoadXaml<object>(request.Value);
                        }
                        catch (Exception ex)
                        {
                            reply.ErrorMessages.Add(ex.Message);
                            return;
                        }
                        break;
                    default:
                        TypeConverter propertyTypeConverter = string.IsNullOrWhiteSpace(request.ValueType)
                            ? foundProperty.Converter
                            : TypeDescriptor.GetConverter(System.Type.GetType(request.ValueType));
                        value = propertyTypeConverter.ConvertFromString(request.Value);
                        break;
                }


                foundProperty.SetValue(element, value);

                //Re-retrive the value in case the dependency property coalesced it
                value = foundProperty.GetValue(element);
                reply.PropertyType = foundProperty.PropertyType.AssemblyQualifiedName;
                reply.ValueType = value?.GetType().AssemblyQualifiedName;
                reply.Value = value?.ToString();
            });
            return reply;
        }

        public override async Task<ResourceResult> GetResource(ResourceQuery request, ServerCallContext context)
        {
            var reply = new ResourceResult();
            await Application.Dispatcher.InvokeAsync(() =>
            {
                FrameworkElement? element = GetCachedElement<FrameworkElement>(request.ElementId);
                object resourceValue = element is null ?
                    Application.TryFindResource(request.Key) :
                    element.TryFindResource(request.Key);

                reply.Value = resourceValue?.ToString();
                reply.ValueType = resourceValue?.GetType().AssemblyQualifiedName;
                reply.Key = request.Key;
            });

            return reply;
        }

        public override async Task<CoordinatesResult> GetCoordinates(CoordinatesQuery request, ServerCallContext context)
        {
            var reply = new CoordinatesResult();
            await Application.Dispatcher.InvokeAsync(() =>
            {
                DependencyObject? dependencyObject = GetCachedElement<DependencyObject>(request.ElementId);
                if (dependencyObject is null)
                {
                    reply.ErrorMessages.Add("Could not find element");
                    return;
                }

                if (dependencyObject is FrameworkElement element)
                {
                    Point topLeft = element.PointToScreen(new Point(0, 0));
                    Point bottomRight = element.PointToScreen(new Point(element.ActualWidth, element.ActualHeight));
                    reply.Left = topLeft.X;
                    reply.Top = topLeft.Y;
                    reply.Right = bottomRight.X;
                    reply.Bottom = bottomRight.Y;
                }
                else
                {
                    reply.ErrorMessages.Add($"Element of type '{dependencyObject.GetType().FullName}' is not a {nameof(FrameworkElement)}");
                }
            });
            return reply;
        }

        public override async Task<ApplicationResult> InitializeApplication(ApplicationConfiguration request, ServerCallContext context)
        {
            var reply = new ApplicationResult();
            await Application.Dispatcher.InvokeAsync(() =>
            {
                if (Application.Resources[Initialized] is Guid value &&
                    value == Initialized)
                {
                    reply.ErrorMessages.Add("Application has already been initialized");
                }
                else
                {
                    Application.Resources[Initialized] = Initialized;
                    AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

                    foreach (string? assembly in (IEnumerable<string?>)request.AssembliesToLoad ?? Array.Empty<string?>())
                    {
                        try
                        {
                            if (assembly is string)
                            {
                                LoadedAssemblies.Add(Assembly.LoadFile(assembly));
                            }
                            else
                            {
                                reply.ErrorMessages.Add("Assemblies names must not be null");
                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            reply.ErrorMessages.Add($"Failed to load '{assembly}'{Environment.NewLine}{e}");
                        }
                    }

                    if (request.ApplicationResourceXaml is { } xaml)
                    {
                        try
                        {
                            ResourceDictionary appResourceDictionary = LoadXaml<ResourceDictionary>(xaml);
                            foreach (var mergedDictionary in appResourceDictionary.MergedDictionaries)
                            {
                                Application.Resources.MergedDictionaries.Add(mergedDictionary);
                            }
                            foreach (object? key in appResourceDictionary.Keys)
                            {
                                Application.Resources.Add(key, appResourceDictionary[key]);
                            }
                        }
                        catch (Exception e)
                        {
                            reply.ErrorMessages.Add($"Error loading application resources{Environment.NewLine}{e}");
                        }
                    }
                }
            });
            return reply;
        }

        public override async Task<WindowResult> CreateWindow(WindowConfiguration request, ServerCallContext context)
        {
            var reply = new WindowResult();
            await Application.Dispatcher.InvokeAsync(() =>
            {
                Window? window = null;
                try
                {
                    window = LoadXaml<Window>(request.Xaml);
                }
                catch (Exception e)
                {
                    reply.ErrorMessages.Add($"Error loading window{Environment.NewLine}{e}");
                }
                if (window is { })
                {
                    reply.WindowsId = VTTest.GetOrSetId(window, KnownElements);
                    window.Show();
                }
                else
                {
                    reply.ErrorMessages.Add("Failed to load window");
                }
            });
            return reply;
        }

        public override async Task<ImageResult> GetImage(ImageQuery request, ServerCallContext context)
        {
            var reply = new ImageResult();
            await Application.Dispatcher.InvokeAsync(async () =>
            {
                FrameworkElement? element = GetCachedElement<FrameworkElement>(request.ElementId);
                if (element is null)
                {
                    reply.ErrorMessages.Add("Could not find element");
                    return;
                }

                Point topLeft = element.PointToScreen(new Point(0, 0));
                Point bottomRight = element.PointToScreen(new Point(element.ActualWidth, element.ActualHeight));

                int left = (int)Math.Floor(topLeft.X);
                int top = (int)Math.Floor(topLeft.Y);
                int width = (int)Math.Ceiling(bottomRight.X - topLeft.X);
                int height = (int)Math.Ceiling(bottomRight.Y - topLeft.Y);

                using var screenBmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using var bmpGraphics = Graphics.FromImage(screenBmp);
                bmpGraphics.CopyFromScreen(left, top, 0, 0, new System.Drawing.Size(width, height));
                using var ms = new MemoryStream();
                screenBmp.Save(ms, ImageFormat.Bmp);
                ms.Position = 0;
                reply.Data = await ByteString.FromStreamAsync(ms);
            });
            return reply;
        }

        private Assembly? CurrentDomain_AssemblyResolve(object? sender, ResolveEventArgs args)
        {
            Assembly? found = LoadedAssemblies.FirstOrDefault(x => x.GetName().FullName == args.Name);
            if (found is { }) return found;

            var assemblyName = new AssemblyName(args.Name!);
            string likelyAssemblyPath = Path.GetFullPath($"{assemblyName.Name}.dll");
            try
            {
                if (File.Exists(likelyAssemblyPath) && Assembly.LoadFile(likelyAssemblyPath) is Assembly localAssemby)
                {
                    LoadedAssemblies.Add(localAssemby);
                    return localAssemby;
                }
            }
            catch(Exception)
            {
                //Ignored
            }
            return null;
        }

        private object? EvaluateQuery(DependencyObject root, string query)
        {
            object? result = null;
            List<string> errorParts = new List<string>();
            DependencyObject? current = root;

            while (query.Length > 0)
            {
                if (current is null)
                {
                    throw new Exception($"Could not resolve '{query}' on null element");
                }

                switch (GetNextQueryType(ref query, out string value))
                {
                    case QueryPartType.Name:
                        result = EvaluateNameQuery(current, value);
                        break;
                    case QueryPartType.Property:
                        result = EvaluatePropertyQuery(current, value);
                        break;
                    case QueryPartType.ChildType:
                        result = EvaluateChildTypeQuery(current, value);
                        break;
                }
                current = result as DependencyObject;
            }

            return result;

            static QueryPartType GetNextQueryType(ref string query, out string value)
            {
                var regex = new Regex(@"(?<=.)[\.\/\~]");

                Match match = regex.Match(query);

                string currentQuery = query;
                if (match.Success)
                {
                    currentQuery = query.Substring(0, match.Index);
                    query = query.Substring(match.Index);
                }
                else
                {
                    query = "";
                }

                QueryPartType rv;
                if (currentQuery.StartsWith('.'))
                {
                    value = currentQuery.Substring(1);
                    rv = QueryPartType.Property;
                }
                else if (currentQuery.StartsWith('/'))
                {
                    value = currentQuery.Substring(1);
                    rv = QueryPartType.ChildType;
                }
                else
                {
                    if (currentQuery.StartsWith('~'))
                    {
                        value = currentQuery.Substring(1);
                    }
                    else
                    {
                        value = currentQuery;
                    }
                    rv = QueryPartType.Name;
                }
                return rv;
            }

            static object EvaluateNameQuery(DependencyObject root, string name)
            {
                return Decendants<FrameworkElement>(root).FirstOrDefault(x => x.Name == name);
            }

            static object EvaluatePropertyQuery(DependencyObject root, string property)
            {
                var properties = TypeDescriptor.GetProperties(root);
                if (properties.Find(property, false) is PropertyDescriptor propertyDescriptor)
                {
                    return propertyDescriptor.GetValue(root);
                }
                throw new Exception($"Failed to find property '{property}' on element of type '{root.GetType().FullName}'");
            }

            static object EvaluateChildTypeQuery(DependencyObject root, string childTypeQuery)
            {
                var indexerRegex = new Regex(@"\[(?<Index>\d+)]$");

                int index = 0;
                Match match = indexerRegex.Match(childTypeQuery);
                if (match.Success)
                {
                    index = int.Parse(match.Groups["Index"].Value);
                    childTypeQuery = childTypeQuery.Substring(0, match.Index);
                }

                foreach (DependencyObject child in Decendants<DependencyObject>(root))
                {
                    if (child.GetType().Name == childTypeQuery)
                    {
                        if (index == 0)
                        {
                            return child;
                        }
                        index--;
                    }
                }
                throw new Exception($"Failed to find child of type '{childTypeQuery}'");
            }
        }

        private enum QueryPartType
        {
            None,
            Name,
            Property,
            ChildType
        }

        private T LoadXaml<T>(string xaml) where T : class
        {
            using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xaml));
            return (T)XamlReader.Load(memoryStream);
        }

        private static IEnumerable<T> Decendants<T>(DependencyObject? parent)
            where T : DependencyObject
        {
            if (parent is null) yield break;

            var queue = new Queue<DependencyObject>();
            queue.Enqueue(parent);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current is T match) yield return match;

                int childrenCount = VisualTreeHelper.GetChildrenCount(current);
                for (int i = 0; i < childrenCount; i++)
                {
                    if (VisualTreeHelper.GetChild(current, i) is DependencyObject child)
                    {
                        queue.Enqueue(child);
                    }
                }
                if (childrenCount == 0)
                {
                    foreach(object? logicalChild in LogicalTreeHelper.GetChildren(current))
                    {
                        if (logicalChild is DependencyObject child)
                        {
                            queue.Enqueue(child);
                        }
                    }
                }
            }
        }

        private static IEnumerable<T> Ancestors<T>(DependencyObject? element)
            where T : DependencyObject
        {
            for (; element != null; element = VisualTreeHelper.GetParent(element))
            {
                if (element is T typedElement)
                {
                    yield return typedElement;
                }
            }
        }

        private TElement? GetCachedElement<TElement>(string id)
            where TElement : DependencyObject
        {
            if (string.IsNullOrWhiteSpace(id)) return default;
            lock (KnownElements)
            {
                if (KnownElements.TryGetValue(id, out WeakReference<DependencyObject>? weakRef) &&
                    weakRef.TryGetTarget(out DependencyObject? element))
                {
                    return element as TElement;
                }
            }
            return null;
        }
    }
}
