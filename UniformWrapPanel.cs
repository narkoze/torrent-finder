using System;
using System.Windows;
using System.Windows.Controls;

namespace TM2
{
    public enum ItemSize
    {
        None,
        Uniform,
        UniformStretchToFit
    }

    public class UniformWrapPanel : WrapPanel
    {
        public static readonly DependencyProperty ItemSizeProperty =
            DependencyProperty.Register(
                "ItemSize",
                typeof(ItemSize),
                typeof(UniformWrapPanel),
                new FrameworkPropertyMetadata(
                    default(ItemSize),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    ItemSizeChanged));
        public ItemSize ItemSize
        {
            get { return (ItemSize)GetValue(ItemSizeProperty); }
            set { SetValue(ItemSizeProperty, value); }
        }
        private static void ItemSizeChanged(
            DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var uniformWrapPanel = sender as UniformWrapPanel;
            if (uniformWrapPanel != null)
            {
                if (uniformWrapPanel.Orientation == Orientation.Horizontal)
                {
                    uniformWrapPanel.ItemWidth = double.NaN;
                }
                else
                {
                    uniformWrapPanel.ItemHeight = double.NaN;
                }
            }
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            var mode = ItemSize;

            if (Children.Count > 0 && mode != ItemSize.None)
            {
                var stretchToFit = mode == ItemSize.UniformStretchToFit;

                if (Orientation == Orientation.Horizontal)
                {
                    var totalWidth = availableSize.Width;

                    ItemWidth = 0.0;
                    foreach (UIElement el in Children)
                    {
                        el.Measure(availableSize);
                        var next = el.DesiredSize;
                        if (!(double.IsInfinity(next.Width) || double.IsNaN(next.Width)))
                        {
                            ItemWidth = Math.Max(next.Width, ItemWidth);
                        }
                    }
                    if (stretchToFit)
                    {
                        if (!double.IsNaN(ItemWidth) && !double.IsInfinity(ItemWidth) && ItemWidth > 0)
                        {
                            var itemsPerRow = (int)(totalWidth / 154);
                            if (itemsPerRow > 0)
                            {
                                ItemWidth = totalWidth / itemsPerRow;
                            }
                        }
                    }
                }
                else
                {
                    var totalHeight = availableSize.Height;

                    ItemHeight = 0.0;
                    foreach (UIElement el in Children)
                    {
                        el.Measure(availableSize);
                        var next = el.DesiredSize;
                        if (!(double.IsInfinity(next.Height) || double.IsNaN(next.Height)))
                        {
                            ItemHeight = Math.Max(next.Height, ItemHeight);
                        }
                    }

                    if (stretchToFit)
                    {
                        if (!double.IsNaN(ItemHeight) && !double.IsInfinity(ItemHeight) && ItemHeight > 0)
                        {
                            var itemsPerColumn = (int)(totalHeight / ItemHeight);
                            if (itemsPerColumn > 0)
                            {
                                ItemHeight = totalHeight / itemsPerColumn;
                            }
                        }
                    }
                }
            }

            return base.MeasureOverride(availableSize);
        }
    }
}
