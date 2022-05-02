using System;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public static class ComboBoxAssist
    {
        #region AttachedProperty : ClassicMode
        /// <summary>
        /// By default ComboBox uses the wrapper popup. Popup can be switched to classic Windows desktop view by means of this attached property.
        /// </summary>
        [Obsolete("ClassicMode is now obsolete and has no affect.")]
        public static readonly DependencyProperty ClassicModeProperty = DependencyProperty.RegisterAttached(
            "ClassicMode",
            typeof(bool),
            typeof(ComboBoxAssist),
            new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        [Obsolete("ClassicMode is now obsolete and has no affect.")]
        public static bool GetClassicMode(DependencyObject element)
            => (bool)element.GetValue(ClassicModeProperty);

        [Obsolete("ClassicMode is now obsolete and has no affect.")]
        public static void SetClassicMode(DependencyObject element, bool value)
            => element.SetValue(ClassicModeProperty, value);
        #endregion

        #region AttachedProperty : ShowSelectedItem
        /// <summary>
        /// By default the selected item is displayed in the drop down list, as per Material Design specifications.
        /// To change this to a behavior of hiding the selected item from the drop down list, set this attached property to false.
        /// </summary>
        public static readonly DependencyProperty ShowSelectedItemProperty = DependencyProperty.RegisterAttached(
            "ShowSelectedItem",
            typeof(bool),
            typeof(ComboBoxAssist),
            new FrameworkPropertyMetadata(true,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetShowSelectedItem(DependencyObject element)
            => (bool)element.GetValue(ShowSelectedItemProperty);

        public static void SetShowSelectedItem(DependencyObject element, bool value)
            => element.SetValue(ShowSelectedItemProperty, value);
        #endregion

        #region AttachedProperty : MaxLength
        /// <summary>
        /// Gets or sets the maximum number of characters that can be manually entered into the text box. <br />
        /// <remarks>
        /// <see cref="TextBox.MaxLength"/> cannot be set for an editable ComboBox. That's why this attached property exists.
        /// </remarks>
        /// </summary>
        public static readonly DependencyProperty MaxLengthProperty =
           DependencyProperty.RegisterAttached(
               name: "MaxLength",
               propertyType: typeof(int),
               ownerType: typeof(ComboBoxAssist),
               defaultMetadata: new FrameworkPropertyMetadata(
                   defaultValue: 0,
                   propertyChangedCallback: OnMaxLenghtChanged)
               );
        private static void OnMaxLenghtChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (obj is ComboBox comboBox)
            {
                comboBox.Loaded +=
                  (s, e) =>
                  {
                      DependencyObject? textBox = comboBox.FindChild<TextBox>("PART_EditableTextBox");
                      if (textBox == null)
                      {
                          return;
                      }

                      textBox.SetValue(TextBox.MaxLengthProperty, args.NewValue);
                  };
            }
        }
        public static int GetMaxLength(DependencyObject element) => (int)element.GetValue(MaxLengthProperty);
        public static void SetMaxLength(DependencyObject element, int value) => element.SetValue(MaxLengthProperty, value);
        #endregion
    }
}
