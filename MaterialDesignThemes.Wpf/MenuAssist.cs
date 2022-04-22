using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class MenuAssist
    {
        private static readonly int DefaulTopLevelMenuItemHeight = 48;

        #region AttachedProperty : TopLevelMenuItemHeight
        public static readonly DependencyProperty TopLevelMenuItemHeightProperty
            = DependencyProperty.RegisterAttached(
                "TopLevelMenuItemHeight",
                typeof(int),
                typeof(MenuAssist),
                new PropertyMetadata(DefaulTopLevelMenuItemHeight)
                );

        public static int GetTopLevelMenuItemHeight(DependencyObject element) => (int)element.GetValue(TopLevelMenuItemHeightProperty);
        public static void SetTopLevelMenuItemHeight(DependencyObject element, int value) => element.SetValue(TopLevelMenuItemHeightProperty, value);
        #endregion
    }
}
