# Contribution Guidelines

PRs to this project are welcome, but before contributing please bear in mind my thoughts below. Whe possible please include unit tests. If you want to get involved but are unsure, please ask (say hello in the [Gitter chat room](http://gitter.im/ButchersBoy/MaterialDesignInXamlToolkit)).  Also note that I wrote these guidelines several months after the library was first made available, with the benefit of hindsight.  Apologies if there are inconsistencies with what you read below and what's already in the library.

## The purposes of this library:

This is a theme library, **not** a control library.  There is a subtle difference.  Yes, there are some new custom controls, but their role is to specifically support either styling of existing WPF controls, or to Material Design components, as per the "Components" section in [Google's guidelines](https://www.google.com/design/spec/material-design/introduction.html).

The role of this library is to give users Material Design themes for standard WPF controls, and custom controls for Google specified components where no parallel exists in WPF, or where existing WPF controls cannot be sufficiently styled to meet the Google specification.  Anything else is outside the scope of this library. WPF is very powerful and flexible when it comes to creating custom controls, but that doesn't mean that I want to fill up this library with random controls.  I want to provide the base tools and controls for Material Design, giving people a springboard to create their own controls where required.

In no way is this library a home for any sort of application or business logic.

## Coding standards:

I code using standard Visual Studio settings, using Resharper, and adopt (most of the) ReSharper's suggestions.  I'd like the code to look pretty similar.

The API is king. If adding anything to the public interface of this library (controls, helpers etc) be mindful of the naming and usage.  Don't be offended if I accept your PR but rename things slightly.

## UWP:

This whole project is a personal hobby, and this is even more true of the UWP section.  I'm not accepting any PRs to UWP as I will be coding on this in my spare time, and using it as a way to learn and explore UWP for my own enlightenment.  If and when the UWP solution becomes more mature I will relax this stance.

## TabControl:

There is no TabControl style, I won't create one, and I wont accept one.  I have Dragablz and don't want the added burden of supporting two styles; even if it is fully complete.  I don't want the duplication and the overhead.  

## Submitting a PR:

Probably the smaller the better (within sensible bounds for the nature of your change); at least keep a single feature to a single branch/PR.
