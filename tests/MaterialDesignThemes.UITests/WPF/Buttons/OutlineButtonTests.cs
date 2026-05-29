using System.Windows.Media;

namespace MaterialDesignThemes.UITests.WPF.Buttons;

public class OutlineButtonTests : TestBase
{
    [Test]
    public async Task OutlinedButton_UsesThemeColorForBorder()
    {
        //Arrange
        IVisualElement<Button> button = await LoadXaml<Button>(
            @"<Button Content=""Button"" Style=""{StaticResource MaterialDesignOutlinedButton}""/>");
        Color midColor = await GetThemeColor("MaterialDesign.Brush.Primary");
        IVisualElement<Border> internalBorder = await button.GetElement<Border>("border");

        //Act
        Color? borderColor = await button.GetBorderBrushColor();
        Color? internalBorderColor = await internalBorder.GetBorderBrushColor();

        //Assert
        await Assert.That(borderColor).IsEqualTo(midColor);
        await Assert.That(internalBorderColor).IsEqualTo(midColor);
    }

    [Test]
    public async Task OutlinedButton_BorderCanBeOverridden()
    {
        //Arrange
        var button = await LoadXaml<Button>(
            @"<Button Content=""Button""
                          Style=""{StaticResource MaterialDesignOutlinedButton}""
                          BorderThickness=""5""
                          BorderBrush=""Red""
                    />");
        IVisualElement<Border> internalBorder = await button.GetElement<Border>("border");

        //Act
        Thickness borderThickness = await internalBorder.GetBorderThickness();
        Color? borderBrush = await internalBorder.GetBorderBrushColor();

        //Assert
        await Assert.That(borderThickness).IsEqualTo(new Thickness(5));
        await Assert.That(borderBrush).IsEqualTo(Colors.Red);
    }

    [Test]
    public async Task OutlinedButton_OnMouseOver_UsesThemeBrush()
    {
        //Arrange
        IVisualElement<Button> button = await LoadXaml<Button>(
            @"<Button Content=""Button"" Style=""{StaticResource MaterialDesignOutlinedButton}""/>");
        Color midColor = await GetThemeColor("MaterialDesign.Brush.Primary");
        IVisualElement<Border> internalBorder = await button.GetElement<Border>("border");

        //Act
        await button.MoveCursorTo(Position.Center);
        await Wait.For(async () =>
        {
            SolidColorBrush? internalBorderBackground = (await internalBorder.GetBackground()) as SolidColorBrush;

            //Assert
            await Assert.That(internalBorderBackground?.Color).IsEqualTo(midColor);
        });
    }
}
