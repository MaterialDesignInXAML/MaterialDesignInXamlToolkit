using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MaterialDesignThemes.Wpf
{
    public class PopupBox : ContentControl
    {
        public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register(
            "PopupContent", typeof (object), typeof (PopupBox), new PropertyMetadata(default(object)));

        public object PopupContent
        {
            get { return (object) GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }

        public static readonly DependencyProperty PopupContentTemplateProperty = DependencyProperty.Register(
            "PopupContentTemplate", typeof (DataTemplate), typeof (PopupBox), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate PopupContentTemplate
        {
            get { return (DataTemplate) GetValue(PopupContentTemplateProperty); }
            set { SetValue(PopupContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty StaysOpenOnEditProperty = DependencyProperty.Register(
            "StaysOpenOnEdit", typeof (bool), typeof (PopupBox), new PropertyMetadata(default(bool)));

        public bool StaysOpenOnEdit
        {
            get { return (bool) GetValue(StaysOpenOnEditProperty); }
            set { SetValue(StaysOpenOnEditProperty, value); }
        }

        public static readonly DependencyProperty IsPopupOpenProperty = DependencyProperty.Register(
            "IsPopupOpen", typeof (bool), typeof (PopupBox), new PropertyMetadata(default(bool)));

        public bool IsPopupOpen
        {
            get { return (bool) GetValue(IsPopupOpenProperty); }
            set { SetValue(IsPopupOpenProperty, value); }
        }
    }
}
