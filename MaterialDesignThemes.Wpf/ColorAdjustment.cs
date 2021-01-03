using System;
using System.Windows.Markup;

namespace MaterialDesignThemes.Wpf
{
    public class ColorAdjustment : MarkupExtension
    {
        public float DesiredContrastRatio { get; set; } = 4.5f;

        public Contrast Contrast { get; set; } = Contrast.Medium;

        public ColorSelection Colors { get; set; } = ColorSelection.All;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}