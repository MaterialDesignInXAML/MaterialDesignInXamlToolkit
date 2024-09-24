using Microsoft.Xaml.Behaviors;

namespace MaterialDesignThemes.Wpf.Behaviors;

/// <summary>
/// Internal behavior exposing the <see cref="TextBox.LineCount"/> (non-DP) as an attached property which we can bind to
/// </summary>
internal class TextBoxLineCountBehavior : Behavior<TextBox>
{
    private void AssociatedObjectOnTextChanged(object sender, TextChangedEventArgs e)
        => AssociatedObject.SetCurrentValue(TextFieldAssist.LineCountProperty, AssociatedObject.LineCount);

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
    }

    protected override void OnDetaching()
    {
        if (AssociatedObject != null)
        {
            AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
        }
        base.OnDetaching();
    }
}
