using ZorUI.Core.Properties;

namespace ZorUI.Styling;

/// <summary>
/// Represents a complete theme for the application.
/// Inspired by Radix UI's theme system and Material Design.
/// </summary>
public class Theme
{
    /// <summary>
    /// Color palette for the theme.
    /// </summary>
    public ColorPalette Colors { get; set; } = new();

    /// <summary>
    /// Typography settings.
    /// </summary>
    public Typography Typography { get; set; } = new();

    /// <summary>
    /// Spacing scale for consistent spacing throughout the app.
    /// </summary>
    public SpacingScale Spacing { get; set; } = new();

    /// <summary>
    /// Radius values for rounded corners.
    /// </summary>
    public RadiusScale Radius { get; set; } = new();

    /// <summary>
    /// Shadow definitions.
    /// </summary>
    public ShadowScale Shadows { get; set; } = new();

    /// <summary>
    /// Whether this is a dark theme.
    /// </summary>
    public bool IsDark { get; set; } = false;

    /// <summary>
    /// Creates a default light theme.
    /// </summary>
    public static Theme Light() => new()
    {
        IsDark = false,
        Colors = ColorPalette.Light()
    };

    /// <summary>
    /// Creates a default dark theme.
    /// </summary>
    public static Theme Dark() => new()
    {
        IsDark = true,
        Colors = ColorPalette.Dark()
    };
}

/// <summary>
/// Color palette with semantic color names.
/// Based on Radix UI color system.
/// </summary>
public class ColorPalette
{
    // Base colors
    public Color Background { get; set; } = Color.White;
    public Color Foreground { get; set; } = Color.Black;
    public Color Card { get; set; } = Color.White;
    public Color CardForeground { get; set; } = Color.Black;
    public Color Popover { get; set; } = Color.White;
    public Color PopoverForeground { get; set; } = Color.Black;

    // Primary colors
    public Color Primary { get; set; } = Color.FromHex("#3B82F6");
    public Color PrimaryForeground { get; set; } = Color.White;
    
    // Secondary colors
    public Color Secondary { get; set; } = Color.FromHex("#64748B");
    public Color SecondaryForeground { get; set; } = Color.White;

    // Muted colors
    public Color Muted { get; set; } = Color.FromHex("#F1F5F9");
    public Color MutedForeground { get; set; } = Color.FromHex("#64748B");

    // Accent colors
    public Color Accent { get; set; } = Color.FromHex("#F1F5F9");
    public Color AccentForeground { get; set; } = Color.FromHex("#0F172A");

    // Destructive colors
    public Color Destructive { get; set; } = Color.FromHex("#EF4444");
    public Color DestructiveForeground { get; set; } = Color.White;

    // Border and input
    public Color Border { get; set; } = Color.FromHex("#E2E8F0");
    public Color Input { get; set; } = Color.FromHex("#E2E8F0");
    public Color Ring { get; set; } = Color.FromHex("#3B82F6");

    // Success, Warning, Info
    public Color Success { get; set; } = Color.FromHex("#10B981");
    public Color Warning { get; set; } = Color.FromHex("#F59E0B");
    public Color Info { get; set; } = Color.FromHex("#3B82F6");
    public Color Error { get; set; } = Color.FromHex("#EF4444");

    public static ColorPalette Light() => new();

    public static ColorPalette Dark() => new()
    {
        Background = Color.FromHex("#0F172A"),
        Foreground = Color.FromHex("#F8FAFC"),
        Card = Color.FromHex("#1E293B"),
        CardForeground = Color.FromHex("#F8FAFC"),
        Popover = Color.FromHex("#1E293B"),
        PopoverForeground = Color.FromHex("#F8FAFC"),
        Primary = Color.FromHex("#3B82F6"),
        PrimaryForeground = Color.White,
        Secondary = Color.FromHex("#475569"),
        SecondaryForeground = Color.White,
        Muted = Color.FromHex("#1E293B"),
        MutedForeground = Color.FromHex("#94A3B8"),
        Accent = Color.FromHex("#1E293B"),
        AccentForeground = Color.FromHex("#F8FAFC"),
        Destructive = Color.FromHex("#DC2626"),
        DestructiveForeground = Color.White,
        Border = Color.FromHex("#334155"),
        Input = Color.FromHex("#334155"),
        Ring = Color.FromHex("#3B82F6")
    };
}

/// <summary>
/// Typography settings for text elements.
/// </summary>
public class Typography
{
    public string FontFamily { get; set; } = "Inter, system-ui, sans-serif";
    
    public double FontSizeXs { get; set; } = 12;
    public double FontSizeSm { get; set; } = 14;
    public double FontSizeBase { get; set; } = 16;
    public double FontSizeLg { get; set; } = 18;
    public double FontSizeXl { get; set; } = 20;
    public double FontSize2Xl { get; set; } = 24;
    public double FontSize3Xl { get; set; } = 30;
    public double FontSize4Xl { get; set; } = 36;

    public double LineHeightTight { get; set; } = 1.25;
    public double LineHeightNormal { get; set; } = 1.5;
    public double LineHeightRelaxed { get; set; } = 1.75;
}

/// <summary>
/// Spacing scale based on a base unit (typically 4px).
/// </summary>
public class SpacingScale
{
    public double BaseUnit { get; set; } = 4;

    public double Space1 => BaseUnit * 1;      // 4px
    public double Space2 => BaseUnit * 2;      // 8px
    public double Space3 => BaseUnit * 3;      // 12px
    public double Space4 => BaseUnit * 4;      // 16px
    public double Space5 => BaseUnit * 5;      // 20px
    public double Space6 => BaseUnit * 6;      // 24px
    public double Space8 => BaseUnit * 8;      // 32px
    public double Space10 => BaseUnit * 10;    // 40px
    public double Space12 => BaseUnit * 12;    // 48px
    public double Space16 => BaseUnit * 16;    // 64px
    public double Space20 => BaseUnit * 20;    // 80px
}

/// <summary>
/// Border radius scale.
/// </summary>
public class RadiusScale
{
    public double None { get; set; } = 0;
    public double Sm { get; set; } = 4;
    public double Base { get; set; } = 6;
    public double Md { get; set; } = 8;
    public double Lg { get; set; } = 12;
    public double Xl { get; set; } = 16;
    public double Full { get; set; } = 9999;
}

/// <summary>
/// Shadow definitions for elevation.
/// </summary>
public class ShadowScale
{
    public string Sm { get; set; } = "0 1px 2px 0 rgba(0, 0, 0, 0.05)";
    public string Base { get; set; } = "0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06)";
    public string Md { get; set; } = "0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06)";
    public string Lg { get; set; } = "0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05)";
    public string Xl { get; set; } = "0 20px 25px -5px rgba(0, 0, 0, 0.1), 0 10px 10px -5px rgba(0, 0, 0, 0.04)";
}
