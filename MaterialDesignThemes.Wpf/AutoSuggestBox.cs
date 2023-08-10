using System.Collections;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

public class AutoSuggestBox : TextBox
{
    #region Consts

    private const string AutoSuggestBoxListPart = "PART_AutoSuggestBoxList";

    #endregion

    #region Properties

    protected ListBox? _autoSuggestBoxList;

    #endregion

    #region Dependency Properties

    public IEnumerable Suggestions
    {
        get => (IEnumerable)GetValue(SuggestionsProperty);
        set => SetValue(SuggestionsProperty, value);
    }

    public static readonly DependencyProperty SuggestionsProperty =
        DependencyProperty.Register("Suggestions", typeof(IEnumerable), typeof(AutoSuggestBox), new PropertyMetadata(null));

    public bool IsAutoSuggestionEnabled
    {
        get => (bool)GetValue(IsAutoSuggestionEnabledProperty);
        set => SetValue(IsAutoSuggestionEnabledProperty, value);
    }

    public static readonly DependencyProperty IsAutoSuggestionEnabledProperty =
        DependencyProperty.Register("IsAutoSuggestionEnabled", typeof(bool), typeof(AutoSuggestBox), new PropertyMetadata(true, OnIsAutoSuggestionEnabledChanged));


    public string AutoSuggestBoxValueMember
    {
        get => (string)GetValue(AutoSuggestBoxValueMemberProperty);
        set => SetValue(AutoSuggestBoxValueMemberProperty, value);
    }
    public static readonly DependencyProperty AutoSuggestBoxValueMemberProperty =
        DependencyProperty.Register("AutoSuggestBoxValueMember", typeof(string), typeof(AutoSuggestBox), new PropertyMetadata(default(string)));


    public string AutoSuggestBoxDisplayMember
    {
        get => (string)GetValue(AutoSuggestBoxDisplayMemberProperty);
        set => SetValue(AutoSuggestBoxDisplayMemberProperty, value);
    }
    public static readonly DependencyProperty AutoSuggestBoxDisplayMemberProperty =
        DependencyProperty.Register("AutoSuggestBoxDisplayMember", typeof(string), typeof(AutoSuggestBox), new PropertyMetadata(default(string)));


    public CornerRadius AutoSuggestBoxCornerRadius
    {
        get => (CornerRadius)GetValue(AutoSuggestBoxCornerRadiusProperty);
        set => SetValue(AutoSuggestBoxCornerRadiusProperty, value);
    }
    public static readonly DependencyProperty AutoSuggestBoxCornerRadiusProperty =
        DependencyProperty.Register("AutoSuggestBoxCornerRadius", typeof(CornerRadius), typeof(AutoSuggestBox), new PropertyMetadata(default(CornerRadius)));

    public Brush AutoSuggestBoxBackground
    {
        get => (Brush)GetValue(AutoSuggestBoxBackgroundProperty);
        set => SetValue(AutoSuggestBoxBackgroundProperty, value);
    }
    public static readonly DependencyProperty AutoSuggestBoxBackgroundProperty =
        DependencyProperty.Register("AutoSuggestBoxBackground", typeof(Brush), typeof(AutoSuggestBox), new PropertyMetadata(default(Brush)));

    public DataTemplate AutoSuggestBoxItemTemplate
    {
        get => (DataTemplate)GetValue(AutoSuggestBoxItemTemplateProperty);
        set => SetValue(AutoSuggestBoxItemTemplateProperty, value);
    }
    public static readonly DependencyProperty AutoSuggestBoxItemTemplateProperty =
        DependencyProperty.Register("AutoSuggestBoxItemTemplate", typeof(DataTemplate), typeof(AutoSuggestBox), new PropertyMetadata(default(DataTemplate)));

    public Style AutoSuggestBoxItemContainerStyle
    {
        get => (Style)GetValue(AutoSuggestBoxItemContainerStyleProperty);
        set => SetValue(AutoSuggestBoxItemContainerStyleProperty, value);
    }
    public static readonly DependencyProperty AutoSuggestBoxItemContainerStyleProperty =
        DependencyProperty.Register("AutoSuggestBoxItemContainerStyle", typeof(Style), typeof(AutoSuggestBox), new PropertyMetadata(default(Style)));

    public Elevation AutoSuggestBoxElevation
    {
        get => (Elevation)GetValue(AutoSuggestBoxElevationProperty);
        set => SetValue(AutoSuggestBoxElevationProperty, value);
    }
    public static readonly DependencyProperty AutoSuggestBoxElevationProperty =
        DependencyProperty.Register("AutoSuggestBoxElevation", typeof(Elevation), typeof(AutoSuggestBox), new PropertyMetadata(default(Elevation)));

    public Brush AutoSuggestBoxBorderBrush
    {
        get => (Brush)GetValue(AutoSuggestBoxBorderBrushProperty);
        set => SetValue(AutoSuggestBoxBorderBrushProperty, value);
    }
    public static readonly DependencyProperty AutoSuggestBoxBorderBrushProperty =
        DependencyProperty.Register("AutoSuggestBoxBorderBrush", typeof(Brush), typeof(AutoSuggestBox), new PropertyMetadata(default(Brush)));

    public Thickness AutoSuggestBoxBorderThickness
    {
        get => (Thickness)GetValue(AutoSuggestBoxBorderThicknessProperty);
        set => SetValue(AutoSuggestBoxBorderThicknessProperty, value);
    }
    public static readonly DependencyProperty AutoSuggestBoxBorderThicknessProperty =
        DependencyProperty.Register("AutoSuggestBoxBorderThickness", typeof(Thickness), typeof(AutoSuggestBox), new PropertyMetadata(default(Thickness)));

    public double AutoSuggestBoxMaxHeight
    {
        get => (double)GetValue(AutoSuggestBoxMaxHeightProperty);
        set => SetValue(AutoSuggestBoxMaxHeightProperty, value);
    }
    public static readonly DependencyProperty AutoSuggestBoxMaxHeightProperty =
        DependencyProperty.Register("AutoSuggestBoxMaxHeight", typeof(double), typeof(AutoSuggestBox), new PropertyMetadata(200.0));


    public bool IsPopupOpen
    {
        get => (bool)GetValue(IsPopupOpenProperty);
        set => SetValue(IsPopupOpenProperty, value);
    }
    public static readonly DependencyProperty IsPopupOpenProperty =
        DependencyProperty.Register("IsPopupOpen", typeof(bool), typeof(AutoSuggestBox), new PropertyMetadata(default(bool)));


    public static readonly RoutedEvent SuggestionChosenEvent =
        EventManager.RegisterRoutedEvent(
            "SuggestionChosen",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<object>),
            typeof(AutoSuggestBox));

    public event RoutedPropertyChangedEventHandler<object> SuggestionChosen
    {
        add { AddHandler(SuggestionChosenEvent, value); }
        remove { RemoveHandler(SuggestionChosenEvent, value); }
    }

    #endregion

    #region Ctors

    static AutoSuggestBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoSuggestBox), new FrameworkPropertyMetadata(typeof(AutoSuggestBox)));
    }

    #endregion

    #region Override methods

    public override void OnApplyTemplate()
    {
        if (GetTemplateChild(AutoSuggestBoxListPart) is ListBox listBox)
        {
            _autoSuggestBoxList = listBox;

            listBox.PreviewMouseDown -= AutoSuggestionListBox_PreviewMouseDown;

            base.OnApplyTemplate();

            listBox.PreviewMouseDown += AutoSuggestionListBox_PreviewMouseDown;
        }
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        base.OnPreviewKeyDown(e);
        if (_autoSuggestBoxList is null) return;
        switch (e.Key)
        {
            case Key.Down:
                IncrementSelection();
                break;
            case Key.Up:
                DecrementSelection();
                break;
            case Key.Enter:
                CommitValueSelection();
                break;
            case Key.Escape:
                CloseAutoSuggestionPopUp();
                break;
            case Key.Tab:
                CommitValueSelection();
                break;
            default:
                return;
        }
        e.Handled = true;
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);
        CloseAutoSuggestionPopUp();
    }

    #endregion

    #region Callback handlers

    private static void OnIsAutoSuggestionEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is AutoSuggestBox instance)
            instance.IsPopupOpen = false;
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        base.OnTextChanged(e);
        if (_autoSuggestBoxList is null)
            return;
        if ((Text.Length == 0 || _autoSuggestBoxList.Items.Count == 0) && IsPopupOpen)
            IsPopupOpen = false;
        else if (Text.Length > 0 && !IsPopupOpen && IsFocused && _autoSuggestBoxList.Items.Count > 0)
            IsPopupOpen = true;
    }
    
    #endregion

    #region Methods

    private void AutoSuggestionListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (_autoSuggestBoxList is not null && e.OriginalSource is FrameworkElement element)
        {
            if (!_autoSuggestBoxList.Items.Contains(element.DataContext))
                return;
            _autoSuggestBoxList.SelectedItem = element.DataContext;
            CommitValueSelection();
        }
    }

    private void CloseAutoSuggestionPopUp()
    {
        IsPopupOpen = false;
    }

    private void CommitValueSelection()
    {
        if (_autoSuggestBoxList is not null && _autoSuggestBoxList.SelectedValue != null)
        {
            var oldValue = Text;
            Text = _autoSuggestBoxList.SelectedValue.ToString();
            if (Text != null)
                CaretIndex = Text.Length;
            CloseAutoSuggestionPopUp();
            var args = new RoutedPropertyChangedEventArgs<object?>(
            oldValue,
            Text
            )
            {
                RoutedEvent = SuggestionChosenEvent
            };
            RaiseEvent(args);
        }
    }

    private void DecrementSelection()
    {
        if (_autoSuggestBoxList is null)
            return;
        if (_autoSuggestBoxList.SelectedIndex == 0 || _autoSuggestBoxList.SelectedIndex == -1)
            _autoSuggestBoxList.SelectedIndex = _autoSuggestBoxList.Items.Count - 1;
        else
            _autoSuggestBoxList.SelectedIndex -= 1;
        _autoSuggestBoxList.ScrollIntoView(_autoSuggestBoxList.SelectedItem);
    }

    private void IncrementSelection()
    {
        if (_autoSuggestBoxList is null)
            return;
        if (_autoSuggestBoxList.SelectedIndex == _autoSuggestBoxList.Items.Count - 1)
            _autoSuggestBoxList.SelectedIndex = 0;
        else
            _autoSuggestBoxList.SelectedIndex += 1;
        _autoSuggestBoxList.ScrollIntoView(_autoSuggestBoxList.SelectedItem);
    }

    #endregion
}
