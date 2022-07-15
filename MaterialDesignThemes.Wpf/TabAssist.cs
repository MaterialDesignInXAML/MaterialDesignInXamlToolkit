namespace MaterialDesignThemes.Wpf
{
    public static class TabAssist
    {
        public static readonly DependencyProperty HasFilledTabProperty = DependencyProperty.RegisterAttached(
            "HasFilledTab", typeof(bool), typeof(TabAssist), new PropertyMetadata(false));

        public static void SetHasFilledTab(DependencyObject element, bool value) => element.SetValue(HasFilledTabProperty, value);

        public static bool GetHasFilledTab(DependencyObject element) => (bool)element.GetValue(HasFilledTabProperty);

    }
}