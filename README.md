<!-- omit in toc -->
# Material Design In XAML Toolkit ![Logo of Material Design in XAML](src/web/images/MD4XAML32.png)

[![NuGet-Themes](https://img.shields.io/nuget/v/MaterialDesignThemes.svg?label=nuget:%20MaterialDesignThemes)](https://www.nuget.org/packages/MaterialDesignThemes/)
[![NuGet-Colors](https://img.shields.io/nuget/v/MaterialDesignColors.svg?label=nuget:%20MaterialDesignColors)](https://www.nuget.org/packages/MaterialDesignColors/)

[![NuGet-Themes-CI](https://img.shields.io/nuget/vpre/MaterialDesignThemes.svg?label=nuget:%20MaterialDesignThemes%20(CI))](https://www.nuget.org/packages/MaterialDesignThemes/)
[![NuGet-Colors-CI](https://img.shields.io/nuget/vpre/MaterialDesignColors.svg?label=nuget:%20MaterialDesignColors%20(CI))](https://www.nuget.org/packages/MaterialDesignColors/)

[![Backers on Open Collective](https://opencollective.com/materialdesigninxaml/backers/badge.svg)](#backers) 
[![Sponsors on Open Collective](https://opencollective.com/materialdesigninxaml/sponsors/badge.svg)](#sponsors) 
[![Chat](https://img.shields.io/badge/chat-grey?logo=discord)][discord-server-url]
[![Issues](https://img.shields.io/github/issues/MaterialDesignInXAML/MaterialDesignInXamlToolkit.svg)](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/issues)


Comprehensive and easy to use Material Design theme and control library for the Windows desktop (WPF).

- Material Design styles for all major WPF Framework controls
- Additional controls to support the theme, including Multi Action Button, Cards, Dialogs, Clock
- Easy configuration of palette (at design _and_ runtime), according to [Google's guidelines](https://material.io/design/)
- Full [Material Design Icons](https://materialdesignicons.com/) icon pack
- Easy transition effects
- Compatible with [Dragablz](https://github.com/ButchersBoy/Dragablz), [MahApps](https://github.com/MahApps/MahApps.Metro)
- Demo applications included in the source project

[See screenshots](#screenshots)

<details>
  <summary>Table of contents</summary>

- [Getting started](#getting-started)
- [Building the source](#building-the-source)
- [Screenshots](#screenshots)
- [More examples](#more-examples)
- [FAQ](#faq)
- [Contributing](#contributing)
- [Mentions](#mentions)
- [Backers](#backers)
- [Sponsors](#sponsors)

</details>

---

## Getting started

> [!NOTE]
> See the [full starting guide](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Getting-Started) for more in-depth information.

This quick guide assumes you have already created a WPF project and are using Microsoft Visual Studio 2022.

* Install the toolkit through the visual NuGet package manager in Visual Studio or use the following command:
```
Install-Package MaterialDesignThemes
```
* Alter your `App.xaml`

```xml
<Application 
  x:Class="Example.App"
  xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
  xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
  xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes"
  StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <materialDesign:BundledTheme BaseTheme="Light" PrimaryColor="DeepPurple" SecondaryColor="Lime" />

                <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign2.Defaults.xaml" /> 
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```
* If you want to target Material Design 3, alter the `ResourceDictionary` line in the snippet above to use `MaterialDesign3.Defaults.xaml`.

* Alter your `MainWindow.xaml`

```xml
<Window [...]
  Style="{StaticResource MaterialDesignWindow}"
  [...] >
```


## Building the source

To build the project, following is required:
* Microsoft Visual Studio 2022
* .NET desktop development workload

This repository also contains 3 different demo applications:
* `MaterialDesignDemo` - Reference WPF app with Material Design 2 styling, this contains all controls and is a recommended tool when developing apps with this library
* `MaterialDesign3Demo` - Reference WPF app with Material Design 3 styling, under development
* `MahAppsDragablzDemo` - Demo app for combining with Dragablz and MahApps

## Screenshots

> [!WARNING]
> The screenshots below are taken from the Material Design 2 demo app.
> It is possible that these are out of date. Run the demo app from latest source to see the latest version.

<details>
  <summary>Show images</summary>

  ![Screenshot of WPF Material Design 2 demo application home page](src/web/images/screen-home.png)
  ![Buttons](src/web/images/screen-buttons.png)
  ![Toggles](src/web/images/screen-toggles.png)
  ![Fields](src/web/images/screen-fields.png)
  ![ComboBoxes](src/web/images/screen-comboboxes.png)
  ![Palette](src/web/images/screen-palette.png)
  ![Color Tools](src/web/images/screen-colortools.png)
  ![Pickers](src/web/images/screen-pickers.png)
  ![Icons](src/web/images/screen-iconpack.png)
  ![Cards](src/web/images/screen-cards.png)
  ![Menus and Toolbars](src/web/images/screen-menutoolbar.png)
  ![Progress Bars](src/web/images/screen-progress.png)
  ![Dialogs](src/web/images/screen-dialogs.png)
  ![Lists](src/web/images/screen-lists.png)
  ![Tree View](src/web/images/screen-treeview.png)
  ![Sliders](src/web/images/screen-sliders.png)
  ![Chips](src/web/images/screen-chips.png)
  ![Typography](src/web/images/screen-typography.png)
  ![Group Box](src/web/images/screen-groupbox.png)
  ![Snackbars](src/web/images/screen-snackbars.png)
  ![Elevation](src/web/images/screen-elevation.png)
</details>


## More examples

* [Keboo/MaterialDesign.Examples](https://github.com/Keboo/MaterialDesignInXaml.Examples)
* [doobry](http://materialdesigninxaml.net/doobry)
* [F1ix](http://materialdesigninxaml.net/f1ix)
* [Motion List](https://github.com/MaterialDesignInXAML/MotionList)

## FAQ

* [How to increase rendering performance?](docs/rendering-performance.md)

## Contributing

Before contributing code read the [Contribution Guidelines](.github/CONTRIBUTING.md)
* GitHub issues are for bugs and feature requests.
* For questions, help and chat in general, please use the [GitHub discussion tab](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/discussions) or the [Discord server][discord-server-url].
* Stack Overflow tag: [`material-design-in-xaml`](http://stackoverflow.com/questions/tagged/material-design-in-xaml)

Want to say thanks? 🙏🏻
* Hit the :star: star :star: button
* If you'd like to make a very much appreciated financial donation please visit <a href='https://opencollective.com/materialdesigninxaml'>open collective</a>

This project exists thanks to all the people who contribute.

<a href="https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=MaterialDesignInXAML/MaterialDesignInXamlToolkit" />
</a>

## Mentions

- **[James Willock](https://github.com/ButchersBoy)
[![Twitter](https://img.shields.io/badge/twitter-%40james__willock-55acee.svg?style=flat-square)](https://twitter.com/James_Willock)** - Founder of the project
- **[Kevin Bost](https://github.com/Keboo)
[![Twitter](https://img.shields.io/badge/twitter-%40kitokeboo-55acee.svg?style=flat-square)](https://twitter.com/kitokeboo)** - Maintainer of the repository
- [Snalty](https://github.com/snalty)
[![Twitter](https://img.shields.io/badge/twitter-%40snalty-55acee.svg?style=flat-square)](https://twitter.com/snalty) - Designer of the logo
- Icon pack sourced from [Material Design Icons](https://materialdesignicons.com/)
- [ControlzEx](https://github.com/ControlzEx/ControlzEx) - Library used in MaterialDesignInXAML
- [Ignace Maes](https://github.com/IgnaceMaes) - Whose [Material Skin](https://github.com/IgnaceMaes/MaterialSkin) project inspired the original material design theme for [Dragablz](https://github.com/ButchersBoy/Dragablz), which in turn led James Willock start this project
- [Material Design Extensions](https://github.com/spiegelp/MaterialDesignExtensions) - A community repository based on this library that provides additional controls and features.
- **[Contributors](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/graphs/contributors)** - A big thank you to all the contributors of the project!

## Backers

Thank you to all our backers! 🙏 [Become a backer.](https://opencollective.com/materialdesigninxaml#backer)

<a href="https://opencollective.com/materialdesigninxaml#backers" target="_blank"><img src="https://opencollective.com/materialdesigninxaml/backers.svg?width=890"></a>

## Sponsors

Support this project by becoming a sponsor. Your logo will show up here with a link to your website. [Become a sponsor.](https://opencollective.com/materialdesigninxaml#sponsor)

<a href="https://opencollective.com/materialdesigninxaml/sponsor/0/website" target="_blank"><img src="https://opencollective.com/materialdesigninxaml/sponsor/0/avatar.svg"></a>
<a href="https://opencollective.com/materialdesigninxaml/sponsor/1/website" target="_blank"><img src="https://opencollective.com/materialdesigninxaml/sponsor/1/avatar.svg"></a>
<a href="https://opencollective.com/materialdesigninxaml/sponsor/2/website" target="_blank"><img src="https://opencollective.com/materialdesigninxaml/sponsor/2/avatar.svg"></a>
<a href="https://opencollective.com/materialdesigninxaml/sponsor/3/website" target="_blank"><img src="https://opencollective.com/materialdesigninxaml/sponsor/3/avatar.svg"></a>
<a href="https://opencollective.com/materialdesigninxaml/sponsor/4/website" target="_blank"><img src="https://opencollective.com/materialdesigninxaml/sponsor/4/avatar.svg"></a>
<a href="https://opencollective.com/materialdesigninxaml/sponsor/5/website" target="_blank"><img src="https://opencollective.com/materialdesigninxaml/sponsor/5/avatar.svg"></a>
<a href="https://opencollective.com/materialdesigninxaml/sponsor/6/website" target="_blank"><img src="https://opencollective.com/materialdesigninxaml/sponsor/6/avatar.svg"></a>
<a href="https://opencollective.com/materialdesigninxaml/sponsor/7/website" target="_blank"><img src="https://opencollective.com/materialdesigninxaml/sponsor/7/avatar.svg"></a>
<a href="https://opencollective.com/materialdesigninxaml/sponsor/8/website" target="_blank"><img src="https://opencollective.com/materialdesigninxaml/sponsor/8/avatar.svg"></a>
<a href="https://opencollective.com/materialdesigninxaml/sponsor/9/website" target="_blank"><img src="https://opencollective.com/materialdesigninxaml/sponsor/9/avatar.svg"></a>

[discord-server-url]: https://discord.keboo.dev