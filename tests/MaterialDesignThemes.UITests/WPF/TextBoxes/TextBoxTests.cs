using System.ComponentModel;
using System.Globalization;
using System.Windows.Media;
using MaterialDesignThemes.UITests.Samples.Validation;

namespace MaterialDesignThemes.UITests.WPF.TextBoxes;

public class TextBoxTests : TestBase
{
    public TextBoxTests(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    [Description("Issue 1883")]
    public async Task OnClearButtonShown_ControlHeightDoesNotChange()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        var grid = await LoadXaml<Grid>(@"
<Grid Margin=""30"">
    <TextBox VerticalAlignment=""Top""
             Text=""Some Text""
             materialDesign:TextFieldAssist.HasClearButton=""True"">
    </TextBox>
</Grid>");
        var textBox = await grid.GetElement<TextBox>("/TextBox");
        var clearButton = await grid.GetElement<Button>("PART_ClearButton");

        await textBox.MoveKeyboardFocus();
        //Delay needed to account for transition storyboard
        await Task.Delay(MaterialDesignTextBox.FocusedAnimationTime);

        double initialHeight = await textBox.GetActualHeight();

        //Act
        await clearButton.LeftClick();

        //Assert
        await Task.Delay(MaterialDesignTextBox.FocusedAnimationTime);

        double height = await textBox.GetActualHeight();
        Assert.Equal(initialHeight, height);

        recorder.Success();
    }

    [Fact]
    [Description("Issue 1883")]
    public async Task OnClearButtonWithHintShown_ControlHeightDoesNotChange()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        var grid = await LoadXaml<Grid>(@"
<Grid Margin=""30"">
    <TextBox Style=""{StaticResource MaterialDesignFloatingHintTextBox}""
        VerticalAlignment=""Top""
        materialDesign:TextFieldAssist.HasClearButton=""True""
        materialDesign:TextFieldAssist.PrefixText =""$""
        materialDesign:TextFieldAssist.SuffixText = ""mm"">
        <materialDesign:HintAssist.Hint>
            <StackPanel Orientation=""Horizontal"" Margin=""-2 0 0 0"">
                <materialDesign:PackIcon Kind=""AccessPoint"" />
                <TextBlock>WiFi</TextBlock>
            </StackPanel>
         </materialDesign:HintAssist.Hint >
    </TextBox>
</Grid>");
        var textBox = await grid.GetElement<TextBox>("/TextBox");
        var clearButton = await grid.GetElement<Button>("PART_ClearButton");

        double initialHeight = await textBox.GetActualHeight();

        //Act
        await textBox.MoveKeyboardFocus();
        //Delay needed to account for transition storyboard
        await Task.Delay(MaterialDesignTextBox.FocusedAnimationTime);

        //Assert
        double height = await textBox.GetActualHeight();
        Assert.Equal(initialHeight, height);

        recorder.Success();
    }

    [Fact]
    [Description("Issue 1979")]
    public async Task OnTextCleared_MultilineTextBox()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        var grid = await LoadXaml<Grid>(@"
<Grid>
    <TextBox Style=""{StaticResource MaterialDesignFilledTextBox}""
             materialDesign:HintAssist.Hint=""Floating hint in a box""
             VerticalAlignment=""Top""/>
</Grid>");
        var textBox = await grid.GetElement<TextBox>("/TextBox");

        Rect initialRect = await textBox.GetCoordinates();
        double initialHeight = await textBox.GetActualHeight();

        await textBox.SetText($"Line 1{Environment.NewLine}Line 2");

        double twoLineHeight = await textBox.GetActualHeight();

        //Act
        await textBox.SetText("");

        //Assert
        await Wait.For(async () => Assert.Equal(initialHeight, await textBox.GetActualHeight()));
        Rect rect = await textBox.GetCoordinates();
        Assert.Equal(initialRect, rect);
        recorder.Success();
    }

    [Fact]
    [Description("Issue 2495")]
    public async Task OnTextBox_WithClearButton_ClearsText()
    {
        await using var recorder = new TestRecorder(App);

        var grid = await LoadXaml<Grid>(@"
<Grid Margin=""30"">
    <TextBox VerticalAlignment=""Top""
             Text=""Some Text""
             materialDesign:TextFieldAssist.HasClearButton=""True"">
    </TextBox>
</Grid>");
        var textBox = await grid.GetElement<TextBox>("/TextBox");
        var clearButton = await textBox.GetElement<Button>("PART_ClearButton");

        string? text = await textBox.GetText();

        Assert.NotNull(text);

        await clearButton.LeftClick();

        await Wait.For(async () =>
        {
            text = await textBox.GetText();
            Assert.Null(text);
        });

        recorder.Success();
    }

    [Fact]
    [Description("Issue 2002")]
    public async Task OnTextBoxDisabled_FloatingHintBackgroundIsOpaque()
    {
        await using var recorder = new TestRecorder(App);

        var grid = await LoadXaml<Grid>(@"
<Grid Background=""Red"">
    <TextBox
        Style=""{StaticResource MaterialDesignOutlinedTextBox}""
        VerticalAlignment=""Top""
        Height=""100""
        Text=""Some content to force hint to float""
        IsEnabled=""false""
        Margin=""30""
        materialDesign:HintAssist.Hint=""This is a text area""/>
</Grid>");
        var textBox = await grid.GetElement<TextBox>("/TextBox");
        //contentGrid is the element just inside of the border
        var contentGrid = await textBox.GetElement<Grid>("ContentGrid");
        var hintBackground = await textBox.GetElement<Border>("HintBackgroundBorder");

        Color background = await hintBackground.GetEffectiveBackground(contentGrid);

        Assert.Equal(255, background.A);
        recorder.Success();
    }

    [Fact]
    [Description("Pull Request 2192")]
    public async Task OnTextBoxHelperTextFontSize_ChangesHelperTextFontSize()
    {
        await using var recorder = new TestRecorder(App);

        var grid = await LoadXaml<Grid>(@"
<Grid Margin=""30"">
    <TextBox VerticalAlignment=""Top""
             Text=""Some Text""
             materialDesign:HintAssist.HelperTextFontSize=""20"">
    </TextBox>
</Grid>");
        var textBox = await grid.GetElement<TextBox>("/TextBox");
        var helpTextBlock = await textBox.GetElement<TextBlock>("/Grid/Canvas/TextBlock");

        double fontSize = await helpTextBlock.GetFontSize();

        Assert.Equal(20, fontSize);
        recorder.Success();
    }

    [Fact]
    public async Task CharacterCount_WithMaxLengthSet_IsDisplayed()
    {
        await using var recorder = new TestRecorder(App);

        var grid = await LoadXaml<Grid>(@"
<Grid Margin=""30"">
    <TextBox
        MaxLength=""10""
    />
</Grid>");
        var textBox = await grid.GetElement<TextBox>("/TextBox");
        var characterCounter = await textBox.GetElement<TextBlock>("CharacterCounterTextBlock");

        Assert.Equal("0 / 10", await characterCounter.GetText());

        await textBox.SetText("12345");

        Assert.Equal("5 / 10", await characterCounter.GetText());

        recorder.Success();
    }

    [Fact]
    public async Task CharacterCount_WithoutMaxLengthSet_IsCollapsed()
    {
        await using var recorder = new TestRecorder(App);

        var grid = await LoadXaml<Grid>(@"
<Grid Margin=""30"">
    <TextBox />
</Grid>");
        var textBox = await grid.GetElement<TextBox>("/TextBox");
        var characterCounter = await textBox.GetElement<TextBlock>("CharacterCounterTextBlock");

        Assert.False(await characterCounter.GetIsVisible());

        recorder.Success();
    }

    [Fact]
    public async Task CharacterCount_WithMaxLengthSetAndCharacterCounterVisibilityCollapsed_IsNotDisplayed()
    {
        await using var recorder = new TestRecorder(App);

        var grid = await LoadXaml<Grid>(@"
<Grid Margin=""30"">
    <TextBox
        MaxLength=""10""
        materialDesign:TextFieldAssist.CharacterCounterVisibility=""Collapsed""
    />
</Grid>");
        var textBox = await grid.GetElement<TextBox>("/TextBox");
        var characterCounter = await textBox.GetElement<TextBlock>("CharacterCounterTextBlock");

        Assert.False(await characterCounter.GetIsVisible());

        recorder.Success();
    }

    [Fact]
    [Description("Issue 2300")]
    public async Task HelperText_CanSetFontColorWithAttachedStyle()
    {
        await using var recorder = new TestRecorder(App);

        var grid = await LoadXaml<Grid>(@"
<Grid Margin=""30"">
    <TextBox
        materialDesign:HintAssist.HelperText=""Test"">
        <materialDesign:HintAssist.HelperTextStyle>
            <Style TargetType=""TextBlock"" BasedOn=""{StaticResource MaterialDesignHelperTextBlock}"">
                <Setter Property=""Foreground"" Value=""Red"" />
            </Style>
        </materialDesign:HintAssist.HelperTextStyle>
    </TextBox>
</Grid>");
        var textBox = await grid.GetElement<TextBox>("/TextBox");
        var helperText = await textBox.GetElement<TextBlock>("HelperTextTextBlock");

        Assert.Equal(Colors.Red, await helperText.GetForegroundColor());

        recorder.Success();
    }

    [Fact]
    [Description("Issue 2362")]
    public async Task FloatingOffset_ValuesGetAppropriatelyApplied()
    {
        await using var recorder = new TestRecorder(App);

        var textBox = await LoadXaml<TextBox>(@"
<TextBox Style=""{StaticResource MaterialDesignFloatingHintTextBox}""
         materialDesign:HintAssist.Hint=""Hint with offset""
         materialDesign:HintAssist.FloatingOffset=""1,-42""
         Margin=""100"" VerticalAlignment=""Center""
         Text=""Something"" />
");
        var hint = await textBox.GetElement<SmartHint>("Hint");
        Point offset = await hint.GetFloatingOffset();

        Assert.Equal(1, offset.X);
        Assert.Equal(-42, offset.Y);

        recorder.Success();
    }

    [Fact]
    [Description("Issue 2390")]
    public async Task ContextMenu_FollowsTextBoxFontFamily()
    {
        await using var recorder = new TestRecorder(App);

        var textBox = await LoadXaml<TextBox>(@"<TextBox FontFamily=""Times New Roman""/>");

        await textBox.RightClick();

        var contextMenu = await textBox.GetElement<ContextMenu>(".ContextMenu");

        FontFamily? textBoxFont = await textBox.GetFontFamily();
        Assert.Contains("Times New Roman", textBoxFont?.FamilyNames.Values ?? []);
        await Wait.For(async () =>
        {
            Assert.Equal(textBoxFont, await contextMenu.GetFontFamily());
        });

        recorder.Success();
    }

    [Fact]
    [Description("Issue 2390")]
    public async Task ContextMenu_UsesInheritedFontFamily()
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel TextElement.FontFamily=""Times New Roman"">
    <TextBox />
</StackPanel>
");
        var textBox = await stackPanel.GetElement<TextBox>("/TextBox");
        await textBox.RightClick();

        var contextMenu = await textBox.GetElement<ContextMenu>(".ContextMenu");

        var textBoxFont = await textBox.GetFontFamily();
        Assert.Equal("Times New Roman", textBoxFont?.FamilyNames.Values.First());
        Assert.Equal(textBoxFont, await contextMenu.GetFontFamily());

        recorder.Success();
    }

    [Fact]
    [Description("Issue 2430")]
    public async Task VerticalContentAlignment_ProperlyAlignsText()
    {
        await using var recorder = new TestRecorder(App);

        var textBox = await LoadXaml<TextBox>($@"
    <TextBox Height=""100"" Text=""Test""/>
");

        var scrollViewer = await textBox.GetElement<ScrollViewer>("PART_ContentHost");
        //The default for this changed with issue 2556.
        //It should be stretch so that the horizontal scroll bar is at the bottom and not
        //pushed to the bottom of the text.
        Assert.Equal(VerticalAlignment.Stretch, await scrollViewer.GetVerticalAlignment());

        foreach (var alignment in Enum.GetValues<VerticalAlignment>())
        {
            await textBox.SetVerticalContentAlignment(alignment);
            Assert.Equal(alignment, await scrollViewer.GetVerticalContentAlignment());
        }

        recorder.Success();
    }

    [Fact]
    [Description("Issue 2596")]
    public async Task OutlinedTextBox_ValidationErrorMargin_MatchesHelperTextMargin()
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <TextBox Style=""{StaticResource MaterialDesignOutlinedTextBox}""
        materialDesign:HintAssist.Hint=""Hint text""
        materialDesign:HintAssist.HelperText=""Helper text"">
        <TextBox.Text>
            <Binding RelativeSource=""{RelativeSource Self}"" Path=""Tag"" UpdateSourceTrigger=""PropertyChanged"">
                <Binding.ValidationRules>
                    <local:NotEmptyValidationRule ValidatesOnTargetUpdated=""True""/>
                </Binding.ValidationRules>
            </Binding>
        </TextBox.Text>
    </TextBox>
</StackPanel>
", ("local", typeof(NotEmptyValidationRule)));

        var textBox = await stackPanel.GetElement<TextBox>("/TextBox");

        var errorViewer = await textBox.GetElement<Border>("DefaultErrorViewer");
        var helperTextTextBlock = await textBox.GetElement<TextBlock>("HelperTextTextBlock");

        Thickness? errorMargin = await errorViewer.GetMargin();
        Thickness? textBoxPadding = await textBox.GetPadding();

        Assert.True(errorMargin.HasValue);
        Assert.True(textBoxPadding.HasValue);
        Assert.True(Math.Abs(errorMargin.Value.Left - textBoxPadding.Value.Left) < double.Epsilon,
            $"Error text does not respect the padding of the TextBox: Error text Margin.Left ({errorMargin.Value.Left}) == TextBox Padding.Left ({textBoxPadding.Value.Left})");

        recorder.Success();
    }

    [Fact]
    [Description("Issue 2596")]
    public async Task FilledTextBox_ValidationErrorMargin_MatchesHelperTextMargin()
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <TextBox Style=""{StaticResource MaterialDesignFilledTextBox}""
        materialDesign:HintAssist.Hint=""Hint text""
        materialDesign:HintAssist.HelperText=""Helper text"">
        <TextBox.Text>
            <Binding RelativeSource=""{RelativeSource Self}"" Path=""Tag"" UpdateSourceTrigger=""PropertyChanged"">
                <Binding.ValidationRules>
                    <local:NotEmptyValidationRule ValidatesOnTargetUpdated=""True""/>
                </Binding.ValidationRules>
            </Binding>
        </TextBox.Text>
    </TextBox>
</StackPanel>
", ("local", typeof(NotEmptyValidationRule)));

        var textBox = await stackPanel.GetElement<TextBox>("/TextBox");

        var errorViewer = await textBox.GetElement<Border>("DefaultErrorViewer");
        var helperTextTextBlock = await textBox.GetElement<TextBlock>("HelperTextTextBlock");

        Thickness? errorMargin = await errorViewer.GetProperty<Thickness>(FrameworkElement.MarginProperty);
        Thickness? textBoxPadding = await textBox.GetProperty<Thickness>(Control.PaddingProperty);

        Assert.True(errorMargin.HasValue);
        Assert.True(textBoxPadding.HasValue);

        Assert.True(Math.Abs(errorMargin.Value.Left - textBoxPadding.Value.Left) < double.Epsilon,
            $"Error text does not respect the padding of the TextBox: Error text Margin.Left ({errorMargin.Value.Left}) == TextBox Padding.Left ({textBoxPadding.Value.Left})");

        recorder.Success();
    }

    [Theory]
    [InlineData("MaterialDesignFloatingHintTextBox", null)]
    [InlineData("MaterialDesignFloatingHintTextBox", 5)]
    [InlineData("MaterialDesignFloatingHintTextBox", 20)]
    [InlineData("MaterialDesignFilledTextBox", null)]
    [InlineData("MaterialDesignFilledTextBox", 5)]
    [InlineData("MaterialDesignFilledTextBox", 20)]
    [InlineData("MaterialDesignOutlinedTextBox", null)]
    [InlineData("MaterialDesignOutlinedTextBox", 5)]
    [InlineData("MaterialDesignOutlinedTextBox", 20)]
    public async Task TextBox_WithHintAndHelperText_RespectsPadding(string styleName, int? padding)
    {
        await using var recorder = new TestRecorder(App);

        // FIXME: Tolerance needed because TextFieldAssist.TextBoxViewMargin is in play and slightly modifies the hint text placement in certain cases.
        const double tolerance = 1.5;

        string styleAttribute = $"Style=\"{{StaticResource {styleName}}}\"";
        string paddingAttribute = padding.HasValue ? $"Padding=\"{padding.Value}\"" : string.Empty;

        var textBox = await LoadXaml<TextBox>($@"
<TextBox {styleAttribute} {paddingAttribute}
  materialDesign:HintAssist.Hint=""Hint text""
  materialDesign:HintAssist.HelperText=""Helper text""
  Width=""200"" VerticalAlignment=""Center"" HorizontalAlignment=""Center"" />
");

        var contentHost = await textBox.GetElement<ScrollViewer>("PART_ContentHost");
        var hint = await textBox.GetElement<SmartHint>("Hint");
        var helperText = await textBox.GetElement<TextBlock>("HelperTextTextBlock");

        Rect? contentHostCoordinates = await contentHost.GetCoordinates();
        Rect? hintCoordinates = await hint.GetCoordinates();
        Rect? helperTextCoordinates = await helperText.GetCoordinates();

        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - hintCoordinates.Value.Left), 0, tolerance);
        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - helperTextCoordinates.Value.Left), 0, tolerance);

        recorder.Success();
    }

    [Theory]
    [InlineData("MaterialDesignFloatingHintTextBox", null)]
    [InlineData("MaterialDesignFloatingHintTextBox", 5)]
    [InlineData("MaterialDesignFloatingHintTextBox", 20)]
    [InlineData("MaterialDesignFilledTextBox", null)]
    [InlineData("MaterialDesignFilledTextBox", 5)]
    [InlineData("MaterialDesignFilledTextBox", 20)]
    [InlineData("MaterialDesignOutlinedTextBox", null)]
    [InlineData("MaterialDesignOutlinedTextBox", 5)]
    [InlineData("MaterialDesignOutlinedTextBox", 20)]
    public async Task TextBox_WithHintAndValidationError_RespectsPadding(string styleName, int? padding)
    {
        await using var recorder = new TestRecorder(App);

        // FIXME: Tolerance needed because TextFieldAssist.TextBoxViewMargin is in play and slightly modifies the hint text placement in certain cases.
        const double tolerance = 1.5;

        string styleAttribute = $"Style=\"{{StaticResource {styleName}}}\"";
        string paddingAttribute = padding.HasValue ? $"Padding=\"{padding.Value}\"" : string.Empty;

        var textBox = await LoadXaml<TextBox>($@"
<TextBox {styleAttribute} {paddingAttribute}
  materialDesign:HintAssist.Hint=""Hint text""
  materialDesign:HintAssist.HelperText=""Helper text""
  Width=""200"" VerticalAlignment=""Center"" HorizontalAlignment=""Center"">
  <TextBox.Text>
    <Binding RelativeSource=""{{RelativeSource Self}}"" Path=""Tag"" UpdateSourceTrigger=""PropertyChanged"">
      <Binding.ValidationRules>
        <local:NotEmptyValidationRule ValidatesOnTargetUpdated=""True""/>
      </Binding.ValidationRules>
    </Binding>
  </TextBox.Text>
</TextBox>
", ("local", typeof(NotEmptyValidationRule)));

        var contentHost = await textBox.GetElement<ScrollViewer>("PART_ContentHost");
        var hint = await textBox.GetElement<SmartHint>("Hint");
        var errorViewer = await textBox.GetElement<Border>("DefaultErrorViewer");

        Rect? contentHostCoordinates = await contentHost.GetCoordinates();
        Rect? hintCoordinates = await hint.GetCoordinates();
        Rect? errorViewerCoordinates = await errorViewer.GetCoordinates();

        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - hintCoordinates.Value.Left), 0, tolerance);
        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - errorViewerCoordinates.Value.Left), 0, tolerance);

        recorder.Success();
    }

    [Theory]
    [InlineData(VerticalAlignment.Stretch, VerticalAlignment.Stretch)]
    [InlineData(VerticalAlignment.Top, VerticalAlignment.Top)]
    [InlineData(VerticalAlignment.Bottom, VerticalAlignment.Bottom)]
    [InlineData(VerticalAlignment.Center, VerticalAlignment.Center)]
    [Description("Issue 3161")]
    public async Task TextBox_MultiLineAndFixedHeight_RespectsVerticalContentAlignment(VerticalAlignment alignment, VerticalAlignment expectedFloatingHintAlignment)
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>($$"""
            <StackPanel>
              <TextBox Style="{StaticResource MaterialDesignFilledTextBox}"
                materialDesign:HintAssist.Hint="Hint text"
                VerticalContentAlignment="{{alignment}}"
                AcceptsReturn="True"
                Height="200" />
            </StackPanel>
            """);

        IVisualElement<TextBox> textBox = await stackPanel.GetElement<TextBox>("/TextBox");
        IVisualElement<Grid> contentGrid = await textBox.GetElement<Grid>("ContentGrid");

        Assert.Equal(expectedFloatingHintAlignment, await contentGrid.GetVerticalAlignment());

        recorder.Success();
    }

    [Fact]
    [Description("Issue 3176")]
    public async Task ValidationErrorTemplate_WithChangingErrors_UpdatesValidation()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement userControl = await LoadUserControl<ValidationUpdates>();
        var textBox = await userControl.GetElement<TextBox>();
        var button = await userControl.GetElement<Button>();
        await button.LeftClick();

        await Wait.For(async() =>
        {
            var errorViewer = await textBox.GetElement("DefaultErrorViewer");
            var textBlock = await errorViewer.GetElement<TextBlock>();

            Assert.Equal("Some error + more", await textBlock.GetText());
        });

        recorder.Success();
    }
}

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        return string.IsNullOrWhiteSpace((value ?? "").ToString())
            ? new ValidationResult(false, "Field is required.")
            : ValidationResult.ValidResult;
    }
}
