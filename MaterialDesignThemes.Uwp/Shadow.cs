using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace MaterialDesignThemes.Uwp
{    
    [TemplatePart(Name = CanvasControlPartName, Type = typeof(CanvasControl))]
    [TemplatePart(Name = ContentPresenterPartName, Type = typeof(ContentPresenter))]
    public sealed class Shadow : ContentControl
    {
        private CanvasControl _canvasControl;
        private ContentPresenter _contentPresenter;
        private const string ContentPresenterPartName = "PART_ContentPresenter";
        private const string CanvasControlPartName = "PART_CanvasControl";

        public Shadow()
        {
            DefaultStyleKey = typeof(Shadow);
            Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_canvasControl == null) return;

            _canvasControl.RemoveFromVisualTree();
            _canvasControl = null;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _canvasControl = GetTemplateChild(CanvasControlPartName) as CanvasControl;
            if (_canvasControl != null)
                _canvasControl.Draw += Draw;

            _contentPresenter = GetTemplateChild(ContentPresenterPartName) as ContentPresenter;
        }

        private void Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (_contentPresenter == null) return;

            var border = VisualTreeHelper.GetChild(_contentPresenter, 0) as Border;
            if (border == null) return;

            var borderPoint = border.TransformToVisual(this).TransformPoint(new Point(0, 0));

            var cl = new CanvasCommandList(sender);
            using (var clds = cl.CreateDrawingSession())
            {
                clds.FillRoundedRectangle(new Rect(borderPoint.X, borderPoint.Y, border.ActualWidth, border.ActualHeight), (float)border.CornerRadius.TopLeft, (float)border.CornerRadius.TopLeft, Color.FromArgb(128, 0, 0, 0));
            }

            var shadowEffect = new Transform2DEffect
            {
                Source =
                    new Transform2DEffect
                    {
                        Source = new ShadowEffect
                        {
                            BlurAmount = 2,
                            ShadowColor = Color.FromArgb(160, 0, 0, 0),
                            Source = cl
                        },
                        //TODO not doing any scaling right now, confirm with larger shadows
                        TransformMatrix = Matrix3x2.CreateScale(1.0f, new Vector2((float)(border.ActualWidth / 2), ((float)border.ActualHeight / 2)))

                    },
                TransformMatrix = Matrix3x2.CreateTranslation(0, 1)
            };

            args.DrawingSession.DrawImage(shadowEffect);
            // args.DrawingSession.DrawImage(cl);
        }
    }
}
