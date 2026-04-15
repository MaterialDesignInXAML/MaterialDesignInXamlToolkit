using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

public class PackIcon : Control
{
    private static readonly Lazy<IDictionary<PackIconKind, string>> _dataIndex
        = new Lazy<IDictionary<PackIconKind, string>>(PackIconDataFactory.Create);

    static PackIcon()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIcon), new FrameworkPropertyMetadata(typeof(PackIcon)));
    }

    public static readonly DependencyProperty KindProperty
        = DependencyProperty.Register(nameof(Kind), typeof(PackIconKind), typeof(PackIcon), new PropertyMetadata(default(PackIconKind), KindPropertyChangedCallback));

    private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        => ((PackIcon)dependencyObject).UpdateData();

    /// <summary>
    /// Gets or sets the icon to display.
    /// </summary>
    public PackIconKind Kind
    {
        get => (PackIconKind)GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }

    private static readonly DependencyPropertyKey DataPropertyKey
        = DependencyProperty.RegisterReadOnly(nameof(Data), typeof(string), typeof(PackIcon), new PropertyMetadata(""));

    // ReSharper disable once StaticMemberInGenericType
    public static readonly DependencyProperty DataProperty = DataPropertyKey.DependencyProperty;

    /// <summary>
    /// Gets the icon path data for the current <see cref="Kind"/>.
    /// </summary>
    [TypeConverter(typeof(GeometryConverter))]
    public string? Data
    {
        get => (string?)GetValue(DataProperty);
        private set => SetValue(DataPropertyKey, value);
    }

    public static readonly DependencyProperty MatchSizeWithProperty =
    DependencyProperty.Register(
        nameof(MatchSizeWith),
        typeof(FrameworkElement),
        typeof(PackIcon),
        new PropertyMetadata(null, OnMatchSizeWithChanged));

    public FrameworkElement? MatchSizeWith
    {
        get => (FrameworkElement?)GetValue(MatchSizeWithProperty);
        set => SetValue(MatchSizeWithProperty, value);
    }

    private static void OnMatchSizeWithChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var icon = (PackIcon)d;

        // Clear old bindings
        BindingOperations.ClearBinding(icon, HeightProperty);
        BindingOperations.ClearBinding(icon, WidthProperty);

        if (e.NewValue is FrameworkElement source)
        {
            var heightBinding = new Binding(nameof(FrameworkElement.ActualHeight))
            {
                Source = source,
                Mode = BindingMode.OneWay
            };

            icon.SetBinding(HeightProperty, heightBinding);
            icon.SetBinding(WidthProperty, heightBinding); // Bind the Width to the height, so that the icon is always rectangular
        }
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        UpdateData();
    }

    private void UpdateData()
    {
        string? data = null;
        _dataIndex.Value?.TryGetValue(Kind, out data);
        Data = data;
    }
}
