using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;

namespace ZorUI.Components.DataDisplay;

/// <summary>
/// Badge component for labels, counts, and status indicators.
/// Similar to Material Badge or Bootstrap Badge.
/// </summary>
public class Badge : Container
{
    /// <summary>
    /// Content of the badge (text or custom element).
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Variant of the badge.
    /// </summary>
    public BadgeVariant Variant { get; set; } = BadgeVariant.Default;

    /// <summary>
    /// Size of the badge.
    /// </summary>
    public BadgeSize Size { get; set; } = BadgeSize.Medium;

    /// <summary>
    /// Whether the badge is a dot (small circle without text).
    /// </summary>
    public bool IsDot { get; set; } = false;

    /// <summary>
    /// Element to attach the badge to (for positioned badges).
    /// </summary>
    public ZorElement? Target { get; set; }

    /// <summary>
    /// Creates a new badge.
    /// </summary>
    public Badge(string? content = null)
    {
        Content = content;
    }

    // Fluent API

    public Badge WithContent(string content)
    {
        Content = content;
        IsDot = false;
        return this;
    }

    public Badge Dot()
    {
        IsDot = true;
        Content = null;
        return this;
    }

    public Badge WithVariant(BadgeVariant variant)
    {
        Variant = variant;
        return this;
    }

    public Badge Primary()
    {
        Variant = BadgeVariant.Primary;
        return this;
    }

    public Badge Secondary()
    {
        Variant = BadgeVariant.Secondary;
        return this;
    }

    public Badge Success()
    {
        Variant = BadgeVariant.Success;
        return this;
    }

    public Badge Warning()
    {
        Variant = BadgeVariant.Warning;
        return this;
    }

    public Badge Error()
    {
        Variant = BadgeVariant.Error;
        return this;
    }

    public Badge Info()
    {
        Variant = BadgeVariant.Info;
        return this;
    }

    public Badge WithSize(BadgeSize size)
    {
        Size = size;
        return this;
    }

    public Badge Small()
    {
        Size = BadgeSize.Small;
        return this;
    }

    public Badge Large()
    {
        Size = BadgeSize.Large;
        return this;
    }

    public Badge AttachTo(ZorElement target)
    {
        Target = target;
        return this;
    }
}

/// <summary>
/// Badge visual variants.
/// </summary>
public enum BadgeVariant
{
    Default,
    Primary,
    Secondary,
    Success,
    Warning,
    Error,
    Info,
    Outline
}

/// <summary>
/// Badge size options.
/// </summary>
public enum BadgeSize
{
    Small,
    Medium,
    Large
}
