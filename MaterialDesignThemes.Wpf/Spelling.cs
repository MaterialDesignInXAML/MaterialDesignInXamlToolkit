using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class Spelling
    {
        public static ResourceKey SpellingSuggestionMenuItemStyleKey { get; } = new ComponentResourceKey(typeof(Spelling), ResourceKeyId.SpellingSuggestionMenuItemStyle);
        public static ResourceKey SpellingIgnoreAllMenuItemStyleKey { get; } = new ComponentResourceKey(typeof(Spelling), ResourceKeyId.SpellingIgnoreAllMenuItemStyle);
        public static ResourceKey SpellingNoSuggestionsMenuItemStyleKey { get; } = new ComponentResourceKey(typeof(Spelling), ResourceKeyId.SpellingNoSuggestionsMenuItemStyle);
        public static ResourceKey SpellingSeparatorStyleKey { get; } = new ComponentResourceKey(typeof(Spelling), ResourceKeyId.SpellingSeparatorStyle);
    }

    internal enum ResourceKeyId
    {
        SpellingSuggestionMenuItemStyle,
        SpellingIgnoreAllMenuItemStyle,
        SpellingNoSuggestionsMenuItemStyle,
        SpellingSeparatorStyle,
    }
}