using System.ComponentModel;

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

    #region DependencyProperty : MinimumProperty
    public int Minimum
    {
        get => (int)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }
    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register(nameof(Minimum), typeof(int), typeof(NumericUpDown), new PropertyMetadata(int.MinValue));
    #endregion DependencyProperty : MinimumProperty

    #region DependencyProperty : MaximumProperty
    public int Maximum
    {
        get => (int)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }
    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(nameof(Maximum), typeof(int), typeof(NumericUpDown), new PropertyMetadata(int.MaxValue));
    #endregion DependencyProperty : MaximumProperty

    #region DependencyProperty : ValueProperty
    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, ValidateInput(value, nameof(Value)));
    }
    public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(int), typeof(NumericUpDown), new PropertyMetadata(0, OnValueChanged));
    #endregion ValueProperty

    #region DependencyProperty : IncreaseCommandProperty
    public ICommand? IncreaseCommand
    {
        get => (ICommand?)GetValue(IncreaseCommandProperty);
        set => SetValue(IncreaseCommandProperty, value);
    }
    public static readonly DependencyProperty IncreaseCommandProperty =
            DependencyProperty.Register(nameof(IncreaseCommand), typeof(ICommand), typeof(NumericUpDown), new PropertyMetadata(default(ICommand?)));
    #endregion DependencyProperty : IncreaseCommandProperty

    #region DependencyProperty : DecreaseCommandProperty
    public ICommand? DecreaseCommand
    {
        get => (ICommand?)GetValue(DecreaseCommandProperty);
        set => SetValue(DecreaseCommandProperty, value);
    }
    public static readonly DependencyProperty DecreaseCommandProperty =
            DependencyProperty.Register(nameof(DecreaseCommand), typeof(ICommand), typeof(NumericUpDown), new PropertyMetadata(default(ICommand?)));
    #endregion DependencyProperty : DecreaseCommandProperty

    #endregion DependencyProperty : DependencyProperties

    #region Event : ValueChangedEvent
    [Category("Behavior")]
    public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<int>), typeof(NumericUpDown));

    public event RoutedPropertyChangedEventHandler<int> ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }
    #endregion Event : ValueChangedEvent

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = d as NumericUpDown;
        var args = new RoutedPropertyChangedEventArgs<int>((int)e.OldValue, (int)e.NewValue)
        {
            RoutedEvent = ValueChangedEvent
        };
        control?.RaiseEvent(args);
    }

    private object ValidateInput(object input, string propertyName = "Value")
    {
        if (!int.TryParse(input.ToString(), out int value))
            return Value;

        value = Math.Min(Maximum, value);
        value = Math.Max(Minimum, value);

        return value;
    }

    public override void OnApplyTemplate()
    {
        if (_increaseButton != null)
            _increaseButton!.Click -= IncreaseButtonOnClick;

        if (_decreaseButton != null)
            _decreaseButton!.Click -= DecreaseButtonOnClick;

        if (_textBoxField != null)
            _textBoxField!.TextChanged -= OnTextBoxFocusLost;

        _increaseButton = GetTemplateChild(IncreaseButtonPartName) as RepeatButton;
        _decreaseButton = GetTemplateChild(DecreaseButtonPartName) as RepeatButton;
        _textBoxField = GetTemplateChild(TextFieldBoxPartName) as TextBox;

        if (_increaseButton != null)
            _increaseButton!.Click += IncreaseButtonOnClick;

        if (_decreaseButton != null)
            _decreaseButton!.Click += DecreaseButtonOnClick;

        if (_textBoxField != null)
        {
            _textBoxField!.LostFocus += OnTextBoxFocusLost;
            _textBoxField.Text = Value.ToString();
        }

        ValueChanged += NumericUpDown_ValueChanged;

        base.OnApplyTemplate();
    }

    private void NumericUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
    {
        if (sender == null) return;
        if (sender is not NumericUpDown) return;

        var tBox = (sender! as NumericUpDown)!._textBoxField;
        if (tBox == null) return;

        tBox.Text = Value.ToString();

    }

    private void OnTextBoxFocusLost(object sender, EventArgs e)
    {
        if (sender == null) return;

        TextBox? tbox = sender as TextBox;
        if (tbox == null) return;

        Value = (int)ValidateInput(tbox.Text);
    }

    private void IncreaseButtonOnClick(object sender, RoutedEventArgs e)
    {
        OnIncrease();
        e.Handled = true;
    }

    private void DecreaseButtonOnClick(object sender, RoutedEventArgs e)
    {
        OnDecrease();
        e.Handled = true;
    }

    private void OnIncrease()
    {
        if (IncreaseCommand?.CanExecute(this) ?? false)
            IncreaseCommand.Execute(this);

        Value += 1;
        if (Value > Maximum) Value = Maximum;
    }

    private void OnDecrease()
    {
        if (DecreaseCommand?.CanExecute(this) ?? false)
            DecreaseCommand.Execute(this);

        Value -= 1;
        if (Value < Minimum) Value = Minimum;
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
        if (e.Delta > 0)
        {
            OnIncrease();
            e.Handled = true;
        }
        else if (e.Delta < 0)
        {
            OnDecrease();
            e.Handled = true;
        }
        base.OnPreviewMouseWheel(e);
    }
}
