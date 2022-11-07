﻿using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo.Domain
{
    public class DemoItem : ViewModelBase
    {
        private readonly Type _contentType;
        private readonly object? _dataContext;

        private object? _content;
        private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement;
        private ScrollBarVisibility _verticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto;
        private Thickness _marginRequirement = new(16);

        private int _notificationNumber = 0;

        public DemoItem(string name, Type contentType, IEnumerable<DocumentationLink> documentation,
            PackIconKind selectedIcon, PackIconKind unselectedIcon, object? dataContext = null)
        {
            Name = name;
            _contentType = contentType;
            _dataContext = dataContext;
            Documentation = documentation;
            SelectedIcon = selectedIcon;
            UnselectedIcon = unselectedIcon;
        }

        public string Name { get; }

        public IEnumerable<DocumentationLink> Documentation { get; }

        public object? Content => _content ??= CreateContent();

        public PackIconKind SelectedIcon { get; set; }
        public PackIconKind UnselectedIcon { get; set; }

        public object? Notifications
        {
            get
            {
                if (_notificationNumber == 0) return null;
                else return _notificationNumber < 100 ? _notificationNumber : "99+";
            }
        }

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

        public void AddNewNotification()
        {
            _notificationNumber++;
            OnPropertyChanged(nameof(Notifications));
        }

        public void DismissAllNotifications()
        {
            _notificationNumber = 0;
            OnPropertyChanged(nameof(Notifications));
        }
    }
}
