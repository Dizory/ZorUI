using ZorUI.Core;
using ZorUI.Core.Properties;

namespace ZorUI.Components.Layout;

/// <summary>
/// Layout that arranges children in a wrapping flow.
/// Similar to CSS flexbox with flex-wrap or SwiftUI's wrapping stack.
/// </summary>
public class Wrap : Container
{
    /// <summary>
    /// Spacing between items.
    /// </summary>
    public double Spacing { get; set; } = 0;

    /// <summary>
    /// Spacing between lines/rows.
    /// </summary>
    public double LineSpacing { get; set; } = 0;

    /// <summary>
    /// Alignment of items along the main axis.
    /// </summary>
    public WrapAlignment Alignment { get; set; } = WrapAlignment.Start;

    /// <summary>
    /// Direction of the wrap (horizontal or vertical).
    /// </summary>
    public Orientation Direction { get; set; } = Orientation.Horizontal;

    /// <summary>
    /// Creates a new wrap layout.
    /// </summary>
    public Wrap(double spacing = 0, params ZorElement[] children) : base(children)
    {
        Spacing = spacing;
    }

    // Fluent API

    public Wrap WithSpacing(double spacing)
    {
        Spacing = spacing;
        return this;
    }

    public Wrap WithLineSpacing(double lineSpacing)
    {
        LineSpacing = lineSpacing;
        return this;
    }

    public Wrap WithAlignment(WrapAlignment alignment)
    {
        Alignment = alignment;
        return this;
    }

    public Wrap Horizontal()
    {
        Direction = Orientation.Horizontal;
        return this;
    }

    public Wrap Vertical()
    {
        Direction = Orientation.Vertical;
        return this;
    }
}

/// <summary>
/// Wrap alignment options.
/// </summary>
public enum WrapAlignment
{
    Start,
    Center,
    End,
    SpaceBetween,
    SpaceAround,
    SpaceEvenly
}
