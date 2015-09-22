using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace MaterialDesignThemes.Uwp
{
    [TemplateVisualState(GroupName = "CommonStates", Name = "Normal")]
    [TemplateVisualState(GroupName = "CommonStates", Name = "Pressed")]    
    public sealed class Ripple : ContentControl, INotifyPropertyChanged
    {
        private double _pointerPressedY;
        private double _pointerPressedX;        

        public Ripple()
        {
            DefaultStyleKey = typeof(Ripple);            
        }

        public static readonly DependencyProperty FeedbackProperty = DependencyProperty.Register(
            "Feedback", typeof (Brush), typeof (Ripple), new PropertyMetadata(default(Brush)));

        public Brush Feedback
        {
            get { return (Brush) GetValue(FeedbackProperty); }
            set { SetValue(FeedbackProperty, value); }
        }

        protected override void OnApplyTemplate()
        {
            VisualStateManager.GoToState(this, "Normal", false);

            base.OnApplyTemplate();
        }

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(this);
            PointerPressedX = point.Position.X;
            PointerPressedY = point.Position.Y;
            
            VisualStateManager.GoToState(this, "Pressed", false);

            base.OnPointerPressed(e);
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", false);

            base.OnPointerReleased(e);
        }

        public double PointerPressedY
        {
            get { return _pointerPressedY; }
            private set
            {
                if (_pointerPressedY == value) return;
                _pointerPressedY = value;
                OnPropertyChanged();
            }
        }

        public double PointerPressedX
        {
            get { return _pointerPressedX; }
            private set
            {
                if (_pointerPressedX == value) return;
                _pointerPressedX = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
