using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Icon from the Material Design Icons project, <see cref="https://materialdesignicons.com/"/>.
    /// </summary>
    public class PackIcon : Control
    {
        private static readonly Lazy<IReadOnlyDictionary<PackIconKind, string>> DataIndex = 
            new Lazy<IReadOnlyDictionary<PackIconKind, string>>(PackIconDataFactory.Create);

        static PackIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIcon), new FrameworkPropertyMetadata(typeof(PackIcon)));
        }

        public static readonly DependencyProperty KindProperty = DependencyProperty.Register(
            "Kind", typeof (PackIconKind), typeof (PackIcon), new PropertyMetadata(default(PackIconKind)));

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconKind Kind
        {
            get { return (PackIconKind) GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        private static readonly DependencyPropertyKey DataPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "Data", typeof (string), typeof (PackIcon),
                new PropertyMetadata(default(string), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((PackIcon)dependencyObject).UpdateData();
        }

        public static readonly DependencyProperty DataProperty =
            DataPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the path data for the icon.
        /// </summary>
        public string Data
        {
            get { return (string) GetValue(DataProperty); }
            private set { SetValue(DataPropertyKey, value); }
        }

        public override void OnApplyTemplate()
        {
            UpdateData();

            base.OnApplyTemplate();
        }

        private void UpdateData()
        {
            Data = DataIndex.Value[Kind];
        }        
    }
}
