using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// This interface is the adapter from UiControl (like <see cref="TextBox"/>, <see cref="ComboBox"/> and others) to <see cref="SmartHint"/>
    /// <para/>
    /// You should implement this interface in order to use SmartHint for your own control.
    /// </summary>
    public interface IHintProxy : IDisposable
    {
        object Content { get; }

        bool IsLoaded { get; }

        bool IsVisible { get; }

        event EventHandler ContentChanged;

        event EventHandler IsVisibleChanged;

        event EventHandler Loaded;
    }
}
