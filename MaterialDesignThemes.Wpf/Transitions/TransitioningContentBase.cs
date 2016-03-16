using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialDesignThemes.Wpf.Transitions
{
    public class TransitioningContentBase : ContentControl, ITransitionEffectSubject
    {
        public const string MatrixTransformPartName = "PART_MatrixTransform";
        public const string RotateTransformPartName = "PART_RotateTransform";
        public const string ScaleTransformPartName = "PART_ScaleTransform";
        public const string SkewTransformPartName = "PART_SkewTransform";
        public const string TranslateTransformPartName = "PART_TranslateTransform";

        private MatrixTransform _matrixTransform;
        private RotateTransform _rotateTransform;
        private ScaleTransform _scaleTransform;
        private SkewTransform _skewTransform;
        private TranslateTransform _translateTransform;

        static TransitioningContentBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (TransitioningContentBase), new FrameworkPropertyMetadata(typeof (TransitioningContentBase)));
        }

        public TransitioningContentBase()
        {
            NameScope.SetNameScope(this, new NameScope());
        }

        public override void OnApplyTemplate()
        {
            _matrixTransform = GetTemplateChild(MatrixTransformPartName) as MatrixTransform;
            _rotateTransform = GetTemplateChild(RotateTransformPartName) as RotateTransform;
            _scaleTransform = GetTemplateChild(ScaleTransformPartName) as ScaleTransform;
            _skewTransform = GetTemplateChild(SkewTransformPartName) as SkewTransform;
            _translateTransform = GetTemplateChild(TranslateTransformPartName) as TranslateTransform;
            
            UnregisterNames(MatrixTransformPartName, RotateTransformPartName, ScaleTransformPartName, SkewTransformPartName, TranslateTransformPartName);
            if (_matrixTransform != null)
                RegisterName(MatrixTransformPartName, _matrixTransform);
            if (_rotateTransform != null)
                RegisterName(RotateTransformPartName, _rotateTransform);
            if (_scaleTransform != null)
                RegisterName(ScaleTransformPartName, _scaleTransform);
            if (_skewTransform != null)
                RegisterName(SkewTransformPartName, _skewTransform);
            if (_translateTransform != null)
                RegisterName(TranslateTransformPartName, _translateTransform);

            base.OnApplyTemplate();
        }

        private void UnregisterNames(params string[] names)
        {
            foreach (var name in names.Where(n => FindName(n) != null))
            {
                UnregisterName(name);
            }            
        }

        public static readonly DependencyProperty OpeningEffectProperty = DependencyProperty.Register("OpeningEffect", typeof (TransitionEffectBase), typeof (TransitioningContentBase), new PropertyMetadata(default(TransitionEffectBase)));

        /// <summary>
        /// Gets or sets the transition to run when the content is loaded and made visible.
        /// </summary>
        [TypeConverter(typeof(TransitionEffectTypeConverter))]
        public TransitionEffectBase OpeningEffect
        {
            get { return (TransitionEffectBase) GetValue(OpeningEffectProperty); }
            set { SetValue(OpeningEffectProperty, value); }
        }

        /// <summary>
        /// Allows multiple transition effects to be combined and run upon the content loading or being made visible.
        /// </summary>
        public ObservableCollection<TransitionEffectBase> OpeningEffects { get; } = new ObservableCollection<TransitionEffectBase>();

        string ITransitionEffectSubject.MatrixTransformName => MatrixTransformPartName;

        string ITransitionEffectSubject.RotateTransformName  => RotateTransformPartName;

        string ITransitionEffectSubject.ScaleTransformName => ScaleTransformPartName;

        string ITransitionEffectSubject.SkewTransformName => SkewTransformPartName;

        string ITransitionEffectSubject.TranslateTransformName => TranslateTransformPartName;

        protected virtual void RunOpeningEffects()
        {
            if (!IsLoaded) return;

            var storyboard = new Storyboard();
            var openingEffect = OpeningEffect?.Build(this);
            if (openingEffect != null)
                storyboard.Children.Add(openingEffect);
            foreach (var effect in OpeningEffects.Select(e => e.Build(this)).Where(tl => tl != null))
            {
                storyboard.Children.Add(effect);
            }

            storyboard.Begin(this);
        }
    }
}