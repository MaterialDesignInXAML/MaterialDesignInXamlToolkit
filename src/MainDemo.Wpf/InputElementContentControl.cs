namespace MaterialDesignDemo;

/// <summary>
/// ContentControl variation simply used to capture the input elements in the "Smart Hint" demo page and apply some common properties.
/// </summary>
internal class InputElementContentControl : ContentControl
{
    public InputElementContentControl() => IsTabStop = false;
}
