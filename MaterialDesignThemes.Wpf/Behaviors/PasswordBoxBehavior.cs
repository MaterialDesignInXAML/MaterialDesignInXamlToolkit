using Microsoft.Xaml.Behaviors;

namespace MaterialDesignThemes.Wpf.Behaviors
{
    internal class PasswordBoxBehavior : Behavior<PasswordBox>
    {
        private void PasswordBoxLoaded(object sender, RoutedEventArgs e) => PasswordBoxAssist.SetPassword(AssociatedObject, AssociatedObject.Password);

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += PasswordBoxLoaded;
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.Loaded -= PasswordBoxLoaded;
            }
            base.OnDetaching();
        }
    }
}
