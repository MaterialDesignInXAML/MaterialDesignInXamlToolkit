namespace MaterialDesignThemes.Wpf
{
    public static class SplitButtonAssist
    {
        public static readonly DependencyProperty PopupElevationProperty = DependencyProperty.RegisterAttached(
            "PopupElevation", typeof(Elevation), typeof(SplitButtonAssist), new PropertyMetadata(default(Elevation)));

        public static void SetPopupElevation(DependencyObject element, Elevation value)
            => element.SetValue(PopupElevationProperty, value);

        public static Elevation GetPopupElevation(DependencyObject element)
            => (Elevation) element.GetValue(PopupElevationProperty);

        public static readonly DependencyProperty PopupUniformCornerRadiusProperty = DependencyProperty.RegisterAttached(
            "PopupUniformCornerRadius", typeof(double), typeof(SplitButtonAssist), new PropertyMetadata(default(double)));

        public static void SetPopupUniformCornerRadius(DependencyObject element, double value)
            => element.SetValue(PopupUniformCornerRadiusProperty, value);

        public static double GetPopupUniformCornerRadius(DependencyObject element)
            => (double) element.GetValue(PopupUniformCornerRadiusProperty);

        public static readonly DependencyProperty PopupContentStyleProperty = DependencyProperty.RegisterAttached(
            "PopupContentStyle", typeof(Style), typeof(SplitButtonAssist), new PropertyMetadata(default(Style)));

        public static void SetPopupContentStyle(DependencyObject element, Style value)
            => element.SetValue(PopupContentStyleProperty, value);

        public static Style GetPopupContentStyle(DependencyObject element)
            => (Style) element.GetValue(PopupContentStyleProperty);

        public static readonly DependencyProperty PopupContentProperty = DependencyProperty.RegisterAttached(
            "PopupContent", typeof(object), typeof(SplitButtonAssist), new PropertyMetadata(default(object)));

        public static void SetPopupContent(DependencyObject element, object value)
            => element.SetValue(PopupContentProperty, value);

        public static object GetPopupContent(DependencyObject element)
            => element.GetValue(PopupContentProperty);

        public static readonly DependencyProperty PopupContentTemplateProperty = DependencyProperty.RegisterAttached(
            "PopupContentTemplate", typeof(DataTemplate), typeof(SplitButtonAssist), new PropertyMetadata(default(DataTemplate)));

        public static void SetPopupContentTemplate(DependencyObject element, DataTemplate value)
            => element.SetValue(PopupContentTemplateProperty, value);

        public static DataTemplate GetPopupContentTemplate(DependencyObject element)
            => (DataTemplate) element.GetValue(PopupContentTemplateProperty);

        public static readonly DependencyProperty PopupContentTemplateSelectorProperty = DependencyProperty.RegisterAttached(
            "PopupContentTemplateSelector", typeof(DataTemplateSelector), typeof(SplitButtonAssist), new PropertyMetadata(default(DataTemplateSelector)));

        public static void SetPopupContentTemplateSelector(DependencyObject element, DataTemplateSelector value)
            => element.SetValue(PopupContentTemplateSelectorProperty, value);

        public static DataTemplateSelector GetPopupContentTemplateSelector(DependencyObject element)
            => (DataTemplateSelector) element.GetValue(PopupContentTemplateSelectorProperty);
    }
}
