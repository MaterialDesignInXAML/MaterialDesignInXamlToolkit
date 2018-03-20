using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

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
            "DecorationVisibility", typeof(Visibility), typeof(TextFieldAssist), new PropertyMetadata(default(Visibility)));

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
        /// Controls the visbility of the text field box.
        /// </summary>
        public static readonly DependencyProperty HasTextFieldBoxProperty = DependencyProperty.RegisterAttached(
            "HasTextFieldBox", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(false));

        public static void SetHasTextFieldBox(DependencyObject element, bool value)
        {
            element.SetValue(HasTextFieldBoxProperty, value);
        }

        public static bool GetHasTextFieldBox(DependencyObject element)
        {
            return (bool)element.GetValue(HasTextFieldBoxProperty);
        }

        /// <summary>
        /// Controls the visibility of the text field area box.
        /// </summary>
        public static readonly DependencyProperty HasTextAreaBoxProperty = DependencyProperty.RegisterAttached(
            "HasTextAreaBox", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(false));

        public static void SetHasTextAreaBox(DependencyObject element, bool value)
        {
            element.SetValue(HasTextAreaBoxProperty, value);
        }

        public static bool GetHasTextAreaBox(DependencyObject element)
        {
            return (bool)element.GetValue(HasTextAreaBoxProperty);
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
                Style spellingSuggestionStyle =
                    contextMenu.TryFindResource(Spelling.SuggestionMenuItemStyleKey) as Style;

                int insertionIndex = 0;
                bool hasSuggestion = false;
                foreach (string suggestion in spellingError.Suggestions)
                {
                    hasSuggestion = true;
                    var menuItem = new MenuItem
                    {
                        CommandTarget = textBoxBase,
                        Command = EditingCommands.CorrectSpellingError,
                        CommandParameter = suggestion,
                        Style = spellingSuggestionStyle,
                        Tag = typeof(Spelling)
                    };
                    contextMenu.Items.Insert(insertionIndex++, menuItem);
                }
                if (!hasSuggestion)
                {
                    contextMenu.Items.Insert(insertionIndex++, new MenuItem
                    {
                        Style = contextMenu.TryFindResource(Spelling.NoSuggestionsMenuItemStyleKey) as Style,
                        Tag = typeof(Spelling)
                    });
                }

                contextMenu.Items.Insert(insertionIndex++, new Separator
                {
                    Style = contextMenu.TryFindResource(Spelling.SeparatorStyleKey) as Style,
                    Tag = typeof(Spelling)
                });

                contextMenu.Items.Insert(insertionIndex++, new MenuItem
                {
                    Command = EditingCommands.IgnoreSpellingError,
                    CommandTarget = textBoxBase,
                    Style = contextMenu.TryFindResource(Spelling.IgnoreAllMenuItemStyleKey) as Style,
                    Tag = typeof(Spelling)
                });

                contextMenu.Items.Insert(insertionIndex, new Separator
                {
                    Style = contextMenu.TryFindResource(Spelling.SeparatorStyleKey) as Style,
                    Tag = typeof(Spelling)
                });
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
            foreach (FrameworkElement item in (from item in menu.Items.OfType<FrameworkElement>()
                                     where ReferenceEquals(item.Tag, typeof(Spelling))
                                     select item).ToList())
            {
                menu.Items.Remove(item);
            }
        }

        /// <summary>
        /// Controls the visbility of the clear button.
        /// </summary>
        public static readonly DependencyProperty HasClearButtonProperty = DependencyProperty.RegisterAttached(
            "HasClearButton", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(false, HasClearButtonChanged));

        private static void HasClearButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as Control; //could be a text box or password box
            if (box == null)
            {
                return;
            }

            if (box.IsLoaded)
                SetClearHandler(box);
            else
                box.Loaded += (sender, args) =>
                    SetClearHandler(box);

        }

        private static void SetClearHandler(Control box)
        {
            var bValue = GetHasClearButton(box);
            var clearButton = box.Template.FindName("PART_ClearButton", box) as Button;
            if (clearButton != null)
            {
                RoutedEventHandler handler = (sender, args) =>
                {
                    (box as TextBox)?.SetCurrentValue(TextBox.TextProperty, null);
                    (box as ComboBox)?.SetCurrentValue(ComboBox.TextProperty, null);
                };
                if (bValue)
                    clearButton.Click += handler;
                else
                    clearButton.Click -= handler;
            }
        }

        public static void SetHasClearButton(DependencyObject element, bool value)
        {
            element.SetValue(HasClearButtonProperty, value);
        }

        public static bool GetHasClearButton(DependencyObject element)
        {
            return (bool)element.GetValue(HasClearButtonProperty);
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
