using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;
using System.Xml;
using CodeDisplayer;
using MaterialDesignColors.WpfExample.Domain;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for TextFields.xaml
    /// </summary>
    public partial class TextFields : UserControl
    {
        public TextFields()
        {
            InitializeComponent();	        
			DataContext = new TextFieldsViewModel();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(@"..\..\TextFields.xaml");
            //XmlDocument xmlDoc =
            //    new MaterialDesignInXamlToolkitGitHubFile(
            //            ownerName: "wongjiahau" ,
            //            branchName: "New-Demo-2" ,
            //            fileName: "TextFields.xaml")
            //        .GetXmlDocument();
            XamlDisplayerPanel.Initialize(xmlDoc);

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {            
        }

    }
}
