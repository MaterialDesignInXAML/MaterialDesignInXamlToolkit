using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class ThemeManager : ContentControl
    {
        //public ThemeManager()
        //{
        //    var dict = CreateEmptyThemeDictionary();
        //    foreach (SolidColorBrush d in dict.Values) d.Freeze();
        //    Resources.MergedDictionaries.Add(dict);
        //    Resources.WithTheme(BaseTheme);
        //    foreach (var p in BaseTheme.GetType().GetProperties().Where(o => o.PropertyType == typeof(Color)))
        //    {
        //        if (Application.Current.Resources.Contains(p.Name)) continue;
        //        Application.Current.Resources[p.Name] = new SolidColorBrush((Color)p.GetValue(BaseTheme));
        //    }

        //    //CommandManager.RegisterClassCommandBinding(typeof(ThemeManager), new CommandBinding(NavigationCommands.FirstPage, FirstPageExecuted));
        //}

        //private void FirstPageExecuted(object sender, ExecutedRoutedEventArgs e)
        //{
        //}

        //private static ResourceDictionary CreateEmptyThemeDictionary()
        //{
        //    return new ResourceDictionary {
        //        ["ValidationErrorColor"] = new SolidColorBrush(),
        //        ["MaterialDesignBackground"] = new SolidColorBrush(),
        //        ["MaterialDesignPaper"] = new SolidColorBrush(),
        //        ["MaterialDesignCardBackground"] = new SolidColorBrush(),
        //        ["MaterialDesignToolBarBackground"] = new SolidColorBrush(),
        //        ["MaterialDesignBody"] = new SolidColorBrush(),
        //        ["MaterialDesignBodyLight"] = new SolidColorBrush(),
        //        ["MaterialDesignColumnHeader"] = new SolidColorBrush(),
        //        ["MaterialDesignCheckBoxOff"] = new SolidColorBrush(),
        //        ["MaterialDesignCheckBoxDisabled"] = new SolidColorBrush(),
        //        ["MaterialDesignTextBoxBorder"] = new SolidColorBrush(),
        //        ["MaterialDesignDivider"] = new SolidColorBrush(),
        //        ["MaterialDesignSelection"] = new SolidColorBrush(),
        //        ["MaterialDesignFlatButtonClick"] = new SolidColorBrush(),
        //        ["MaterialDesignFlatButtonRipple"] = new SolidColorBrush(),
        //        ["MaterialDesignToolTipBackground"] = new SolidColorBrush(),
        //        ["MaterialDesignChipBackground"] = new SolidColorBrush(),
        //        ["MaterialDesignSnackbarBackground"] = new SolidColorBrush(),
        //        ["MaterialDesignSnackbarMouseOver"] = new SolidColorBrush(),
        //        ["MaterialDesignSnackbarRipple"] = new SolidColorBrush(),
        //        ["MaterialDesignTextFieldBoxBackground"] = new SolidColorBrush(),
        //        ["MaterialDesignTextFieldBoxHoverBackground"] = new SolidColorBrush(),
        //        ["MaterialDesignTextFieldBoxDisabledBackground"] = new SolidColorBrush(),
        //        ["MaterialDesignTextAreaBorder"] = new SolidColorBrush(),
        //        ["MaterialDesignTextAreaInactiveBorder"] = new SolidColorBrush()
        //    };
        //}

        //public static readonly DependencyProperty BaseThemeProperty = DependencyProperty.Register(nameof(BaseTheme), typeof(IBaseTheme), typeof(ThemeManager), new PropertyMetadata(MaterialDesignTheme.Light, new PropertyChangedCallback(BaseThemeChanged)));

        //private static void BaseThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    if (e.NewValue == e.OldValue) return;

        //    ((ThemeManager)d).Resources.WithTheme((IBaseTheme)e.NewValue);
        //}

        //public IBaseTheme BaseTheme
        //{
        //    get { return (IBaseTheme)GetValue(BaseThemeProperty); }
        //    set { SetValue(BaseThemeProperty, value); }
        //}

    }
}
