using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class TabItemAssist
    {
        #region AttachedProperty : TopIconProperty
        public static readonly DependencyProperty TopIconProperty
            = DependencyProperty.RegisterAttached("TopIcon", typeof(PackIconKind?), typeof(TabItemAssist),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static object GetTopIcon(DependencyObject element)
            => element.GetValue(TopIconProperty);
        public static void SetTopIcon(DependencyObject element, object value)
            => element.SetValue(TopIconProperty, value);
        #endregion

    }
}
