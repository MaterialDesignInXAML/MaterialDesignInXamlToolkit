using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf {
    /// <summary>
    /// PopupBoxItem is items made for PopupBox 
    /// </summary>

    [TemplatePart(Name = PackIconPartName , Type = typeof(ContentControl))]
    [TemplatePart(Name = TextBlockPartName , Type = typeof(ContentControl))]
    public class PopupBoxItem : ButtonBase {
        public const string PackIconPartName = "IconControl";
        public const string TextBlockPartName = "TextControl";
        private ContentControl _textControl;
        private ContentControl _iconControl;

        static PopupBoxItem() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupBoxItem) , new FrameworkPropertyMetadata(typeof(PopupBoxItem)));
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            _iconControl = (ContentControl) GetTemplateChild(PackIconPartName);
            _textControl = (ContentControl) GetTemplateChild(TextBlockPartName);
        }

        #region  IconProperty
        public object Icon {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty , value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register
            (nameof(Icon) , typeof(object) , typeof(PopupBoxItem) , 
            new FrameworkPropertyMetadata(default(object) , OnIconPropertyChanged));     

        private static void OnIconPropertyChanged(DependencyObject d , DependencyPropertyChangedEventArgs e) {
            var popupBoxItem = (PopupBoxItem)d;
            var newValue = e.NewValue;
            if (popupBoxItem._iconControl == null) return;
            popupBoxItem._iconControl.Content = newValue;
        }

        #endregion

        #region TextProperty
        public object Text {
            get { return (object)GetValue(TextProperty); }
            set { SetValue(TextProperty , value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text) , typeof(object) , typeof(PopupBoxItem) , new FrameworkPropertyMetadata("Example" , OnTextPropertyChanged));

        private static void OnTextPropertyChanged(DependencyObject d , DependencyPropertyChangedEventArgs e) {
            var control = d as PopupBoxItem;
            if (control._textControl == null) return;
            control._textControl.Content = (object)e.NewValue;
        }
        #endregion
    }
}
