using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

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
        /// Controls the visibility of the filled text field.
        /// </summary>
        public static readonly DependencyProperty HasFilledTextFieldProperty = DependencyProperty.RegisterAttached(
            "HasFilledTextField", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(false));

        public static void SetHasFilledTextField(DependencyObject element, bool value)
        {
            element.SetValue(HasFilledTextFieldProperty, value);
        }

        public static bool GetHasFilledTextField(DependencyObject element)
        {
            return (bool)element.GetValue(HasFilledTextFieldProperty);
        }

        /// <summary>
        /// Controls the visibility of the text field area box.
        /// </summary>
        public static readonly DependencyProperty HasOutlinedTextFieldProperty = DependencyProperty.RegisterAttached(
            "HasOutlinedTextField", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(false));

        public static void SetHasOutlinedTextField(DependencyObject element, bool value)
        {
            element.SetValue(HasOutlinedTextFieldProperty, value);
        }

        public static bool GetHasOutlinedTextField(DependencyObject element)
        {
            return (bool)element.GetValue(HasOutlinedTextFieldProperty);
        }

        /// <summary>
        /// Controls the corner radius of the surrounding box.
        /// </summary>
        public static readonly DependencyProperty TextFieldCornerRadiusProperty = DependencyProperty.RegisterAttached(
            "TextFieldCornerRadius", typeof(CornerRadius), typeof(TextFieldAssist), new PropertyMetadata(new CornerRadius(0.0)));

        public static void SetTextFieldCornerRadius(DependencyObject element, CornerRadius value)
        {
            element.SetValue(TextFieldCornerRadiusProperty, value);
        }

        public static CornerRadius GetTextFieldCornerRadius(DependencyObject element)
        {
            return (CornerRadius)element.GetValue(TextFieldCornerRadiusProperty);
        }

        /// <summary>
        /// Controls the corner radius of the bottom line of the surrounding box.
        /// </summary>
        public static readonly DependencyProperty UnderlineCornerRadiusProperty = DependencyProperty.RegisterAttached(
            "UnderlineCornerRadius", typeof(CornerRadius), typeof(TextFieldAssist), new PropertyMetadata(new CornerRadius(0.0)));

        public static void SetUnderlineCornerRadius(DependencyObject element, CornerRadius value)
        {
            element.SetValue(UnderlineCornerRadiusProperty, value);
        }

        public static CornerRadius GetUnderlineCornerRadius(DependencyObject element)
        {
            return (CornerRadius)element.GetValue(UnderlineCornerRadiusProperty);
        }

        /// <summary>
        /// Controls the highlighting style of a text box.
        /// </summary>
        public static readonly DependencyProperty NewSpecHighlightingEnabledProperty = DependencyProperty.RegisterAttached(
            "NewSpecHighlightingEnabled", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(false));

        public static void SetNewSpecHighlightingEnabled(DependencyObject element, bool value)
        {
            element.SetValue(NewSpecHighlightingEnabledProperty, value);
        }

        public static bool GetNewSpecHighlightingEnabled(DependencyObject element)
        {
            return (bool)element.GetValue(NewSpecHighlightingEnabledProperty);
        }

        /// <summary>
        /// Enables a ripple effect on focusing the text box.
        /// </summary>
        public static readonly DependencyProperty RippleOnFocusEnabledProperty = DependencyProperty.RegisterAttached(
            "RippleOnFocusEnabled", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(false));

        public static void SetRippleOnFocusEnabled(DependencyObject element, bool value)
        {
            element.SetValue(RippleOnFocusEnabledProperty, value);
        }

        public static bool GetRippleOnFocusEnabled(DependencyObject element)
        {
            return (bool)element.GetValue(RippleOnFocusEnabledProperty);
        }

        /// <summary>
        /// The color for highlighting effects on the border of a text box.
        /// </summary>
        public static readonly DependencyProperty UnderlineBrushProperty = DependencyProperty.RegisterAttached(
            "UnderlineBrush", typeof(Brush), typeof(TextFieldAssist), new PropertyMetadata(null));

        /// <summary>
        /// Sets the color for highlighting effects on the border of a text box.
        /// </summary>
        public static void SetUnderlineBrush(DependencyObject element, Brush value)
        {
            element.SetValue(UnderlineBrushProperty, value);
        }

        /// <summary>
        /// Gets the color for highlighting effects on the border of a text box.
        /// </summary>
        public static Brush GetUnderlineBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(UnderlineBrushProperty);
        }

        /// <summary>
        /// Automatically inserts spelling suggestions into the text box context menu.
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
            if (element is TextBoxBase textBox)
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
            if (textBoxBase is TextBox textBox)
            {
                return textBox.GetSpellingError(textBox.CaretIndex);
            }

            if (textBoxBase is RichTextBox richTextBox)
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

            if ((textBox.Template.FindName("PART_ContentHost", textBox) as ScrollViewer)?.Content is FrameworkElement frameworkElement)
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
            if (!(dependencyObject is Control box))
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
