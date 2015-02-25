# Material Design In XAML Toolkit

![Alt text](https://dragablz.files.wordpress.com/2015/02/materialdesigndemo31.gif "Material Design Themes")

# Summary

 * XAML Resources for [Google swatches displayed](http://www.google.co.uk/design/spec/style/color.html#color-ui-color-application)
 * WPF control themes
 * Small sample apps showing how to include chosen colours and use the resources:
   * WPF
   * WinRT
 * Compatible with [MahApps](http://mahapps.com) and [Dragablz](https://github.com/ButchersBoy/Dragablz).  See demo app in source and [Mash Up! blog](http://dragablz.net/2015/02/25/material-design-in-xaml-mash-up/).
 * Includes hacky console app which was used to generate the XAML colour definitions

# How Can I Use The Colours?

1. Choose your source:
 * The [NuGet package](https://www.nuget.org/packages/MaterialDesignColors/) which contains a compiled .dll of ResourceDictionary instances (currently WPF only)
 * Manually pull from [Themes](https://github.com/ButchersBoy/MaterialDesignColorsInXamlToolkit/tree/master/Themes) directory and include in your project
2. In your App.XAML choose the primary palette and secondary palette, as in [this example](https://github.com/ButchersBoy/MaterialDesignColorsInXamlToolkit/blob/master/MaterialDesignColors.UniversalExample/App.xaml) (this is identical for WPF/WinRT)
3. Make colourful things.
 
# How Can I Use The Themes?

* Here's the [Nuget package](https://www.nuget.org/packages/MaterialDesignThemes/).
* Or fire up the MaterialDesignColors.WpfExample project in the source code.

# A Brief History

In creating a Material Design TabControl theme for [Dragablz](https://github.com/ButchersBoy/Dragablz), the first step was getting hold of all the colours.  The resulting resource dictionaries work well stand alone, so are completely re-usable. 

Then I took a step further and created a few simple styles which turned out looking OK.

![Alt text](https://dragablz.files.wordpress.com/2015/02/materialdesigndemo23.png "Material Design Themes")

