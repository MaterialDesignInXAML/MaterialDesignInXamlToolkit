using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace MaterialDesignColors.WpfExample.Domain
{
    public class DemoItem : INotifyPropertyChanged
    {
        private string _name;
        private object _content;

        public DemoItem(string name, object content, IEnumerable<DocumentationLink> documentation)
        {
            _name = name;
            Content = content;
            Documentation = documentation;
        }

        public string Name
        {
            get { return _name; }
            set { this.MutateVerbose(ref _name, value, RaisePropertyChanged()); }
        }

        public object Content
        {
            get { return _content; }
            set { this.MutateVerbose(ref _content, value, RaisePropertyChanged()); }
        }

        public IEnumerable<DocumentationLink> Documentation { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
