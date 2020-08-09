using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using MaterialDesignThemes.Wpf.Transitions;

namespace MaterialDesignThemes.Wpf
{
    public static class ValidationAssist
    {
        #region ShowOnFocusProperty

        /// <summary>
        /// The hint property
        /// </summary>
        public static readonly DependencyProperty OnlyShowOnFocusProperty = DependencyProperty.RegisterAttached(
            "OnlyShowOnFocus",
            typeof(bool),
            typeof(ValidationAssist),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetOnlyShowOnFocus(DependencyObject element)
        {
            return (bool)element.GetValue(OnlyShowOnFocusProperty);
        }

        public static void SetOnlyShowOnFocus(DependencyObject element, bool value)
        {
            element.SetValue(OnlyShowOnFocusProperty, value);
        }

        #endregion

        #region UsePopupProperty

        /// <summary>
        /// The hint property
        /// </summary>
        public static readonly DependencyProperty UsePopupProperty = DependencyProperty.RegisterAttached(
            "UsePopup",
            typeof(bool),
            typeof(ValidationAssist),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetUsePopup(DependencyObject element)
        {
            return (bool)element.GetValue(UsePopupProperty);
        }

        public static void SetUsePopup(DependencyObject element, bool value)
        {
            element.SetValue(UsePopupProperty, value);
        }

        #endregion

        /// <summary>
        /// The hint property
        /// </summary>
        public static readonly DependencyProperty PopupPlacementProperty = DependencyProperty.RegisterAttached(
            "PopupPlacement",
            typeof(PlacementMode),
            typeof(ValidationAssist),
            new FrameworkPropertyMetadata(PlacementMode.Bottom, FrameworkPropertyMetadataOptions.Inherits));

        public static PlacementMode GetPopupPlacement(DependencyObject element)
        {
            return (PlacementMode)element.GetValue(PopupPlacementProperty);
        }

        public static void SetPopupPlacement(DependencyObject element, PlacementMode value)
        {
            element.SetValue(PopupPlacementProperty, value);
        }

        /// <summary>
        /// Framework use only.
        /// </summary>
        public static readonly DependencyProperty SuppressProperty = DependencyProperty.RegisterAttached(
            "Suppress", typeof(bool), typeof(ValidationAssist), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Framework use only.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetSuppress(DependencyObject element, bool value)
        {
            element.SetValue(SuppressProperty, value);
        }

        /// <summary>
        /// Framework use only.
        /// </summary>
        public static bool GetSuppress(DependencyObject element)
        {
            return (bool)element.GetValue(SuppressProperty);
        }

        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached(
            "Background", typeof(Brush), typeof(ValidationAssist), new PropertyMetadata(default(Brush)));

        public static void SetBackground(DependencyObject element, Brush value)
        {
            element.SetValue(BackgroundProperty, value);
        }

        public static Brush GetBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(BackgroundProperty);
        }



        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.RegisterAttached("FontSize", typeof(double), typeof(ValidationAssist), new PropertyMetadata(10.0));

        public static void SetFontSize(DependencyObject element, double value)
        {
            element.SetValue(FontSizeProperty, value);
        }

        public static double GetFontSize(DependencyObject element)
        {
            return (double)element.GetValue(FontSizeProperty);
        }

        public static readonly DependencyProperty HasErrorProperty = DependencyProperty.RegisterAttached(
            "HasError",
            typeof(bool),
            typeof(ValidationAssist),
            new PropertyMetadata(default(bool)));

        public static void SetHasError(DependencyObject element, bool value)
        {
            element.SetValue(HasErrorProperty, value);
        }

        public static bool GetHasError(DependencyObject element)
        {
            return (bool)element.GetValue(HasErrorProperty);
        }

        public static readonly DependencyProperty HorizontalAlignmentProperty = DependencyProperty.RegisterAttached(
            "HorizontalAlignment", typeof(HorizontalAlignment), typeof(ValidationAssist), new PropertyMetadata(HorizontalAlignment.Left));

        public static void SetHorizontalAlignment(DependencyObject element, HorizontalAlignment value) => element.SetValue(HorizontalAlignmentProperty, value);
        public static HorizontalAlignment GetHorizontalAlignment(DependencyObject element) => (HorizontalAlignment) element.GetValue(HorizontalAlignmentProperty);

        public static readonly DependencyProperty ValidateeProperty = DependencyProperty.RegisterAttached(
            "Validatee", typeof(DependencyObject), typeof(ValidationAssist), new PropertyMetadata(null, ValidateeChangedCallback));
        public static void SetValidatee(DependencyObject element, DependencyObject value) => element.SetValue(ValidateeProperty, value);
        public static DependencyObject GetValidatee(DependencyObject element) => (DependencyObject)element.GetValue(ValidateeProperty);

        public static readonly DependencyProperty ValidateeOpacityProperty = DependencyProperty.RegisterAttached(
            "ValidateeOpacity", typeof(double), typeof(ValidationAssist), new FrameworkPropertyMetadata(1.0));
        public static void SetValidateeOpacity(DependencyObject element, double value) => element.SetValue(ValidateeOpacityProperty, value);
        public static double GetValidateeOpacity(DependencyObject element) => (double)element.GetValue(ValidateeOpacityProperty);

        private static readonly DependencyProperty DetachValidateeProperty = DependencyProperty.RegisterAttached(
            "DetachValidatee", typeof(Action), typeof(ValidationAssist), new PropertyMetadata(null));
        private static void SetDetachValidatee(DependencyObject element, Action value) => element.SetValue(DetachValidateeProperty, value);
        private static Action GetDetachValidatee(DependencyObject element) => (Action)element.GetValue(DetachValidateeProperty);

        private static void ValidateeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GetDetachValidatee(d)?.Invoke();
            SetDetachValidatee(d, Attach(d as FrameworkElement, e.NewValue as DependencyObject));

            static Action Attach(FrameworkElement element, DependencyObject validatee)
            {
                if (element is null)
                    return null;
                var slide = validatee.GetVisualAncestry().OfType<TransitionerSlide>().FirstOrDefault();
                if (slide is null)
                    return null;

                EventHandler opacityChanged = (sender, e) => SetValidateeOpacity(element, slide.Opacity);
                RoutedEventHandler unloaded = (sender, e) => GetDetachValidatee(element)?.Invoke();
                slide.OpacityChanged += opacityChanged;
                element.Unloaded += unloaded;
                return () => {
                    slide.OpacityChanged -= opacityChanged;
                    element.Unloaded -= unloaded;
                    SetDetachValidatee(element, null);
                };
            }
        }
    }
}
