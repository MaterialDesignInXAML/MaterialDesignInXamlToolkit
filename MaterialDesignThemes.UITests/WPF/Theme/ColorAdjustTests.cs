﻿using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.UITests.Samples.Theme;

namespace MaterialDesignThemes.UITests.WPF.Theme
{
    public class ColorAdjustTests : TestBase
    {
        public ColorAdjustTests(ITestOutputHelper output)
            : base(output)
        { }

        public static IEnumerable<object[]> PrimaryColors()
        {
            return Enum.GetValues(typeof(PrimaryColor))
                .OfType<PrimaryColor>()
                .Select(x => new object[] { x });
        }

        [Theory]
        [MemberData(nameof(PrimaryColors))]
        public async Task PrimaryColor_AdjustToTheme(PrimaryColor primary)
        {
            await using var recorder = new TestRecorder(App);

            await App.InitializeWithMaterialDesign(BaseTheme.Light, primary, colorAdjustment: new ColorAdjustment());

            IWindow window = await App.CreateWindow<ColorAdjustWindow>();

            Color? windowBackground = await window.GetBackgroundColor();

            var themeToggle = await window.GetElement<ToggleButton>("/ToggleButton");
            var largeText = await window.GetElement<TextBlock>("/TextBlock[0]");
            var smallText = await window.GetElement<TextBlock>("/TextBlock[1]");

            await AssertContrastRatio();

            await themeToggle.LeftClick();
            await Wait.For(async () => await window.GetBackgroundColor() != windowBackground);

            await AssertContrastRatio();

            recorder.Success();

            async Task AssertContrastRatio()
            {
                const double tolerance = 0.1;
                Color? largeTextForeground = await largeText.GetForegroundColor();
                Color largeTextBackground = await largeText.GetEffectiveBackground();

                Color? smallTextForeground = await smallText.GetForegroundColor();
                Color smallTextBackground = await smallText.GetEffectiveBackground();

                var largeContrastRatio = ColorAssist.ContrastRatio(largeTextForeground.Value, largeTextBackground);
                Assert.True(largeContrastRatio >= MaterialDesignSpec.MinimumContrastLargeText - tolerance, $"Large font contrast ratio '{largeContrastRatio}' does not meet material design spec {MaterialDesignSpec.MinimumContrastLargeText}");
                var smallContrastRatio = ColorAssist.ContrastRatio(smallTextForeground.Value, smallTextBackground);
                Assert.True(smallContrastRatio >= MaterialDesignSpec.MinimumContrastSmallText - tolerance, $"Small font contrast ratio '{smallContrastRatio}' does not meet material design spec {MaterialDesignSpec.MinimumContrastSmallText}");
            }
        }
    }
}
