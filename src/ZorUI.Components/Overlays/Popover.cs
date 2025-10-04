using ZorUI.Core;
using ZorUI.Components.Layout;

namespace ZorUI.Components.Overlays;

/// <summary>
/// Popover component for displaying floating content.
/// Similar to Radix UI Popover or Bootstrap Popover.
/// </summary>
public class Popover : Container
{
    /// <summary>
    /// Whether the popover is currently open.
    /// </summary>
    public bool IsOpen { get; set; } = false;

    /// <summary>
    /// Element that triggers the popover.
    /// </summary>
    public ZorElement? Trigger { get; set; }

    /// <summary>
    /// Content to display in the popover.
    /// </summary>
    public ZorElement? Content { get; set; }

    /// <summary>
    /// Placement of the popover relative to the trigger.
    /// </summary>
    public PopoverPlacement Placement { get; set; } = PopoverPlacement.Bottom;

    /// <summary>
    /// Whether clicking outside should close the popover.
    /// </summary>
    public bool CloseOnOutsideClick { get; set; } = true;

    /// <summary>
    /// Whether the popover should close when content is clicked.
    /// </summary>
    public bool CloseOnContentClick { get; set; } = false;

    /// <summary>
    /// Offset from the trigger element in pixels.
    /// </summary>
    public double Offset { get; set; } = 8;

    /// <summary>
    /// Called when the popover opens.
    /// </summary>
    public Action? OnOpen { get; set; }

    /// <summary>
    /// Called when the popover closes.
    /// </summary>
    public Action? OnClose { get; set; }

    /// <summary>
    /// Creates a new popover.
    /// </summary>
    public Popover()
    {
    }

    // Fluent API

    public Popover WithTrigger(ZorElement trigger)
    {
        Trigger = trigger;
        return this;
    }

    public Popover WithContent(ZorElement content)
    {
        Content = content;
        return this;
    }

    public Popover WithPlacement(PopoverPlacement placement)
    {
        Placement = placement;
        return this;
    }

    public Popover WithOffset(double offset)
    {
        Offset = offset;
        return this;
    }

    public Popover WithCloseOnOutsideClick(bool close = true)
    {
        CloseOnOutsideClick = close;
        return this;
    }

    public Popover WithCloseOnContentClick(bool close = true)
    {
        CloseOnContentClick = close;
        return this;
    }

    public Popover WithOnOpen(Action onOpen)
    {
        OnOpen = onOpen;
        return this;
    }

    public Popover WithOnClose(Action onClose)
    {
        OnClose = onClose;
        return this;
    }

    /// <summary>
    /// Opens the popover.
    /// </summary>
    public void Open()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            OnOpen?.Invoke();
            MarkNeedsRebuild();
        }
    }

    /// <summary>
    /// Closes the popover.
    /// </summary>
    public void Close()
    {
        if (IsOpen)
        {
            IsOpen = false;
            OnClose?.Invoke();
            MarkNeedsRebuild();
        }
    }

    /// <summary>
    /// Toggles the popover.
    /// </summary>
    public void Toggle()
    {
        if (IsOpen)
            Close();
        else
            Open();
    }
}

/// <summary>
/// Popover placement options.
/// </summary>
public enum PopoverPlacement
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
