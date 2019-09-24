using System.Collections.ObjectModel;

namespace MahAppsDragablzDemo
{
    public class MahViewModel
    {
        public ObservableCollection<GridRowData> GridData { get; }

        public MahViewModel()
        {
            GridData = new ObservableCollection<GridRowData> {
                new GridRowData {
                    IsChecked = false,
                    Text = "Mars",
                    EnumValue = EnumValues.ValueA,
                    IntValue = 4879
                },
                new GridRowData {
                    IsChecked = false,
                    Text = "Venus",
                    EnumValue = EnumValues.ValueB,
                    IntValue = 12104
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Earth",
                    EnumValue = EnumValues.ValueC,
                    IntValue = 12742
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Merkur",
                    EnumValue = EnumValues.ValueA,
                    IntValue = 6779
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Jupiter",
                    EnumValue = EnumValues.ValueD,
                    IntValue = 139822
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Saturn",
                    EnumValue = EnumValues.ValueC,
                    IntValue = 116464
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Uranus",
                    EnumValue = EnumValues.ValueC,
                    IntValue = 50724
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Neptun",
                    EnumValue = EnumValues.ValueB,
                    IntValue = 49244
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Pluto",
                    EnumValue = EnumValues.ValueA,
                    IntValue = 2370
                }
            };
        }
    }

    public class GridRowData
    {
        public bool IsChecked { get; set; }
        public string Text { get; set; }
        public EnumValues EnumValue { get; set; }
        public int IntValue { get; set; }
    }

    public enum EnumValues
    {
        ValueA, ValueB, ValueC, ValueD
    }
}
