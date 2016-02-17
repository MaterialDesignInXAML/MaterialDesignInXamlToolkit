using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// 
    /// </summary>
    [TemplateVisualState(GroupName="ActivationStates", Name = ActiveStateName)]
    [TemplateVisualState(GroupName = "ActivationStates", Name = InactiveStateName)]
    public class Underline : Control
    {
        public const string ActiveStateName = "Active";
        public const string InactiveStateName = "Inactive";

        static Underline()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Underline), new FrameworkPropertyMetadata(typeof(Underline)));
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            nameof(IsActive), typeof(bool), typeof(Underline),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, IsActivePropertyChangedCallback));

        private static void IsActivePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((Underline)dependencyObject).GotoVisualState(!TransitionAssist.GetDisableTransitions(dependencyObject));
        }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            GotoVisualState(false);
        }

        private void GotoVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, SelectStateName(), useTransitions);
        }

        private string SelectStateName()
        {
            return IsActive ? ActiveStateName : InactiveStateName;
        }
    }
}