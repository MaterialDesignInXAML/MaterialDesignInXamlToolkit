using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Specifies the display state of the text field's character counter.
    /// </summary>
    public enum CharacterCounterVisibility
    {
        /// <summary>
        /// Do not display.
        /// </summary>
        Collapsed,
        /// <summary>
        /// Display if both <see cref="TextBox.MaxLength"/> is non-zero and it's focused.
        /// </summary>
        WhenFocused,
        /// <summary>
        /// Display if <see cref="TextBox.MaxLength"/> is non-zero.
        /// </summary>
        Visible
    }
}
