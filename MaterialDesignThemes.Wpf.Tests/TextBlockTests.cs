using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class TextBlockTests
    {
        [StaFact]
        [Description("Issue 1301")]
        //[ClassData(typeof(AllStyles<TextBlock>))]
        public void DefaultVerticalAlignment_ShouldBeStretch()
        {
            //NB: Having trouble converting this to a theory
            //https://github.com/AArnott/Xunit.StaFact/issues/30
            foreach (var styleKey in MdixHelper.GetStyleKeysFor<TextBlock>())
            {
                var textBlock = new TextBlock();
                textBlock.ApplyStyle(styleKey, false);

                Assert.Equal(VerticalAlignment.Stretch, textBlock.VerticalAlignment);
            }
        }
    }
}