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
    /// A card is a content control, styled according to Material Design guidelines.
    /// </summary>
    public class Card : ContentControl
    {
        static Card()
		{
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Card), new FrameworkPropertyMetadata(typeof(Card)));
		}
    }
}
