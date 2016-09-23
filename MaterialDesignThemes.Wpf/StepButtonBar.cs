using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Convenience control for the navigation buttons inside a <see cref="Stepper"/>.
    /// Setting the content for a button will show it. Otherwise the button will be collapsed.
    /// </summary>
    public class StepButtonBar : Control
    {
        public static readonly DependencyProperty BackProperty = DependencyProperty.Register(
                nameof(Back), typeof(object), typeof(StepButtonBar), new PropertyMetadata(null));

        /// <summary>
        /// The content for the back button.
        /// </summary>
        public object Back
        {
            get
            {
                return GetValue(BackProperty);
            }

            set
            {
                SetValue(BackProperty, value);
            }
        }

        public static readonly DependencyProperty CancelProperty = DependencyProperty.Register(
                nameof(Cancel), typeof(object), typeof(StepButtonBar), new PropertyMetadata(null));

        /// <summary>
        /// The content for the cancel button.
        /// </summary>
        public object Cancel
        {
            get
            {
                return GetValue(CancelProperty);
            }

            set
            {
                SetValue(CancelProperty, value);
            }
        }

        public static readonly DependencyProperty ContinueProperty = DependencyProperty.Register(
                nameof(Continue), typeof(object), typeof(StepButtonBar), new PropertyMetadata(null));

        /// <summary>
        /// The content for the continue button.
        /// </summary>
        public object Continue
        {
            get
            {
                return GetValue(ContinueProperty);
            }

            set
            {
                SetValue(ContinueProperty, value);
            }
        }

        internal static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
                nameof(Mode), typeof(StepperLayout), typeof(StepButtonBar), new PropertyMetadata(StepperLayout.Horizontal));

        internal StepperLayout Mode
        {
            get
            {
                return (StepperLayout)GetValue(ModeProperty);
            }

            set
            {
                SetValue(ModeProperty, value);
            }
        }

        static StepButtonBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StepButtonBar), new FrameworkPropertyMetadata(typeof(StepButtonBar)));
        }

        public StepButtonBar() : base() { }

        public override void OnApplyTemplate()
        {
            // read the Orientation of the Stepper and set it as the Mode
            //     - changing the Layout throws the UI of the Stepper and builds a new one
            //     - therefore this method will be called for a new instance and the changes of the Layout will be applied to Mode
            Stepper stepper = FindStepper();

            if (stepper != null)
            {
                Mode = stepper.Layout;
            }

            base.OnApplyTemplate();
        }

        private Stepper FindStepper()
        {
            DependencyObject element = VisualTreeHelper.GetParent(this);

            while (element != null && !(element is Stepper))
            {
                element = VisualTreeHelper.GetParent(element);
            }

            return element as Stepper;
        }
    }
}
