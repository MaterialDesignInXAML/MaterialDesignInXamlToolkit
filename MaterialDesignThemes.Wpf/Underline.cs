using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf
{
    public class Underline : Control
    {
        static Underline()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Underline), new FrameworkPropertyMetadata(typeof(Underline)));
        }

        public static readonly DependencyProperty BindIsKeyboardFocusedProperty = DependencyProperty.Register(
            "BindIsKeyboardFocused", typeof(bool), typeof(Underline),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender, BindIsKeyboardFocusedPropertyChangedCallback));

        public bool BindIsKeyboardFocused
        {
            get { return (bool)GetValue(BindIsKeyboardFocusedProperty); }
            set { SetValue(BindIsKeyboardFocusedProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive", typeof(bool), typeof(Underline),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        private void UpdateBindIsKeyboardFocused()
        {
            if (BindIsKeyboardFocused)
            {
                BindingOperations.ClearBinding(this, Underline.IsActiveProperty);

                var uiElement = TemplatedParent as UIElement;
                if (uiElement != null)
                {
                    Binding isKeyboardFocusedBinding = new Binding
                    {
                        Path = new PropertyPath("IsKeyboardFocused"),
                        Source = uiElement
                    };
                    this.SetBinding(Underline.IsActiveProperty, isKeyboardFocusedBinding);
                }
            }
            else
            {
                BindingOperations.ClearBinding(this, Underline.IsActiveProperty);
            }
        }

        private static void BindIsKeyboardFocusedPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Underline)?.UpdateBindIsKeyboardFocused();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateBindIsKeyboardFocused();
        }
    }
}