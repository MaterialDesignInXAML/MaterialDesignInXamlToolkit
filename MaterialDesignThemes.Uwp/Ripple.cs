using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
<<<<<<< HEAD
=======
using Windows.Devices.Input;
>>>>>>> a27409f27850f2f66b86905963924270e29b69a7
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
<<<<<<< HEAD
        private double _pointerPressedY;
        private double _pointerPressedX;        
=======
        private double _rippleSize;
        private double _rippleX;
        private double _rippleY;
>>>>>>> a27409f27850f2f66b86905963924270e29b69a7

        public Ripple()
        {
            DefaultStyleKey = typeof(Ripple);            
<<<<<<< HEAD
=======
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            RippleSize = Math.Max(sizeChangedEventArgs.NewSize.Width, sizeChangedEventArgs.NewSize.Height) * RippleSizeMultiplier;
            Clip = new RectangleGeometry() { Rect = new Windows.Foundation.Rect(0, 0, sizeChangedEventArgs.NewSize.Width, sizeChangedEventArgs.NewSize.Height) };
>>>>>>> a27409f27850f2f66b86905963924270e29b69a7
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
<<<<<<< HEAD
            var point = e.GetCurrentPoint(this);
            PointerPressedX = point.Position.X;
            PointerPressedY = point.Position.Y;
            
            VisualStateManager.GoToState(this, "Pressed", false);
=======
            if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
            {
                var point = e.GetCurrentPoint(this);

                RippleX = point.Position.X - RippleSize/2;
                RippleY = point.Position.Y - RippleSize/2;

                VisualStateManager.GoToState(this, "Normal", true);
                VisualStateManager.GoToState(this, "MousePressed", true);
            }
>>>>>>> a27409f27850f2f66b86905963924270e29b69a7

            base.OnPointerPressed(e);
        }

<<<<<<< HEAD
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
=======
        public static readonly DependencyProperty RippleSizeMultiplierProperty = DependencyProperty.Register(
            "RippleSizeMultiplier", typeof (double), typeof (Ripple), new PropertyMetadata(1.75));

        public double RippleSizeMultiplier
        {
            get { return (double) GetValue(RippleSizeMultiplierProperty); }
            set { SetValue(RippleSizeMultiplierProperty, value); }
        }

        public double RippleSize
        {
            get { return _rippleSize; }
            private set
            {
                if (_rippleSize == value) return;
                _rippleSize = value;
                OnPropertyChanged();
            }
        }

        public double RippleY
        {
            get { return _rippleY; }
            private set
            {
                if (_rippleY == value) return;
                _rippleY = value;
>>>>>>> a27409f27850f2f66b86905963924270e29b69a7
                OnPropertyChanged();
            }
        }

<<<<<<< HEAD
        public double PointerPressedX
        {
            get { return _pointerPressedX; }
            private set
            {
                if (_pointerPressedX == value) return;
                _pointerPressedX = value;
=======
        public double RippleX
        {
            get { return _rippleX; }
            private set
            {
                if (_rippleX == value) return;
                _rippleX = value;
>>>>>>> a27409f27850f2f66b86905963924270e29b69a7
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
