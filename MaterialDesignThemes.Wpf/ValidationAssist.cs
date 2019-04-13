using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

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
            return (PlacementMode)element.GetValue(UsePopupProperty);
        }

        public static void SetPopupPlacement(DependencyObject element, PlacementMode value)
        {
            element.SetValue(UsePopupProperty, value);
        }

        /// <summary>
        /// Framework use only.
        /// </summary>
        public static readonly DependencyProperty SuppressProperty = DependencyProperty.RegisterAttached(
            "Suppress", typeof (bool), typeof (ValidationAssist), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.Inherits));

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
            return (bool) element.GetValue(SuppressProperty);
        }

        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached(
            "Background", typeof(Brush), typeof(ValidationAssist), new PropertyMetadata(default(Brush)));

        public static void SetBackground(DependencyObject element, Brush value)
        {
            element.SetValue(BackgroundProperty, value);
        }

        public static Brush GetBackground(DependencyObject element)
        {
            return (Brush) element.GetValue(BackgroundProperty);
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
    }
}
