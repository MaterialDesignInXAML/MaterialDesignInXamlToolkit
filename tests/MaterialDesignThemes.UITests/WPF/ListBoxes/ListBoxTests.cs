using System.ComponentModel;


namespace MaterialDesignThemes.UITests.WPF.ListBoxes;

public class ListBoxTests : TestBase
{
    [Test]
    public async Task OnMouseOver_BackgroundIsSet()
    {
        var listBox = await LoadXaml<ListBox>(@"
<ListBox MinWidth=""200"">
    <ListBoxItem Content=""Item1"" />
    <ListBoxItem Content=""Item2"" />
    <ListBoxItem Content=""Item3"" />
    <ListBoxItem Content=""Item4"" />
</ListBox>
");

        var listBoxItem = await listBox.GetElement<ListBoxItem>("/ListBoxItem[2]");
        await Assert.That(await listBoxItem.GetContent()).IsEqualTo("Item3");
        var mouseOverBorder = await listBoxItem.GetElement<Border>("MouseOverBorder");

        await listBox.MoveCursorTo(Position.TopLeft);
        await Wait.For(async () => await Assert.That(await mouseOverBorder.GetOpacity()).IsEqualTo(0.0));

        await mouseOverBorder.MoveCursorTo();
        await Wait.For(async () =>
        {
            double opacity = await mouseOverBorder.GetOpacity();
            Output.WriteLine($"Got opacity {opacity}");
            await Assert.That(opacity).IsEqualTo(1);
        });

        //Color effectiveBackground = await mouseOverBorder.GetEffectiveBackground();
        //Color? foreground = await listBoxItem.GetForegroundColor();
        //foreground = foreground?.FlattenOnto(effectiveBackground);

        //float? contrastRatio = foreground?.ContrastRatio(effectiveBackground);
        //await Assert.True(contrastRatio >= MaterialDesignSpec.MinimumContrastSmallText);
    }

    [Test]
    [Description("Issue 1994")]
    [Arguments("MaterialDesignFilterChipListBox")]
    [Arguments("MaterialDesignFilterChipPrimaryListBox")]
    [Arguments("MaterialDesignFilterChipSecondaryListBox")]
    [Arguments("MaterialDesignFilterChipOutlineListBox")]
    [Arguments("MaterialDesignFilterChipPrimaryOutlineListBox")]
    [Arguments("MaterialDesignFilterChipSecondaryOutlineListBox")]
    [Arguments("MaterialDesignChoiceChipListBox")]
    [Arguments("MaterialDesignChoiceChipPrimaryListBox")]
    [Arguments("MaterialDesignChoiceChipSecondaryListBox")]
    [Arguments("MaterialDesignChoiceChipOutlineListBox")]
    [Arguments("MaterialDesignChoiceChipPrimaryOutlineListBox")]
    [Arguments("MaterialDesignChoiceChipSecondaryOutlineListBox")]
    public async Task OnClickChoiceChipListBox_ChangesSelectedItem(string listBoxStyle)
    {
        var listBox = await LoadXaml<ListBox>($@"
<ListBox x:Name=""ChipsListBox"" Style=""{{StaticResource {listBoxStyle}}}"">
    <ListBoxItem>Mercury</ListBoxItem>
    <ListBoxItem>Venus</ListBoxItem>
    <ListBoxItem>Earth</ListBoxItem>
    <ListBoxItem>Pluto</ListBoxItem>
</ListBox>
");
        var earth = await listBox.GetElement<ListBoxItem>("/ListBoxItem[2]");
        await earth.LeftClick();

        await Wait.For(async () => await Assert.That(await listBox.GetSelectedIndex()).IsEqualTo(2));
    }

    [Test]
    public async Task ScrollBarAssist_ButtonsVisibility_HidesButtonsOnMinimalistStyle()
    {
        string xaml = @"<ListBox Height=""300"" Width=""300""
materialDesign:ScrollBarAssist.ButtonsVisibility=""Collapsed"" 
ScrollViewer.HorizontalScrollBarVisibility=""Visible"" 
ScrollViewer.VerticalScrollBarVisibility=""Visible"">
<ListBox.Resources>
    <Style BasedOn=""{StaticResource MaterialDesignScrollBarMinimal}"" TargetType=""{x:Type ScrollBar}"" />
</ListBox.Resources>
";
        for (int i = 0; i < 50; i++)
        {
            xaml += $"    <ListBoxItem>This is a pretty long meaningless text just to make horizontal scrollbar visible</ListBoxItem>{Environment.NewLine}";
        }
        xaml += "</ListBox>";

        var listBox = await LoadXaml<ListBox>(xaml);
        var verticalScrollBar = await listBox.GetElement<ScrollBar>("PART_VerticalScrollBar");
        var horizontalScrollBar = await listBox.GetElement<ScrollBar>("PART_HorizontalScrollBar");

        await Assert.That(await verticalScrollBar.GetActualWidth()).IsCloseTo(17.0, 1.0);
        var verticalThumb = await verticalScrollBar.GetElement<Border>("/Thumb~border");
        await Assert.That(await verticalThumb.GetActualWidth()).IsCloseTo(10.0, 1.0);
        var upButton = await verticalScrollBar.GetElement<RepeatButton>("PART_LineUpButton");
        await Assert.That(await upButton.GetIsVisible()).IsFalse();
        var downButton = await verticalScrollBar.GetElement<RepeatButton>("PART_LineDownButton");
        await Assert.That(await downButton.GetIsVisible()).IsFalse();

        await Assert.That(await horizontalScrollBar.GetActualHeight()).IsCloseTo(17.0, 1.0);
        var horizontalThumb = await horizontalScrollBar.GetElement<Border>("/Thumb~border");
        await Assert.That(await horizontalThumb.GetActualHeight()).IsCloseTo(10.0, 1.0);
        var leftButton = await horizontalScrollBar.GetElement<RepeatButton>("PART_LineLeftButton");
        await Assert.That(await leftButton.GetIsVisible()).IsFalse();
        var rightButton = await horizontalScrollBar.GetElement<RepeatButton>("PART_LineRightButton");
        await Assert.That(await rightButton.GetIsVisible()).IsFalse();
    }

    [Test]
    public async Task OnListBoxAssist_WithShowSelectDisabled_SelectionIsDisabled()
    {
        var listBox = await LoadXaml<ListBox>($@"
<ListBox materialDesign:ListBoxItemAssist.ShowSelection=""False"">
    <ListBoxItem>Mercury</ListBoxItem>
    <ListBoxItem>Venus</ListBoxItem>
    <ListBoxItem>Earth</ListBoxItem>
    <ListBoxItem>Pluto</ListBoxItem>
</ListBox>
");
        var earth = await listBox.GetElement<ListBoxItem>("/ListBoxItem[2]");
        await earth.LeftClick();
        var selectedBorder = await earth.GetElement<Border>("SelectedBorder");
        await Wait.For(async () => await Assert.That(await selectedBorder.GetIsVisible()).IsFalse() == false);
    }

    [Test]
    [Description("Issue 3188")]
    public async Task OnToggle_ShouldGrabFocus()
    {
        var stackPanel = await LoadXaml<StackPanel>("""
            <StackPanel Orientation="Vertical">
                <ListBox MinWidth="200"
                         materialDesign:ListBoxAssist.IsToggle="True">
                    <ListBoxItem Content="Item1" />
                    <ListBoxItem Content="Item2" />
                    <ListBoxItem Content="Item3" />
                    <ListBoxItem Content="Item4" />
                </ListBox>
                <TextBox />
            </StackPanel>
            """);

        var listBox = await stackPanel.GetElement<ListBox>("/ListBox");
        var textBox = await stackPanel.GetElement<TextBox>("/TextBox");
        var listBoxItem = await listBox.GetElement<ListBoxItem>("/ListBoxItem[2]");

        await textBox.LeftClick();
        await Wait.For(async () => await Assert.That(await textBox.GetIsKeyboardFocusWithin()).IsTrue());

        // Act
        await listBoxItem.LeftClick();

        // Assert
        await Wait.For(async () => await Assert.That(await listBox.GetIsKeyboardFocusWithin()).IsTrue());
    }
}
