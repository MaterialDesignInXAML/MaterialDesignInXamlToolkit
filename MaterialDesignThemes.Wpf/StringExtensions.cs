﻿using System.Globalization;

namespace MaterialDesignThemes.Wpf
{
    internal static class StringExtensions
    {
        public static string ToTitleCase(this string text, CultureInfo culture, string separator = " ")
        {
            TextInfo textInfo = culture.TextInfo;

            string lowerText = textInfo.ToLower(text);
            string[] words = lowerText.Split(new[] { separator }, StringSplitOptions.None);

            return String.Join(separator, words.Select(v => textInfo.ToTitleCase(v)));
        }
    }
}
