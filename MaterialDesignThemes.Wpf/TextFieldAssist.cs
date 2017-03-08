using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Helper properties for working with text fields.
    /// </summary>
    public static class TextFieldAssist
    {
        /// <summary>
        /// The text box view margin property
        /// </summary>
        public static readonly DependencyProperty TextBoxViewMarginProperty = DependencyProperty.RegisterAttached(
            "TextBoxViewMargin",
            typeof(Thickness),
            typeof(TextFieldAssist),
            new FrameworkPropertyMetadata(new Thickness(double.NegativeInfinity), FrameworkPropertyMetadataOptions.Inherits, TextBoxViewMarginPropertyChangedCallback));

        /// <summary>
        /// Sets the text box view margin.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetTextBoxViewMargin(DependencyObject element, Thickness value)
        {
            element.SetValue(TextBoxViewMarginProperty, value);
        }

        /// <summary>
        /// Gets the text box view margin.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The <see cref="Thickness" />.
        /// </returns>
        public static Thickness GetTextBoxViewMargin(DependencyObject element)
        {
            return (Thickness)element.GetValue(TextBoxViewMarginProperty);
        }


        /// <summary>
        /// Controls the visibility of the underline decoration.
        /// </summary>
        public static readonly DependencyProperty DecorationVisibilityProperty = DependencyProperty.RegisterAttached(
            "DecorationVisibility", typeof (Visibility), typeof (TextFieldAssist), new PropertyMetadata(default(Visibility)));

        /// <summary>
        /// Controls the visibility of the underline decoration.
        /// </summary>
        public static void SetDecorationVisibility(DependencyObject element, Visibility value)
        {
            element.SetValue(DecorationVisibilityProperty, value);
        }

        /// <summary>
        /// Controls the visibility of the underline decoration.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Visibility GetDecorationVisibility(DependencyObject element)
        {
            return (Visibility)element.GetValue(DecorationVisibilityProperty);
        }

        /// <summary>
        /// Automatially inserts spelling suggestions into the text box context menu.
        /// </summary>
        public static readonly DependencyProperty IncludeSpellingSuggestionsProperty = DependencyProperty.RegisterAttached(
            "IncludeSpellingSuggestions", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(default(bool), IncludeSpellingSuggestionsChanged));

        public static void SetIncludeSpellingSuggestions(TextBoxBase element, bool value)
        {
            element.SetValue(IncludeSpellingSuggestionsProperty, value);
        }

        public static bool GetIncludeSpellingSuggestions(TextBoxBase element)
        {
            return (bool)element.GetValue(IncludeSpellingSuggestionsProperty);
        }

        private static void IncludeSpellingSuggestionsChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            var textBox = element as TextBoxBase;
            if (textBox != null)
            {
                if ((bool)e.NewValue)
                {
                    textBox.ContextMenuOpening += TextBoxOnContextMenuOpening;
                    textBox.ContextMenuClosing += TextBoxOnContextMenuClosing;
                }
                else
                {
                    textBox.ContextMenuOpening -= TextBoxOnContextMenuOpening;
                    textBox.ContextMenuClosing -= TextBoxOnContextMenuClosing;
                }
            }
        }

        private static void TextBoxOnContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var textBoxBase = sender as TextBoxBase;

            ContextMenu contextMenu = textBoxBase?.ContextMenu;
            if (contextMenu == null) return;
            
            RemoveSpellingSuggestions(contextMenu);

            if (!SpellCheck.GetIsEnabled(textBoxBase)) return;

            SpellingError spellingError = GetSpellingError(textBoxBase);
            if (spellingError != null)
            {
                int insertionIndex = 0;
                bool hasSuggestion = false;
                foreach (string suggestion in spellingError.Suggestions)
                {
                    hasSuggestion = true;
                    var menuItem = new SpellingSuggestionMenuItem(suggestion)
                    {
                        CommandTarget = textBoxBase
                    };
                    contextMenu.Items.Insert(insertionIndex++, menuItem);
                }
                if (!hasSuggestion)
                {
                    contextMenu.Items.Insert(insertionIndex++, new SpellingNoSuggestionsMenuItem());
                }

                contextMenu.Items.Insert(insertionIndex++, new Separator { Tag = typeof(SpellingSuggestionMenuItem) });

                contextMenu.Items.Insert(insertionIndex++, new SpellingIgnoreAllMenuItem
                {
                    CommandTarget = textBoxBase
                });

                contextMenu.Items.Insert(insertionIndex, new Separator { Tag = typeof(SpellingSuggestionMenuItem) });
            }
        }

        private static SpellingError GetSpellingError(TextBoxBase textBoxBase)
        {
            var textBox = textBoxBase as TextBox;
            if (textBox != null)
            {
                return textBox.GetSpellingError(textBox.CaretIndex);
            }
            var richTextBox = textBoxBase as RichTextBox;
            if (richTextBox != null)
            {
                return richTextBox.GetSpellingError(richTextBox.CaretPosition);
            }
            return null;
        }

        private static void TextBoxOnContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            var contextMenu = (sender as TextBoxBase)?.ContextMenu;
            if (contextMenu != null)
            {
                RemoveSpellingSuggestions(contextMenu);
            }
        }

        private static void RemoveSpellingSuggestions(ContextMenu menu)
        {
            foreach (object item in (from item in menu.Items.OfType<object>()
                                     let separator = item as Separator
                                     where item is SpellingSuggestionMenuItem ||
                                           item is SpellingIgnoreAllMenuItem ||
                                           item is SpellingNoSuggestionsMenuItem ||
                                           ReferenceEquals(separator?.Tag, typeof(SpellingSuggestionMenuItem))
                                     select item).ToList())
            {
                menu.Items.Remove(item);
            }
        }

        #region Methods

        /// <summary>
        /// Applies the text box view margin.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="margin">The margin.</param>
        private static void ApplyTextBoxViewMargin(Control textBox, Thickness margin)
        {
            if (margin.Equals(new Thickness(double.NegativeInfinity)))
            {
                return;
            }

            var frameworkElement = (textBox.Template.FindName("PART_ContentHost", textBox) as ScrollViewer)?.Content as FrameworkElement;
            if (frameworkElement != null)
            {
                frameworkElement.Margin = margin;
            }
        }

        /// <summary>
        /// The text box view margin property changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyPropertyChangedEventArgs">The dependency property changed event args.</param>
        private static void TextBoxViewMarginPropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var box = dependencyObject as Control; //could be a text box or password box
            if (box == null)
            {
                return;
            }

            if (box.IsLoaded)
            {
                ApplyTextBoxViewMargin(box, (Thickness)dependencyPropertyChangedEventArgs.NewValue);
            }

            box.Loaded += (sender, args) =>
            {
                var textBox = (Control)sender;
                ApplyTextBoxViewMargin(textBox, GetTextBoxViewMargin(textBox));
            };
        }

        #endregion
    }
}
