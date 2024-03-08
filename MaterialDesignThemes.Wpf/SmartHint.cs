using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using MaterialDesignThemes.Wpf.Converters;

namespace MaterialDesignThemes.Wpf;

/// <summary>
/// A control that implement placeholder behavior. Can work as a simple placeholder either as a floating hint, see <see cref="UseFloating"/> property.
/// <para/>
/// To set a target control you should set the HintProxy property. Use the <see cref="HintProxyFabricConverter.Instance"/> converter which converts a control into the IHintProxy interface.
/// </summary>
[TemplateVisualState(GroupName = ContentStatesGroupName, Name = HintRestingPositionName)]
[TemplateVisualState(GroupName = ContentStatesGroupName, Name = HintFloatingPositionName)]
public class SmartHint : Control
{
    public const string ContentStatesGroupName = "ContentStates";

    public const string HintRestingPositionName = "HintRestingPosition";
    public const string HintFloatingPositionName = "HintFloatingPosition";

    #region ManagedProperty

    public static readonly DependencyProperty HintProxyProperty = DependencyProperty.Register(
        nameof(HintProxy), typeof(IHintProxy), typeof(SmartHint), new PropertyMetadata(default(IHintProxy?), HintProxyPropertyChangedCallback));

    public IHintProxy? HintProxy
    {
        get => (IHintProxy)GetValue(HintProxyProperty);
        set => SetValue(HintProxyProperty, value);
    }

    #endregion

    #region HintProperty

    public static readonly DependencyProperty HintProperty = DependencyProperty.Register(
        nameof(Hint), typeof(object), typeof(SmartHint), new PropertyMetadata(null));

    public object? Hint
    {
        get => GetValue(HintProperty);
        set => SetValue(HintProperty, value);
    }

    #endregion

    #region IsContentNullOrEmpty

    private static readonly DependencyPropertyKey IsContentNullOrEmptyPropertyKey =
        DependencyProperty.RegisterReadOnly(
            "IsContentNullOrEmpty", typeof(bool), typeof(SmartHint),
            new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty IsContentNullOrEmptyProperty =
        IsContentNullOrEmptyPropertyKey.DependencyProperty;

    public bool IsContentNullOrEmpty
    {
        get => (bool)GetValue(IsContentNullOrEmptyProperty);
        private set => SetValue(IsContentNullOrEmptyPropertyKey, value);
    }

    #endregion

    #region IsHintInFloatingPosition

    private static readonly DependencyPropertyKey IsHintInFloatingPositionPropertyKey =
        DependencyProperty.RegisterReadOnly(
            "IsHintInFloatingPosition", typeof(bool), typeof(SmartHint),
            new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty IsHintInFloatingPositionProperty =
        IsHintInFloatingPositionPropertyKey.DependencyProperty;

    public bool IsHintInFloatingPosition
    {
        get => (bool)GetValue(IsHintInFloatingPositionProperty);
        private set => SetValue(IsHintInFloatingPositionPropertyKey, value);
    }

    #endregion

    #region UseFloating

    public static readonly DependencyProperty UseFloatingProperty = DependencyProperty.Register(
        nameof(UseFloating), typeof(bool), typeof(SmartHint), new PropertyMetadata(false));

    public bool UseFloating
    {
        get => (bool)GetValue(UseFloatingProperty);
        set => SetValue(UseFloatingProperty, value);
    }

    #endregion

    #region FloatingScale & FloatingOffset

    public static readonly DependencyProperty FloatingScaleProperty = DependencyProperty.Register(
        nameof(FloatingScale), typeof(double), typeof(SmartHint), new PropertyMetadata(.74));

    public double FloatingScale
    {
        get => (double)GetValue(FloatingScaleProperty);
        set => SetValue(FloatingScaleProperty, value);
    }

    public static readonly DependencyProperty FloatingOffsetProperty = DependencyProperty.Register(
        nameof(FloatingOffset), typeof(Point), typeof(SmartHint), new PropertyMetadata(new Point(1, -16)));

    public Point FloatingOffset
    {
        get => (Point)GetValue(FloatingOffsetProperty);
        set => SetValue(FloatingOffsetProperty, value);
    }

    #endregion

    #region HintOpacity

    public static readonly DependencyProperty HintOpacityProperty = DependencyProperty.Register(
        nameof(HintOpacity), typeof(double), typeof(SmartHint), new PropertyMetadata(.46));

    public double HintOpacity
    {
        get => (double)GetValue(HintOpacityProperty);
        set => SetValue(HintOpacityProperty, value);
    }

    #endregion

    public static readonly DependencyProperty InitialVerticalOffsetProperty = DependencyProperty.Register(
        nameof(InitialVerticalOffset), typeof(double), typeof(SmartHint), new PropertyMetadata(default(double)));

    public double InitialVerticalOffset
    {
        get => (double) GetValue(InitialVerticalOffsetProperty);
        set => SetValue(InitialVerticalOffsetProperty, value);
    }

    public static readonly DependencyProperty InitialHorizontalOffsetProperty = DependencyProperty.Register(
        nameof(InitialHorizontalOffset), typeof(double), typeof(SmartHint), new PropertyMetadata(default(double)));

    public double InitialHorizontalOffset
    {
        get => (double)GetValue(InitialHorizontalOffsetProperty);
        set => SetValue(InitialHorizontalOffsetProperty, value);
    }

    public static readonly DependencyProperty FloatingTargetProperty = DependencyProperty.Register(
        nameof(FloatingTarget), typeof(FrameworkElement), typeof(SmartHint), new PropertyMetadata(default(FrameworkElement)));

    public FrameworkElement? FloatingTarget
    {
        get => (FrameworkElement?)GetValue(FloatingTargetProperty);
        set => SetValue(FloatingTargetProperty, value);
    }

    public static readonly DependencyProperty FloatingAlignmentProperty = DependencyProperty.Register(
        nameof(FloatingAlignment), typeof(VerticalAlignment), typeof(SmartHint), new PropertyMetadata(System.Windows.VerticalAlignment.Bottom));

    public VerticalAlignment FloatingAlignment
    {
        get => (VerticalAlignment) GetValue(FloatingAlignmentProperty);
        set => SetValue(FloatingAlignmentProperty, value);
    }

    public static readonly DependencyProperty FloatingMarginProperty = DependencyProperty.Register(
        nameof(FloatingMargin), typeof(Thickness), typeof(SmartHint), new PropertyMetadata(default(Thickness)));

    public Thickness FloatingMargin
    {
        get => (Thickness) GetValue(FloatingMarginProperty);
        set => SetValue(FloatingMarginProperty, value);
    }

    static SmartHint()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SmartHint), new FrameworkPropertyMetadata(typeof(SmartHint)));
    }

    private static void HintProxyPropertyChangedCallback(DependencyObject? dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
        var smartHint = dependencyObject as SmartHint;
        if (smartHint is null) return;

        if (dependencyPropertyChangedEventArgs.OldValue is IHintProxy oldHintProxy)
        {
            oldHintProxy.IsVisibleChanged -= smartHint.OnHintProxyIsVisibleChanged;
            oldHintProxy.ContentChanged -= smartHint.OnHintProxyContentChanged;
            oldHintProxy.Loaded -= smartHint.OnHintProxyContentChanged;
            oldHintProxy.FocusedChanged -= smartHint.OnHintProxyFocusedChanged;
            oldHintProxy.Dispose();
        }

        if (dependencyPropertyChangedEventArgs.NewValue is IHintProxy newHintProxy)
        {
            newHintProxy.IsVisibleChanged += smartHint.OnHintProxyIsVisibleChanged;
            newHintProxy.ContentChanged += smartHint.OnHintProxyContentChanged;
            newHintProxy.Loaded += smartHint.OnHintProxyContentChanged;
            newHintProxy.FocusedChanged += smartHint.OnHintProxyFocusedChanged;
            smartHint.RefreshState(false);
        }
    }

    protected virtual void OnHintProxyFocusedChanged(object? sender, EventArgs e)
    {
        if (HintProxy is { } hintProxy)
        {
            if (hintProxy.IsLoaded)
                RefreshState(true);
            else
                hintProxy.Loaded += HintProxySetStateOnLoaded;
        }
    }

    protected virtual void OnHintProxyContentChanged(object? sender, EventArgs e)
    {
        IsContentNullOrEmpty = HintProxy?.IsEmpty() == true;

        if (HintProxy is { } hintProxy)
        {
            if (hintProxy.IsLoaded)
                RefreshState(true);
            else
                hintProxy.Loaded += HintProxySetStateOnLoaded;
        }
    }

    private void HintProxySetStateOnLoaded(object? sender, EventArgs e)
    {
        RefreshState(false);
        if (HintProxy is { } hintProxy)
        {
            hintProxy.Loaded -= HintProxySetStateOnLoaded;
        }
    }

    protected virtual void OnHintProxyIsVisibleChanged(object? sender, EventArgs e)
        => RefreshState(false);

    private void RefreshState(bool useTransitions)
    {
        IHintProxy? proxy = HintProxy;

        if (proxy is null) return;
        if (!proxy.IsVisible) return;

        var action = new Action(() =>
        {
            string state = string.Empty;

            bool isEmpty = proxy.IsEmpty();
            bool isFocused = proxy.IsFocused();

            if (UseFloating)
                state = !isEmpty || isFocused ? HintFloatingPositionName : HintRestingPositionName;
            else
                state = !isEmpty ? HintFloatingPositionName : HintRestingPositionName;

            IsHintInFloatingPosition = state == HintFloatingPositionName;

            VisualStateManager.GoToState(this, state, useTransitions);
        });

        if (DesignerProperties.GetIsInDesignMode(this))
        {
            action();
        }
        else
        {
            Dispatcher.BeginInvoke(action);
        }
    }
}

public class FloatingHintTranslateTransformConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is null
            || values.Length < 5
            || !double.TryParse(values[0]!.ToString(), out double scale)
            || !double.TryParse(values[1]!.ToString(), out double lower)
            || !double.TryParse(values[2]!.ToString(), out double upper)
            || values[3] is not SmartHint hint
            || values[4] is not Point floatingOffset)
        {
            return Transform.Identity;
        }

        // Back-compatible behavior, fall back to using the non-nullable floatingOffset if it has a non-default value
        if (hint.FloatingTarget is null || floatingOffset != HintAssist.DefaultFloatingOffset)
        {
            /* As a consequence of Math.Min() which is used below to ensure the initial offset is respected (in filled style)
               the SmartHint will not be able to "float downwards". I believe this is acceptable though.
             */
            return new TranslateTransform
            {
                X = scale * floatingOffset.X,
                Y = Math.Min(hint.InitialVerticalOffset, scale * floatingOffset.Y)
            };
        }
        return new TranslateTransform
        {
            X = GetFloatingTargetHorizontalOffset() * scale,
            Y = GetFloatingTargetVerticalOffset() * scale
        };

        double GetFloatingTargetHorizontalOffset()
        {
            return hint.InitialHorizontalOffset + hint.HorizontalContentAlignment switch
            {
                HorizontalAlignment.Center => 0,
                HorizontalAlignment.Right => hint.FloatingMargin.Right,
                _ => -hint.FloatingMargin.Left,
            };
        }

        double GetFloatingTargetVerticalOffset()
        {
            double offset = hint.FloatingTarget.TranslatePoint(new Point(0, 0), hint).Y;
            offset += hint.InitialVerticalOffset;
            offset -= hint.ActualHeight;

            double scalePercentage = upper + (lower - upper) * scale;
            offset += hint.FloatingAlignment switch
            {
                VerticalAlignment.Top => hint.ActualHeight - (hint.ActualHeight * upper * scalePercentage),
                VerticalAlignment.Bottom => hint.ActualHeight * upper * (1 - scalePercentage),
                _ => (hint.ActualHeight * upper * (1 - scalePercentage)) / 2,
            };
            return offset;
        }
    }

    public object[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

public class FloatingHintScaleTransformConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values?.Length != 3
            || values.Any(v => v == null)
            || !double.TryParse(values[0]!.ToString(), out double scale)
            || !double.TryParse(values[1]!.ToString(), out double lower)
            || !double.TryParse(values[2]!.ToString(), out double upper))
        {
            return Transform.Identity;
        }
        double scalePercentage = upper + (lower - upper) * scale;
        return new ScaleTransform(scalePercentage, scalePercentage);
    }

    public object[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

public class FloatingHintMarginConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values?.Length != 2
            || values.Any(v => v == null)
            || !double.TryParse(values[0]!.ToString(), out double scale)
            || values[1] is not Thickness floatingMargin)
        {
            return Transform.Identity;
        }

        return floatingMargin with
        {
            Left = floatingMargin.Left * scale,
            Top = floatingMargin.Top * scale,
            Right = floatingMargin.Right * scale,
            Bottom = floatingMargin.Bottom * scale,
        };
    }

    public object[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
