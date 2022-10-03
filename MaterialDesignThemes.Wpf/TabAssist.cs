namespace MaterialDesignThemes.Wpf
{
    public static class TabAssist
    {
        public static readonly DependencyProperty HasFilledTabProperty = DependencyProperty.RegisterAttached(
            "HasFilledTab", typeof(bool), typeof(TabAssist), new PropertyMetadata(false));

        public static void SetHasFilledTab(DependencyObject element, bool value) => element.SetValue(HasFilledTabProperty, value);

        public static bool GetHasFilledTab(DependencyObject element) => (bool)element.GetValue(HasFilledTabProperty);

        public static readonly DependencyProperty HasUniformTabWidthProperty = DependencyProperty.RegisterAttached(
            "HasUniformTabWidth", typeof(bool), typeof(TabAssist), new PropertyMetadata(false));

        public static void SetHasUniformTabWidth(DependencyObject element, bool value) => element.SetValue(HasUniformTabWidthProperty, value);

        public static bool GetHasUniformTabWidth(DependencyObject element) => (bool)element.GetValue(HasUniformTabWidthProperty);

    }
}