using ZorUI.Core;
using ZorUI.Core.Properties;

namespace ZorUI.Components.Layout;

/// <summary>
/// Horizontal stack layout that arranges children horizontally.
/// Similar to SwiftUI HStack or CSS flexbox row.
/// </summary>
public class HStack : Container
{
    /// <summary>
    /// Spacing between children.
    /// </summary>
    public double Spacing { get; set; } = 0;

    /// <summary>
    /// Vertical alignment of children.
    /// </summary>
    public VerticalAlignment Alignment { get; set; } = VerticalAlignment.Center;

    /// <summary>
    /// Creates a new horizontal stack with optional spacing and children.
    /// </summary>
    public HStack(double spacing = 0, params ZorElement[] children) : base(children)
    {
        Spacing = spacing;
    }

    /// <summary>
    /// Fluent API: Sets the spacing between children.
    /// </summary>
    public HStack WithSpacing(double spacing)
    {
        Spacing = spacing;
        return this;
    }

    /// <summary>
    /// Fluent API: Sets the vertical alignment.
    /// </summary>
    public HStack WithAlignment(VerticalAlignment alignment)
    {
        Alignment = alignment;
        return this;
    }

    /// <summary>
    /// Fluent API: Adds a child element.
    /// </summary>
    public new HStack AddChild(ZorElement child)
    {
        base.AddChild(child);
        return this;
    }
}

/// <summary>
/// Helper class for creating HStack with fluent syntax.
/// </summary>
public static class HStackBuilder
{
    /// <summary>
    /// Creates an HStack with a builder pattern.
    /// </summary>
    public static HStack Create(double spacing = 0, VerticalAlignment alignment = VerticalAlignment.Center,
                                Action<HStack>? configure = null)
    {
        var stack = new HStack(spacing) { Alignment = alignment };
        configure?.Invoke(stack);
        return stack;
    }
}
