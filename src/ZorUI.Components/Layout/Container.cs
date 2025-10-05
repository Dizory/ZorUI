using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Styling;

namespace ZorUI.Components.Layout;

/// <summary>
/// Base container element that can hold children.
/// This is the foundation for all layout components.
/// </summary>
public class Container : ZorElement
{
    /// <summary>
    /// Style applied to this container.
    /// </summary>
    public Style Style { get; set; } = new();

    /// <summary>
    /// Padding inside the container.
    /// </summary>
    public EdgeInsets? Padding { get; set; }

    /// <summary>
    /// Margin outside the container.
    /// </summary>
    public EdgeInsets? Margin { get; set; }

    /// <summary>
    /// Width of the container.
    /// </summary>
    public double? Width { get; set; }

    /// <summary>
    /// Height of the container.
    /// </summary>
    public double? Height { get; set; }

    /// <summary>
    /// Background color of the container.
    /// </summary>
    public Color? Background { get; set; }

    /// <summary>
    /// Creates a new container with optional children.
    /// </summary>
    public Container(params ZorElement[] children)
    {
        foreach (var child in children)
        {
            AddChild(child);
        }
    }

    /// <summary>
    /// Fluent API: Sets the padding.
    /// </summary>
    public Container WithPadding(EdgeInsets padding)
    {
        Padding = padding;
        return this;
    }

    /// <summary>
    /// Fluent API: Sets all padding.
    /// </summary>
    public Container WithPadding(double value)
    {
        Padding = EdgeInsets.All(value);
        return this;
    }

    /// <summary>
    /// Fluent API: Sets the margin.
    /// </summary>
    public Container WithMargin(EdgeInsets margin)
    {
        Margin = margin;
        return this;
    }

    /// <summary>
    /// Fluent API: Sets the width.
    /// </summary>
    public Container WithWidth(double width)
    {
        Width = width;
        return this;
    }

    /// <summary>
    /// Fluent API: Sets the height.
    /// </summary>
    public Container WithHeight(double height)
    {
        Height = height;
        return this;
    }

    /// <summary>
    /// Fluent API: Sets the size.
    /// </summary>
    public Container WithSize(double width, double height)
    {
        Width = width;
        Height = height;
        return this;
    }

    /// <summary>
    /// Fluent API: Sets the background color.
    /// </summary>
    public Container WithBackground(Color color)
    {
        Background = color;
        return this;
    }

    /// <summary>
    /// Fluent API: Applies a style.
    /// </summary>
    public Container WithStyle(Style style)
    {
        Style = style;
        return this;
    }
}
