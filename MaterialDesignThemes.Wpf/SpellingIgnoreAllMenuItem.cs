using System.Windows.Controls;
using System.Windows.Documents;

namespace MaterialDesignThemes.Wpf
{
    public class SpellingIgnoreAllMenuItem : MenuItem
    {
        public SpellingIgnoreAllMenuItem()
        {
            Command = EditingCommands.IgnoreSpellingError;
        }
    }
}