namespace MaterialDesignThemes.Wpf;

[TemplatePart(Name = RichTextBoxPart, Type = typeof(RichTextBox))]
public class ChipsTextBox : TextBox
{
    private const string RichTextBoxPart = "PART_RichTextBox";

    private RichTextBox? _richTextBox;

    static ChipsTextBox()
        => DefaultStyleKeyProperty.OverrideMetadata(typeof(ChipsTextBox), new FrameworkPropertyMetadata(typeof(ChipsTextBox)));

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (GetTemplateChild(RichTextBoxPart) is RichTextBox richTextBox)
        {
            _richTextBox = richTextBox;
            _richTextBox.PreviewTextInput += RichTextBoxOnPreviewTextInput;
            DataObject.AddPastingHandler(_richTextBox, RichTextBoxOnPasting);
            _richTextBox.TextChanged += RichTextBoxOnTextChanged;
        }
    }

    private void RichTextBoxOnPasting(object sender, DataObjectPastingEventArgs e)
    {
        if (!Equals(DataFormats.UnicodeText, e.FormatToApply))
        {
            e.Handled = true;
            e.CancelCommand();
        }
    }

    private void RichTextBoxOnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        
    }


    private void RichTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        
    }
}
