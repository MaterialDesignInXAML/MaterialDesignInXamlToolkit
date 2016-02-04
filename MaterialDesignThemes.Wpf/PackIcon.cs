using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ControlzEx;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Icon from the Material Design Icons project, <see cref="https://materialdesignicons.com/"/>.
    /// </summary>
    public class PackIcon : PackIconBase<PackIconKind>
    {        
        static PackIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIcon), new FrameworkPropertyMetadata(typeof(PackIcon)));
        }

        public PackIcon() : base(PackIconDataFactory.Create) { }
    }
}
