using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VTTests
{
    public static class VTMixins
    {
        public static async Task<IVisualElement> CreateWindowWith(this IApp app, string xaml)
        {
            string windowXaml = @$"<Window
        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
        xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
        xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
        xmlns:materialDesign=""http://materialdesigninxaml.net/winfx/xaml/themes""
        mc:Ignorable=""d""
        Height=""800"" Width=""1100""
        TextElement.Foreground=""{{DynamicResource MaterialDesignBody}}""
        TextElement.FontWeight=""Regular""
        TextElement.FontSize=""13""
        TextOptions.TextFormattingMode=""Ideal"" 
        TextOptions.TextRenderingMode=""Auto""
        Background=""{{DynamicResource MaterialDesignPaper}}""
        FontFamily=""{{materialDesign:MaterialDesignFont}}"" 
        Title=""Test Window""
        WindowStartupLocation=""CenterScreen"">
        {xaml}
</Window>";
            IWindow window = await app.CreateWindow(windowXaml);
            return await window.GetElement(".Content");
        }

        public static async Task<IVisualElement> CreateWindowWithUserControl<TControl>(this IApp app)
            where TControl : UserControl
        {
            string windowXaml = @$"<Window
        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
        xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
        xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
        xmlns:materialDesign=""http://materialdesigninxaml.net/winfx/xaml/themes""
        xmlns:local=""clr-namespace:{typeof(TControl).Namespace};assembly={typeof(TControl).Assembly.GetName().Name}""
        mc:Ignorable=""d""
        Height=""800"" Width=""1100""
        TextElement.Foreground=""{{DynamicResource MaterialDesignBody}}""
        TextElement.FontWeight=""Regular""
        TextElement.FontSize=""13""
        TextOptions.TextFormattingMode=""Ideal"" 
        TextOptions.TextRenderingMode=""Auto""
        Background=""{{DynamicResource MaterialDesignPaper}}""
        FontFamily=""{{materialDesign:MaterialDesignFont}}"" 
        Title=""Test Window""
        WindowStartupLocation=""CenterScreen"">
        <local:{typeof(TControl).Name} />
</Window>";
            IWindow window = await app.CreateWindow(windowXaml);
            return await window.GetElement(".Content.Content");
        }

        public static async Task Save(this IImage image, string filePath)
        {
            await using var file = File.OpenWrite(filePath);
            await image.Save(file);
        }
    }
}
