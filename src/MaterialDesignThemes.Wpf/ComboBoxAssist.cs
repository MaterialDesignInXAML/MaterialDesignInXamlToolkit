namespace MaterialDesignThemes.Wpf;

public static class ComboBoxAssist
{
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
           defaultMetadata: new FrameworkPropertyMetadata(defaultValue: 0)
           );
    public static int GetMaxLength(DependencyObject element) => (int)element.GetValue(MaxLengthProperty);
    public static void SetMaxLength(DependencyObject element, int value) => element.SetValue(MaxLengthProperty, value);
    #endregion

    #region AttachedProperty : CustomPopupPlacementCallback
    public static readonly DependencyProperty CustomPopupPlacementCallbackProperty =
        DependencyProperty.RegisterAttached(
            "CustomPopupPlacementCallback",
            typeof(CustomPopupPlacementCallback),
            typeof(ComboBoxAssist),
            new FrameworkPropertyMetadata(default(CustomPopupPlacementCallback),
                FrameworkPropertyMetadataOptions.AffectsRender));

    public static void SetCustomPopupPlacementCallback(DependencyObject element, CustomPopupPlacementCallback value) => element.SetValue(CustomPopupPlacementCallbackProperty, value);
    public static CustomPopupPlacementCallback GetCustomPopupPlacementCallback(DependencyObject element) => (CustomPopupPlacementCallback) element.GetValue(CustomPopupPlacementCallbackProperty);
    #endregion
}
