namespace MaterialDesignThemes.UITests.WPF.Calendars;

public class CalendarTests : TestBase
{
    public CalendarTests()
    {
        AttachedDebuggerToRemoteProcess = false;
    }

    [Test]
    public async Task CalendarDayButton_WithFocus_DoesNotCaptureTheMouseByDefault()
    {
        //Arrange
        IVisualElement<DialogHost> dialogHost = await LoadXaml<DialogHost>("""
            <materialDesign:DialogHost>
              <Button
                x:Name="OpenDialogButton"
                Margin="8,0,0,0"
                Command="{x:Static materialDesign:DialogHost.OpenDialogCommand}"
                Content="Open Dialog">
                <Button.CommandParameter>
                  <Grid>
                    <Grid.RowDefinitions>
                      <RowDefinition
                        Height="Auto" />
                      <RowDefinition
                        Height="Auto" />
                    </Grid.RowDefinitions>
                    <Viewbox
                      Width="300"
                      Stretch="Uniform">
                      <Calendar
                        x:Name="Calendar"
                        Margin="-1,-4,-1,0" />
                    </Viewbox>
                    <StackPanel
                      Grid.Row="1"
                      Margin="8"
                      HorizontalAlignment="Right"
                      Orientation="Horizontal">
                      <Button
                        Command="{x:Static materialDesign:DialogHost.CloseDialogCommand}"
                        CommandParameter="0"
                        Content="CANCEL"
                        Style="{StaticResource MaterialDesignFlatButton}" />
                      <Button
                        Command="{x:Static materialDesign:DialogHost.CloseDialogCommand}"
                        CommandParameter="1"
                        Content="OK"
                        Style="{StaticResource MaterialDesignFlatButton}" />
                    </StackPanel>
                  </Grid>
                </Button.CommandParameter>
              </Button>
            </materialDesign:DialogHost>
            """);

        var button = await dialogHost.GetElement<Button>("OpenDialogButton");
        await button.LeftClick();

        IVisualElement<Popup> popup = await dialogHost.GetElement<Popup>();
        IVisualElement<Calendar> calendar = await popup.GetElement<Calendar>("Calendar");
        IVisualElement<CalendarDayButton> firstItem = await Wait.For(async () => await calendar.GetElement<CalendarDayButton>("[Content=1]"));
        await firstItem.LeftClick();

        IVisualElement<Button> okButton = await popup.GetElement<Button>("[Content=OK]");
        await okButton.LeftClick();

        await Wait.For(async () => await dialogHost.GetIsOpen(), new Retry(5, TimeSpan.FromSeconds(5)));
    }
}
