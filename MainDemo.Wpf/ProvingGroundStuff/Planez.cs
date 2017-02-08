using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Planerator
{
    /// <summary>
    /// Planerator - dead simple interactive 3D in WPF.  Read more at 
    /// http://blogs.msdn.com/greg_schechter/archive/2007/10/26/enter-the-planerator-dead-simple-3d-in-wpf-with-a-stupid-name.aspx
    /// 
    /// Greg Schechter - Fall 2007
    /// </summary>
    [ContentProperty("Child")]
    public class Planerator : FrameworkElement
    {
        #region Public API

        #region Dependency Properties

        public static readonly DependencyProperty RotationXProperty =
            DependencyProperty.Register("RotationX", typeof(double), typeof(Planerator), new UIPropertyMetadata(0.0, (d, args) => ((Planerator)d).UpdateRotation()));
        public static readonly DependencyProperty RotationYProperty =
            DependencyProperty.Register("RotationY", typeof(double), typeof(Planerator), new UIPropertyMetadata(0.0, (d, args) => ((Planerator)d).UpdateRotation()));
        public static readonly DependencyProperty RotationZProperty =
            DependencyProperty.Register("RotationZ", typeof(double), typeof(Planerator), new UIPropertyMetadata(0.0, (d, args) => ((Planerator)d).UpdateRotation()));
        public static readonly DependencyProperty FieldOfViewProperty =
            DependencyProperty.Register("FieldOfView", typeof(double), typeof(Planerator), new UIPropertyMetadata(45.0, (d, args) => ((Planerator)d).Update3D(),
                (d, val) => Math.Min(Math.Max((double)val, 0.5), 179.9))); // clamp to a meaningful range


        public double RotationX
        {
            get { return (double)GetValue(RotationXProperty); }
            set { SetValue(RotationXProperty, value); }
        }
        public double RotationY
        {
            get { return (double)GetValue(RotationYProperty); }
            set { SetValue(RotationYProperty, value); }
        }
        public double RotationZ
        {
            get { return (double)GetValue(RotationZProperty); }
            set { SetValue(RotationZProperty, value); }
        }
        public double FieldOfView
        {
            get { return (double)GetValue(FieldOfViewProperty); }
            set { SetValue(FieldOfViewProperty, value); }
        }

        #endregion

        public FrameworkElement Child
        {
            get
            {
                return _originalChild;
            }
            set
            {
                if (_originalChild != value)
                {
                    this.RemoveVisualChild(_visualChild);
                    this.RemoveLogicalChild(_logicalChild);

                    // Wrap child with special decorator that catches layout invalidations. 
                    _originalChild = value;
                    _logicalChild = new LayoutInvalidationCatcher() { Child = _originalChild };
                    _visualChild = CreateVisualChild();

                    this.AddVisualChild(_visualChild);

                    // Need to use a logical child here to make sure databinding operations get down to it,
                    // since otherwise the child appears only as the Visual to a Viewport2DVisual3D, which 
                    // doesn't have databinding operations pass into it from above.
                    this.AddLogicalChild(_logicalChild);
                    this.InvalidateMeasure();
                }
            }
        }

        #endregion

        #region Layout Stuff

        protected override Size MeasureOverride(Size availableSize)
        {
            Size result;
            if (_logicalChild != null)
            {
                // Measure based on the size of the logical child, since we want to align with it.
                _logicalChild.Measure(availableSize);
                result = _logicalChild.DesiredSize;
                _visualChild.Measure(result);
            }
            else
            {
                result = new Size(0, 0);
            }
            return result;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (_logicalChild != null)
            {
                _logicalChild.Arrange(new Rect(finalSize));
                _visualChild.Arrange(new Rect(finalSize));
                Update3D();
            }
            return base.ArrangeOverride(finalSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visualChild;

        }

        protected override int VisualChildrenCount
        {
            get
            {
                return _visualChild == null ? 0 : 1;
            }
        }

        #endregion

        #region 3D Stuff

        private FrameworkElement CreateVisualChild()
        {
            MeshGeometry3D simpleQuad = new MeshGeometry3D()
            {
                Positions = new Point3DCollection(_mesh),
                TextureCoordinates = new PointCollection(_texCoords),
                TriangleIndices = new Int32Collection(_indices)
            };

            // Front material is interactive, back material is not.
            Material frontMaterial = new DiffuseMaterial(Brushes.White);
            frontMaterial.SetValue(Viewport2DVisual3D.IsVisualHostMaterialProperty, true);

            VisualBrush vb = new VisualBrush(_logicalChild);
            SetCachingForObject(vb);  // big perf wins by caching!!
            Material backMaterial = new DiffuseMaterial(vb);

            _rotationTransform.Rotation = _quaternionRotation;
            var xfGroup = new Transform3DGroup() { Children = { _scaleTransform, _rotationTransform } };

            GeometryModel3D backModel = new GeometryModel3D() { Geometry = simpleQuad, Transform = xfGroup, BackMaterial = backMaterial };
            Model3DGroup m3dGroup = new Model3DGroup()
            {
                Children = { new DirectionalLight(Colors.White, new Vector3D(0, 0, -1)),
                                 new DirectionalLight(Colors.White, new Vector3D(0.1, -0.1, 1)),
                                 backModel }
            };

            // Non-interactive Visual3D consisting of the backside, and two lights.
            ModelVisual3D mv3d = new ModelVisual3D() { Content = m3dGroup };

            // Interactive frontside Visual3D
            Viewport2DVisual3D frontModel = new Viewport2DVisual3D() { Geometry = simpleQuad, Visual = _logicalChild, Material = frontMaterial, Transform = xfGroup };

            // Cache the brush in the VP2V3 by setting caching on it.  Big perf wins.
            SetCachingForObject(frontModel);

            // Scene consists of both the above Visual3D's.
            _viewport3d = new Viewport3D() { ClipToBounds = false, Children = { mv3d, frontModel } };

            UpdateRotation();

            return _viewport3d;
        }

        private void SetCachingForObject(DependencyObject d)
        {
            RenderOptions.SetCachingHint(d, CachingHint.Cache);
            RenderOptions.SetCacheInvalidationThresholdMinimum(d, 0.5);
            RenderOptions.SetCacheInvalidationThresholdMaximum(d, 2.0);
        }

        private void UpdateRotation()
        {
            Quaternion qx = new Quaternion(_xAxis, RotationX);
            Quaternion qy = new Quaternion(_yAxis, RotationY);
            Quaternion qz = new Quaternion(_zAxis, RotationZ);

            _quaternionRotation.Quaternion = qx * qy * qz;
        }

        private void Update3D()
        {
            // Use GetDescendantBounds for sizing and centering since DesiredSize includes layout whitespace, whereas GetDescendantBounds 
            // is tighter
            Rect logicalBounds = VisualTreeHelper.GetDescendantBounds(_logicalChild);
            double w = logicalBounds.Width;
            double h = logicalBounds.Height;

            // Create a camera that looks down -Z, with up as Y, and positioned right halfway in X and Y on the element, 
            // and back along Z the right distance based on the field-of-view is the same projected size as the 2D content
            // that it's looking at.  See http://blogs.msdn.com/greg_schechter/archive/2007/04/03/camera-construction-in-parallaxui.aspx
            // for derivation of this camera.
            double fovInRadians = FieldOfView * (Math.PI / 180);
            double zValue = w / Math.Tan(fovInRadians / 2) / 1.5;
            _viewport3d.Camera = new PerspectiveCamera(new Point3D(w / 2, h / 2, zValue),
                                                       -_zAxis,
                                                       _yAxis,
                                                       FieldOfView);


            _scaleTransform.ScaleX = w;
            _scaleTransform.ScaleY = h;
            _rotationTransform.CenterX = w / 2;
            _rotationTransform.CenterY = h / 2;
        }

        #endregion

        #region Private Classes

        /// <summary>
        /// Wrap this around a class that we want to catch the measure and arrange 
        /// processes occuring on, and propagate to the parent Planerator, if any.
        /// Do this because layout invalidations don't flow up out of a 
        /// Viewport2DVisual3D object.
        /// </summary>
        private class LayoutInvalidationCatcher : Decorator
        {
            protected override Size MeasureOverride(Size constraint)
            {
                Planerator pl = this.Parent as Planerator;
                if (pl != null)
                {
                    pl.InvalidateMeasure();
                }
                return base.MeasureOverride(constraint);
            }

            protected override Size ArrangeOverride(Size arrangeSize)
            {
                Planerator pl = this.Parent as Planerator;
                if (pl != null)
                {
                    pl.InvalidateArrange();
                }
                return base.ArrangeOverride(arrangeSize);
            }
        }

        #endregion

        #region Private data

        // Instance data
        private FrameworkElement _logicalChild;
        private FrameworkElement _visualChild;
        private FrameworkElement _originalChild;

        private QuaternionRotation3D _quaternionRotation = new QuaternionRotation3D();
        private RotateTransform3D _rotationTransform = new RotateTransform3D();
        private Viewport3D _viewport3d;
        private ScaleTransform3D _scaleTransform = new ScaleTransform3D();

        // Static data
        static private readonly Point3D[] _mesh = new Point3D[] { new Point3D(0, 0, 0), new Point3D(0, 1, 0), new Point3D(1, 1, 0), new Point3D(1, 0, 0) };
        static private readonly Point[] _texCoords = new Point[] { new Point(0, 1), new Point(0, 0), new Point(1, 0), new Point(1, 1) };
        static private readonly int[] _indices = new int[] { 0, 2, 1, 0, 3, 2 };
        static private readonly Vector3D _xAxis = new Vector3D(1, 0, 0);
        static private readonly Vector3D _yAxis = new Vector3D(0, 1, 0);
        static private readonly Vector3D _zAxis = new Vector3D(0, 0, 1);

        #endregion
    }


}
