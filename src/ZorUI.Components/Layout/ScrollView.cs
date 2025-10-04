using ZorUI.Core;

namespace ZorUI.Components.Layout;

/// <summary>
/// Scrollable container for content that exceeds available space.
/// Similar to SwiftUI ScrollView or HTML overflow:scroll.
/// </summary>
public class ScrollView : Container
{
    /// <summary>
    /// Scroll direction.
    /// </summary>
    public ScrollDirection Direction { get; set; } = ScrollDirection.Vertical;

    /// <summary>
    /// Whether to show scrollbar.
    /// </summary>
    public bool ShowScrollbar { get; set; } = true;

    /// <summary>
    /// Whether scrolling is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Bounce effect at edges (iOS-style).
    /// </summary>
    public bool BounceEnabled { get; set; } = true;

    /// <summary>
    /// Current scroll position.
    /// </summary>
    public double ScrollPosition { get; set; } = 0;

    /// <summary>
    /// Called when scroll position changes.
    /// </summary>
    public Action<double>? OnScroll { get; set; }

    /// <summary>
    /// Called when scrolled to end.
    /// </summary>
    public Action? OnScrollEnd { get; set; }

    /// <summary>
    /// Creates a new scroll view.
    /// </summary>
    public ScrollView(params ZorElement[] children) : base(children)
    {
    }

    // Fluent API

    public ScrollView Horizontal()
    {
        Direction = ScrollDirection.Horizontal;
        return this;
    }

    public ScrollView Vertical()
    {
        Direction = ScrollDirection.Vertical;
        return this;
    }

    public ScrollView Both()
    {
        Direction = ScrollDirection.Both;
        return this;
    }

    public ScrollView WithShowScrollbar(bool show = true)
    {
        ShowScrollbar = show;
        return this;
    }

    public ScrollView WithBounce(bool bounce = true)
    {
        BounceEnabled = bounce;
        return this;
    }

    public ScrollView WithOnScroll(Action<double> onScroll)
    {
        OnScroll = onScroll;
        return this;
    }

    public ScrollView WithOnScrollEnd(Action onScrollEnd)
    {
        OnScrollEnd = onScrollEnd;
        return this;
    }

    /// <summary>
    /// Scrolls to a specific position.
    /// </summary>
    public void ScrollTo(double position, bool animated = true)
    {
        ScrollPosition = position;
        OnScroll?.Invoke(position);
        MarkNeedsRebuild();
    }

    /// <summary>
    /// Scrolls to the top/start.
    /// </summary>
    public void ScrollToTop(bool animated = true)
    {
        ScrollTo(0, animated);
    }

    /// <summary>
    /// Scrolls to the bottom/end.
    /// </summary>
    public void ScrollToBottom(bool animated = true)
    {
        // Implementation would depend on content size
        // This is a placeholder
        OnScrollEnd?.Invoke();
    }
}

/// <summary>
/// Scroll direction options.
/// </summary>
public enum ScrollDirection
{
    Vertical,
    Horizontal,
    Both
}
