using ZorUI.Core;
using ZorUI.Core.Properties;

namespace ZorUI.Components.Layout;

/// <summary>
/// Z-axis stack layout that overlays children on top of each other.
/// Similar to SwiftUI ZStack or CSS position: absolute.
/// Later children appear on top of earlier children.
/// </summary>
public class ZStack : Container
{
    /// <summary>
    /// Alignment of all children within the stack.
    /// </summary>
    public Alignment Alignment { get; set; } = Alignment.Center;

    /// <summary>
    /// Creates a new Z-stack with optional children.
    /// </summary>
    public ZStack(params ZorElement[] children) : base(children)
    {
    }

    /// <summary>
    /// Fluent API: Sets the alignment.
    /// </summary>
    public ZStack WithAlignment(Alignment alignment)
    {
        Alignment = alignment;
        return this;
    }

    /// <summary>
    /// Fluent API: Adds a child element.
    /// </summary>
    public new ZStack AddChild(ZorElement child)
    {
        base.AddChild(child);
        return this;
    }
}
