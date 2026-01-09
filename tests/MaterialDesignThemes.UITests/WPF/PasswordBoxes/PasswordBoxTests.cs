using System.ComponentModel;
using MaterialDesignThemes.UITests.Samples.PasswordBox;
using MaterialDesignThemes.UITests.WPF.TextBoxes;


namespace MaterialDesignThemes.UITests.WPF.PasswordBoxes;

public class PasswordBoxTests : TestBase
{
    [Test]
    public async Task OnClearButtonShown_LayoutDoesNotChange()
    {
        //Arrange
        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <PasswordBox materialDesign:TextFieldAssist.HasClearButton=""True""/>
</StackPanel>");
        var passwordBox = await stackPanel.GetElement<PasswordBox>("/PasswordBox");

        var initialRect = await passwordBox.GetCoordinates();

        //Act
        await passwordBox.SetPassword("x");

        //Assert
        var rect = await passwordBox.GetCoordinates();
        await Assert.That(rect).IsEqualTo(initialRect);
    }

    [Test]
    [Description("Pull Request 2192")]
    public async Task OnPasswordBoxHelperTextFontSize_ChangesHelperTextFontSize()
    {
        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <PasswordBox materialDesign:HintAssist.HelperTextFontSize=""20""/>
</StackPanel>");
        var passwordBox = await stackPanel.GetElement<PasswordBox>("/PasswordBox");
        var helpTextBlock = await passwordBox.GetElement<TextBlock>("/Grid/Canvas/TextBlock");

        double fontSize = await helpTextBlock.GetFontSize();

        await Assert.That(fontSize).IsEqualTo(20);
    }

    [Test]
    [Description("Issue 2495")]
    public async Task OnPasswordBox_WithClearButton_ClearsPassword()
    {
        var grid = await LoadXaml<Grid>(@"
<Grid Margin=""30"">
    <PasswordBox materialDesign:TextFieldAssist.HasClearButton=""True"" />
</Grid>");
        var passwordBox = await grid.GetElement<PasswordBox>("/PasswordBox");
        var clearButton = await passwordBox.GetElement<Button>("PART_ClearButton");

        await passwordBox.SendKeyboardInput($"Test");

        string? password = await passwordBox.GetPassword();

        await Assert.That(password).IsNotNull();

        await clearButton.LeftClick();

        await Wait.For(async () =>
        {
            password = await passwordBox.GetPassword();
            await Assert.That(password).IsNull();
        });
    }

    [Test]
    [Description("PR 2828 and Issue 2930")]
    public async Task RevealPasswordBox_WithBoundPasswordProperty_RespectsThreeWayBinding()
    {
        await App.InitializeWithMaterialDesign();
        IWindow window = await App.CreateWindow<BoundPasswordBoxWindow>();
        var userControl = await window.GetElement<BoundPasswordBox>();
        await userControl.SetProperty(nameof(BoundPasswordBox.UseRevealStyle), true);
        var passwordBox = await userControl.GetElement<PasswordBox>("PasswordBox");
        var clearTextPasswordTextBox = await passwordBox.GetElement<TextBox>("RevealPasswordTextBox");
        var revealPasswordButton = await passwordBox.GetElement<ToggleButton>("RevealPasswordButton");

        // Act 1 (Update in PasswordBox updates VM and RevealPasswordTextBox)
        await passwordBox.SendKeyboardInput($"1");
        string? boundText1 = await userControl.GetProperty<string>(nameof(BoundPasswordBox.ViewModelPassword));
        string? password1 = await passwordBox.GetProperty<string>(nameof(PasswordBox.Password));
        string? clearTextPassword1 = await clearTextPasswordTextBox.GetProperty<string>(TextBox.TextProperty);

        // Act 2 (Update in RevealPasswordTextBox updates PasswordBox and VM)
        await Task.Delay(50, TestContext.Current!.CancellationToken);
        await revealPasswordButton.LeftClick();
        await Task.Delay(50, TestContext.Current.CancellationToken);   // Wait for the "clear text TextBox" to become visible
        await clearTextPasswordTextBox.SendKeyboardInput($"2");
        string? boundText2 = await userControl.GetProperty<string>(nameof(BoundPasswordBox.ViewModelPassword));
        string? password2 = await passwordBox.GetProperty<string>(nameof(PasswordBox.Password));
        string? clearTextPassword2 = await clearTextPasswordTextBox.GetProperty<string>(TextBox.TextProperty);

        // Act 3 (Update in VM updates PasswordBox and RevealPasswordTextBox)
        await userControl.SetProperty(nameof(BoundPasswordBox.ViewModelPassword), "3");
        string? boundText3 = await userControl.GetProperty<string>(nameof(BoundPasswordBox.ViewModelPassword));
        string? password3 = await passwordBox.GetProperty<string>(nameof(PasswordBox.Password));
        string? clearTextPassword3 = await clearTextPasswordTextBox.GetProperty<string>(TextBox.TextProperty);

        // Assert
        await Assert.That(boundText1).IsEqualTo("1");
        await Assert.That(password1).IsEqualTo("1");
        await Assert.That(clearTextPassword1).IsEqualTo("1");

        await Assert.That(boundText2).IsEqualTo("12");
        await Assert.That(password2).IsEqualTo("12");
        await Assert.That(clearTextPassword2).IsEqualTo("12");

        await Assert.That(boundText3).IsEqualTo("3");
        await Assert.That(password3).IsEqualTo("3");
        await Assert.That(clearTextPassword3).IsEqualTo("3");
    }

    [Test]
    [Description("Issue 2930")]
    public async Task PasswordBox_WithBoundPasswordProperty_RespectsBinding()
    {
        await App.InitializeWithMaterialDesign();
        IWindow window = await App.CreateWindow<BoundPasswordBoxWindow>();
        var userControl = await window.GetElement<BoundPasswordBox>();
        await userControl.SetProperty(nameof(BoundPasswordBox.UseRevealStyle), false);
        var passwordBox = await userControl.GetElement<PasswordBox>("PasswordBox");

        // Act 1 (Update in PasswordBox updates VM)
        await passwordBox.SendKeyboardInput($"1");
        string? boundText1 = await userControl.GetProperty<string>(nameof(BoundPasswordBox.ViewModelPassword));
        string? password1 = await passwordBox.GetProperty<string>(nameof(PasswordBox.Password));

        // Act 2 (Update in VM updates PasswordBox)
        await userControl.SetProperty(nameof(BoundPasswordBox.ViewModelPassword), "2");
        string? boundText2 = await userControl.GetProperty<string>(nameof(BoundPasswordBox.ViewModelPassword));
        string? password2 = await passwordBox.GetProperty<string>(nameof(PasswordBox.Password));

        // Assert
        await Assert.That(boundText1).IsEqualTo("1");
        await Assert.That(password1).IsEqualTo("1");

        await Assert.That(boundText2).IsEqualTo("2");
        await Assert.That(password2).IsEqualTo("2");
    }

    [Test]
    [Description("Issue 2998")]
    public async Task PasswordBox_WithRevealStyle_RespectsMaxLength()
    {
        var grid = await LoadXaml<Grid>(@"
<Grid Margin=""30"">
    <PasswordBox MaxLength=""5"" Style=""{StaticResource MaterialDesignFloatingHintRevealPasswordBox}"" />
</Grid>");
        var passwordBox = await grid.GetElement<PasswordBox>("/PasswordBox");
        var revealPasswordTextBox = await passwordBox.GetElement<TextBox>("RevealPasswordTextBox");

        int maxLength1 = await passwordBox.GetMaxLength();
        int maxLength2 = await revealPasswordTextBox.GetMaxLength();

        // Assert
        await Assert.That(maxLength2).IsEqualTo(maxLength1);
    }

    [Test]
    [Arguments("MaterialDesignFloatingHintPasswordBox", null)]
    [Arguments("MaterialDesignFloatingHintPasswordBox", 5)]
    [Arguments("MaterialDesignFloatingHintPasswordBox", 20)]
    [Arguments("MaterialDesignFloatingHintRevealPasswordBox", null)]
    [Arguments("MaterialDesignFloatingHintRevealPasswordBox", 5)]
    [Arguments("MaterialDesignFloatingHintRevealPasswordBox", 20)]
    [Arguments("MaterialDesignFilledPasswordBox", null)]
    [Arguments("MaterialDesignFilledPasswordBox", 5)]
    [Arguments("MaterialDesignFilledPasswordBox", 20)]
    [Arguments("MaterialDesignFilledRevealPasswordBox", null)]
    [Arguments("MaterialDesignFilledRevealPasswordBox", 5)]
    [Arguments("MaterialDesignFilledRevealPasswordBox", 20)]
    [Arguments("MaterialDesignOutlinedPasswordBox", null)]
    [Arguments("MaterialDesignOutlinedPasswordBox", 5)]
    [Arguments("MaterialDesignOutlinedPasswordBox", 20)]
    [Arguments("MaterialDesignOutlinedRevealPasswordBox", null)]
    [Arguments("MaterialDesignOutlinedRevealPasswordBox", 5)]
    [Arguments("MaterialDesignOutlinedRevealPasswordBox", 20)]
    public async Task PasswordBox_WithHintAndHelperText_RespectsPadding(string styleName, int? padding)
    {
        // FIXME: Tolerance needed because TextFieldAssist.TextBoxViewMargin is in play and slightly modifies the hint text placement in certain cases.
        const double tolerance = 1.5;

        string styleAttribute = $"Style=\"{{StaticResource {styleName}}}\"";
        string paddingAttribute = padding.HasValue ? $"Padding=\"{padding.Value}\"" : string.Empty;

        var passwordBox = await LoadXaml<PasswordBox>($@"
<PasswordBox {styleAttribute} {paddingAttribute}
  materialDesign:HintAssist.Hint=""Hint text""
  materialDesign:HintAssist.HelperText=""Helper text""
  Width=""200"" VerticalAlignment=""Center"" HorizontalAlignment=""Center"" />
");

        var contentHost = await passwordBox.GetElement<ScrollViewer>("PART_ContentHost");
        var hint = await passwordBox.GetElement<SmartHint>("Hint");
        var helperText = await passwordBox.GetElement<TextBlock>("HelperTextTextBlock");

        Rect? contentHostCoordinates = await contentHost.GetCoordinates();
        Rect? hintCoordinates = await hint.GetCoordinates();
        Rect? helperTextCoordinates = await helperText.GetCoordinates();

        await Assert.That(Math.Abs(contentHostCoordinates.Value.Left - hintCoordinates.Value.Left))
            .IsCloseTo(0, tolerance);
        await Assert.That(Math.Abs(contentHostCoordinates.Value.Left - helperTextCoordinates.Value.Left))
            .IsCloseTo(0, tolerance);
    }

    [Test]
    [Arguments("MaterialDesignFloatingHintPasswordBox", null)]
    [Arguments("MaterialDesignFloatingHintPasswordBox", 5)]
    [Arguments("MaterialDesignFloatingHintPasswordBox", 20)]
    [Arguments("MaterialDesignFloatingHintRevealPasswordBox", null)]
    [Arguments("MaterialDesignFloatingHintRevealPasswordBox", 5)]
    [Arguments("MaterialDesignFloatingHintRevealPasswordBox", 20)]
    [Arguments("MaterialDesignFilledPasswordBox", null)]
    [Arguments("MaterialDesignFilledPasswordBox", 5)]
    [Arguments("MaterialDesignFilledPasswordBox", 20)]
    [Arguments("MaterialDesignFilledRevealPasswordBox", null)]
    [Arguments("MaterialDesignFilledRevealPasswordBox", 5)]
    [Arguments("MaterialDesignFilledRevealPasswordBox", 20)]
    [Arguments("MaterialDesignOutlinedPasswordBox", null)]
    [Arguments("MaterialDesignOutlinedPasswordBox", 5)]
    [Arguments("MaterialDesignOutlinedPasswordBox", 20)]
    [Arguments("MaterialDesignOutlinedRevealPasswordBox", null)]
    [Arguments("MaterialDesignOutlinedRevealPasswordBox", 5)]
    [Arguments("MaterialDesignOutlinedRevealPasswordBox", 20)]
    public async Task PasswordBox_WithHintAndValidationError_RespectsPadding(string styleName, int? padding)
    {
        // FIXME: Tolerance needed because TextFieldAssist.TextBoxViewMargin is in play and slightly modifies the hint text placement in certain cases.
        const double tolerance = 1.5;

        string styleAttribute = $"Style=\"{{StaticResource {styleName}}}\"";
        string paddingAttribute = padding.HasValue ? $"Padding=\"{padding.Value}\"" : string.Empty;

        var passwordBox = await LoadXaml<PasswordBox>($@"
<PasswordBox {styleAttribute} {paddingAttribute}
  materialDesign:HintAssist.Hint=""Hint text""
  materialDesign:HintAssist.HelperText=""Helper text""
  Width=""200"" VerticalAlignment=""Center"" HorizontalAlignment=""Center"">
  <materialDesign:PasswordBoxAssist.Password>
    <Binding RelativeSource=""{{RelativeSource Self}}"" Path=""Tag"" UpdateSourceTrigger=""PropertyChanged"">
      <Binding.ValidationRules>
        <local:NotEmptyValidationRule ValidatesOnTargetUpdated=""True""/>
      </Binding.ValidationRules>
    </Binding>
  </materialDesign:PasswordBoxAssist.Password>
</PasswordBox>
", ("local", typeof(NotEmptyValidationRule)));

        var contentHost = await passwordBox.GetElement<ScrollViewer>("PART_ContentHost");
        var hint = await passwordBox.GetElement<SmartHint>("Hint");
        var errorViewer = await passwordBox.GetElement<Border>("DefaultErrorViewer");

        Rect? contentHostCoordinates = await contentHost.GetCoordinates();
        Rect? hintCoordinates = await hint.GetCoordinates();
        Rect? errorViewerCoordinates = await errorViewer.GetCoordinates();

        await Assert.That(Math.Abs(contentHostCoordinates.Value.Left - hintCoordinates.Value.Left))
            .IsCloseTo(0, tolerance);
        await Assert.That(Math.Abs(contentHostCoordinates.Value.Left - errorViewerCoordinates.Value.Left))
            .IsCloseTo(0, tolerance);
    }

    [Test]
    [Skip("Ignoring until I can figure out why this doesn't work on the GitHub Actions runner")]
    [Description("Issue 3095")]
    public async Task PasswordBox_WithRevealedPassword_RespectsKeyboardTabNavigation()
    {
        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel Orientation=""Vertical"">
  <TextBox x:Name=""TextBox1"" Width=""200"" />
  <PasswordBox x:Name=""PasswordBox"" Width=""200""
               materialDesign:PasswordBoxAssist.IsPasswordRevealed=""True""
               Style=""{StaticResource MaterialDesignFloatingHintRevealPasswordBox}"" />
  <TextBox x:Name=""TextBox2"" Width=""200"" />
</StackPanel>");

        var textBox1 = await stackPanel.GetElement<TextBox>("TextBox1");
        var passwordBox = await stackPanel.GetElement<PasswordBox>("PasswordBox");
        var revealPasswordTextBox = await passwordBox.GetElement<TextBox>("RevealPasswordTextBox");
        var textBox2 = await stackPanel.GetElement<TextBox>("TextBox2");

        // Assert Tab forward
        await textBox1.MoveKeyboardFocus();
        await Assert.That(await textBox1.GetIsKeyboardFocused()).IsTrue();
        await textBox1.SendKeyboardInput($"{Key.Tab}");
        await Assert.That(await revealPasswordTextBox.GetIsKeyboardFocused()).IsTrue();
        await revealPasswordTextBox.SendKeyboardInput($"{Key.Tab}");
        await Assert.That(await textBox2.GetIsKeyboardFocused()).IsTrue();

        // Assert Tab backwards
        await textBox2.SendKeyboardInput($"{ModifierKeys.Shift}{Key.Tab}");
        await Assert.That(await revealPasswordTextBox.GetIsKeyboardFocused()).IsTrue();
        await revealPasswordTextBox.SendKeyboardInput($"{Key.Tab}{ModifierKeys.None}");
        await Assert.That(await textBox1.GetIsKeyboardFocused()).IsTrue();
    }

    [Test]
    [Description("Issue 3799")]
    public async Task PasswordBox_WithRevealButtonIsTabStopSetToFalse_RespectsKeyboardTabNavigation()
    {
        var stackPanel = await LoadXaml<StackPanel>("""
            <StackPanel Orientation="Vertical">
              <PasswordBox x:Name="PasswordBox" Width="200"
                           materialDesign:PasswordBoxAssist.IsRevealButtonTabStop="False"
                           Style="{StaticResource MaterialDesignFilledRevealPasswordBox}" />
              <TextBox x:Name="TextBox" Width="200" />
            </StackPanel>
            """);

        var passwordBox = await stackPanel.GetElement<PasswordBox>("PasswordBox");
        var textBox = await stackPanel.GetElement<TextBox>("TextBox");

        // Assert Tab forward
        await passwordBox.MoveKeyboardFocus();
        await Assert.That(await passwordBox.GetIsKeyboardFocused()).IsTrue();
        await passwordBox.SendKeyboardInput($"{Key.Tab}");
        await Assert.That(await textBox.GetIsKeyboardFocused()).IsTrue();
        
        // Assert Tab backwards
        await textBox.SendKeyboardInput($"{ModifierKeys.Shift}{Key.Tab}{ModifierKeys.None}");
        await Assert.That(await passwordBox.GetIsKeyboardFocused()).IsTrue();
    }
}
