using System.Windows.Controls;
using System.Windows.Documents;

namespace MaterialDesignThemes.Wpf
{
    public class SpellingSuggestionMenuItem : MenuItem
    {
        public string Suggestion { get; }
        public SpellingSuggestionMenuItem(string spellingSuggestion)
        {
            Suggestion = spellingSuggestion;
            Command = EditingCommands.CorrectSpellingError;
            CommandParameter = spellingSuggestion;
        }
    }
}