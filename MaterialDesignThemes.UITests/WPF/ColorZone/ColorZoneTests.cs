using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.ColorZone
{
    public class ColorZoneTests : TestBase
    {
        public ColorZoneTests(ITestOutputHelper output)
            : base(output)
        {

        }

        [Theory]
        [InlineData(ColorZoneMode.Standard, "MaterialDesignPaper", "MaterialDesignBody")]
        [InlineData(ColorZoneMode.Inverted, "MaterialDesignBody", "MaterialDesignPaper")]
        [InlineData(ColorZoneMode.PrimaryLight, "PrimaryHueLightBrush", "PrimaryHueLightForegroundBrush")]
        [InlineData(ColorZoneMode.PrimaryMid, "PrimaryHueMidBrush", "PrimaryHueMidForegroundBrush")]
        [InlineData(ColorZoneMode.PrimaryDark, "PrimaryHueDarkBrush", "PrimaryHueDarkForegroundBrush")]
        [InlineData(ColorZoneMode.Accent, "SecondaryHueMidBrush", "SecondaryHueMidForegroundBrush")]
        [InlineData(ColorZoneMode.Light, "MaterialDesignLightBackground", "MaterialDesignLightForeground")]
        [InlineData(ColorZoneMode.Dark, "MaterialDesignDarkBackground", "MaterialDesignDarkForeground")]
        public async Task Mode_SetsThemeColors(ColorZoneMode mode, string backgroundBrush, string foregroundBrush)
        {
            await using var recorder = new TestRecorder(App);

            IVisualElement colorZone = await LoadXaml(@$"
<materialDesign:ColorZone Mode=""{mode}""/>
");
            Color background = await GetThemeColor(backgroundBrush);
            Color foreground = await GetThemeColor(foregroundBrush);

            Assert.Equal(background, await colorZone.GetBackgroundColor());
            Assert.Equal(foreground, await colorZone.GetForegroundColor());

            recorder.Success();
        }
    }
}
