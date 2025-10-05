using ZorUI.Core;
using ZorUI.Core.Properties;

namespace ZorUI.Components.Layout;

/// <summary>
/// Vertical stack layout that arranges children vertically.
/// Similar to SwiftUI VStack or CSS flexbox column.
/// </summary>
public class VStack : Container
{
    /// <summary>
    /// Spacing between children.
    /// </summary>
    public double Spacing { get; set; } = 0;

    /// <summary>
    /// Horizontal alignment of children.
    /// </summary>
    public HorizontalAlignment Alignment { get; set; } = HorizontalAlignment.Center;

    /// <summary>
    /// Creates a new vertical stack with optional spacing and children.
    /// </summary>
    public VStack(double spacing = 0, params ZorElement[] children) : base(children)
    {
        Spacing = spacing;
    }

    /// <summary>
    /// Fluent API: Sets the spacing between children.
    /// </summary>
    public VStack WithSpacing(double spacing)
    {
        Spacing = spacing;
        return this;
    }

    /// <summary>
    /// Fluent API: Sets the horizontal alignment.
    /// </summary>
    public VStack WithAlignment(HorizontalAlignment alignment)
    {
        Alignment = alignment;
        return this;
    }

    /// <summary>
    /// Fluent API: Adds a child element.
    /// </summary>
    public new VStack AddChild(ZorElement child)
    {
        base.AddChild(child);
        return this;
    }
}

/// <summary>
/// Helper class for creating VStack with fluent syntax.
/// Usage: VStack.Create(spacing: 16, children => { ... })
/// </summary>
public static class VStackBuilder
{
    /// <summary>
    /// Creates a VStack with a builder pattern.
    /// </summary>
    public static VStack Create(double spacing = 0, HorizontalAlignment alignment = HorizontalAlignment.Center, 
                                Action<VStack>? configure = null)
    {
        var stack = new VStack(spacing) { Alignment = alignment };
        configure?.Invoke(stack);
        return stack;
    }
}
