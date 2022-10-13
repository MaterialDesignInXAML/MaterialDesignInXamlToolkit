using System.Windows.Media;
using MaterialDesignColors;
using Microsoft.Win32;

namespace MaterialDesignThemes.Wpf.Theming
{
    public class Theme
    {
        public static Theme Light { get; } = new LightTheme();
        public static Theme Dark { get; } = new DarkTheme();

        /// <summary>
        /// Get the current Windows theme.
        /// Based on ControlzEx
        /// https://github.com/ControlzEx/ControlzEx/blob/48230bb023c588e1b7eb86ea83f7ddf7d25be735/src/ControlzEx/Theming/WindowsThemeHelper.cs#L19
        /// </summary>
        /// <returns></returns>
        public static BaseTheme? GetSystemTheme()
        {
            try
            {
                var registryValue = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", null);

                if (registryValue is null)
                {
                    return null;
                }

                return Convert.ToBoolean(registryValue) ? BaseTheme.Light : BaseTheme.Dark;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //TODO: is this useful?
        //public static Theme Create(IBaseTheme baseTheme, Color primary, Color accent)
        //{
        //    if (baseTheme is null) throw new ArgumentNullException(nameof(baseTheme));
        //    var theme = new Theme();

        //    theme.SetBaseTheme(baseTheme);
        //    theme.SetPrimaryColor(primary);
        //    theme.SetSecondaryColor(accent);

        //    return theme;
        //}

        internal Theme()
        { }

        public static Theme Create(BaseTheme baseTheme, Color primary, Color accent)
        {
            var theme = new Theme();

            theme.SetBaseTheme(baseTheme);
            theme.SetPrimaryColor(primary);
            theme.SetSecondaryColor(accent);

            return theme;
        }

        public ColorPair PrimaryLight { get; set; }
        public ColorPair PrimaryMid { get; set; }
        public ColorPair PrimaryDark { get; set; }

        public ColorPair SecondaryLight { get; set; }
        public ColorPair SecondaryMid { get; set; }
        public ColorPair SecondaryDark { get; set; }

        public Color Background { get; set; }
        public Color Foreground { get; set; }
        public Color LightForeground { get; set; }

        public Color ValidationError { get; set; }

        public ButtonBrushes Button { get; } = new();
        public CardBrushes Card { get; } = new();
        public CheckBoxBrushes CheckBox { get; } = new();
        public ChipBrushes Chip { get; } = new();
        public ColorPickerBrushes ColorPicker { get; } = new();
        public ComboBoxBrushes ComboBox { get; } = new();
        public DataGridBrushes DataGrid { get; } = new();
        public DatePickerBrushes DatePicker { get; } = new();
        public DrawerHostBrushes DrawerHost { get; } = new();
        public GridSplitterBrushes GridSplitter { get; } = new();
        public ListBoxBrushes ListBox { get; } = new();
        public ListViewBrushes ListView { get; } = new();
        public MenuBrushes Menu { get; } = new();
        public PasswordBoxBrushes PasswordBox { get; } = new();
        public RadioButtonBrushes RadioButton { get; } = new();
        public RatingBarBrushes RatingBar { get; } = new();
        public ScrollBarBrushes ScrollBar { get; } = new();
        public SeparatorBrushes Separator { get; } = new();
        public SliderBurshes Slider { get; } = new();
        public SnackBarBrushes SnackBar { get; } = new();
        public TabControlBrushes TabControl { get; } = new();
        public TextBoxBrushes TextBox { get; } = new();
        public TimePickerBrushes TimePicker { get; } = new();
        public ToggleButtonBrushes ToggleButton { get; } = new();
        public ToolBarBrushes ToolBar { get; } = new();
        public ToolTipBrushes ToolTip { get; } = new();

        //TODO: Conver these to control specific brushes
        public Color Divider { get; set; }
        public Color Selection { get; set; }

        public ColorAdjustment? ColorAdjustment { get; set; }

        public sealed class ButtonBrushes
        {
            public Color FlatClick { get; set; }
            public Color FlatRipple { get; set; }
        }

        public sealed class CardBrushes
        {
            public Color Background { get; set; }
        }

        public sealed class CheckBoxBrushes
        {
            public Color Off { get; set; }
            public Color Disabled { get; set; }
        }

        public sealed class ChipBrushes
        {
            public Color Background { get; set; }
            public Color OutlineCheckedBorder { get; set; }
        }

        public sealed class ColorPickerBrushes
        {
            public Color SliderThumbDisabled { get; set; }
        }

        public sealed class ComboBoxBrushes
        {
            public Color Border { get; set; }
            public Color HoverBackground { get; set; }
            public Color ForegroundDisabled { get; set; }
            public Color FilledBackground { get; set; }
            public Color OutlineInactiveBorder { get; set; }
        }

        public sealed class DataGridBrushes
        {
            public Color Border { get; set; }
            public Color CellFocusBorder { get; set; }
            public Color CellSelectedBackground { get; set; }
            public Color ColumnHeaderForeground { get; set; }
            public Color PopupBorder { get; set; }
            public Color RowHoverBackground { get; set; }
            public Color RowSelectedBackground { get; set; }
            public Color SelectAllButtonPressed { get; set; }

            public DataGridComboBoxBrushes ComboBox { get; } = new();
        }

        public sealed class DataGridComboBoxBrushes
        {
            public Color Border { get; set; }

            public Color ItemHoverBackground { get; set; }
            public Color ItemSelectedBackground { get; set; }

            public Color ToggleDisabled { get; set; }
        }

        public sealed class DatePickerBrushes
        {
            public Color Border { get; set; }
            public Color HoverBackground { get; set; }
            public Color FilledBackground { get; set; }
            public Color OutlineBorder { get; set; }
            public Color OutlineInactiveBorder { get; set; }
        }

        public sealed class DrawerHostBrushes
        {
            public Color OverlayBackground { get; set; } = Colors.Black;
            public Color LeftDrawerBackground { get; set; }
            public Color TopDrawerBackground { get; set; }
            public Color RightDrawerBackground { get; set; }
            public Color BottomDrawerBackground { get; set; }
        }

        public sealed class GridSplitterBrushes
        {
            public Color Background { get; set; }
            public Color PreviewBackground { get; set; }
        }

        public sealed class ListBoxBrushes
        {
            public Color Border { get; set; }
            public Color SelectedBackground { get; set; }
        }

        public sealed class ListViewBrushes
        {
            public Color Border { get; set; }
            public Color ColumnHeaderForeground { get; set; }
            public Color HoverBackground { get; set; }
            public Color FocusBorder { get; set; }
            public Color SelectedBackground { get; set; }
        }

        public sealed class MenuBrushes
        {

        }

        public sealed class PasswordBoxBrushes
        {
            public Color Border { get; set; }
            public Color HoverBackground { get; set; }
            public Color FilledBackground { get; set; }
            public Color OutlineBorder { get; set; }
            public Color OutlineInactiveBorder { get; set; }
        }

        public sealed class RadioButtonBrushes
        {
            public Color Checked { get; set; }
            public Color Off { get; set; }
            public Color Disabled { get; set; }
            public Color TabProgressIndicatorForeground { get; set; }
            public Color TabRipple { get; set; }
            public Color ToolBorder { get; set; }
        }

        public sealed class RatingBarBrushes
        {
            public Color Ripple { get; set; }
        }

        public sealed class ScrollBarBrushes
        {
            public Color Foreground { get; set; }
            public Color Border { get; set; }
            public Color Pressed { get; set; }
        }

        public sealed class SeparatorBrushes
        {
            public Color Background { get; set; }
            public Color Border { get; set; }
        }

        public sealed class SliderBurshes
        {
            public Color Disabled { get; set; }
            public Color LabelBackground { get; set; }
        }

        public sealed class SnackBarBrushes
        {
            public Color Background { get; set; }
            public Color MouseOver { get; set; }
            public Color Ripple { get; set; }
        }

        public sealed class TabControlBrushes
        {
            public Color Ripple { get; set; }
            public Color TabDivider { get; set; }
        }

        public sealed class TextBoxBrushes
        {
            public Color Border { get; set; }
            public Color HoverBackground { get; set; }
            public Color DisabledBackground { get; set; }

            public Color FilledBackground { get; set; }

            public Color OutlineBorder { get; set; }
            public Color OutlineInactiveBorder { get; set; }
        }

        public sealed class TimePickerBrushes
        {
            public Color Border { get; set; }
            public Color HoverBackground { get; set; }

            public Color FilledBackground { get; set; }

            public Color OutlineBorder { get; set; }
            public Color OutlineInactiveBorder { get; set; }
        }

        public sealed class ToggleButtonBrushes
        {

        }

        public sealed class ToolBarBrushes
        {
            public Color Background { get; set; }

            public Color HoverBackground { get; set; }

            public Color OverflowBackground { get; set; }
            public Color OverflowBorder { get; set; }

            public Color Ripple { get; set; }

            public Color Separator { get; set; }

            public Color ThumbForeground { get; set; }

            public ToolBarItemBrushes Item { get; } = new();
        }

        public sealed class ToolBarItemBrushes
        {
            public Color Foreground { get; set; }
            public Color Background { get; set; }
        }

        public sealed class ToolTipBrushes
        {
            public Color Background { get; set; }
            public Color Foreground { get; set; }
        }
    }
}
