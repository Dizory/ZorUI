using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Primitives;

namespace ZorUI.Components.DataDisplay;

/// <summary>
/// Avatar component for displaying user profile images or initials.
/// Similar to Radix UI Avatar or Material Avatar.
/// </summary>
public class Avatar : ZorElement
{
    /// <summary>
    /// Image source URL.
    /// </summary>
    public string? ImageSource { get; set; }

    /// <summary>
    /// Alternative text for the image.
    /// </summary>
    public string? AltText { get; set; }

    /// <summary>
    /// Initials to display if image is not available.
    /// </summary>
    public string? Initials { get; set; }

    /// <summary>
    /// Fallback icon to display if both image and initials are not available.
    /// </summary>
    public ZorElement? FallbackIcon { get; set; }

    /// <summary>
    /// Size of the avatar.
    /// </summary>
    public AvatarSize Size { get; set; } = AvatarSize.Medium;

    /// <summary>
    /// Custom size in pixels (overrides Size).
    /// </summary>
    public double? CustomSize { get; set; }

    /// <summary>
    /// Shape of the avatar.
    /// </summary>
    public AvatarShape Shape { get; set; } = AvatarShape.Circle;

    /// <summary>
    /// Status indicator.
    /// </summary>
    public AvatarStatus? Status { get; set; }

    /// <summary>
    /// Creates a new avatar.
    /// </summary>
    public Avatar(string? imageSource = null, string? initials = null)
    {
        ImageSource = imageSource;
        Initials = initials;
    }

    // Fluent API

    public Avatar WithImage(string imageSource, string? altText = null)
    {
        ImageSource = imageSource;
        AltText = altText;
        return this;
    }

    public Avatar WithInitials(string initials)
    {
        Initials = initials;
        return this;
    }

    public Avatar WithFallbackIcon(ZorElement icon)
    {
        FallbackIcon = icon;
        return this;
    }

    public Avatar WithSize(AvatarSize size)
    {
        Size = size;
        CustomSize = null;
        return this;
    }

    public Avatar WithCustomSize(double size)
    {
        CustomSize = size;
        return this;
    }

    public Avatar Small()
    {
        Size = AvatarSize.Small;
        return this;
    }

    public Avatar Large()
    {
        Size = AvatarSize.Large;
        return this;
    }

    public Avatar ExtraLarge()
    {
        Size = AvatarSize.ExtraLarge;
        return this;
    }

    public Avatar WithShape(AvatarShape shape)
    {
        Shape = shape;
        return this;
    }

    public Avatar Circle()
    {
        Shape = AvatarShape.Circle;
        return this;
    }

    public Avatar Square()
    {
        Shape = AvatarShape.Square;
        return this;
    }

    public Avatar Rounded()
    {
        Shape = AvatarShape.Rounded;
        return this;
    }

    public Avatar WithStatus(AvatarStatus status)
    {
        Status = status;
        return this;
    }
}

/// <summary>
/// Avatar size options.
/// </summary>
public enum AvatarSize
{
    ExtraSmall,  // 24px
    Small,       // 32px
    Medium,      // 40px
    Large,       // 48px
    ExtraLarge   // 64px
}

/// <summary>
/// Avatar shape options.
/// </summary>
public enum AvatarShape
{
    Circle,
    Square,
    Rounded
}

/// <summary>
/// Avatar status indicator options.
/// </summary>
public enum AvatarStatus
{
    Online,
    Offline,
    Away,
    Busy
}
