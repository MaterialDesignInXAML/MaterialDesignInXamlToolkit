using System;
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

        private MatrixTransform? _matrixTransform;
        private RotateTransform? _rotateTransform;
        private ScaleTransform? _scaleTransform;
        private SkewTransform? _skewTransform;
        private TranslateTransform? _translateTransform;

        static TransitioningContentBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransitioningContentBase), new FrameworkPropertyMetadata(typeof(TransitioningContentBase)));
        }

        public override void OnApplyTemplate()
        {
            FrameworkElement nameScopeRoot = GetNameScopeRoot();

            _matrixTransform = GetTemplateChild(MatrixTransformPartName) as MatrixTransform;
            _rotateTransform = GetTemplateChild(RotateTransformPartName) as RotateTransform;
            _scaleTransform = GetTemplateChild(ScaleTransformPartName) as ScaleTransform;
            _skewTransform = GetTemplateChild(SkewTransformPartName) as SkewTransform;
            _translateTransform = GetTemplateChild(TranslateTransformPartName) as TranslateTransform;

            UnregisterNames(MatrixTransformPartName, RotateTransformPartName, ScaleTransformPartName, SkewTransformPartName, TranslateTransformPartName);
            if (_matrixTransform != null)
                nameScopeRoot.RegisterName(MatrixTransformPartName, _matrixTransform);
            if (_rotateTransform != null)
                nameScopeRoot.RegisterName(RotateTransformPartName, _rotateTransform);
            if (_scaleTransform != null)
                nameScopeRoot.RegisterName(ScaleTransformPartName, _scaleTransform);
            if (_skewTransform != null)
                nameScopeRoot.RegisterName(SkewTransformPartName, _skewTransform);
            if (_translateTransform != null)
                nameScopeRoot.RegisterName(TranslateTransformPartName, _translateTransform);

            base.OnApplyTemplate();

            RunOpeningEffects();

            void UnregisterNames(params string[] names)
            {
                foreach (var name in names.Where(n => FindName(n) != null))
                {
                    UnregisterName(name);
                }
            }
        }

        public static readonly DependencyProperty OpeningEffectProperty = DependencyProperty.Register("OpeningEffect", typeof(TransitionEffectBase), typeof(TransitioningContentBase), new PropertyMetadata(default(TransitionEffectBase)));

        /// <summary>
        /// Gets or sets the transition to run when the content is loaded and made visible.
        /// </summary>
        [TypeConverter(typeof(TransitionEffectTypeConverter))]
        public TransitionEffectBase? OpeningEffect
        {
            get => (TransitionEffectBase?)GetValue(OpeningEffectProperty);
            set => SetValue(OpeningEffectProperty, value);
        }

        public static readonly DependencyProperty OpeningEffectsOffsetProperty = DependencyProperty.Register(
            "OpeningEffectsOffset", typeof(TimeSpan), typeof(TransitioningContentBase), new PropertyMetadata(default(TimeSpan)));

        /// <summary>
        /// Delay offset to be applied to all opening effect transitions.
        /// </summary>
        public TimeSpan OpeningEffectsOffset
        {
            get => (TimeSpan)GetValue(OpeningEffectsOffsetProperty);
            set => SetValue(OpeningEffectsOffsetProperty, value);
        }

        /// <summary>
        /// Allows multiple transition effects to be combined and run upon the content loading or being made visible.
        /// </summary>
        public ObservableCollection<TransitionEffectBase> OpeningEffects { get; } = new ObservableCollection<TransitionEffectBase>();

        string ITransitionEffectSubject.MatrixTransformName => MatrixTransformPartName;

        string ITransitionEffectSubject.RotateTransformName => RotateTransformPartName;

        string ITransitionEffectSubject.ScaleTransformName => ScaleTransformPartName;

        string ITransitionEffectSubject.SkewTransformName => SkewTransformPartName;

        string ITransitionEffectSubject.TranslateTransformName => TranslateTransformPartName;

        TimeSpan ITransitionEffectSubject.Offset => OpeningEffectsOffset;

        protected virtual void RunOpeningEffects()
        {
            if (!IsLoaded || _matrixTransform is null)
            {
                return;
            }

            var storyboard = new Storyboard();
            Timeline? openingEffect = OpeningEffect?.Build(this);
            if (openingEffect != null)
                storyboard.Children.Add(openingEffect);
            foreach (var effect in OpeningEffects.Select(e => e.Build(this)).Where(tl => tl is not null))
            {
                storyboard.Children.Add(effect);
            }

            storyboard.Begin(GetNameScopeRoot());
        }

        private FrameworkElement GetNameScopeRoot()
        {
            //https://github.com/ButchersBoy/MaterialDesignInXamlToolkit/issues/950
            //Only set the NameScope if the child does not already have a TemplateNameScope set
            if (VisualChildrenCount > 0 && GetVisualChild(0) is FrameworkElement fe && NameScope.GetNameScope(fe) != null)
            {
                return fe;
            }

            if (NameScope.GetNameScope(this) is null)
            {
                NameScope.SetNameScope(this, new NameScope());
            }

            return this;
        }
    }
}