# Material Design Colors In XAML Toolkit

Google Material Design Colors Swatches Specified in XAML.

# What's Included?

 * XAML Resources for Google swatches displayed [here](http://www.google.co.uk/design/spec/style/color.html#color-ui-color-application)
 * Small sample apps showing how to include chosen colours and use the resources:
   * WPF
   * WinRT
 * Hacky console app which was used to generate the XAML

# How Can I Use Them?

1. Choose your source:
 * The [NuGet package](https://www.nuget.org/packages/MaterialDesignColors/) which contains a compiled .dll of ResourceDictionary instances (currently WPF only)
 * Manually pull from [Themes](https://github.com/ButchersBoy/MaterialDesignColorsInXamlToolkit/tree/master/Themes) directory and include in your project
2. In your App.XAML choose the primary palette and secondary palette, as in [this example](https://github.com/ButchersBoy/MaterialDesignColorsInXamlToolkit/blob/master/MaterialDesignColors.UniversalExample/App.xaml) (this is identical for WPF/WinRT)
3. Make colourful things.
 
# Why?

I intend to create a Material Design TabControl theme for [Dragablz](https://github.com/ButchersBoy/Dragablz). The first step being getting hold of all the colours, but I guess the resources might come in handy to others. 
