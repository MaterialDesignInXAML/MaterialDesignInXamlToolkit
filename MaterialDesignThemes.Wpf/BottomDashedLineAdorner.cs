using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class BottomDashedLineAdorner : Adorner
    {
        private const double DefaultThicknessScale = 1.33;
        private const double DefaultOpacity = 0.56;

        public BottomDashedLineAdorner(UIElement adornedElement) : base(adornedElement) { }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var element = AdornedElement;
            var rect = new Rect(element.RenderSize);

            var pen = new Pen(GetBrush(element), GetThickness(element).Bottom * GetThicknessScale(element))
            {
                DashStyle = DashStyles.Dash,
                DashCap = PenLineCap.Round
            };

            drawingContext.PushOpacity(GetBrushOpacity(element));
            drawingContext.DrawLine(pen, rect.BottomLeft, rect.BottomRight);
            drawingContext.Pop();
        }

        public static readonly DependencyProperty IsAttachedProperty = DependencyProperty.RegisterAttached(
            "IsAttached", typeof(bool), typeof(BottomDashedLineAdorner), new PropertyMetadata(default(bool), OnIsAttachedChanged));

        public static void SetIsAttached(DependencyObject element, bool value) => element.SetValue(IsAttachedProperty, value);
        public static bool GetIsAttached(DependencyObject element) => (bool) element.GetValue(IsAttachedProperty);

        private static void OnIsAttachedChanged(DependencyObject @object, DependencyPropertyChangedEventArgs args)
        {
            var element = (UIElement) @object;
            if (args.OldValue is bool oldValue && oldValue)
                element.RemoveAdornersOfType<BottomDashedLineAdorner>();

            if (args.NewValue is bool newValue && newValue)
                element.AddAdornerOfType(new BottomDashedLineAdorner(element));
        }

        public static readonly DependencyProperty BrushProperty = DependencyProperty.RegisterAttached(
            "Brush", typeof(Brush), typeof(BottomDashedLineAdorner), new PropertyMetadata(default(Brush)));

        public static void SetBrush(DependencyObject element, Brush value) => element.SetValue(BrushProperty, value);
        public static Brush GetBrush(DependencyObject element) => (Brush) element.GetValue(BrushProperty);

        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.RegisterAttached(
            "Thickness", typeof(Thickness), typeof(BottomDashedLineAdorner), new PropertyMetadata(new Thickness(1)));

        public static void SetThickness(DependencyObject element, Thickness value) => element.SetValue(ThicknessProperty, value);
        public static Thickness GetThickness(DependencyObject element) => (Thickness) element.GetValue(ThicknessProperty);

        public static readonly DependencyProperty ThicknessScaleProperty = DependencyProperty.RegisterAttached(
            "ThicknessScale", typeof(double), typeof(BottomDashedLineAdorner), new PropertyMetadata(DefaultThicknessScale));

        public static void SetThicknessScale(DependencyObject element, double value) => element.SetValue(ThicknessScaleProperty, value);
        public static double GetThicknessScale(DependencyObject element) => (double) element.GetValue(ThicknessScaleProperty);

        public static readonly DependencyProperty BrushOpacityProperty = DependencyProperty.RegisterAttached(
            "BrushOpacity", typeof(double), typeof(BottomDashedLineAdorner), new PropertyMetadata(DefaultOpacity));

        public static void SetBrushOpacity(DependencyObject element, double value) => element.SetValue(BrushOpacityProperty, value);
        public static double GetBrushOpacity(DependencyObject element) => (double) element.GetValue(BrushOpacityProperty);
    }
}