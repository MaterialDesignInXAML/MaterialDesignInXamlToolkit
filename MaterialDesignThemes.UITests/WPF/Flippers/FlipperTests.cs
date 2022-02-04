using System.Threading.Tasks;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.Flippers
{
    public class FlipperTests : TestBase
    {
        public FlipperTests(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public async Task TranslateTransform_Animation_Works()
        {
            await using var recorder = new TestRecorder(App);

            await App.InitialzeWithMaterialDesign();

            IWindow window = await App.CreateWindow(@"
<Window xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
        xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
        xmlns:local=""clr-namespace:WpfApp1""
        xmlns:materialDesign=""http://materialdesigninxaml.net/winfx/xaml/themes""
        xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
        Title=""MainWindow""
        Width=""300"" Height=""300""
        WindowStartupLocation=""CenterScreen""
        mc:Ignorable=""d"">

    <Window.Resources>
        <Storyboard x:Key=""LoadWindow"">
            <DoubleAnimationUsingKeyFrames Storyboard.TargetName=""txtUser"" Storyboard.TargetProperty=""(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"">
                <EasingDoubleKeyFrame KeyTime=""0"" Value=""800"" />
                <EasingDoubleKeyFrame KeyTime=""0:0:0.5"" Value=""800"" />
                <EasingDoubleKeyFrame KeyTime=""0:0:0.7"" Value=""0"" />
            </DoubleAnimationUsingKeyFrames>
            <DoubleAnimationUsingKeyFrames Storyboard.TargetName=""AcceptButton"" Storyboard.TargetProperty=""(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"">
                <EasingDoubleKeyFrame KeyTime=""0"" Value=""800"" />
                <EasingDoubleKeyFrame KeyTime=""0:0:0.5"" Value=""800"" />
                <EasingDoubleKeyFrame KeyTime=""0:0:0.9"" Value=""0"" />
            </DoubleAnimationUsingKeyFrames>
            <DoubleAnimationUsingKeyFrames Storyboard.TargetName=""CancelButton"" Storyboard.TargetProperty=""(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"">
                <EasingDoubleKeyFrame KeyTime=""0"" Value=""800"" />
                <EasingDoubleKeyFrame KeyTime=""0:0:0.5"" Value=""800"" />
                <EasingDoubleKeyFrame KeyTime=""0:0:1.1"" Value=""0"" />
            </DoubleAnimationUsingKeyFrames>
        </Storyboard>
    </Window.Resources>

    <Window.Triggers>
        <EventTrigger RoutedEvent=""Loaded"">
            <BeginStoryboard Storyboard=""{StaticResource LoadWindow}"" />
        </EventTrigger>
    </Window.Triggers>

    <Grid Margin=""10"">
        <materialDesign:Flipper SnapsToDevicePixels=""True"">
            <materialDesign:Flipper.FrontContent>
                <StackPanel>
                    <TextBox x:Name=""txtUser"">
                        <TextBox.RenderTransform>
                            <TransformGroup>
                                <ScaleTransform />
                                <SkewTransform />
                                <RotateTransform />
                                <TranslateTransform />
                            </TransformGroup>
                        </TextBox.RenderTransform>
                    </TextBox>

                    <Button x:Name=""AcceptButton""
                            Margin=""0,15,0,5""
                            materialDesign:ShadowAssist.ShadowDepth=""Depth2""
                            KeyboardNavigation.IsTabStop=""False"">
                        <Button.RenderTransform>
                            <TransformGroup>
                                <ScaleTransform />
                                <SkewTransform />
                                <RotateTransform />
                                <TranslateTransform />
                            </TransformGroup>
                        </Button.RenderTransform>
                        <StackPanel Orientation=""Horizontal"">
                            <materialDesign:PackIcon Width=""24"" Height=""24"" Kind=""AccountCheck"" />
                            <TextBlock Margin=""4,0,0,0"" VerticalAlignment=""Center"" Text=""Aceptar"" />
                        </StackPanel>
                    </Button>

                    <Button x:Name=""CancelButton""
                            Margin=""0,5""
                            materialDesign:ShadowAssist.ShadowDepth=""Depth2"" KeyboardNavigation.IsTabStop=""False"">
                        <Button.RenderTransform>
                            <TransformGroup>
                                <ScaleTransform />
                                <SkewTransform />
                                <RotateTransform />
                                <TranslateTransform />
                            </TransformGroup>
                        </Button.RenderTransform>
                        <StackPanel Orientation=""Horizontal"">
                            <materialDesign:PackIcon Width=""24"" Height=""24"" Kind=""CloseBox"" />
                            <TextBlock Margin=""4,0,0,0"" VerticalAlignment=""Center"" Text=""Cancelar"" />
                        </StackPanel>
                    </Button>
                </StackPanel>
            </materialDesign:Flipper.FrontContent>
        </materialDesign:Flipper>
    </Grid>
</Window>
");
            await Task.Delay(25_000);
            recorder.Success();
        }
    }
}
