using System;
using System.ComponentModel;
using System.Linq;

namespace MaterialDesignDemo.Domain
{
    public class FieldsViewModel : INotifyPropertyChanged
    {
        private string? _name;
        private string? _name2;

        public string? Name
        {
            get => _name;
            set => this.MutateVerbose(ref _name, value, RaisePropertyChanged());
        }

        public string? Name2
        {
            get => _name2;
            set => this.MutateVerbose(ref _name2, value, RaisePropertyChanged());
        }

        public DemoItem DemoItem => new DemoItem("Mr. Test", null, Enumerable.Empty<DocumentationLink>());

        public event PropertyChangedEventHandler? PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged() => args => PropertyChanged?.Invoke(this, args);
    }
}
