using ZorUI.Core;
using ZorUI.Components.Primitives;

namespace ZorUI.Components.Overlays;

/// <summary>
/// Tooltip component for displaying helpful text on hover.
/// Similar to Radix UI Tooltip or HTML title attribute.
/// </summary>
public class Tooltip : ZorElement
{
    /// <summary>
    /// Element that triggers the tooltip on hover.
    /// </summary>
    public ZorElement? Trigger { get; set; }

    /// <summary>
    /// Tooltip content (text or custom element).
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Placement of the tooltip relative to the trigger.
    /// </summary>
    public TooltipPlacement Placement { get; set; } = TooltipPlacement.Top;

    /// <summary>
    /// Delay before showing the tooltip (in milliseconds).
    /// </summary>
    public int ShowDelay { get; set; } = 700;

    /// <summary>
    /// Delay before hiding the tooltip (in milliseconds).
    /// </summary>
    public int HideDelay { get; set; } = 0;

    /// <summary>
    /// Whether the tooltip is currently visible.
    /// </summary>
    public bool IsVisible { get; private set; } = false;

    /// <summary>
    /// Creates a new tooltip.
    /// </summary>
    public Tooltip(string content)
    {
        Content = content;
    }

    // Fluent API

    public Tooltip WithTrigger(ZorElement trigger)
    {
        Trigger = trigger;
        return this;
    }

    public Tooltip WithPlacement(TooltipPlacement placement)
    {
        Placement = placement;
        return this;
    }

    public Tooltip WithShowDelay(int delayMs)
    {
        ShowDelay = delayMs;
        return this;
    }

    public Tooltip WithHideDelay(int delayMs)
    {
        HideDelay = delayMs;
        return this;
    }

    /// <summary>
    /// Shows the tooltip.
    /// </summary>
    public void Show()
    {
        IsVisible = true;
        MarkNeedsRebuild();
    }

    /// <summary>
    /// Hides the tooltip.
    /// </summary>
    public void Hide()
    {
        IsVisible = false;
        MarkNeedsRebuild();
    }
}

/// <summary>
/// Tooltip placement options.
/// </summary>
public enum TooltipPlacement
{
    Top,
    TopStart,
    TopEnd,
    Bottom,
    BottomStart,
    BottomEnd,
    Left,
    LeftStart,
    LeftEnd,
    Right,
    RightStart,
    RightEnd
}
