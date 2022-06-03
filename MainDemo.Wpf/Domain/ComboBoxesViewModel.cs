using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MaterialDesignDemo.Domain
{
    public class ComboBoxesViewModel : ViewModelBase
    {
        private int? _selectedValueOne;
        private string? _selectedTextTwo;
        private string? _selectedValidationOutlined;
        private string? _selectedValidationFilled;

        public ComboBoxesViewModel()
        {
            LongIntegerList = new List<int>(Enumerable.Range(0, 1000));
            ShortStringList = new[]
            {
                "Item 1",
                "Item 2",
                "Item 3"
            };

            SelectedValueOne = LongIntegerList.Skip(2).First();
            SelectedTextTwo = null;

            LongStringList = new List<string>();

            for(int i = 0; i < 1000; i++)
            {
                LongStringList.Add(Path.GetRandomFileName());
            }
        }

        public int? SelectedValueOne
        {
            get => _selectedValueOne;
            set => SetProperty(ref _selectedValueOne, value);
        }

        public string? SelectedTextTwo
        {
            get => _selectedTextTwo;
            set => SetProperty(ref _selectedTextTwo, value);
        }

        public string? SelectedValidationFilled
        {
            get => _selectedValidationFilled;
            set => SetProperty(ref _selectedValidationFilled, value);
        }

        public string? SelectedValidationOutlined
        {
            get => _selectedValidationOutlined;
            set => SetProperty(ref _selectedValidationOutlined, value);
        }

        public IList<int> LongIntegerList { get; }
        public IList<string> ShortStringList { get; }
        public IList<string> LongStringList { get; }
    }
}
