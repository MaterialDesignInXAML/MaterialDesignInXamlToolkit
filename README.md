# Material Design In XAML Toolkit

![Alt text](https://github.com/ButchersBoy/MaterialDesignInXamlToolkit/blob/master/web/MashUp.gif "Material Design Demo")

# Summary

 * XAML Resources for [Google swatches displayed](http://www.google.co.uk/design/spec/style/color.html#color-ui-color-application)
 * WPF control themes
 * Small sample apps within solution showing how to include chosen colours and use the resources:
   * MaterialDesignColors.WpfExample - illustrates palette use and control themes in WPF
   * MahMaterialDragablzMashUp - illustrates combination of [Dragablz](https://github.com/ButchersBoy/Dragablz), [MahApps](https://github.com/MahApps/MahApps.Metro), and Material Design for saweet UI.
   * MaterialDesignColors.UniversalExample - illustrates palette use in WinRT
 * Compatible with [MahApps](http://mahapps.com) and [Dragablz](https://github.com/ButchersBoy/Dragablz).  See demo app in source and [Mash Up! blog](http://dragablz.net/2015/02/25/material-design-in-xaml-mash-up/).
 * Includes hacky console app which was used to generate the XAML colour definitions

[![Gitter](https://img.shields.io/badge/Gitter-Join%20Chat-green.svg?style=flat-square)](https://gitter.im/ButchersBoy/MaterialDesignInXamlToolkit)
[![Build](https://img.shields.io/appveyor/ci/ButchersBoy/MaterialDesignInXamlToolkit.svg?style=flat-square)](https://ci.appveyor.com/project/ButchersBoy/materialdesigninxamltoolkit)
[![NuGet-Themes](https://img.shields.io/nuget/dt/MaterialDesignThemes.svg?label=NuGet-Themes&style=flat-square)](https://www.nuget.org/packages/MaterialDesignThemes/)
[![NuGet-Themes](https://img.shields.io/nuget/dt/MaterialDesignColors.svg?label=NuGet-Colors&style=flat-square)](https://www.nuget.org/packages/MaterialDesignColors/)
[![Issues](https://img.shields.io/github/issues/ButchersBoy/MaterialDesignInXamlToolkit.svg?style=flat-square)](https://github.com/ButchersBoy/MaterialDesignInXamlToolkit/issues)

# How Can I Use The Colours?

1. Choose your source:
 * Install the [NuGet package](https://www.nuget.org/packages/MaterialDesignColors/) ```Install-Package MaterialDesignColors```
 * Manually pull from [Themes](https://github.com/ButchersBoy/MaterialDesignColorsInXamlToolkit/tree/master/Themes) directory and include in your project
2. In your App.XAML choose the primary palette and secondary palette, and configure standard brushes, as in [this example](https://github.com/ButchersBoy/MaterialDesignColorsInXamlToolkit/blob/master/MaterialDesignColors.UniversalExample/App.xaml) (this is identical for WPF/WinRT)
3. Make colourful things.
4. Want help choosing your palette? Try the [Palette Builder](https://rawgit.com/ButchersBoy/MaterialDesignInXamlToolkit/master/web/PaletteBuilder.html) (this is very much a work-in-progress)
 
# How Can I Use The Themes?

* Install [Nuget package](https://www.nuget.org/packages/MaterialDesignThemes/) ```Install-Package MaterialDesignThemes```
* Import required resource dictionaries, and use styles as desired.  For examples, see the MaterialDesignColors.WpfExample project in the source code.

# More Pics

![Alt text](https://dragablz.files.wordpress.com/2015/04/datepicker.gif "Date Picker & Calendar Demo")

# A Brief History

In creating a Material Design TabControl theme for [Dragablz](https://github.com/ButchersBoy/Dragablz), the first step was getting hold of all the colours.  The resulting resource dictionaries work well stand alone, so are completely re-usable. 

Then I took a step further and created a few simple styles which turned out looking OK.

![Alt text](https://dragablz.files.wordpress.com/2015/02/materialdesigndemo23.png "Material Design Themes")

