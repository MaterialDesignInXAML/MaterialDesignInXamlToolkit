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
			"Hint", typeof (string), typeof (TextField), new PropertyMetadata(default(string)));

		public static void SetHint(DependencyObject element, string value)
		{
			element.SetValue(HintProperty, value);
		}

		public static string GetHint(DependencyObject element)
		{
			return (string) element.GetValue(HintProperty);
		}
	}
}
