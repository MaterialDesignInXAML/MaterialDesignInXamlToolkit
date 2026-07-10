using System.Collections;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

[TemplatePart(Name = AutoSuggestBoxListPart, Type = typeof(ListBox))]
public class AutoSuggestBox : TextBox
{
    public static bool? GetIsInteractiveElement(DependencyObject obj)
        => (bool?)obj.GetValue(IsInteractiveElementProperty);

    public static void SetIsInteractiveElement(DependencyObject obj, bool? value)
        => obj.SetValue(IsInteractiveElementProperty, value);

    public static readonly DependencyProperty IsInteractiveElementProperty =
        DependencyProperty.RegisterAttached("IsInteractiveElement", typeof(bool?), typeof(AutoSuggestBox), new PropertyMetadata(null));

    private const string AutoSuggestBoxListPart = "PART_AutoSuggestBoxList";

    protected ListBox? _autoSuggestBoxList;

    #region Dependency Properties

    public IEnumerable? Suggestions
    {
        get => (IEnumerable)GetValue(SuggestionsProperty);
        set => SetValue(SuggestionsProperty, value);
    }

    public static readonly DependencyProperty SuggestionsProperty =
        DependencyProperty.Register(nameof(Suggestions), typeof(IEnumerable), typeof(AutoSuggestBox), new PropertyMetadata(null));


    public string ValueMember
    {
        get => (string)GetValue(ValueMemberProperty);
        set => SetValue(ValueMemberProperty, value);
    }
    public static readonly DependencyProperty ValueMemberProperty =
        DependencyProperty.Register(nameof(ValueMember), typeof(string), typeof(AutoSuggestBox), new PropertyMetadata(default(string)));


    public string DisplayMember
    {
        get => (string)GetValue(DisplayMemberProperty);
        set => SetValue(DisplayMemberProperty, value);
    }
    public static readonly DependencyProperty DisplayMemberProperty =
        DependencyProperty.Register(nameof(DisplayMember), typeof(string), typeof(AutoSuggestBox), new PropertyMetadata(default(string)));

    public Brush DropDownBackground
    {
        get => (Brush)GetValue(DropDownBackgroundProperty);
        set => SetValue(DropDownBackgroundProperty, value);
    }
    public static readonly DependencyProperty DropDownBackgroundProperty =
        DependencyProperty.Register(nameof(DropDownBackground), typeof(Brush), typeof(AutoSuggestBox), new PropertyMetadata(default(Brush)));

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }
    public static readonly DependencyProperty ItemTemplateProperty =
        DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(AutoSuggestBox), new PropertyMetadata(default(DataTemplate)));

    public Style ItemContainerStyle
    {
        get => (Style)GetValue(ItemContainerStyleProperty);
        set => SetValue(ItemContainerStyleProperty, value);
    }
    public static readonly DependencyProperty ItemContainerStyleProperty =
        DependencyProperty.Register(nameof(ItemContainerStyle), typeof(Style), typeof(AutoSuggestBox), new PropertyMetadata(default(Style)));

    public Elevation DropDownElevation
    {
        get => (Elevation)GetValue(DropDownElevationProperty);
        set => SetValue(DropDownElevationProperty, value);
    }
    public static readonly DependencyProperty DropDownElevationProperty =
        DependencyProperty.Register(nameof(DropDownElevation), typeof(Elevation), typeof(AutoSuggestBox), new PropertyMetadata(default(Elevation)));

    public double DropDownMaxHeight
    {
        get => (double)GetValue(DropDownMaxHeightProperty);
        set => SetValue(DropDownMaxHeightProperty, value);
    }
    public static readonly DependencyProperty DropDownMaxHeightProperty =
        DependencyProperty.Register(nameof(DropDownMaxHeight), typeof(double), typeof(AutoSuggestBox), new PropertyMetadata(200.0));


    public bool IsSuggestionOpen
    {
        get => (bool)GetValue(IsSuggestionOpenProperty);
        set => SetValue(IsSuggestionOpenProperty, value);
    }
    public static readonly DependencyProperty IsSuggestionOpenProperty =
        DependencyProperty.Register(nameof(IsSuggestionOpen), typeof(bool), typeof(AutoSuggestBox), new PropertyMetadata(default(bool)));

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(object),
            typeof(AutoSuggestBox),
            new FrameworkPropertyMetadata(default(object), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    public object SelectedValue
    {
        get => GetValue(SelectedValueProperty);
        set => SetValue(SelectedValueProperty, value);
    }
    public static readonly DependencyProperty SelectedValueProperty =
        DependencyProperty.Register(nameof(SelectedValue), typeof(object), typeof(AutoSuggestBox), new PropertyMetadata(default(object)));

    public static readonly RoutedEvent SuggestionChosenEvent =
        EventManager.RegisterRoutedEvent(
            nameof(SuggestionChosen),
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<object>),
            typeof(AutoSuggestBox));

    public event RoutedPropertyChangedEventHandler<object> SuggestionChosen
    {
        add => AddHandler(SuggestionChosenEvent, value);
        remove => RemoveHandler(SuggestionChosenEvent, value);
    }

    public bool ShowSuggestionsOnFocus
    {
        get => (bool)GetValue(ShowSuggestionsOnFocusProperty);
        set => SetValue(ShowSuggestionsOnFocusProperty, value);
    }

    public static readonly DependencyProperty ShowSuggestionsOnFocusProperty =
        DependencyProperty.Register(
            nameof(ShowSuggestionsOnFocus),
            typeof(bool),
            typeof(AutoSuggestBox),
            new PropertyMetadata(false));

    #endregion

    protected override void OnGotFocus(RoutedEventArgs e)
    {
        base.OnGotFocus(e);

        if (ShowSuggestionsOnFocus &&
            _autoSuggestBoxList is not null &&
            _autoSuggestBoxList.Items.Count > 0 &&
            !IsSuggestionOpen)
        {
            IsSuggestionOpen = true;
        }
    }
    static AutoSuggestBox() => DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoSuggestBox), new FrameworkPropertyMetadata(typeof(AutoSuggestBox)));

    #region Override methods

    public override void OnApplyTemplate()
    {
        _autoSuggestBoxList?.PreviewMouseDown -= AutoSuggestionListBox_PreviewMouseDown;

        if (GetTemplateChild(AutoSuggestBoxListPart) is ListBox listBox)
        {
            _autoSuggestBoxList = listBox;

            base.OnApplyTemplate();

            listBox.PreviewMouseDown += AutoSuggestionListBox_PreviewMouseDown;
        }
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
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
        base.OnPreviewKeyDown(e);
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);
        if (_autoSuggestBoxList is { } list &&
            (list.IsKeyboardFocused || list.IsKeyboardFocusWithin))
        {
            return;
        }
        CloseAutoSuggestionPopUp();
    }
    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        base.OnTextChanged(e);
        if (_autoSuggestBoxList is null)
            return;

        bool hasItems = _autoSuggestBoxList.Items.Count > 0;
        bool isEmpty = string.IsNullOrEmpty(Text);

        bool shouldOpen =
            IsFocused &&
            hasItems &&
            (ShowSuggestionsOnFocus || !isEmpty);

        bool shouldClose =
            !hasItems ||
            (!ShowSuggestionsOnFocus && isEmpty);

        if (shouldOpen)
        {
            IsSuggestionOpen = true;
        }
        else if (shouldClose)
        {
            IsSuggestionOpen = false;
        }
    }

    #endregion

    #region Callback handlers

    private void AutoSuggestionListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (_autoSuggestBoxList is null || e.OriginalSource is not FrameworkElement element)
            return;

        // If the user clicked on an interactive element, let it handle the event.
        if (IsInteractiveElement(element))
        {
            return;
        }

        var selectedItem = element.DataContext;
        if (!_autoSuggestBoxList.Items.Contains(selectedItem))
            return;

        e.Handled = true;

        if (!Equals(_autoSuggestBoxList.SelectedItem, selectedItem))
        {
            void OnSelectionChanged(object s, SelectionChangedEventArgs args)
            {
                _autoSuggestBoxList.SelectionChanged -= OnSelectionChanged;
                CommitValueSelection();
            }

            _autoSuggestBoxList.SelectionChanged += OnSelectionChanged;
            _autoSuggestBoxList.SelectedItem = selectedItem;
        }
        else
        {
            _autoSuggestBoxList.SelectedItem = selectedItem;
            CommitValueSelection();
        }
    }



    #endregion

    #region Methods
    private bool IsInteractiveElement(DependencyObject? element)
    {
        return element.GetVisualAncestry()
            .Where(x => x != this)
            .Select(IsInteractive)
            .FirstOrDefault(x => x is not null) ?? false;

        static bool? IsInteractive(DependencyObject element)
        {
            if (GetIsInteractiveElement(element) is bool isInteractiveElement)
            {
                return isInteractiveElement;
            }
            if (element is ButtonBase or TextBoxBase or ComboBox or Hyperlink)
            {
                return true;
            }
            return null;
        }
    }

    private void CloseAutoSuggestionPopUp()
    {
        IsSuggestionOpen = false;
    }

    private bool CommitValueSelection()
        => CommitValueSelection(_autoSuggestBoxList?.SelectedValue);

    private bool CommitValueSelection(object? selectedValue)
    {
        if (IsSuggestionOpen == false)
        {
            return false;
        }

        string oldValue = Text;
        Text = selectedValue?.ToString();
        if (Text != null)
        {
            CaretIndex = Text.Length;
        }
        SetCurrentValue(SelectedItemProperty, selectedValue);
        CloseAutoSuggestionPopUp();
        var args = new RoutedPropertyChangedEventArgs<object?>(oldValue, Text)
        {
            RoutedEvent = SuggestionChosenEvent
        };
        RaiseEvent(args);
        return true;
    }

    private void DecrementSelection()
    {
        if (_autoSuggestBoxList is null || Suggestions is null)
            return;
        ICollectionView collectionView = CollectionViewSource.GetDefaultView(Suggestions);

        // If we're at the first item, wrap around to the last.
        if (collectionView.CurrentPosition == 0)
            collectionView.MoveCurrentToLast();
        else
            collectionView.MoveCurrentToPrevious();
        _autoSuggestBoxList.ScrollIntoView(_autoSuggestBoxList.SelectedItem);
    }

    private void IncrementSelection()
    {
        if (_autoSuggestBoxList is null || Suggestions is null)
            return;
        ICollectionView collectionView = CollectionViewSource.GetDefaultView(Suggestions);
        int itemCount = GetItemCount(collectionView);

        // If we're at the last item, wrap around to the first.
        if (collectionView.CurrentPosition == itemCount - 1)
            collectionView.MoveCurrentToFirst();
        else
            collectionView.MoveCurrentToNext();
        _autoSuggestBoxList.ScrollIntoView(_autoSuggestBoxList.SelectedItem);
    }

    private static int GetItemCount(ICollectionView collectionView)
    {
        if (collectionView is ListCollectionView lcv)
        {
            return lcv.Count;
        }
        return collectionView.Cast<object>().Count();
    }

    #endregion
}
