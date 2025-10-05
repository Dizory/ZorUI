using ZorUI.Core;
using ZorUI.Core.Properties;

namespace ZorUI.Components.Primitives;

/// <summary>
/// Icon element for displaying vector icons.
/// Supports various icon sets (Material Icons, FontAwesome, custom SVG, etc.).
/// </summary>
public class Icon : ZorElement
{
    /// <summary>
    /// Name or identifier of the icon.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Size of the icon.
    /// </summary>
    public double Size { get; set; } = 24;

    /// <summary>
    /// Color of the icon.
    /// </summary>
    public Color? Color { get; set; }

    /// <summary>
    /// Icon set/library to use.
    /// </summary>
    public string IconSet { get; set; } = "default";

    /// <summary>
    /// Creates a new icon element.
    /// </summary>
    public Icon(string name)
    {
        Name = name;
    }

    // Fluent API

    public Icon WithSize(double size)
    {
        Size = size;
        return this;
    }

    public Icon Small()
    {
        Size = 16;
        return this;
    }

    public Icon Medium()
    {
        Size = 24;
        return this;
    }

    public Icon Large()
    {
        Size = 32;
        return this;
    }

    public Icon WithColor(Color color)
    {
        Color = color;
        return this;
    }

    public Icon FromSet(string iconSet)
    {
        IconSet = iconSet;
        return this;
    }
}
