using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignDemo.Domain;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MaterialDesignDemo
{
    public class PackIconKindGroup
    {
        public PackIconKindGroup(PackIconKind kind, string[] all)
        {
            Kind = kind;
            Aliases = all;
            PrimaryName = all[0];
            ToolTip = string.Join(Environment.NewLine, all);
        }

        public PackIconKind Kind { get; }
        public string[] Aliases { get; }
        public string PrimaryName { get; }
        public string ToolTip { get; }
    }

    public class IconPackViewModel : INotifyPropertyChanged
    {
        private readonly Lazy<IEnumerable<PackIconKindGroup>> _packIconKinds;
        private readonly ISnackbarMessageQueue _snackbarMessageQueue;

        public IconPackViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            _snackbarMessageQueue = snackbarMessageQueue ?? throw new ArgumentNullException(nameof(snackbarMessageQueue));

            OpenDotComCommand = new AnotherCommandImplementation(OpenDotCom);
            SearchCommand = new AnotherCommandImplementation(Search);
            CopyToClipboardCommand = new AnotherCommandImplementation(CopyToClipboard);

            _packIconKinds = new Lazy<IEnumerable<PackIconKindGroup>>(() =>
                Enum.GetNames(typeof(PackIconKind))
                    .GroupBy(k => (PackIconKind) Enum.Parse(typeof(PackIconKind), k))
                    .Select(g => new PackIconKindGroup(g.Key, g.OrderBy(i => i.ToString(), StringComparer.InvariantCultureIgnoreCase).ToArray()))
                    .OrderBy(i => i.Aliases[0], StringComparer.InvariantCultureIgnoreCase)
                    .ToList());
        }

        public ICommand OpenDotComCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand CopyToClipboardCommand { get; }

        private IEnumerable<PackIconKindGroup> _kinds;
        private PackIconKindGroup _group;
        private PackIconKind? _kind;

        public IEnumerable<PackIconKindGroup> Kinds
        {
            get => _kinds ??= _packIconKinds.Value;
            set
            {
                _kinds = value;
                OnPropertyChanged();
            }
        }

        public PackIconKindGroup Group
        {
            get => _group;
            set
            {
                _group = value;
                Kind = value?.Kind;
                OnPropertyChanged();
            }
        }

        public PackIconKind? Kind
        {
            get => _kind;
            set
            {
                _kind = value;
                OnPropertyChanged();
            }
        }

        private void OpenDotCom(object obj)
        {
            Link.OpenInBrowser("https://materialdesignicons.com/");
        }

        private async void Search(object obj)
        {
            var text = obj as string;
            if (string.IsNullOrWhiteSpace(text))
                Kinds = _packIconKinds.Value;
            else
                Kinds = await Task.Run(() => _packIconKinds.Value
                    .Where(x => x.ToolTip.ToString().IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    .ToList());
        }

        private void CopyToClipboard(object obj)
        {
            var toBeCopied = $"<materialDesign:PackIcon Kind=\"{obj}\" />";
            Clipboard.SetDataObject(toBeCopied);
            _snackbarMessageQueue.Enqueue(toBeCopied + " copied to clipboard");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}