using System.Diagnostics;
using System.Windows.Documents;
using Microsoft.Xaml.Behaviors.Core;

namespace MaterialDesignThemes.Wpf;

[TemplatePart(Name = RichTextBoxPart, Type = typeof(RichTextBox))]
public class ChipsTextBox : TextBox
{
    private const string RichTextBoxPart = "PART_RichTextBox";

    private RichTextBox? _richTextBox;
    private bool _addingChip = false;

    static ChipsTextBox()
        => DefaultStyleKeyProperty.OverrideMetadata(typeof(ChipsTextBox), new FrameworkPropertyMetadata(typeof(ChipsTextBox)));

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (GetTemplateChild(RichTextBoxPart) is RichTextBox richTextBox)
        {
            _richTextBox = richTextBox;
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

    private void RichTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (_richTextBox is null || _addingChip)
            return;

        foreach (var change in e.Changes)
        {
            string? backText = _richTextBox.CaretPosition?.GetTextInRun(LogicalDirection.Backward);
            if (backText is not null && backText.EndsWith(" "))
            {
                string chipContent = backText.Trim();
                _addingChip = true;
                InlineUIContainer inlineContainer = new InlineUIContainer(ChipFactory(chipContent));
                if (_richTextBox.Document.Blocks.LastBlock is Paragraph p)
                {
                    _richTextBox.CaretPosition?.DeleteTextInRun(-backText.Length);
                    if (_richTextBox.CaretPosition?.Parent is Inline currentInline)
                    {
                        p.Inlines.InsertBefore(currentInline, inlineContainer);
                    }
                    else
                    {
                        p.Inlines.Add(inlineContainer);
                    }
                    _richTextBox.CaretPosition = inlineContainer.ElementEnd.GetNextInsertionPosition(LogicalDirection.Forward);
                }
                _addingChip = false;
            }
        }
    }

    private static Chip ChipFactory(string text) =>
        new()
        {
            Content = text,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Height = 26,
            Margin = new Thickness(0),

            /* TODO: The delete button is currently not interactive?! Does not respond to MouseEnter/Leave nor does the DeleteCommand fire. How do we fix that?... */
            IsDeletable = true,
            IsHitTestVisible = true,
            DeleteCommand = new ActionCommand(() =>
            {
                Debug.WriteLine("Delete chip command fired");
            })
        };
}
