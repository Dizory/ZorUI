using ZorUI.Core;
using ZorUI.Core.Properties;

namespace ZorUI.Core.Performance;

/// <summary>
/// Virtual scrolling container that only renders visible items for better performance.
/// </summary>
/// <typeparam name="T">Type of items to display</typeparam>
public class VirtualScrollView<T> : ZorComponent
{
    private List<T> _items = new();
    private int _visibleStart = 0;
    private int _visibleCount = 20;
    private double _itemHeight = 40;
    private double _scrollPosition = 0;
    private double _containerHeight = 600;

    public List<T> Items
    {
        get => _items;
        set
        {
            _items = value;
            UpdateVisibleRange();
        }
    }

    public double ItemHeight
    {
        get => _itemHeight;
        set
        {
            _itemHeight = value;
            UpdateVisibleRange();
        }
    }

    public double ContainerHeight
    {
        get => _containerHeight;
        set
        {
            _containerHeight = value;
            UpdateVisibleRange();
        }
    }

    public Func<T, ZorElement>? ItemTemplate { get; set; }

    public override ZorElement Build()
    {
        var visibleItems = GetVisibleItems();
        var totalHeight = _items.Count * _itemHeight;
        var offsetTop = _visibleStart * _itemHeight;

        var container = new ZorElement
        {
            ElementType = "VirtualScrollContainer"
        };

        container.Metadata["TotalHeight"] = totalHeight;
        container.Metadata["OffsetTop"] = offsetTop;
        container.Metadata["OnScroll"] = new Action<double>(HandleScroll);

        foreach (var item in visibleItems)
        {
            if (ItemTemplate != null)
            {
                container.AddChild(ItemTemplate(item));
            }
        }

        return container;
    }

    private List<T> GetVisibleItems()
    {
        var start = Math.Max(0, _visibleStart);
        var end = Math.Min(_items.Count, _visibleStart + _visibleCount);
        return _items.GetRange(start, end - start);
    }

    private void UpdateVisibleRange()
    {
        var itemsPerView = (int)Math.Ceiling(_containerHeight / _itemHeight);
        _visibleCount = itemsPerView + 5; // Buffer
        _visibleStart = (int)Math.Floor(_scrollPosition / _itemHeight);
    }

    private void HandleScroll(double position)
    {
        _scrollPosition = position;
        UpdateVisibleRange();
        MarkNeedsRebuild();
    }

    // Fluent API

    public VirtualScrollView<T> WithItems(List<T> items)
    {
        Items = items;
        return this;
    }

    public VirtualScrollView<T> WithItemTemplate(Func<T, ZorElement> template)
    {
        ItemTemplate = template;
        return this;
    }

    public VirtualScrollView<T> WithItemHeight(double height)
    {
        ItemHeight = height;
        return this;
    }

    public VirtualScrollView<T> WithContainerHeight(double height)
    {
        ContainerHeight = height;
        return this;
    }
}
