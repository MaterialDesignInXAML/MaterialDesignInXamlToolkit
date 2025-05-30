using System.ComponentModel;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests;

public class TextBlockTests
{
    [Test, STAThreadExecutor]
    [Description("Issue 1301")]
    //[ClassData(typeof(AllStyles<TextBlock>))]
    public async Task DefaultVerticalAlignment_ShouldBeStretch()
    {
        //NB: Having trouble converting this to a theory
        //https://github.com/AArnott/Xunit.StaFact/issues/30
        foreach (var styleKey in MdixHelper.GetStyleKeysFor<TextBlock>())
        {
            var textBlock = new TextBlock();
            textBlock.ApplyStyle(styleKey, false);

            await Assert.That(textBlock.VerticalAlignment).IsEqualTo(VerticalAlignment.Stretch);
        }
    }
}
