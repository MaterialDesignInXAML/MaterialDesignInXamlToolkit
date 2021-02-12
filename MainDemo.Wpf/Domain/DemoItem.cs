using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignDemo.Domain
{
    public class DemoItem : ViewModelBase
    {
        private readonly Type _contentType;
        private readonly object? _dataContext;

        private object? _content;
        private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement;
        private ScrollBarVisibility _verticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto;
        private Thickness _marginRequirement = new(16);

        public DemoItem(string name, Type contentType, IEnumerable<DocumentationLink> documentation, object? dataContext = null)
        {
            Name = name;
            _contentType = contentType;
            _dataContext = dataContext;
            Documentation = documentation;
        }

        public string Name { get; }

        public IEnumerable<DocumentationLink> Documentation { get; }

        public object? Content => _content ??= CreateContent();

        public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement
        {
            get => _horizontalScrollBarVisibilityRequirement;
            set => SetProperty(ref _horizontalScrollBarVisibilityRequirement, value);
        }

        public ScrollBarVisibility VerticalScrollBarVisibilityRequirement
        {
            get => _verticalScrollBarVisibilityRequirement;
            set => SetProperty(ref _verticalScrollBarVisibilityRequirement, value);
        }

        public Thickness MarginRequirement
        {
            get => _marginRequirement;
            set => SetProperty(ref _marginRequirement, value);
        }

        private object? CreateContent()
        {
            var content = Activator.CreateInstance(_contentType);
            if (_dataContext != null && content is FrameworkElement element)
            {
                element.DataContext = _dataContext;
            }

            return content;
        }
    }
}
