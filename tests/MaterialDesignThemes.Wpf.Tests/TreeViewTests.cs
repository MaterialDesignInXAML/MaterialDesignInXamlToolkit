using System.ComponentModel;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Tests;

[TestExecutor<STAThreadExecutor>]
public class TreeViewTests
{
    [Test]
    [Description("Issue 2135")]
    public async Task TreeViewItemBackgroundShouldBeInherited()
    {
        var expectedBackgroundBrush = new SolidColorBrush(Colors.HotPink);
        var item = new TreeViewItem { Header = "Test" };
        item.ApplyDefaultStyle();

        item.Background = expectedBackgroundBrush;

        var contentGrid = item.FindVisualChild<Grid>("ContentGrid");
        await Assert.That(contentGrid.Background).IsEqualTo(expectedBackgroundBrush);
    }
}
