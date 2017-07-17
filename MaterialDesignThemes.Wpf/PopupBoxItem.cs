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
    public class PopupBoxItem : ButtonBase {
        private readonly TextBlock _textBlock;
        private readonly PackIcon _icon;
        public PopupBoxItem() {
            _textBlock = new TextBlock() { Text = Text , VerticalAlignment = VerticalAlignment.Center };
            _icon = new PackIcon() { Kind = this.Icon , Margin = new Thickness(0,0,10,0), Width = 20 , Height = 20 , VerticalAlignment = VerticalAlignment.Center };
            this.Content = new Button() {
                Content = new StackPanel() {                    
                    Orientation = Orientation.Horizontal ,
                    Children = { _icon , _textBlock }
                } ,                
            };                        
            
        }

        #region  IconProperty
        public PackIconKind Icon {
            get { return (PackIconKind)GetValue(IconProperty); }
            set { SetValue(IconProperty , value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon" , typeof(PackIconKind) , typeof(PopupBoxItem) , new PropertyMetadata(PackIconKind.AccountCircle , OnIconPropertyChanged));

        private static void OnIconPropertyChanged(DependencyObject d , DependencyPropertyChangedEventArgs e) {
            var control = d as PopupBoxItem;
            control._icon.Kind = (PackIconKind)e.NewValue;
        }
        #endregion

        #region TextProperty
        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty , value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text" , typeof(string) , typeof(PopupBoxItem) , new PropertyMetadata("Example" , OnTextPropertyChanged));



        private static void OnTextPropertyChanged(DependencyObject d , DependencyPropertyChangedEventArgs e) {
            var control = d as PopupBoxItem;
            control._textBlock.Text = (string)e.NewValue;
        }
        #endregion
    }
}
