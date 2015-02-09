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

In creating a Material Design TabControl theme for [Dragablz](https://github.com/ButchersBoy/Dragablz), the first step was getting hold of all the colours.  The resulting resource dictionaries work well stand alone, so are completely re-usable. 

And here's the proof in the pudding, [Dragablz](https://github.com/ButchersBoy/Dragablz) themed with Material Design styling and colours:

![Alt text](https://dragablz.files.wordpress.com/2015/02/materialdesigndemo1.gif "Material Design style")

