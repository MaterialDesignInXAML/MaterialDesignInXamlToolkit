using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    [TemplateVisualState(GroupName = MaterialDesignTextStatesGroupName, Name = MaterialDesignStateTextEmptyName)]
    [TemplateVisualState(GroupName = MaterialDesignTextStatesGroupName, Name = MaterialDesignStateTextNotEmptyName)]
    public class SmartHint : Control
    {
        public const string MaterialDesignTextStatesGroupName = "MaterialDesignTextStates";
        public const string MaterialDesignStateTextEmptyName = "MaterialDesignStateTextEmpty";
        public const string MaterialDesignStateTextNotEmptyName = "MaterialDesignStateTextNotEmpty";

        #region ManagedProperty

        public static readonly DependencyProperty HintProxyProperty = DependencyProperty.Register(
            nameof(HintProxy), typeof(IHintProxy), typeof(SmartHint), new PropertyMetadata(default(IHintProxy), HintProxyPropertyChangedCallback));

        public IHintProxy HintProxy
        {
            get { return (IHintProxy)GetValue(HintProxyProperty); }
            set { SetValue(HintProxyProperty, value); }
        }

        #endregion

        #region HintProperty

        public static readonly DependencyProperty HintProperty = DependencyProperty.Register(
            nameof(Hint), typeof(string), typeof(SmartHint), new PropertyMetadata(default(string)));

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        #endregion

        #region IsTextNullOrEmpty

        public static readonly DependencyProperty IsTextNullOrEmptyProperty = DependencyProperty.Register(
            nameof(IsTextNullOrEmpty), typeof(bool), typeof(SmartHint), new PropertyMetadata(default(bool)));

        public bool IsTextNullOrEmpty
        {
            get { return (bool)GetValue(IsTextNullOrEmptyProperty); }
            set { SetValue(IsTextNullOrEmptyProperty, value); }
        }

        #endregion

        #region UseFloating

        public static readonly DependencyProperty UseFloatingProperty = DependencyProperty.Register(
            nameof(UseFloating), typeof(bool), typeof(SmartHint), new PropertyMetadata(false));

        public bool UseFloating
        {
            get { return (bool) GetValue(UseFloatingProperty); }
            set { SetValue(UseFloatingProperty, value); }
        }

        #endregion

        #region HintOpacity

        public static readonly DependencyProperty HintOpacityProperty = DependencyProperty.Register(
            nameof(HintOpacity), typeof(double), typeof(SmartHint), new PropertyMetadata(.46));

        public double HintOpacity
        {
            get { return (double) GetValue(HintOpacityProperty); }
            set { SetValue(HintOpacityProperty, value); }
        }

        #endregion

        static SmartHint()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SmartHint), new FrameworkPropertyMetadata(typeof(SmartHint)));
        }

        public SmartHint()
        {
            IsHitTestVisible = false;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
        }

        private static void HintProxyPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var smartHint = dependencyObject as SmartHint;
            if (smartHint == null) return;

            var hintProxy = dependencyPropertyChangedEventArgs.OldValue as IHintProxy;

            if (hintProxy != null)
            {
                hintProxy.IsVisibleChanged -= smartHint.OnHintProxyIsVisibleChanged;
                hintProxy.TextChanged -= smartHint.OnHintProxyTextChanged;
                hintProxy.Loaded -= smartHint.OnHintProxyTextChanged;
                hintProxy.Dispose();
            }

            hintProxy = dependencyPropertyChangedEventArgs.NewValue as IHintProxy;
            if (hintProxy != null)
            {
                hintProxy.IsVisibleChanged += smartHint.OnHintProxyIsVisibleChanged;
                hintProxy.TextChanged += smartHint.OnHintProxyTextChanged;
                hintProxy.Loaded += smartHint.OnHintProxyTextChanged;
                smartHint.RefreshState(false);
            }
        }

        protected virtual void OnHintProxyTextChanged(object sender, EventArgs e)
        {
            IsTextNullOrEmpty = String.IsNullOrEmpty(HintProxy.Text);

            if (HintProxy.IsLoaded)
            {
                RefreshState(true);
            }
            else
            {
                HintProxy.Loaded += HintProxySetStateOnLoaded;
            }
        }

        private void HintProxySetStateOnLoaded(object sender, EventArgs e)
        {
            RefreshState(false);
            HintProxy.Loaded -= HintProxySetStateOnLoaded;
        }

        protected virtual void OnHintProxyIsVisibleChanged(object sender, EventArgs e)
        {
            RefreshState(false);
        }

        private void RefreshState(bool useTransitions)
        {
            if (HintProxy == null) return;
            if (!HintProxy.IsVisible) return;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                var state = string.IsNullOrEmpty(HintProxy.Text)
                    ? MaterialDesignStateTextEmptyName
                    : MaterialDesignStateTextNotEmptyName;

                VisualStateManager.GoToState(this, state, useTransitions);
            }));
        }
    }

    public interface IHintProxy : IDisposable
    {
        string Text { get; }
        bool IsLoaded { get; }
        bool IsVisible { get; }

        event EventHandler TextChanged;
        event EventHandler IsVisibleChanged;
        event EventHandler Loaded;
    }
}
