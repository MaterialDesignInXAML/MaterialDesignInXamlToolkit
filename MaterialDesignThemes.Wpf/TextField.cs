using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;

namespace MaterialDesignThemes.Wpf
{
	public static class TextField
	{
		public static readonly DependencyProperty TextBoxViewMarginProperty = DependencyProperty.RegisterAttached(
			"TextBoxViewMargin", typeof (Thickness), typeof (TextField), new PropertyMetadata(new Thickness(double.NegativeInfinity), TextBoxViewMarginPropertyChangedCallback));

		private static void TextBoxViewMarginPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			var textBoxBase = dependencyObject as TextBoxBase;
			if (textBoxBase == null) return;

			if (textBoxBase.IsLoaded)
				ApplyTextBoxViewMargin(textBoxBase, (Thickness)dependencyPropertyChangedEventArgs.NewValue);

			textBoxBase.Loaded += (sender, args) =>
			{
				var textBox = (TextBoxBase) sender;
				ApplyTextBoxViewMargin(textBox, GetTextBoxViewMargin(textBox));
			};			
        }

		public static void SetTextBoxViewMargin(DependencyObject element, Thickness value)
		{			
            element.SetValue(TextBoxViewMarginProperty, value);			
		}

		public static Thickness GetTextBoxViewMargin(DependencyObject element)
		{
			return (Thickness) element.GetValue(TextBoxViewMarginProperty);
		}

		private static void ApplyTextBoxViewMargin(TextBoxBase textBox, Thickness margin)
		{
			if (margin.Equals(new Thickness(double.NegativeInfinity))) return;

			var scrollViewer = textBox.Template.FindName("PART_ContentHost", textBox) as ScrollViewer;
			if (scrollViewer == null) return;
			var frameworkElement = scrollViewer.Content as FrameworkElement;

			//remove nice new sytax until i get appveyor working	
			//var frameworkElement = (textBox.Template.FindName("PART_ContentHost", textBox) as ScrollViewer)?.Content as FrameworkElement;
			if (frameworkElement != null)
				frameworkElement.Margin = margin;
		}

		public static readonly DependencyProperty HintProperty = DependencyProperty.RegisterAttached(
			"Hint", typeof (string), typeof (TextField), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.Inherits));

		public static void SetHint(DependencyObject element, string value)
		{
			element.SetValue(HintProperty, value);
		}

		public static string GetHint(DependencyObject element)
		{
			return (string) element.GetValue(HintProperty);
		}
	}

    public static class PasswordField
    {
        public static readonly DependencyProperty ManagedProperty = DependencyProperty.RegisterAttached(
            "Managed", typeof (PasswordBox), typeof (PasswordField), new PropertyMetadata(default(PasswordBox), ManagedPropertyChangedCallback));

        private static void ManagedPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var passwordBox = dependencyPropertyChangedEventArgs.OldValue as PasswordBox;
            if (passwordBox != null)
            {
                passwordBox.PasswordChanged -= PasswordBoxOnPasswordChanged;
            }

            passwordBox = dependencyPropertyChangedEventArgs.NewValue as PasswordBox;
            if (passwordBox != null)
            {
                passwordBox.PasswordChanged += PasswordBoxOnPasswordChanged;
                ConfigureHint(passwordBox);
            }
        }

        private static void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            ConfigureHint((PasswordBox)sender);
        }

        private static void ConfigureHint(PasswordBox passwordBox)
        {
            passwordBox.SetValue(HintVisibilityProperty, passwordBox.SecurePassword.Length == 0 ? Visibility.Visible : Visibility.Hidden);
        }

        public static readonly DependencyProperty HintVisibilityProperty = DependencyProperty.RegisterAttached(
            "HintVisibility", typeof (Visibility), typeof (PasswordField), new PropertyMetadata(default(Visibility)));

        public static void SetHintVisibility(DependencyObject element, Visibility value)
        {
            element.SetValue(HintVisibilityProperty, value);
        }

        public static Visibility GetHintVisibility(DependencyObject element)
        {
            return (Visibility) element.GetValue(HintVisibilityProperty);
        }

        public static void SetManaged(DependencyObject element, PasswordBox value)
        {
            element.SetValue(ManagedProperty, value);
        }

        public static PasswordBox GetManaged(DependencyObject element)
        {
            return (PasswordBox) element.GetValue(ManagedProperty);
        }
    }
}
