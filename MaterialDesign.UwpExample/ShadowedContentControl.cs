using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.Foundation.Numerics;
using Windows.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace MaterialDesign.UwpExample
{
    [TemplatePart(Name=CanvasControlPartName, Type=typeof(CanvasControl))]
    public sealed class ShadowedContentControl : ContentControl
    {
        public const string CanvasControlPartName = "PART_CanvasControl";

        public ShadowedContentControl()
        {
            this.DefaultStyleKey = typeof(ShadowedContentControl);
        }

        protected override void OnApplyTemplate()
        {
            var canvasControl = GetTemplateChild(CanvasControlPartName) as CanvasControl;
            canvasControl.Draw += OnDraw;

            base.OnApplyTemplate();
        }

        private void OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawEllipse(new Vector2(100), 75, 125, Colors.Yellow);                
        }
    }
}
