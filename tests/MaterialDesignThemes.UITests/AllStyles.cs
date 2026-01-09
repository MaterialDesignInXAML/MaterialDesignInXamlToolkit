using MaterialDesignColors;

namespace MaterialDesignThemes.UITests;

public class AllStyles : TestBase
{
    [Test]
    [Arguments("Button", "MaterialDesignRaisedButton")]
    [Arguments("Calendar", "MaterialDesignCalendarPortrait")]
    [Arguments("CheckBox", "MaterialDesignCheckBox")]
    [Arguments("ComboBox", "MaterialDesignComboBox")]
    [Arguments("DataGrid", "MaterialDesignDataGrid")]
    [Arguments("DatePicker", "MaterialDesignDatePicker")]
    [Arguments("Expander", "MaterialDesignExpander")]
    [Arguments("GridSplitter", "MaterialDesignGridSplitter")]
    [Arguments("GroupBox", "MaterialDesignGroupBox")]
    [Arguments("Label", "MaterialDesignLabel")]
    [Arguments("ListBox", "MaterialDesignListBox")]
    [Arguments("ListView", "MaterialDesignListView")]
    [Arguments("Menu", "MaterialDesignMenu")]
    [Arguments("PasswordBox", "MaterialDesignPasswordBox")]
    [Arguments("ProgressBar", "MaterialDesignLinearProgressBar")]
    [Arguments("RadioButton", "MaterialDesignRadioButton")]
    [Arguments("RichTextBox", "MaterialDesignRichTextBox")]
    [Arguments("ScrollBar", "MaterialDesignScrollBar")]
    [Arguments("ScrollViewer", "MaterialDesignScrollViewer")]
    [Arguments("Slider", "MaterialDesignSlider")]
    [Arguments("TabControl", "MaterialDesignTabControl")]
    [Arguments("TextBox", "MaterialDesignTextBox")]
    [Arguments("ToggleButton", "MaterialDesignSwitchToggleButton")]
    [Arguments("ToolBar", "MaterialDesignToolBar")]
    [Arguments("TreeView", "MaterialDesignTreeView")]
    public async Task LoadStyleInIsolation_CanBeLoaded(string controlName, string styleName)
    {
        string applicationResourceXaml = $$"""
<ResourceDictionary 
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes">
  <ResourceDictionary.MergedDictionaries>
    <materialDesign:BundledTheme BaseTheme="{{BaseTheme.Inherit}}" PrimaryColor="{{PrimaryColor.Purple}}" SecondaryColor="{{SecondaryColor.Blue}}" />

    <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{{controlName}}.xaml" />
  </ResourceDictionary.MergedDictionaries>

  <Style TargetType="{x:Type {{controlName}}}" BasedOn="{StaticResource {{styleName}}}" />
</ResourceDictionary>
""";

        await App.Initialize(applicationResourceXaml,
            Path.GetFullPath("MaterialDesignColors.dll"),
            Path.GetFullPath("MaterialDesignThemes.Wpf.dll"),
            System.Reflection.Assembly.GetExecutingAssembly().Location);

        IWindow window = await App.CreateWindow($$"""
             <Window
                    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
                    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
                    xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes"
                    mc:Ignorable="d"
                    Height="800" Width="1100"
                    TextElement.Foreground="{DynamicResource MaterialDesignBody}"
                    TextElement.FontWeight="Regular"
                    TextElement.FontSize="13"
                    TextOptions.TextFormattingMode="Ideal" 
                    TextOptions.TextRenderingMode="Auto"
                    Background="{DynamicResource MaterialDesignPaper}"
                    FontFamily="{materialDesign:MaterialDesignFont}" 
                    Title="Test Window"
                    Topmost="True"
                    WindowStartupLocation="CenterScreen">
              <{{controlName}} />
            </Window>
            """);

        await Assert.That(await window.GetIsVisible()).IsTrue();
    }
}
