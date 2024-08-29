using System.ComponentModel;
using System.Globalization;

namespace MaterialDesignThemes.Wpf;

[TemplatePart(Name = IncreaseButtonPartName, Type = typeof(RepeatButton))]
[TemplatePart(Name = DecreaseButtonPartName, Type = typeof(RepeatButton))]
[TemplatePart(Name = TextBoxPartName, Type = typeof(TextBox))]
public class NumericUpDown : Control
{
    public const string IncreaseButtonPartName = "PART_IncreaseButton";
    public const string DecreaseButtonPartName = "PART_DecreaseButton";
    public const string TextBoxPartName = "PART_TextBox";

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
    public decimal Minimum
    {
        get => (decimal)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }
    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register(nameof(Minimum), typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(decimal.MinValue, OnMinimumChanged));

    private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        NumericUpDown ctrl = (NumericUpDown)d;
        ctrl.CoerceValue(ValueProperty);
        ctrl.CoerceValue(MaximumProperty);
    }

    #endregion DependencyProperty : MinimumProperty

    #region DependencyProperty : MaximumProperty

    public decimal Maximum
    {
        get => (decimal)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(nameof(Maximum), typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(decimal.MaxValue, OnMaximumChanged, CoerceMaximum));

    private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        NumericUpDown ctrl = (NumericUpDown)d;
        ctrl.CoerceValue(ValueProperty);
    }

    private static object? CoerceMaximum(DependencyObject d, object? value)
    {
        if (d is NumericUpDown numericUpDown &&
            value is decimal numericValue)
        {
            return Math.Max(numericUpDown.Minimum, numericValue);
        }
        return value;
    }

    #endregion DependencyProperty : MaximumProperty

    #region DependencyProperty : ValueProperty
    public decimal Value
    {
        get => (decimal)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(decimal), typeof(NumericUpDown), new FrameworkPropertyMetadata(0m, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnNumericValueChanged, CoerceNumericValue));

    private static void OnNumericValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is NumericUpDown numericUpDown)
        {
            var args = new RoutedPropertyChangedEventArgs<decimal>((decimal)e.OldValue, (decimal)e.NewValue)
            {
                RoutedEvent = ValueChangedEvent
            };
            numericUpDown.RaiseEvent(args);
            if (numericUpDown._textBoxField is { } textBox)
            {
                textBox.Text = ((decimal)e.NewValue).ToString(CultureInfo.CurrentUICulture);
            }

            if (numericUpDown._increaseButton is { } increaseButton)
            {
                increaseButton.IsEnabled = numericUpDown.Value <= numericUpDown.Maximum;
            }

            if (numericUpDown._decreaseButton is { } decreaseButton)
            {
                decreaseButton.IsEnabled = numericUpDown.Value >= numericUpDown.Minimum;
            }
        }
    }

    private static object? CoerceNumericValue(DependencyObject d, object? value)
    {
        if (d is NumericUpDown numericUpDown &&
            value is decimal numericValue)
        {
            numericValue = Math.Min(numericUpDown.Maximum, numericValue);
            numericValue = Math.Max(numericUpDown.Minimum, numericValue);
            return numericValue;
        }
        return value;
    }
    #endregion ValueProperty

    #region DependencyProperty : ValueStep
    /// <summary>
    /// The size of value for each increase or decrease
    /// </summary>
    public decimal ValueMinStep
    {
        get { return (decimal)GetValue(ValueMinStepProperty); }
        set { SetValue(ValueMinStepProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ValueStep.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ValueMinStepProperty =
        DependencyProperty.Register("ValueMinStep", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(1m));
    #endregion 

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
        _textBoxField = GetTemplateChild(TextBoxPartName) as TextBox;

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
            if (decimal.TryParse(textBoxField.Text, out decimal numericValue))
            {
                SetCurrentValue(ValueProperty, numericValue);
            }
            else
            {
                //undo
            }
        }
    }

    private void IncreaseButtonOnClick(object sender, RoutedEventArgs e) => OnIncrease();

    private void DecreaseButtonOnClick(object sender, RoutedEventArgs e) => OnDecrease();

    private void OnIncrease()
    {
        SetCurrentValue(ValueProperty, Value + ValueMinStep);
    }

    private void OnDecrease()
    {
        SetCurrentValue(ValueProperty, Value - ValueMinStep);
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
