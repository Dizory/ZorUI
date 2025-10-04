using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Primitives;

namespace ZorUI.Icons;

/// <summary>
/// Enhanced icon component using ZorIcon enum (Radix UI style icons).
/// </summary>
public class ZorIconComponent : ZorElement
{
    /// <summary>
    /// The icon to display.
    /// </summary>
    public ZorIcon Icon { get; set; }
    
    /// <summary>
    /// Icon size in pixels.
    /// </summary>
    public double Size { get; set; } = 24;
    
    /// <summary>
    /// Icon color.
    /// </summary>
    public Color? IconColor { get; set; }
    
    /// <summary>
    /// Stroke width for the icon.
    /// </summary>
    public double StrokeWidth { get; set; } = 2;
    
    /// <summary>
    /// Whether the icon should be filled.
    /// </summary>
    public bool IsFilled { get; set; } = false;

    /// <summary>
    /// Creates an icon component.
    /// </summary>
    public ZorIconComponent(ZorIcon icon)
    {
        Icon = icon;
        ElementType = "ZorIcon";
    }

    /// <summary>
    /// Gets the SVG path for this icon.
    /// </summary>
    public string GetSvgPath()
    {
        return IconRegistry.GetPath(Icon);
    }

    /// <summary>
    /// Sets the icon size.
    /// </summary>
    public ZorIconComponent WithSize(double size)
    {
        Size = size;
        return this;
    }

    /// <summary>
    /// Sets the icon color.
    /// </summary>
    public ZorIconComponent WithColor(Color color)
    {
        IconColor = color;
        return this;
    }

    /// <summary>
    /// Sets the stroke width.
    /// </summary>
    public ZorIconComponent WithStrokeWidth(double width)
    {
        StrokeWidth = width;
        return this;
    }

    /// <summary>
    /// Sets whether the icon should be filled.
    /// </summary>
    public ZorIconComponent Filled(bool filled = true)
    {
        IsFilled = filled;
        return this;
    }

    /// <summary>
    /// Small size (16px).
    /// </summary>
    public ZorIconComponent Small()
    {
        Size = 16;
        return this;
    }

    /// <summary>
    /// Medium size (24px) - default.
    /// </summary>
    public ZorIconComponent Medium()
    {
        Size = 24;
        return this;
    }

    /// <summary>
    /// Large size (32px).
    /// </summary>
    public ZorIconComponent Large()
    {
        Size = 32;
        return this;
    }

    /// <summary>
    /// Extra large size (48px).
    /// </summary>
    public ZorIconComponent ExtraLarge()
    {
        Size = 48;
        return this;
    }
}

/// <summary>
/// Helper class for creating icon components.
/// </summary>
public static class ZIcon
{
    /// <summary>
    /// Creates a Home icon.
    /// </summary>
    public static ZorIconComponent Home() => new(ZorIcon.Home);
    
    /// <summary>
    /// Creates a Menu icon.
    /// </summary>
    public static ZorIconComponent Menu() => new(ZorIcon.Menu);
    
    /// <summary>
    /// Creates a Close icon.
    /// </summary>
    public static ZorIconComponent Close() => new(ZorIcon.Close);
    
    /// <summary>
    /// Creates a User icon.
    /// </summary>
    public static ZorIconComponent User() => new(ZorIcon.User);
    
    /// <summary>
    /// Creates a Settings icon.
    /// </summary>
    public static ZorIconComponent Settings() => new(ZorIcon.Settings);
    
    /// <summary>
    /// Creates a Search icon.
    /// </summary>
    public static ZorIconComponent Search() => new(ZorIcon.Search);
    
    /// <summary>
    /// Creates a Plus icon.
    /// </summary>
    public static ZorIconComponent Plus() => new(ZorIcon.Plus);
    
    /// <summary>
    /// Creates a Check icon.
    /// </summary>
    public static ZorIconComponent Check() => new(ZorIcon.Check);
    
    /// <summary>
    /// Creates an icon from enum.
    /// </summary>
    public static ZorIconComponent From(ZorIcon icon) => new(icon);
}
