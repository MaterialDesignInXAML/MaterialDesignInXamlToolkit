using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    internal class HorizontalStepperHeaderTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            StepperStepViewModel step = item as StepperStepViewModel;

            if (element != null && item != null)
            {
                if (step.IsFirstStep)
                {
                    return element.FindResource("firstHorizontalStepHeaderTemplate") as DataTemplate;
                }
                else if (step.IsLastStep)
                {
                    return element.FindResource("lastHorizontalStepHeaderTemplate") as DataTemplate;
                }
                else
                {
                    return element.FindResource("intermediateHorizontalStepHeaderTemplate") as DataTemplate;
                }
            }

            return null;
        }
    }
}
