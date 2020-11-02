using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using XamlTest;
using Xunit;
using Xunit.Abstractions;
using WpfButton=System.Windows.Controls.Button;

namespace MaterialDesignThemes.UITests.WPF.Button
{
    public class OutlineButtonTests : TestBase
    {
        public OutlineButtonTests(ITestOutputHelper output)
            : base(output)
        { }

        [Fact]
        public async Task OutlinedButton_UsesThemeColorForBorder()
        {
            await using var recorder = new TestRecorder(App);

            //Arrange
            IVisualElement button = await LoadXaml(
                @"<Button Content=""Button"" Style=""{StaticResource MaterialDesignOutlinedButton}""/>");
            Color midColor = await GetThemeColor("PrimaryHueMidBrush");
            IVisualElement? internalBorder = await button.GetElement("border");

            //Act
            Color? borderColor = await button.GetProperty<Color?>(nameof(WpfButton.BorderBrush));
            Color? internalBorderColor = await internalBorder.GetProperty<Color?>(nameof(Border.BorderBrush));

            //Assert
            Assert.Equal(midColor, borderColor);
            Assert.Equal(midColor, internalBorderColor);

            recorder.Success();
        }

        [Fact]
        public async Task OutlinedButton_BorderCanBeOverridden()
        {
            await using var recorder = new TestRecorder(App);

            //Arrange
            IVisualElement button = await LoadXaml(
                @"<Button Content=""Button""
                          Style=""{StaticResource MaterialDesignOutlinedButton}""
                          BorderThickness=""5""
                          BorderBrush=""Red""
                    />");
            Color midColor = await GetThemeColor("PrimaryHueMidBrush");
            IVisualElement? internalBorder = await button.GetElement("border");

            //Act
            Thickness borderThickness = await internalBorder.GetProperty<Thickness>(nameof(Border.BorderThickness));
            SolidColorBrush borderBrush = await internalBorder.GetProperty<SolidColorBrush>(nameof(Border.BorderBrush));

            //Assert
            Assert.Equal(new Thickness(5), borderThickness);
            Assert.Equal(Colors.Red, borderBrush.Color);

            recorder.Success();
        }

        [Fact]
        public async Task OutlinedButton_OnMouseOver_UsesThemeBrush()
        {
            await using var recorder = new TestRecorder(App);

            //Arrange
            IVisualElement button = await LoadXaml(
                @"<Button Content=""Button"" Style=""{StaticResource MaterialDesignOutlinedButton}""/>");
            Color midColor = await GetThemeColor("PrimaryHueMidBrush");
            IVisualElement? internalBorder = await button.GetElement("border");

            //Act
            await button.MoveCursorToElement(Position.Center);
            await Wait.For(async () =>
            {
                SolidColorBrush internalBorderBackground = await internalBorder.GetProperty<SolidColorBrush>(nameof(Border.Background));

                //Assert
                Assert.Equal(midColor, internalBorderBackground.Color);
            });
            
            recorder.Success();
        }
    }
}
