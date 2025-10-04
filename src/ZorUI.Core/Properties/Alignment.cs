namespace ZorUI.Core.Properties;

/// <summary>
/// Defines how content is aligned within a container.
/// Similar to CSS flexbox alignment or SwiftUI Alignment.
/// </summary>
public enum Alignment
{
    /// <summary>
    /// Align to the top-left corner.
    /// </summary>
    TopLeading,

    /// <summary>
    /// Align to the top-center.
    /// </summary>
    Top,

    /// <summary>
    /// Align to the top-right corner.
    /// </summary>
    TopTrailing,

    /// <summary>
    /// Align to the left-center.
    /// </summary>
    Leading,

    /// <summary>
    /// Align to the center.
    /// </summary>
    Center,

    /// <summary>
    /// Align to the right-center.
    /// </summary>
    Trailing,

    /// <summary>
    /// Align to the bottom-left corner.
    /// </summary>
    BottomLeading,

    /// <summary>
    /// Align to the bottom-center.
    /// </summary>
    Bottom,

    /// <summary>
    /// Align to the bottom-right corner.
    /// </summary>
    BottomTrailing
}

/// <summary>
/// Horizontal alignment options.
/// </summary>
public enum HorizontalAlignment
{
    /// <summary>
    /// Align to the left.
    /// </summary>
    Left,

    /// <summary>
    /// Align to the center.
    /// </summary>
    Center,

    /// <summary>
    /// Align to the right.
    /// </summary>
    Right,

    /// <summary>
    /// Stretch to fill horizontal space.
    /// </summary>
    Stretch
}

/// <summary>
/// Vertical alignment options.
/// </summary>
public enum VerticalAlignment
{
    /// <summary>
    /// Align to the top.
    /// </summary>
    Top,

    /// <summary>
    /// Align to the center.
    /// </summary>
    Center,

    /// <summary>
    /// Align to the bottom.
    /// </summary>
    Bottom,

    /// <summary>
    /// Stretch to fill vertical space.
    /// </summary>
    Stretch
}
