using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class SwatchHelper
    {
        public static IEnumerable<ISwatch> Swatches = new ISwatch[]
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
    }
}
