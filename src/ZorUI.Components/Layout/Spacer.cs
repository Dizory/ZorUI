using ZorUI.Core;

namespace ZorUI.Components.Layout;

/// <summary>
/// A flexible space that expands to fill available space in a stack.
/// Similar to SwiftUI Spacer or CSS flex-grow.
/// </summary>
public class Spacer : ZorElement
{
    /// <summary>
    /// Minimum size for this spacer.
    /// </summary>
    public double MinLength { get; set; } = 0;

    /// <summary>
    /// Creates a new spacer with optional minimum length.
    /// </summary>
    public Spacer(double minLength = 0)
    {
        MinLength = minLength;
    }

    /// <summary>
    /// Fluent API: Sets the minimum length.
    /// </summary>
    public Spacer WithMinLength(double minLength)
    {
        MinLength = minLength;
        return this;
    }
}

/// <summary>
/// Fixed-size spacer with specific dimensions.
/// </summary>
public class FixedSpacer : ZorElement
{
    /// <summary>
    /// Width of the spacer.
    /// </summary>
    public double? Width { get; set; }

    /// <summary>
    /// Height of the spacer.
    /// </summary>
    public double? Height { get; set; }

    /// <summary>
    /// Creates a fixed-size spacer.
    /// </summary>
    public FixedSpacer(double? width = null, double? height = null)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Creates a horizontal spacer with specified width.
    /// </summary>
    public static FixedSpacer Horizontal(double width) => new(width, null);

    /// <summary>
    /// Creates a vertical spacer with specified height.
    /// </summary>
    public static FixedSpacer Vertical(double height) => new(null, height);
}
