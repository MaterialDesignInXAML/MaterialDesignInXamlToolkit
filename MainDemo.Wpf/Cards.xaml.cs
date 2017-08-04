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

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for Cards.xaml
    /// </summary>
    public partial class Cards : UserControl
    {
        public Cards()
        {
            InitializeComponent();            
            //var xmlDoc = new XmlDocument();
            //string source = File.ReadAllText(@"..\..\Cards.xaml");
            //xmlDoc.LoadXml(source);
            XmlDocument xmlDoc =
                new MaterialDesignInXamlToolkitGitHubFile(
                    ownerName: "wongjiahau" ,
                    branchName: "New-Demo-2" ,
                    fileName: "Cards.xaml")
                 .GetXmlDocument();
            XamlDisplayerPanel.Initialize(xmlDoc);
        }


        private void Flipper_OnIsFlippedChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            System.Diagnostics.Debug.WriteLine("Card is flipped = " + e.NewValue); 
        }
    }
}
