using System.ComponentModel;
using System.Windows.Media;

using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests;

public class TreeViewTests
{
    [Test, STAThreadExecutor]
    [Description("Issue 2135")]
    public async Task TreeViewItemBackgroundShouldBeInherited()
    {
        var expectedBackgroundBrush = new SolidColorBrush(Colors.HotPink);
        var item = new TreeViewItem { Header = "Test" };
        item.ApplyDefaultStyle();

        item.Background = expectedBackgroundBrush;

        var contentGrid = item.FindVisualChild<Grid>("ContentGrid");

        /* Unmerged change from project 'MaterialDesignThemes.Wpf.Tests(net8.0-windows)'
        Before:
                await Assert.That(contentGrid.Background).IsEqualTo(expectedBackgroundBrush);
        After:
                Assert.That(expectedBackgroundBrush, contentGrid.Background);
        */
        await Assert.That(contentGrid.Background).IsEqualTo(contentGrid.Background).IsEqualTo(expectedBackgroundBrush);
    }
}
