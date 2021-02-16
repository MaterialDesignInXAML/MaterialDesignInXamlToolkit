using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo.Domain
{
    public class IconPackViewModel : ViewModelBase
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
                    .Select(g => new PackIconKindGroup(g))
                    .OrderBy(x => x.Kind)
                    .ToList());
        }

        public ICommand OpenDotComCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand CopyToClipboardCommand { get; }

        private IEnumerable<PackIconKindGroup>? _kinds;
        private PackIconKindGroup? _group;
        private string? _kind;
        private PackIconKind _packIconKind;

        public IEnumerable<PackIconKindGroup> Kinds
        {
            get => _kinds ??= _packIconKinds.Value;
            set => SetProperty(ref _kinds, value);
        }

        public PackIconKindGroup? Group
        {
            get => _group;
            set
            {
                if (SetProperty(ref _group, value))
                {
                    Kind = value?.Kind;
                }
            }
        }

        public string? Kind
        {
            get => _kind;
            set
            {
                if (SetProperty(ref _kind, value))
                    PackIconKind = value != null ? (PackIconKind) Enum.Parse(typeof(PackIconKind), value) : default;
            }
        }

        public PackIconKind PackIconKind
        {
            get => _packIconKind;
            set => SetProperty(ref _packIconKind, value);
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
            {
                Kinds = await Task.Run(() => _packIconKinds.Value
                    .Where(x => x.Aliases.Any(a => a.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0))
                    .ToList());
            }
        }

        private void CopyToClipboard(object obj)
        {
            var toBeCopied = $"<materialDesign:PackIcon Kind=\"{obj}\" />";
            Clipboard.SetDataObject(toBeCopied);
            _snackbarMessageQueue.Enqueue(toBeCopied + " copied to clipboard");
        }
    }
}