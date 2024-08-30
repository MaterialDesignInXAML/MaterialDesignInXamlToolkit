using System.ComponentModel;
using System.Globalization;

namespace MaterialDesignThemes.Wpf;


public class UpDownBase<T, TArithmetic> : UpDownBase
    where TArithmetic : IArithmetic<T>, new()
{
    private static readonly TArithmetic _arithmetic = new TArithmetic();

    #region DependencyProperties

    #region DependencyProperty : MinimumProperty

    public virtual T Minimum
    {
        get => (T)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }
    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register(nameof(Minimum), typeof(T), typeof(UpDownBase<T, TArithmetic>), new PropertyMetadata(_arithmetic.MinValue(), OnMinimumChanged));

    protected static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        UpDownBase<T, TArithmetic> ctrl = (UpDownBase<T, TArithmetic>)d;
        ctrl.CoerceValue(ValueProperty);
        ctrl.CoerceValue(MaximumProperty);
    }

    #endregion DependencyProperty : MinimumProperty

    #region DependencyProperty : MaximumProperty

    public T Maximum
    {
        get => (T)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(nameof(Maximum), typeof(T), typeof(UpDownBase<T, TArithmetic>), new PropertyMetadata(_arithmetic.MaxValue(), OnMaximumChanged, CoerceMaximum));

    private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        UpDownBase<T, TArithmetic> ctrl = (UpDownBase<T, TArithmetic>)d;
        ctrl.CoerceValue(ValueProperty);
    }

    private static object? CoerceMaximum(DependencyObject d, object? value)
    {
        if (d is UpDownBase<T, TArithmetic> upDonw &&
            value is T numericValue)
        {
            return _arithmetic.Max(upDonw.Minimum, numericValue);
        }
        return value;
    }

    #endregion DependencyProperty : MaximumProperty

    #region DependencyProperty : ValueProperty
    public T Value
    {
        get => (T)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(T), typeof(UpDownBase<T, TArithmetic>), new FrameworkPropertyMetadata(default(T), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnNumericValueChanged, CoerceNumericValue));

    private static void OnNumericValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is UpDownBase<T, TArithmetic> upDownBase)
        {
            var args = new RoutedPropertyChangedEventArgs<T>((T)e.OldValue, (T)e.NewValue)
            {
                RoutedEvent = ValueChangedEvent
            };
            upDownBase.RaiseEvent(args);

            if (upDownBase._textBoxField is { } textBox)
            {
                textBox.Text = _arithmetic.ConvertToString((T)e.NewValue);
            }

            if (upDownBase._increaseButton is { } increaseButton)
            {
                increaseButton.IsEnabled = _arithmetic.Compare(upDownBase.Value, upDownBase.Maximum) <= 0;
            }

            if (upDownBase._decreaseButton is { } decreaseButton)
            {
                decreaseButton.IsEnabled = _arithmetic.Compare(upDownBase.Value, upDownBase.Minimum) >= 0;
            }
        }
    }

    private static object? CoerceNumericValue(DependencyObject d, object? value)
    {
        if (d is UpDownBase<T, TArithmetic> upDonw &&
            value is T numericValue)
        {
            numericValue = _arithmetic.Min(upDonw.Maximum, numericValue);
            numericValue = _arithmetic.Max(upDonw.Minimum, numericValue);
            return numericValue;
        }
        return value;
    }
    #endregion ValueProperty

    #region DependencyProperty : ValueStep
    /// <summary>
    /// The step of value for each increase or decrease
    /// </summary>
    public T ValueStep
    {
        get { return (T)GetValue(ValueStepProperty); }
        set { SetValue(ValueStepProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ValueStep.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ValueStepProperty =
        DependencyProperty.Register("ValueStep", typeof(T), typeof(UpDownBase<T, TArithmetic>), new PropertyMetadata(default(T)));
    #endregion 

    #region DependencyProperty : AllowChangeOnScroll

    public bool AllowChangeOnScroll
    {
        get => (bool)GetValue(AllowChangeOnScrollProperty);
        set => SetValue(AllowChangeOnScrollProperty, value);
    }

    public static readonly DependencyProperty AllowChangeOnScrollProperty =
        DependencyProperty.Register(nameof(AllowChangeOnScroll), typeof(bool), typeof(UpDownBase<T, TArithmetic>), new PropertyMetadata(false));

    #endregion

    #endregion DependencyProperties

    #region Event : ValueChangedEvent
    [Category("Behavior")]
    public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<T>), typeof(UpDownBase<T, TArithmetic>));

    public event RoutedPropertyChangedEventHandler<T> ValueChanged
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
            _textBoxField.Text = _arithmetic.ConvertToString(Value);
        }

        base.OnApplyTemplate();
    }

    private void OnTextBoxFocusLost(object sender, EventArgs e)
    {
        if (_textBoxField is { } textBoxField)
        {
            if (_arithmetic.TryParse(textBoxField.Text, out T value))
            {
                SetCurrentValue(ValueProperty, value);
            }
            else
            {
                textBoxField.Text = _arithmetic.ConvertToString(Value);
            }
        }
    }

    private void IncreaseButtonOnClick(object sender, RoutedEventArgs e) => OnIncrease();

    private void DecreaseButtonOnClick(object sender, RoutedEventArgs e) => OnDecrease();

    private void OnIncrease()
    {
        SetCurrentValue(ValueProperty, _arithmetic.Add(Value, ValueStep));
    }

    private void OnDecrease()
    {
        SetCurrentValue(ValueProperty, _arithmetic.Subtract(Value, ValueStep));
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

[TemplatePart(Name = IncreaseButtonPartName, Type = typeof(RepeatButton))]
[TemplatePart(Name = DecreaseButtonPartName, Type = typeof(RepeatButton))]
[TemplatePart(Name = TextBoxPartName, Type = typeof(TextBox))]
public class UpDownBase : Control
{
    public const string IncreaseButtonPartName = "PART_IncreaseButton";
    public const string DecreaseButtonPartName = "PART_DecreaseButton";
    public const string TextBoxPartName = "PART_TextBox";

    protected TextBox? _textBoxField;
    protected RepeatButton? _decreaseButton;
    protected RepeatButton? _increaseButton;

    static UpDownBase()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(UpDownBase), new FrameworkPropertyMetadata(typeof(UpDownBase)));
    }

    public object? IncreaseContent
    {
        get => GetValue(IncreaseContentProperty);
        set => SetValue(IncreaseContentProperty, value);
    }

    // Using a DependencyProperty as the backing store for IncreaseContent.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IncreaseContentProperty =
        DependencyProperty.Register(nameof(IncreaseContent), typeof(object), typeof(UpDownBase), new PropertyMetadata(null));

    public object? DecreaseContent
    {
        get => GetValue(DecreaseContentProperty);
        set => SetValue(DecreaseContentProperty, value);
    }

    // Using a DependencyProperty as the backing store for DecreaseContent.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DecreaseContentProperty =
        DependencyProperty.Register(nameof(DecreaseContent), typeof(object), typeof(UpDownBase), new PropertyMetadata(null));

}

public interface IArithmetic<T>
{
    T Add(T value1, T value2);

    T Subtract(T value1, T value2);

    int Compare(T value1, T value2);

    string ConvertToString(T value);

    T MinValue();

    T MaxValue();

    T Max(T value1, T value2);

    T Min(T value1, T value2);

    bool TryParse(string text, out T value);
}
