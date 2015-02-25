# Material Design In XAML Toolkit

* Google Material Design Color Swatches Specified in XAML.
* Some basic WPF themes

![Alt text](https://dragablz.files.wordpress.com/2015/02/materialdesigndemo3.gif "Material Design Themes")

# What's Included?

 * XAML Resources for Google swatches displayed [here](http://www.google.co.uk/design/spec/style/color.html#color-ui-color-application)
* A few WPF themes used to generate the above example 
* Small sample apps showing how to include chosen colours and use the resources:
   * WPF
   * WinRT
 * Hacky console app which was used to generate the XAML

# How Can I Use The Colours?

1. Choose your source:
 * The [NuGet package](https://www.nuget.org/packages/MaterialDesignColors/) which contains a compiled .dll of ResourceDictionary instances (currently WPF only)
 * Manually pull from [Themes](https://github.com/ButchersBoy/MaterialDesignColorsInXamlToolkit/tree/master/Themes) directory and include in your project
2. In your App.XAML choose the primary palette and secondary palette, as in [this example](https://github.com/ButchersBoy/MaterialDesignColorsInXamlToolkit/blob/master/MaterialDesignColors.UniversalExample/App.xaml) (this is identical for WPF/WinRT)
3. Make colourful things.
 
# How Can I Use The Themes?

* Here's the [Nuget package](https://www.nuget.org/packages/MaterialDesignThemes/).
* Or fire up the MaterialDesignColors.WpfExample project in the source code.

# Why?

In creating a Material Design TabControl theme for [Dragablz](https://github.com/ButchersBoy/Dragablz), the first step was getting hold of all the colours.  The resulting resource dictionaries work well stand alone, so are completely re-usable. 

Then I took a step further and created a few simple styles which turned out looking OK.

![Alt text](https://dragablz.files.wordpress.com/2015/02/materialdesigndemo22.png "Material Design Themes")

