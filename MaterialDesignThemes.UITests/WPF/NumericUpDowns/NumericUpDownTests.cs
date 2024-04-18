namespace MaterialDesignThemes.UITests.WPF.NumericUpDowns;

public class NumericUpDownTests(ITestOutputHelper output) : TestBase(output)
{
    [Fact]
    public async Task NumericButtons_IncreaseAndDecreaseValue()
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Value="1" />
        """);
        var plusButton = await numericUpDown.GetElement<RepeatButton>("PART_IncreaseButton");
        var minusButton = await numericUpDown.GetElement<RepeatButton>("PART_DecreaseButton");
        var textBox = await numericUpDown.GetElement<TextBox>("PART_TextBoxField");

        Assert.Equal("1", await textBox.GetText());
        Assert.Equal(1, await numericUpDown.GetValue());

        await plusButton.LeftClick();
        await Wait.For(async () =>
        {
            Assert.Equal("2", await textBox.GetText());
            Assert.Equal(2, await numericUpDown.GetValue());
        });

        await minusButton.LeftClick();
        await Wait.For(async () =>
        {
            Assert.Equal("1", await textBox.GetText());
            Assert.Equal(1, await numericUpDown.GetValue());
        });
    }

    [Fact]
    public async Task NumericButtons_WithMaximum_DisablesPlusButton()
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Value="1" Maximum="2" />
        """);
        var plusButton = await numericUpDown.GetElement<RepeatButton>("PART_IncreaseButton");
        var minusButton = await numericUpDown.GetElement<RepeatButton>("PART_DecreaseButton");
        var textBox = await numericUpDown.GetElement<TextBox>("PART_TextBoxField");

        await plusButton.LeftClick();
        await Wait.For(async () =>
        {
            Assert.Equal("2", await textBox.GetText());
            Assert.Equal(2, await numericUpDown.GetValue());
        });

        Assert.False(await plusButton.GetIsEnabled());

        await minusButton.LeftClick();
        await Wait.For(async () =>
        {
            Assert.Equal("1", await textBox.GetText());
            Assert.Equal(1, await numericUpDown.GetValue());
        });

        Assert.True(await plusButton.GetIsEnabled());
    }

    [Fact]
    public async Task NumericButtons_WithMinimum_DisablesMinusButton()
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Value="2" Minimum="1" />
        """);
        var plusButton = await numericUpDown.GetElement<RepeatButton>("PART_IncreaseButton");
        var minusButton = await numericUpDown.GetElement<RepeatButton>("PART_DecreaseButton");
        var textBox = await numericUpDown.GetElement<TextBox>("PART_TextBoxField");

        await minusButton.LeftClick();
        await Wait.For(async () =>
        {
            Assert.Equal("1", await textBox.GetText());
            Assert.Equal(1, await numericUpDown.GetValue());
        });

        Assert.False(await minusButton.GetIsEnabled());

        await plusButton.LeftClick();
        await Wait.For(async () =>
        {
            Assert.Equal("2", await textBox.GetText());
            Assert.Equal(2, await numericUpDown.GetValue());
        });

        Assert.True(await minusButton.GetIsEnabled());
    }
}
