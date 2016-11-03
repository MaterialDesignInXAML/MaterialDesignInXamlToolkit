using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace VirtualCollection.VirtualCollection
{
    public class VirtualizingWrapPanel : VirtualizingPanel, IScrollInfo
    {
        private const double ScrollLineAmount = 16.0;

        private Size _extentSize;
        private Size _viewportSize;
        private Point _offset;
        private ItemsControl _itemsControl;
        private readonly Dictionary<UIElement, Rect> _childLayouts = new Dictionary<UIElement, Rect>();

        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register(nameof(ItemWidth), typeof(double), typeof(VirtualizingWrapPanel), new PropertyMetadata(1.0, HandleItemDimensionChanged));

        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(VirtualizingWrapPanel), new PropertyMetadata(1.0, HandleItemDimensionChanged));

        private static readonly DependencyProperty VirtualItemIndexProperty =
            DependencyProperty.RegisterAttached("VirtualItemIndex", typeof(int), typeof(VirtualizingWrapPanel), new PropertyMetadata(-1));
        private IRecyclingItemContainerGenerator _itemsGenerator;

        private bool _isInMeasure;

        private static int GetVirtualItemIndex(DependencyObject obj)
        {
            return (int)obj.GetValue(VirtualItemIndexProperty);
        }

        private static void SetVirtualItemIndex(DependencyObject obj, int value)
        {
            obj.SetValue(VirtualItemIndexProperty, value);
        }

        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        public VirtualizingWrapPanel()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                Dispatcher.BeginInvoke(new Action(Initialize));
            }
        }

        private void Initialize()
        {
            _itemsControl = ItemsControl.GetItemsOwner(this);
            _itemsGenerator = (IRecyclingItemContainerGenerator)ItemContainerGenerator;

            InvalidateMeasure();
        }

        protected override void OnItemsChanged(object sender, ItemsChangedEventArgs args)
        {
            base.OnItemsChanged(sender, args);

            InvalidateMeasure();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (_itemsControl == null)
            {
                return new Size(double.IsInfinity(availableSize.Width) ? 0 : availableSize.Width,
                    double.IsInfinity(availableSize.Height) ? 0 : availableSize.Height);
            }

            _isInMeasure = true;
            _childLayouts.Clear();

            var extentInfo = GetExtentInfo(availableSize, ItemHeight);

            EnsureScrollOffsetIsWithinConstrains(extentInfo);

            var layoutInfo = GetLayoutInfo(availableSize, ItemHeight, extentInfo);

            RecycleItems(layoutInfo);

            // Determine where the first item is in relation to previously realized items
            var generatorStartPosition = _itemsGenerator.GeneratorPositionFromIndex(layoutInfo.FirstRealizedItemIndex);

            var visualIndex = 0;

            var currentX = layoutInfo.FirstRealizedItemLeft;
            var currentY = layoutInfo.FirstRealizedLineTop;

            using (_itemsGenerator.StartAt(generatorStartPosition, GeneratorDirection.Forward, true))
            {
                for (var itemIndex = layoutInfo.FirstRealizedItemIndex; itemIndex <= layoutInfo.LastRealizedItemIndex; itemIndex++, visualIndex++)
                {
                    bool newlyRealized;

                    var child = (UIElement)_itemsGenerator.GenerateNext(out newlyRealized);
                    SetVirtualItemIndex(child, itemIndex);

                    if (newlyRealized)
                    {
                        InsertInternalChild(visualIndex, child);
                    }
                    else
                    {
                        // check if item needs to be moved into a new position in the Children collection
                        if (visualIndex < Children.Count)
                        {
                            if (Children[visualIndex] != child)
                            {
                                var childCurrentIndex = Children.IndexOf(child);

                                if (childCurrentIndex >= 0)
                                {
                                    RemoveInternalChildRange(childCurrentIndex, 1);
                                }

                                InsertInternalChild(visualIndex, child);
                            }
                        }
                        else
                        {
                            // we know that the child can't already be in the children collection
                            // because we've been inserting children in correct visualIndex order,
                            // and this child has a visualIndex greater than the Children.Count
                            AddInternalChild(child);
                        }
                    }

                    // only prepare the item once it has been added to the visual tree
                    _itemsGenerator.PrepareItemContainer(child);

                    child.Measure(new Size(ItemWidth, ItemHeight));

                    _childLayouts.Add(child, new Rect(currentX, currentY, ItemWidth, ItemHeight));

                    if (currentX + ItemWidth * 2 >= availableSize.Width)
                    {
                        // wrap to a new line
                        currentY += ItemHeight;
                        currentX = 0;
                    }
                    else
                    {
                        currentX += ItemWidth;
                    }
                }
            }

            RemoveRedundantChildren();
            UpdateScrollInfo(availableSize, extentInfo);

            var desiredSize = new Size(double.IsInfinity(availableSize.Width) ? 0 : availableSize.Width,
                                       double.IsInfinity(availableSize.Height) ? 0 : availableSize.Height);

            _isInMeasure = false;

            return desiredSize;
        }

        private void EnsureScrollOffsetIsWithinConstrains(ExtentInfo extentInfo)
        {
            _offset.Y = Clamp(_offset.Y, 0, extentInfo.MaxVerticalOffset);
        }

        private void RecycleItems(ItemLayoutInfo layoutInfo)
        {
            foreach (var child in Children.OfType<UIElement>())
            {
                var virtualItemIndex = GetVirtualItemIndex(child);

                if (virtualItemIndex < layoutInfo.FirstRealizedItemIndex || virtualItemIndex > layoutInfo.LastRealizedItemIndex)
                {
                    var generatorPosition = _itemsGenerator.GeneratorPositionFromIndex(virtualItemIndex);
                    if (generatorPosition.Index >= 0)
                    {
                        _itemsGenerator.Recycle(generatorPosition, 1);
                    }
                }

                SetVirtualItemIndex(child, -1);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (var child in Children.OfType<UIElement>())
            {
                child.Arrange(_childLayouts[child]);
            }

            return finalSize;
        }

        private void UpdateScrollInfo(Size availableSize, ExtentInfo extentInfo)
        {
            _viewportSize = availableSize;
            _extentSize = new Size(availableSize.Width, extentInfo.ExtentHeight);

            InvalidateScrollInfo();
        }

        private void RemoveRedundantChildren()
        {
            // iterate backwards through the child collection because we're going to be
            // removing items from it
            for (var i = Children.Count - 1; i >= 0; i--)
            {
                var child = Children[i];

                // if the virtual item index is -1, this indicates
                // it is a recycled item that hasn't been reused this time round
                if (GetVirtualItemIndex(child) == -1)
                {
                    RemoveInternalChildRange(i, 1);
                }
            }
        }

        private ItemLayoutInfo GetLayoutInfo(Size availableSize, double itemHeight, ExtentInfo extentInfo)
        {
            if (_itemsControl == null)
            {
                return new ItemLayoutInfo();
            }

            // we need to ensure that there is one realized item prior to the first visible item, and one after the last visible item,
            // so that keyboard navigation works properly. For example, when focus is on the first visible item, and the user
            // navigates up, the ListBox selects the previous item, and the scrolls that into view - and this triggers the loading of the rest of the items 
            // in that row

            var firstVisibleLine = (int)Math.Floor(VerticalOffset / itemHeight);

            var firstRealizedIndex = Math.Max(extentInfo.ItemsPerLine * firstVisibleLine - 1, 0);
            var firstRealizedItemLeft = firstRealizedIndex % extentInfo.ItemsPerLine * ItemWidth - HorizontalOffset;
            var firstRealizedItemTop = (firstRealizedIndex / extentInfo.ItemsPerLine) * itemHeight - VerticalOffset;

            var firstCompleteLineTop = (firstVisibleLine == 0 ? firstRealizedItemTop : firstRealizedItemTop + ItemHeight);
            var completeRealizedLines = (int)Math.Ceiling((availableSize.Height - firstCompleteLineTop) / itemHeight);

            var lastRealizedIndex = Math.Min(firstRealizedIndex + completeRealizedLines * extentInfo.ItemsPerLine + 2, _itemsControl.Items.Count - 1);

            return new ItemLayoutInfo
            {
                FirstRealizedItemIndex = firstRealizedIndex,
                FirstRealizedItemLeft = firstRealizedItemLeft,
                FirstRealizedLineTop = firstRealizedItemTop,
                LastRealizedItemIndex = lastRealizedIndex,
            };
        }

        private ExtentInfo GetExtentInfo(Size viewPortSize, double itemHeight)
        {
            if (_itemsControl == null)
            {
                return new ExtentInfo();
            }

            var itemsPerLine = Math.Max((int)Math.Floor(viewPortSize.Width / ItemWidth), 1);
            var totalLines = (int)Math.Ceiling((double)_itemsControl.Items.Count / itemsPerLine);
            var extentHeight = Math.Max(totalLines * ItemHeight, viewPortSize.Height);

            return new ExtentInfo()
            {
                ItemsPerLine = itemsPerLine,
                TotalLines = totalLines,
                ExtentHeight = extentHeight,
                MaxVerticalOffset = extentHeight - viewPortSize.Height,
            };
        }

        public void LineUp()
        {
            SetVerticalOffset(VerticalOffset - ScrollLineAmount);
        }

        public void LineDown()
        {
            SetVerticalOffset(VerticalOffset + ScrollLineAmount);
        }

        public void LineLeft()
        {
            SetHorizontalOffset(HorizontalOffset + ScrollLineAmount);
        }

        public void LineRight()
        {
            SetHorizontalOffset(HorizontalOffset - ScrollLineAmount);
        }

        public void PageUp()
        {
            SetVerticalOffset(VerticalOffset - ViewportHeight);
        }

        public void PageDown()
        {
            SetVerticalOffset(VerticalOffset + ViewportHeight);
        }

        public void PageLeft()
        {
            SetHorizontalOffset(HorizontalOffset + ItemWidth);
        }

        public void PageRight()
        {
            SetHorizontalOffset(HorizontalOffset - ItemWidth);
        }

        public void MouseWheelUp()
        {
            SetVerticalOffset(VerticalOffset - ScrollLineAmount * SystemParameters.WheelScrollLines);
        }

        public void MouseWheelDown()
        {
            SetVerticalOffset(VerticalOffset + ScrollLineAmount * SystemParameters.WheelScrollLines);
        }

        public void MouseWheelLeft()
        {
            SetHorizontalOffset(HorizontalOffset - ScrollLineAmount * SystemParameters.WheelScrollLines);
        }

        public void MouseWheelRight()
        {
            SetHorizontalOffset(HorizontalOffset + ScrollLineAmount * SystemParameters.WheelScrollLines);
        }

        public void SetHorizontalOffset(double offset)
        {
            if (_isInMeasure)
            {
                return;
            }

            offset = Clamp(offset, 0, ExtentWidth - ViewportWidth);
            _offset = new Point(offset, _offset.Y);

            InvalidateScrollInfo();
            InvalidateMeasure();
        }

        public void SetVerticalOffset(double offset)
        {
            if (_isInMeasure)
            {
                return;
            }

            offset = Clamp(offset, 0, ExtentHeight - ViewportHeight);
            _offset = new Point(_offset.X, offset);

            InvalidateScrollInfo();
            InvalidateMeasure();
        }

        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            return new Rect();
        }

        public Rect MakeVisible(UIElement visual, Rect rectangle)
        {
            return new Rect();
        }

        public ItemLayoutInfo GetVisibleItemsRange()
        {
            return GetLayoutInfo(_viewportSize, ItemHeight, GetExtentInfo(_viewportSize, ItemHeight));
        }

        public bool CanVerticallyScroll
        {
            get;
            set;
        }

        public bool CanHorizontallyScroll
        {
            get;
            set;
        }

        public double ExtentWidth
        {
            get { return _extentSize.Width; }
        }

        public double ExtentHeight
        {
            get { return _extentSize.Height; }
        }

        public double ViewportWidth
        {
            get { return _viewportSize.Width; }
        }

        public double ViewportHeight
        {
            get { return _viewportSize.Height; }
        }

        public double HorizontalOffset
        {
            get { return _offset.X; }
        }

        public double VerticalOffset
        {
            get { return _offset.Y; }
        }

        public ScrollViewer ScrollOwner
        {
            get;
            set;
        }

        private void InvalidateScrollInfo()
        {
            if (ScrollOwner != null)
            {
                ScrollOwner.InvalidateScrollInfo();
            }
        }

        private static void HandleItemDimensionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapPanel = (d as VirtualizingWrapPanel);

            wrapPanel.InvalidateMeasure();
        }


        private double Clamp(double value, double min, double max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        internal class ExtentInfo
        {
            public int ItemsPerLine;
            public int TotalLines;
            public double ExtentHeight;
            public double MaxVerticalOffset;
        }
    }

    public class ItemLayoutInfo
    {
        public int FirstRealizedItemIndex;
        public double FirstRealizedLineTop;
        public double FirstRealizedItemLeft;
        public int LastRealizedItemIndex;
    }
}
