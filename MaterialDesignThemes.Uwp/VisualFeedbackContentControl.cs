using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace MaterialDesignThemes.Uwp
{
    public sealed class VisualFeedbackContentControl : ContentControl, INotifyPropertyChanged
    {
        private double _pointerX;
        private double _pointerY;
        private double _pointerPressedY;
        private double _pointerPressedX;


        public VisualFeedbackContentControl()
        {
            this.DefaultStyleKey = typeof(VisualFeedbackContentControl);
        }

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {            
            var point = e.GetCurrentPoint(this);            
            PointerPressedX = point.Position.X;
            PointerPressedY = point.Position.Y;

            base.OnPointerPressed(e);
        }

        protected override void OnPointerMoved(PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(this);
            PointerX = point.Position.X;
            PointerY = point.Position.Y;
        }

        public double PointerX
        {
            get { return _pointerX; }
            private set
            {
                if (_pointerX == value) return;
                _pointerX = value;
                OnPropertyChanged();
            }
        }

        public double PointerY
        {
            get { return _pointerY; }
            private set
            {
                if (_pointerY == value) return;
                _pointerY = value;
                OnPropertyChanged();
            }
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
