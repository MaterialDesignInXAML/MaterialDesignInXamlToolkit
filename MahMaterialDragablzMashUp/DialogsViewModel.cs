using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;

namespace MahMaterialDragablzMashUp
{
    public class DialogsViewModel
    {
        public ICommand ShowInputDialogCommand { get; }

        public ICommand ShowProgressDialogCommand { get; }

        public DialogsViewModel()
        {
            ShowInputDialogCommand = new AnotherCommandImplementation(_ => InputDialog());
            ShowProgressDialogCommand = new AnotherCommandImplementation(_ => ProgressDialog());
        }

        private void InputDialog()
        {
            var metroDialogSettings = new MetroDialogSettings
            {
                CustomResourceDictionary =
                    new ResourceDictionary
                    {
                        Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.MahApps.Dialogs.xaml")
                    },
                NegativeButtonText = "CANCEL",                
                SuppressDefaultResources = true
            };

            DialogCoordinator.Instance.ShowInputAsync(this, "MahApps Dialog", "Using Material Design Themes", metroDialogSettings);
        }

        private void ProgressDialog()
        {
            var metroDialogSettings = new MetroDialogSettings
            {
                CustomResourceDictionary =
                    new ResourceDictionary
                    {
                        Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.MahApps.Dialogs.xaml")
                    },
                NegativeButtonText = "CANCEL",
                SuppressDefaultResources = true
            };

            DialogCoordinator.Instance.ShowProgressAsync(this, "MahApps Dialog", "Using Material Design Themes (WORK IN PROGRESS)", true, metroDialogSettings);
        }
    }    
}
