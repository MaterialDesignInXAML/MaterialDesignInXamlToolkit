using System;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    internal class ComboBoxAssistManagedOverlayInfo
    {
        public ComboBoxAssistManagedOverlayInfo(ComboBox owner, TextBox originalTextBox, TextBox cloneTextBox)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (originalTextBox == null) throw new ArgumentNullException(nameof(originalTextBox));
            if (cloneTextBox == null) throw new ArgumentNullException(nameof(cloneTextBox));

            Owner = owner;
            OriginalTextBox = originalTextBox;
            CloneTextBox = cloneTextBox;
        }

        public ComboBox Owner { get; }

        public TextBox OriginalTextBox { get; }

        public TextBox CloneTextBox { get; }
    }
}