using ZorUI.Core;
using ZorUI.Core.Properties;

namespace ZorUI.Components.Layout;

/// <summary>
/// Visual separator line between elements.
/// Similar to Radix UI Separator or HTML hr.
/// </summary>
public class Divider : ZorElement
{
    /// <summary>
    /// Orientation of the divider.
    /// </summary>
    public Orientation Orientation { get; set; } = Orientation.Horizontal;

    /// <summary>
    /// Thickness of the divider line.
    /// </summary>
    public double Thickness { get; set; } = 1;

    /// <summary>
    /// Color of the divider.
    /// </summary>
    public Color? Color { get; set; }

    /// <summary>
    /// Length of the divider (null means full length).
    /// </summary>
    public double? Length { get; set; }

    /// <summary>
    /// Creates a new divider.
    /// </summary>
    public Divider(Orientation orientation = Orientation.Horizontal)
    {
        Orientation = orientation;
    }

    /// <summary>
    /// Fluent API: Sets the thickness.
    /// </summary>
    public Divider WithThickness(double thickness)
    {
        Thickness = thickness;
        return this;
    }

    /// <summary>
    /// Fluent API: Sets the color.
    /// </summary>
    public Divider WithColor(Color color)
    {
        Color = color;
        return this;
    }

    /// <summary>
    /// Fluent API: Sets the length.
    /// </summary>
    public Divider WithLength(double length)
    {
        Length = length;
        return this;
    }
}

/// <summary>
/// Orientation options for elements.
/// </summary>
public enum Orientation
{
    /// <summary>
    /// Horizontal orientation (left to right).
    /// </summary>
    Horizontal,

    /// <summary>
    /// Vertical orientation (top to bottom).
    /// </summary>
    Vertical
}
