using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignColors.WpfExample;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo
{
    public class IconPackViewModel : INotifyPropertyChanged
    {
        private readonly Lazy<IEnumerable<PackIconKind>> _packIconKinds;

        public IconPackViewModel()
        {
            OpenDotComCommand = new AnotherCommandImplementation(OpenDotCom);
            SearchCommand = new AnotherCommandImplementation(Search);
            CopyToClipboardCommand = new AnotherCommandImplementation(CopyToClipboard);
            _packIconKinds = new Lazy<IEnumerable<PackIconKind>>(() =>
                Enum.GetValues(typeof (PackIconKind)).OfType<PackIconKind>()
                    .OrderBy(k => k.ToString(), StringComparer.InvariantCultureIgnoreCase).ToList()
                );

        }

        public ICommand OpenDotComCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand CopyToClipboardCommand { get; }

        private IEnumerable<PackIconKind> _kinds;
        public IEnumerable<PackIconKind> Kinds
        {
            get { return _kinds ?? (_kinds = _packIconKinds.Value); }
            set
            {
                _kinds = value;
                OnPropertyChanged();
            }
        }

        private void OpenDotCom(object obj)
        {
            Process.Start("https://materialdesignicons.com/");
        }

        private void Search(object obj)
        {
            var text = obj as string;
            if (string.IsNullOrWhiteSpace(text))
                Kinds = _packIconKinds.Value;
            else
                Kinds =
                    _packIconKinds.Value.Where(
                        x => x.ToString().IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0);
        }

        private void CopyToClipboard(object obj)
        {
            var kind = (PackIconKind?)obj;
            string toBeCopied = $"<materialDesign:PackIcon Kind=\"{kind}\" />";
            Clipboard.SetDataObject(toBeCopied);
            MainWindow.Snackbar.MessageQueue.Enqueue($"{toBeCopied} is copied to clipboard.");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
