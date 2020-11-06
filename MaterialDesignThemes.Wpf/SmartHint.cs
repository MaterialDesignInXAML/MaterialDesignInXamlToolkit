using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf.Converters;

namespace MaterialDesignThemes.Wpf
{

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
}
