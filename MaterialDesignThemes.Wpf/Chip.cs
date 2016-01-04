using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public class Chip : Control
    {
        static Chip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Chip), new FrameworkPropertyMetadata(typeof(Chip)));
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof (object), typeof (Chip), new PropertyMetadata(default(object)));

        public object Icon
        {
            get { return (object) GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof (string), typeof (Chip), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty IsDeletableProperty = DependencyProperty.Register(
            "IsDeletable", typeof (bool), typeof (Chip), new PropertyMetadata(default(bool)));

        public bool IsDeletable
        {
            get { return (bool) GetValue(IsDeletableProperty); }
            set { SetValue(IsDeletableProperty, value); }
        }
    }
}
