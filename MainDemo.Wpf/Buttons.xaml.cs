using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using CodeDisplayer;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignDemo.Helper;

namespace MaterialDesignColors.WpfExample
{
	/// <summary>
	/// Interaction logic for Buttons.xaml
	/// </summary>
	public partial class Buttons : UserControl
	{
		public Buttons()
		{
			InitializeComponent();
			FloatingActionDemoCommand = new AnotherCommandImplementation(Execute);
			XamlDisplayerPanel.Initialize(new SourceRouter(this.GetType().Name).GetSource());
		}

		public ICommand FloatingActionDemoCommand { get; }

		private void Execute(object o)
		{
			Console.WriteLine("Floating action button command. - " + (o ?? "NULL").ToString());
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("Just checking we haven't suppressed the button.");
		}

		private void PopupBox_OnOpened(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("Just making sure the popup has opened.");
		}

		private void PopupBox_OnClosed(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("Just making sure the popup has closed.");
		}

		private void CountingButton_OnClick(object sender, RoutedEventArgs e)
		{
			if (CountingBadge.Badge == null || Equals(CountingBadge.Badge, ""))
				CountingBadge.Badge = 0;

			var next = int.Parse(CountingBadge.Badge.ToString()) + 1;

			CountingBadge.Badge = next < 21 ? (object)next : null;

		}
	}
}
