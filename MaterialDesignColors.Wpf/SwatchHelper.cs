using System;
using System.Collections.Generic;
using System.Windows.Media;
using MaterialDesignColors.Recommended;

namespace MaterialDesignColors
{
    public class SwatchHelper
    {
        public static IEnumerable<ISwatch> Swatches { get; } = new ISwatch[]
        {
            new RedSwatch(),
            new PinkSwatch(),
            new PurpleSwatch(),
            new DeepPurpleSwatch(),
            new IndigoSwatch(),
            new BlueSwatch(),
            new LightBlueSwatch(),
            new CyanSwatch(),
            new TealSwatch(),
            new GreenSwatch(),
            new LightGreenSwatch(),
            new LimeSwatch(),
            new YellowSwatch(),
            new AmberSwatch(),
            new OrangeSwatch(),
            new DeepOrangeSwatch(),
            new BrownSwatch(),
            new GreySwatch(),
            new BlueGreySwatch(),
        };

        public static Color Lookup(MaterialDesignColor color)
        {
            switch (color)
            {
                case MaterialDesignColor.Red: return RedSwatch.Primary.Red500;
                case MaterialDesignColor.Pink: return PinkSwatch.Primary.Pink500;
                case MaterialDesignColor.Purple: return PurpleSwatch.Primary.Purple500;
                case MaterialDesignColor.DeepPurple: return DeepPurpleSwatch.Primary.DeepPurple500;
                case MaterialDesignColor.Indigo: return IndigoSwatch.Primary.Indigo500;
                case MaterialDesignColor.Blue: return BlueSwatch.Primary.Blue500;
                case MaterialDesignColor.LightBlue: return LightBlueSwatch.Primary.LightBlue500;
                case MaterialDesignColor.Cyan: return CyanSwatch.Primary.Cyan500;
                case MaterialDesignColor.Teal: return TealSwatch.Primary.Teal500;
                case MaterialDesignColor.Green: return GreenSwatch.Primary.Green500;
                case MaterialDesignColor.LightGreen: return LightGreenSwatch.Primary.LightGreen500;
                case MaterialDesignColor.Lime: return LimeSwatch.Primary.Lime500;
                case MaterialDesignColor.Yellow: return YellowSwatch.Primary.Yellow500;
                case MaterialDesignColor.Amber: return AmberSwatch.Primary.Amber500;
                case MaterialDesignColor.Orange: return OrangeSwatch.Primary.Orange500;
                case MaterialDesignColor.DeepOrange: return DeepOrangeSwatch.Primary.DeepOrange500;
                case MaterialDesignColor.Brown: return BrownSwatch.Primary.Brown500;
                case MaterialDesignColor.Grey: return GreySwatch.Primary.Grey500;
                case MaterialDesignColor.BlueGrey: return BlueGreySwatch.Primary.BlueGrey500;

                case MaterialDesignColor.RedAccent: return RedSwatch.Accent.Red700;
                case MaterialDesignColor.PinkAccent: return PinkSwatch.Accent.Pink700;
                case MaterialDesignColor.PurpleAccent: return PurpleSwatch.Accent.Purple700;
                case MaterialDesignColor.DeepPurpleAccent: return DeepPurpleSwatch.Accent.DeepPurple700;
                case MaterialDesignColor.IndigoAccent: return IndigoSwatch.Accent.Indigo700;
                case MaterialDesignColor.BlueAccent: return BlueSwatch.Accent.Blue700;
                case MaterialDesignColor.LightBlueAccent: return LightBlueSwatch.Accent.LightBlue700;
                case MaterialDesignColor.CyanAccent: return CyanSwatch.Accent.Cyan700;
                case MaterialDesignColor.TealAccent: return TealSwatch.Accent.Teal700;
                case MaterialDesignColor.GreenAccent: return GreenSwatch.Accent.Green700;
                case MaterialDesignColor.LightGreenAccent: return LightGreenSwatch.Accent.LightGreen700;
                case MaterialDesignColor.LimeAccent: return LimeSwatch.Accent.Lime700;
                case MaterialDesignColor.YellowAccent: return YellowSwatch.Accent.Yellow700;
                case MaterialDesignColor.AmberAccent: return AmberSwatch.Accent.Amber700;
                case MaterialDesignColor.OrangeAccent: return OrangeSwatch.Accent.Orange700;
                case MaterialDesignColor.DeepOrangeAccent: return DeepOrangeSwatch.Accent.DeepOrange700;
                default: throw new ArgumentException($"No color defined for {nameof(MaterialDesignColor)}.{color}", nameof(color));
            }
        }
    }
}
