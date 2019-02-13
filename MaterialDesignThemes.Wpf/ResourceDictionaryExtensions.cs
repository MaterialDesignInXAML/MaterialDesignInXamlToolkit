using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialDesignThemes.Wpf
{
    public static class ResourceDictionaryExtensions
    {
        /// <summary>
        /// Replaces a certain entry anywhere in the source dictionary and its merged dictionaries
        /// </summary>
        /// <param name="sourceDictionary">The source dictionary to start with</param>
        /// <param name="name">The entry to replace</param>
        /// <param name="value">The new entry value</param>
        public static void ReplaceEntry(this ResourceDictionary sourceDictionary, object name, object value)
        {
            if (sourceDictionary.Contains(name))
            {
                if (sourceDictionary[name] is SolidColorBrush brush &&
                    !brush.IsFrozen && value is SolidColorBrush newBrush)
                {
                    var animation = new ColorAnimation {
                        From = brush.Color,
                        To = newBrush.Color,
                        Duration = new Duration(TimeSpan.FromMilliseconds(300))
                    };
                    brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                }
                else
                {
                    sourceDictionary[name] = value; //Set value normally
                }
            }

            foreach (var dictionary in sourceDictionary.MergedDictionaries)
            {
                ReplaceEntry(dictionary, name, value);
            }
        }
    }
}
