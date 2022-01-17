using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using MaterialDesignDemo.Domain;
using Microsoft.Win32;
using BluwolfIcons;
using System.Windows.Media.Imaging;
using System.IO;

namespace MaterialDesignDemo.Domain
{
    public class IconPackViewModel : ViewModelBase
    {
        private readonly Lazy<IEnumerable<PackIconKindGroup>> _packIconKinds;
        private readonly ISnackbarMessageQueue _snackbarMessageQueue;

        public IconPackViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            _snackbarMessageQueue = snackbarMessageQueue ?? throw new ArgumentNullException(nameof(snackbarMessageQueue));

            OpenDotComCommand = new AnotherCommandImplementation(OpenDotCom);
            SearchCommand = new AnotherCommandImplementation(Search);
            CopyToClipboardCommand = new AnotherCommandImplementation(CopyToClipboard);

            _packIconKinds = new Lazy<IEnumerable<PackIconKindGroup>>(() =>
                Enum.GetNames(typeof(PackIconKind))
                    .GroupBy(k => (PackIconKind)Enum.Parse(typeof(PackIconKind), k))
                    .Select(g => new PackIconKindGroup(g))
                    .OrderBy(x => x.Kind)
                    .ToList());

            var helper = new PaletteHelper();
            if (helper.GetThemeManager() is { } themeManager)
            {
                themeManager.ThemeChanged += ThemeManager_ThemeChanged;
            }
            SetDefaultIconColors();
        }

        private void ThemeManager_ThemeChanged(object? sender, ThemeChangedEventArgs e)
            => SetDefaultIconColors();

        public ICommand OpenDotComCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand CopyToClipboardCommand { get; }

        private IEnumerable<PackIconKindGroup>? _kinds;
        private PackIconKindGroup? _group;
        private string? _kind;
        private PackIconKind _packIconKind;

        public IEnumerable<PackIconKindGroup> Kinds
        {
            get => _kinds ??= _packIconKinds.Value;
            set => SetProperty(ref _kinds, value);
        }

        public PackIconKindGroup? Group
        {
            get => _group;
            set
            {
                if (SetProperty(ref _group, value))
                {
                    Kind = value?.Kind;
                }
            }
        }

        public string? Kind
        {
            get => _kind;
            set
            {
                if (SetProperty(ref _kind, value))
                {
                    PackIconKind = value != null ? (PackIconKind)Enum.Parse(typeof(PackIconKind), value) : default;
                }
            }
        }

        public PackIconKind PackIconKind
        {
            get => _packIconKind;
            set => SetProperty(ref _packIconKind, value);
        }

        private void OpenDotCom(object obj)
            => Link.OpenInBrowser("https://materialdesignicons.com/");

        private async void Search(object obj)
        {
            var text = obj as string;
            if (string.IsNullOrWhiteSpace(text))
            {
                Kinds = _packIconKinds.Value;
            }
            else
            {
                Kinds = await Task.Run(() => _packIconKinds.Value
                    .Where(x => x.Aliases.Any(a => a.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0))
                    .ToList());
            }
        }

        private void CopyToClipboard(object obj)
        {
            var toBeCopied = $"<materialDesign:PackIcon Kind=\"{obj}\" />";
            Clipboard.SetDataObject(toBeCopied);
            _snackbarMessageQueue.Enqueue(toBeCopied + " copied to clipboard");
        }

        private void SetDefaultIconColors()
        {
            var helper = new PaletteHelper();
            ITheme theme = helper.GetTheme();
            GeneratedIconBackground = theme.Paper;
            GeneratedIconForeground = theme.PrimaryMid.Color;
        }

        private Color _generatedIconBackground;
        public Color GeneratedIconBackground
        {
            get => _generatedIconBackground;
            set => SetProperty(ref _generatedIconBackground, value);
        }

        private Color _generatedIconForeground;
        public Color GeneratedIconForeground
        {
            get => _generatedIconForeground;
            set => SetProperty(ref _generatedIconForeground, value);
        }

        private ICommand? _saveIconCommand;
        public ICommand SaveIconCommand => _saveIconCommand ??= new AnotherCommandImplementation(OnSaveIcon);

        private void OnSaveIcon(object _)
        {
            var saveDialog = new SaveFileDialog
            {
                DefaultExt = ".ico",
                Title = "Save Icon (.ico)",
                Filter = "Icon Files|*.ico|All Files|*",
                CheckPathExists = true,
                OverwritePrompt = true,
                RestoreDirectory = true
            };
            if (saveDialog.ShowDialog() != true) return;

            var icon = new Icon();

            //TODO: Make this size list configurable
            foreach (var size in new[] { 256, 128, 64, 48, 32, 24, 16 })
            {
                RenderTargetBitmap bmp = RenderImage(size);
                icon.Images.Add(new BmpIconImage(bmp));
            }

            icon.Save(saveDialog.FileName);

            RenderTargetBitmap RenderImage(int size)
            {
                var packIcon = new PackIcon
                {
                    Kind = PackIconKind,
                    Background = new SolidColorBrush(Colors.Transparent),
                    Foreground = new SolidColorBrush(Color.FromRgb(0x17, 0x92, 0x87)),
                    Width = size,
                    Height = size,
                    Style = (Style)Application.Current.FindResource(typeof(PackIcon))
                };
                packIcon.Measure(new Size(size, size));
                packIcon.Arrange(new Rect(0, 0, size, size));
                packIcon.UpdateLayout();

                RenderTargetBitmap bmp = new(size, size, 96, 96, PixelFormats.Pbgra32);
                bmp.Render(packIcon);
                return bmp;
            }
        }
    }
}