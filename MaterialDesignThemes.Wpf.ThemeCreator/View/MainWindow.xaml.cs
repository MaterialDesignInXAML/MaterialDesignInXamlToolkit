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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace MaterialDesignThemes.Wpf.ThemeCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string path = null;

        private void NewProject_Button_Click(object sender, RoutedEventArgs e)
        {
            NewProjectDialog.IsOpen = true;
        }

        private void NewProjectDialog_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter == null) { return; }
            ViewModel.EditorViewModel vm = (ViewModel.EditorViewModel)FindResource("VM");
            if (newdarkbased.IsChecked ?? false) { vm.Brushes = Model.BrushColor.FromDark(); } else { vm.Brushes = Model.BrushColor.FromLight(); }
            CustomBaseColorTheme theme = BuildTheme();
            ApplyTheme(theme);
        }

        private void OpenProject_Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "XAML Files (*.xaml|*.xaml;)";
            ofd.Multiselect = false;
            ofd.CheckFileExists = true;
            ofd.Title = "Select theme project file to edit...";
            if (ofd.ShowDialog() == false) { return; }

            path = ofd.FileName;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    CustomBaseColorTheme project = (CustomBaseColorTheme)XamlReader.Load(fs);
                    fs.Close();
                    fs.Dispose();
                    ViewModel.EditorViewModel vm = (ViewModel.EditorViewModel)FindResource("VM");
                    vm.Brushes = Model.BrushColor.LoadFromXAML(project);
                    CustomBaseColorTheme theme = BuildTheme();
                    ApplyTheme(theme);
                }
            }
            catch //(Exception ex)
            {
                commonDialogMessage.Text = "Invalid XAML File";
                CommonDialog.IsOpen = true;
            }
        }

        private void SaveProject_Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(path))
            { 
                SaveProjectAs_Button_Click(sender, e);
                return;
            }
            Flush();
        }

        private void SaveProjectAs_Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "XAML Files (*.xaml|*.xaml;)";
            sfd.Title = "Choose the location to save...";
            sfd.FileName = "YourOwnThemeColors.xaml";
            if (sfd.ShowDialog() == false) { return; }
            path = sfd.FileName;
            Flush();
        }

        private void Flush()
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                var th = BuildTheme();
                //XamlWriter.Save(th, fs);
                var xmlWriterSettings = new XmlWriterSettings() { Indent = true, NewLineOnAttributes = true };
                using (XmlWriter xmlWriter = XmlWriter.Create(fs, xmlWriterSettings))
                {
                    th.Clear();
                    System.Xaml.XamlServices.Save(xmlWriter, th);
                }
                fs.Close();
                fs.Dispose();
            }
        }


        private bool lockrefresh = true;
        private void EditorViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (lockrefresh) { return; }
            CustomBaseColorTheme builttheme = BuildTheme();
            ApplyTheme(builttheme);
        }

        private void EditColorButton_Click(object sender, RoutedEventArgs e)
        {
            coloreditordialoghost.IsOpen = true;

            ViewModel.EditorViewModel vm = (ViewModel.EditorViewModel)FindResource("VM");
            Model.BrushColor entry = (Model.BrushColor)((Button)sender).DataContext;
            if (entry == null) { return; }

            coloreditordialogacceptbutton.CommandParameter = entry;
            cpicker.Color = entry.Color;
        }

        private void AcceptColor(object sender, DialogClosingEventArgs eventArgs)
        {
            lockrefresh = false;
            ViewModel.EditorViewModel vm = (ViewModel.EditorViewModel)FindResource("VM");
            Model.BrushColor entry = (Model.BrushColor)eventArgs.Parameter;
            if (entry == null) { return; }
            entry.Color = cpicker.Color;
            lockrefresh = true;
            CustomBaseColorTheme theme = BuildTheme();
            ApplyTheme(theme);
        }

        private CustomBaseColorTheme BuildTheme()
        {
            ViewModel.EditorViewModel vm = (ViewModel.EditorViewModel)FindResource("VM");

            CustomBaseColorTheme result = new CustomBaseColorTheme();
            result.DesignTime = true;
            foreach (Model.BrushColor entry in vm.Brushes)
            {
                result.GetType().GetProperty(entry.Name).SetValue(result, entry.Color);
            }
            return result;
        }

        private void ApplyTheme(CustomBaseColorTheme theme)
        {
            samplegrid.Resources.MergedDictionaries[0] = theme;
            theme.SetTheme();
        }

    }
}
