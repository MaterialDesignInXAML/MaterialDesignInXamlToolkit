﻿using System.ComponentModel;
using System.Windows.Media;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class TreeViewTests
    {
        [StaFact]
        [Description("Issue 2135")]
        public void TreeViewItemBackgroundShouldBeInherited()
        {
            var expectedBackgroundBrush = new SolidColorBrush(Colors.HotPink);
            var item = new TreeViewItem { Header = "Test" };
            item.ApplyDefaultStyle();

            item.Background = expectedBackgroundBrush;

            var contentGrid = item.FindVisualChild<Grid>("ContentGrid");
            Assert.Equal(expectedBackgroundBrush, contentGrid.Background);
        }
    }
}
