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
        public static void SetTextBoxViewMargin(DependencyObject element, Thickness value) => element.SetValue(TextBoxViewMarginProperty, value);

        /// <summary>
        /// Gets the text box view margin.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The <see cref="Thickness" />.
        /// </returns>
        public static Thickness GetTextBoxViewMargin(DependencyObject element) => (Thickness)element.GetValue(TextBoxViewMarginProperty);

        /// <summary>
        /// Controls the visibility of the underline decoration.
        /// </summary>
        public static readonly DependencyProperty DecorationVisibilityProperty = DependencyProperty.RegisterAttached(
            "DecorationVisibility", typeof(Visibility), typeof(TextFieldAssist), new PropertyMetadata(default(Visibility)));

        /// <summary>
        /// Controls the visibility of the underline decoration.
        /// </summary>
        public static void SetDecorationVisibility(DependencyObject element, Visibility value) => element.SetValue(DecorationVisibilityProperty, value);

        /// <summary>
        /// Controls the visibility of the underline decoration.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Visibility GetDecorationVisibility(DependencyObject element) => (Visibility)element.GetValue(DecorationVisibilityProperty);

        /// <summary>
        /// The attached WPF property for getting or setting the <see cref="Brush"/> value for an underline decoration.
        /// </summary>
        public static readonly DependencyProperty UnderlineBrushProperty = DependencyProperty.RegisterAttached(
            "UnderlineBrush", typeof(Brush), typeof(TextFieldAssist), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// Sets the <see cref="Brush"/> used for underline decoration.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetUnderlineBrush(DependencyObject element, Brush value) => element.SetValue(UnderlineBrushProperty, value);

        /// <summary>
        /// Gets the <see cref="Brush"/> used for underline decoration.
        /// </summary>
        /// <param name="element"></param>
        public static Brush GetUnderlineBrush(DependencyObject element) => (Brush)element.GetValue(UnderlineBrushProperty);

        /// <summary>
        /// Controls the visbility of the text field box.
        /// </summary>
        public static readonly DependencyProperty HasFilledTextFieldProperty = DependencyProperty.RegisterAttached(
            "HasFilledTextField", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(false));

        public static void SetHasFilledTextField(DependencyObject element, bool value) => element.SetValue(HasFilledTextFieldProperty, value);

        public static bool GetHasFilledTextField(DependencyObject element) => (bool)element.GetValue(HasFilledTextFieldProperty);

        /// <summary>
        /// Controls the visibility of the text field area box.
        /// </summary>
        public static readonly DependencyProperty HasOutlinedTextFieldProperty = DependencyProperty.RegisterAttached(
            "HasOutlinedTextField", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(false));

        public static void SetHasOutlinedTextField(DependencyObject element, bool value) => element.SetValue(HasOutlinedTextFieldProperty, value);

        public static bool GetHasOutlinedTextField(DependencyObject element) => (bool)element.GetValue(HasOutlinedTextFieldProperty);

        /// <summary>
        /// Controls the corner radius of the surrounding box.
        /// </summary>
        public static readonly DependencyProperty TextFieldCornerRadiusProperty = DependencyProperty.RegisterAttached(
            "TextFieldCornerRadius", typeof(CornerRadius), typeof(TextFieldAssist), new PropertyMetadata(new CornerRadius(0.0)));

        public static void SetTextFieldCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(TextFieldCornerRadiusProperty, value);

        public static CornerRadius GetTextFieldCornerRadius(DependencyObject element) => (CornerRadius)element.GetValue(TextFieldCornerRadiusProperty);

        /// <summary>
        /// Controls the corner radius of the bottom line of the surrounding box.
        /// </summary>
        public static readonly DependencyProperty UnderlineCornerRadiusProperty = DependencyProperty.RegisterAttached(
            "UnderlineCornerRadius", typeof(CornerRadius), typeof(TextFieldAssist), new PropertyMetadata(new CornerRadius(0.0)));

        public static void SetUnderlineCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(UnderlineCornerRadiusProperty, value);

        public static CornerRadius GetUnderlineCornerRadius(DependencyObject element) => (CornerRadius)element.GetValue(UnderlineCornerRadiusProperty);

        /// <summary>
        /// Controls the highlighting style of a text box.
        /// </summary>
        public static readonly DependencyProperty NewSpecHighlightingEnabledProperty = DependencyProperty.RegisterAttached(
            "NewSpecHighlightingEnabled", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(false));

        public static void SetNewSpecHighlightingEnabled(DependencyObject element, bool value) => element.SetValue(NewSpecHighlightingEnabledProperty, value);

        public static bool GetNewSpecHighlightingEnabled(DependencyObject element) => (bool)element.GetValue(NewSpecHighlightingEnabledProperty);

        /// <summary>
        /// Enables a ripple effect on focusing the text box.
        /// </summary>
        public static readonly DependencyProperty RippleOnFocusEnabledProperty = DependencyProperty.RegisterAttached(
            "RippleOnFocusEnabled", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(false));

        public static void SetRippleOnFocusEnabled(DependencyObject element, bool value) => element.SetValue(RippleOnFocusEnabledProperty, value);

        public static bool GetRippleOnFocusEnabled(DependencyObject element) => (bool)element.GetValue(RippleOnFocusEnabledProperty);

        /// <summary>
        /// Automatically inserts spelling suggestions into the text box context menu.
        /// </summary>
        public static readonly DependencyProperty IncludeSpellingSuggestionsProperty = DependencyProperty.RegisterAttached(
            "IncludeSpellingSuggestions", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(default(bool), IncludeSpellingSuggestionsChanged));

        public static void SetIncludeSpellingSuggestions(TextBoxBase element, bool value) => element.SetValue(IncludeSpellingSuggestionsProperty, value);

        public static bool GetIncludeSpellingSuggestions(TextBoxBase element) => (bool)element.GetValue(IncludeSpellingSuggestionsProperty);

        /// <summary>
        /// SuffixText dependency property
        /// </summary>
        public static readonly DependencyProperty SuffixTextProperty = DependencyProperty.RegisterAttached(
            "SuffixText", typeof(string), typeof(TextFieldAssist), new PropertyMetadata(default(string?)));

        public static void SetSuffixText(DependencyObject element, string? value)
            => element.SetValue(SuffixTextProperty, value);

        public static string? GetSuffixText(DependencyObject element)
            => (string?)element.GetValue(SuffixTextProperty);

        /// <summary>
        /// PrefixText dependency property
        /// </summary>
        public static readonly DependencyProperty PrefixTextProperty = DependencyProperty.RegisterAttached(
            "PrefixText", typeof(string), typeof(TextFieldAssist), new PropertyMetadata(default(string?)));

        public static void SetPrefixText(DependencyObject element, string? value)
            => element.SetValue(PrefixTextProperty, value);

        public static string? GetPrefixText(DependencyObject element)
            => (string?)element.GetValue(PrefixTextProperty);

        /// <summary>
        /// Controls the visbility of the clear button.
        /// </summary>
        public static readonly DependencyProperty HasClearButtonProperty = DependencyProperty.RegisterAttached(
            "HasClearButton", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(false));

        public static void SetHasClearButton(DependencyObject element, bool value)
            => element.SetValue(HasClearButtonProperty, value);

        public static bool GetHasClearButton(DependencyObject element)
            => (bool)element.GetValue(HasClearButtonProperty);

        /// <summary>
        /// Controls visibility of the leading icon
        /// </summary>
        public static readonly DependencyProperty HasLeadingIconProperty = DependencyProperty.RegisterAttached(
            "HasLeadingIcon", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(default(bool)));

        public static void SetHasLeadingIcon(DependencyObject element, bool value)
            => element.SetValue(HasLeadingIconProperty, value);

        public static bool GetHasLeadingIcon(DependencyObject element)
            => (bool)element.GetValue(HasLeadingIconProperty);

        /// <summary>
        /// Controls the leading icon
        /// </summary>
        public static readonly DependencyProperty LeadingIconProperty = DependencyProperty.RegisterAttached(
            "LeadingIcon", typeof(PackIconKind), typeof(TextFieldAssist), new PropertyMetadata());

        public static void SetLeadingIcon(DependencyObject element, PackIconKind value)
            => element.SetValue(LeadingIconProperty, value);

        public static PackIconKind GetLeadingIcon(DependencyObject element)
            => (PackIconKind)element.GetValue(LeadingIconProperty);

        /// <summary>
        /// Controls the size of the leading icon
        /// </summary>
        public static readonly DependencyProperty LeadingIconSizeProperty = DependencyProperty.RegisterAttached(
            "LeadingIconSize", typeof(double), typeof(TextFieldAssist), new PropertyMetadata(20.0));

        public static void SetLeadingIconSize(DependencyObject element, double value)
            => element.SetValue(LeadingIconSizeProperty, value);

        public static double GetLeadingIconSize(DependencyObject element)
            => (double)element.GetValue(LeadingIconSizeProperty);

        /// <summary>
        /// Controls visibility of the trailing icon
        /// </summary>
        public static readonly DependencyProperty HasTrailingIconProperty = DependencyProperty.RegisterAttached(
            "HasTrailingIcon", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(default(bool)));

        public static void SetHasTrailingIcon(DependencyObject element, bool value)
            => element.SetValue(HasTrailingIconProperty, value);

        public static bool GetHasTrailingIcon(DependencyObject element)
            => (bool)element.GetValue(HasTrailingIconProperty);

        /// <summary>
        /// Controls the trailing icon
        /// </summary>
        public static readonly DependencyProperty TrailingIconProperty = DependencyProperty.RegisterAttached(
            "TrailingIcon", typeof(PackIconKind), typeof(TextFieldAssist), new PropertyMetadata());

        public static void SetTrailingIcon(DependencyObject element, PackIconKind value)
            => element.SetValue(TrailingIconProperty, value);

        public static PackIconKind GetTrailingIcon(DependencyObject element)
            => (PackIconKind)element.GetValue(TrailingIconProperty);

        /// <summary>
        /// Controls the size of the trailing icon
        /// </summary>
        public static readonly DependencyProperty TrailingIconSizeProperty = DependencyProperty.RegisterAttached(
            "TrailingIconSize", typeof(double), typeof(TextFieldAssist), new PropertyMetadata(20.0));

        public static void SetTrailingIconSize(DependencyObject element, double value)
            => element.SetValue(TrailingIconSizeProperty, value);

        public static double GetTrailingIconSize(DependencyObject element)
            => (double)element.GetValue(TrailingIconSizeProperty);

        public static Style GetCharacterCounterStyle(DependencyObject obj) => (Style)obj.GetValue(CharacterCounterStyleProperty);

        public static void SetCharacterCounterStyle(DependencyObject obj, Style value) => obj.SetValue(CharacterCounterStyleProperty, value);

        public static readonly DependencyProperty CharacterCounterStyleProperty =
            DependencyProperty.RegisterAttached("CharacterCounterStyle", typeof(Style), typeof(TextFieldAssist), new PropertyMetadata(null));

        public static Visibility GetCharacterCounterVisibility(DependencyObject obj)
            => (Visibility)obj.GetValue(CharacterCounterVisibilityProperty);

        public static void SetCharacterCounterVisibility(DependencyObject obj, Visibility value)
            => obj.SetValue(CharacterCounterVisibilityProperty, value);

        public static readonly DependencyProperty CharacterCounterVisibilityProperty =
            DependencyProperty.RegisterAttached("CharacterCounterVisibility", typeof(Visibility), typeof(TextFieldAssist),
                new PropertyMetadata(Visibility.Visible));

        #region Methods

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

            ContextMenu? contextMenu = textBoxBase?.ContextMenu;
            if (contextMenu is null) return;

            RemoveSpellingSuggestions(contextMenu);

            if (!SpellCheck.GetIsEnabled(textBoxBase)) return;

            SpellingError? spellingError = GetSpellingError(textBoxBase);
            if (spellingError != null)
            {
                Style? spellingSuggestionStyle =
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

        private static SpellingError? GetSpellingError(TextBoxBase? textBoxBase)
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
            foreach (FrameworkElement item in
                (from item in menu.Items.OfType<FrameworkElement>()
                 where ReferenceEquals(item.Tag, typeof(Spelling))
                 select item).ToList())
            {
                menu.Items.Remove(item);
            }
        }

        /// <summary>
        /// Applies the text box view margin.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="margin">The margin.</param>
        private static void ApplyTextBoxViewMargin(Control textBox, Thickness margin)
        {
            if (margin.Equals(new Thickness(double.NegativeInfinity))
                || textBox.Template is null)
            {
                return;
            }

            if (textBox is ComboBox
                && textBox.Template.FindName("PART_EditableTextBox", textBox) is TextBox editableTextBox)
            {
                textBox = editableTextBox;
                if (textBox.Template is null) return;
                textBox.ApplyTemplate();
            }

            if (textBox.Template.FindName("PART_ContentHost", textBox) is ScrollViewer scrollViewer
                && scrollViewer.Content is FrameworkElement frameworkElement)
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
            if (dependencyObject is not Control box)
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