using System.ComponentModel;
using System.Globalization;
using System.Windows.Automation.Peers;

namespace MaterialDesignThemes.Wpf;

[TemplatePart(Name = IncreaseButtonPartName, Type = typeof(RepeatButton))]
[TemplatePart(Name = DecreaseButtonPartName, Type = typeof(RepeatButton))]
[TemplatePart(Name = TextFieldBoxPartName, Type = typeof(TextBox))]
public class NumericUpDown : Control
{
    public const string IncreaseButtonPartName = "PART_IncreaseButton";
    public const string DecreaseButtonPartName = "PART_DecreaseButton";
    public const string TextFieldBoxPartName = "PART_TextBoxField";

    private TextBox? _textBoxField;
    private RepeatButton? _decreaseButton;
    private RepeatButton? _increaseButton;

    static NumericUpDown()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
    }

    #region DependencyProperties



    public object? IncreaseContent
    {
        get => GetValue(IncreaseContentProperty);
        set => SetValue(IncreaseContentProperty, value);
    }

    // Using a DependencyProperty as the backing store for IncreaseContent.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IncreaseContentProperty =
        DependencyProperty.Register(nameof(IncreaseContent), typeof(object), typeof(NumericUpDown), new PropertyMetadata(null));

    public object? DecreaseContent
    {
        get => GetValue(DecreaseContentProperty);
        set => SetValue(DecreaseContentProperty, value);
    }

    // Using a DependencyProperty as the backing store for DecreaseContent.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DecreaseContentProperty =
        DependencyProperty.Register(nameof(DecreaseContent), typeof(object), typeof(NumericUpDown), new PropertyMetadata(null));

    #region DependencyProperty : MinimumProperty
    public int Minimum
    {
        get => (int)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }
    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register(nameof(Minimum), typeof(int), typeof(NumericUpDown), new PropertyMetadata(int.MinValue, OnMinimumChanged));

    private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        NumericUpDown ctrl = (NumericUpDown)d;
        ctrl.CoerceValue(ValueProperty);
        ctrl.CoerceValue(MaximumProperty);
    }

    #endregion DependencyProperty : MinimumProperty

    #region DependencyProperty : MaximumProperty

    public int Maximum
    {
        get => (int)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(nameof(Maximum), typeof(int), typeof(NumericUpDown), new PropertyMetadata(int.MaxValue, OnMaximumChanged, CoerceMaximum));

    private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        NumericUpDown ctrl = (NumericUpDown)d;
        ctrl.CoerceValue(ValueProperty);
    }

    private static object? CoerceMaximum(DependencyObject d, object? value)
    {
        if (d is NumericUpDown numericUpDown &&
            value is int numericValue)
        {
            return Math.Max(numericUpDown.Minimum, numericValue);
        }
        return value;
    }

    #endregion DependencyProperty : MaximumProperty

    #region DependencyProperty : ValueProperty
    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(int), typeof(NumericUpDown), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnNumericValueChanged, CoerceNumericValue));

    private static void OnNumericValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is NumericUpDown numericUpDown)
        {
            var args = new RoutedPropertyChangedEventArgs<int>((int)e.OldValue, (int)e.NewValue)
            {
                RoutedEvent = ValueChangedEvent
            };
            numericUpDown.RaiseEvent(args);
            if (numericUpDown._textBoxField is { } textBox)
            {
                textBox.Text = ((int)e.NewValue).ToString(CultureInfo.CurrentUICulture);
            }

            if (numericUpDown._increaseButton is { } increaseButton)
            {
                increaseButton.IsEnabled = numericUpDown.Value != numericUpDown.Maximum;
            }

            if (numericUpDown._decreaseButton is { } decreaseButton)
            {
                decreaseButton.IsEnabled = numericUpDown.Value != numericUpDown.Minimum;
            }
        }
    }

    private static object? CoerceNumericValue(DependencyObject d, object? value)
    {
        if (d is NumericUpDown numericUpDown &&
            value is int numericValue)
        {
            numericValue = Math.Min(numericUpDown.Maximum, numericValue);
            numericValue = Math.Max(numericUpDown.Minimum, numericValue);
            return numericValue;
        }
        return value;
    }
    #endregion ValueProperty

    #region DependencyProperty : AllowChangeOnScroll

    public bool AllowChangeOnScroll
    {
        get => (bool)GetValue(AllowChangeOnScrollProperty);
        set => SetValue(AllowChangeOnScrollProperty, value);
    }

    public static readonly DependencyProperty AllowChangeOnScrollProperty =
        DependencyProperty.Register(nameof(AllowChangeOnScroll), typeof(bool), typeof(NumericUpDown), new PropertyMetadata(false));

    #endregion

    #endregion DependencyProperties

    #region Event : ValueChangedEvent
    [Category("Behavior")]
    public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<int>), typeof(NumericUpDown));

    public event RoutedPropertyChangedEventHandler<int> ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }
    #endregion Event : ValueChangedEvent

    public override void OnApplyTemplate()
    {
        if (_increaseButton != null)
            _increaseButton.Click -= IncreaseButtonOnClick;

        if (_decreaseButton != null)
            _decreaseButton.Click -= DecreaseButtonOnClick;
        if (_textBoxField != null)
            _textBoxField.TextChanged -= OnTextBoxFocusLost;

        _increaseButton = GetTemplateChild(IncreaseButtonPartName) as RepeatButton;
        _decreaseButton = GetTemplateChild(DecreaseButtonPartName) as RepeatButton;
        _textBoxField = GetTemplateChild(TextFieldBoxPartName) as TextBox;

        if (_increaseButton != null)
            _increaseButton.Click += IncreaseButtonOnClick;

        if (_decreaseButton != null)
            _decreaseButton.Click += DecreaseButtonOnClick;

        if (_textBoxField != null)
        {
            _textBoxField.LostFocus += OnTextBoxFocusLost;
            _textBoxField.Text = Value.ToString(CultureInfo.CurrentUICulture);
        }

        base.OnApplyTemplate();
    }

    private void OnTextBoxFocusLost(object sender, EventArgs e)
    {
        if (_textBoxField is { } textBoxField)
        {
            if (int.TryParse(textBoxField.Text, NumberStyles.Integer, CultureInfo.CurrentUICulture, out int numericValue))
            {
                SetCurrentValue(ValueProperty, numericValue);
            }
            else
            {
                textBoxField.Text = Value.ToString(CultureInfo.CurrentUICulture);
            }
        }
    }

    private void IncreaseButtonOnClick(object sender, RoutedEventArgs e) => OnIncrease();

    private void DecreaseButtonOnClick(object sender, RoutedEventArgs e) => OnDecrease();

    private void OnIncrease()
    {
        SetCurrentValue(ValueProperty, Value + 1);
    }

    private void OnDecrease()
    {
        SetCurrentValue(ValueProperty, Value - 1);
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        if (e.Key == Key.Up)
        {
            OnIncrease();
            e.Handled = true;
        }
        else if (e.Key == Key.Down)
        {
            OnDecrease();
            e.Handled = true;
        }
        base.OnPreviewKeyDown(e);
    }

    protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
    {
        if (IsKeyboardFocusWithin && AllowChangeOnScroll)
        {
            if (e.Delta > 0)
            {
                OnIncrease();
            }
            else if (e.Delta < 0)
            {
                OnDecrease();
            }
            e.Handled = true;
        }
        base.OnPreviewMouseWheel(e);
    }
}
