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

    [TemplatePart(Name = PackIconPartName , Type = typeof(PackIcon))]
    [TemplatePart(Name = TextBlockPartName , Type = typeof(TextBlock))]
    public class PopupBoxItem : ButtonBase {
        public const string PackIconPartName = "PackIcon";
        public const string TextBlockPartName = "TextBlock";
        private TextBlock _textBlock;
        private PackIcon _icon;

        static PopupBoxItem() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupBoxItem) , new FrameworkPropertyMetadata(typeof(PopupBoxItem)));
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            _icon = GetTemplateChild(PackIconPartName) as PackIcon;
            _textBlock = GetTemplateChild(TextBlockPartName) as TextBlock;
        }

        #region  IconProperty
        public PackIconKind Icon {
            get { return (PackIconKind)GetValue(IconProperty); }
            set { SetValue(IconProperty , value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register
            (nameof(Icon) , typeof(PackIconKind) , typeof(PopupBoxItem) , 
            new FrameworkPropertyMetadata(default(PackIconKind) , OnIconPropertyChanged));

        private static object IconPropertyCoerceValueCallBack(DependencyObject d, object basevalue) {
            var popupBoxItem = (PopupBoxItem)d;
            var newValue = (PackIconKind)basevalue;
            if (popupBoxItem._icon == null) return newValue;
            popupBoxItem._icon.Kind = newValue;
            return newValue;
        }

        private static void OnIconPropertyChanged(DependencyObject d , DependencyPropertyChangedEventArgs e) {
            var popupBoxItem = (PopupBoxItem)d;
            var newValue = (PackIconKind)e.NewValue;
            if (popupBoxItem._icon == null) return;
            popupBoxItem._icon.Kind = newValue;
        }

        #endregion

        #region TextProperty
        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty , value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text) , typeof(string) , typeof(PopupBoxItem) , new FrameworkPropertyMetadata("Example" , OnTextPropertyChanged));

        private static void OnTextPropertyChanged(DependencyObject d , DependencyPropertyChangedEventArgs e) {
            var control = d as PopupBoxItem;
            if (control._textBlock == null) return;
            control._textBlock.Text = (string)e.NewValue;
        }
        #endregion
    }
}
