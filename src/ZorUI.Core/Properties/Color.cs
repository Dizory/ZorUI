namespace ZorUI.Core.Properties;

/// <summary>
/// Represents a color in RGBA format.
/// Values are in the range 0-255 for RGB and 0-1 for alpha.
/// </summary>
public readonly struct Color : IEquatable<Color>
{
    /// <summary>
    /// Red component (0-255).
    /// </summary>
    public byte R { get; init; }

    /// <summary>
    /// Green component (0-255).
    /// </summary>
    public byte G { get; init; }

    /// <summary>
    /// Blue component (0-255).
    /// </summary>
    public byte B { get; init; }

    /// <summary>
    /// Alpha (opacity) component (0.0-1.0).
    /// </summary>
    public double A { get; init; }

    /// <summary>
    /// Creates a new color from RGBA values.
    /// </summary>
    public Color(byte r, byte g, byte b, double a = 1.0)
    {
        R = r;
        G = g;
        B = b;
        A = Math.Clamp(a, 0.0, 1.0);
    }

    /// <summary>
    /// Creates a color from RGB values (opaque).
    /// </summary>
    public static Color FromRgb(byte r, byte g, byte b) => new(r, g, b, 1.0);

    /// <summary>
    /// Creates a color from a hex string (e.g., "#FF5733" or "FF5733").
    /// </summary>
    public static Color FromHex(string hex)
    {
        hex = hex.TrimStart('#');
        
        if (hex.Length == 6)
        {
            return new Color(
                Convert.ToByte(hex.Substring(0, 2), 16),
                Convert.ToByte(hex.Substring(2, 2), 16),
                Convert.ToByte(hex.Substring(4, 2), 16),
                1.0
            );
        }
        else if (hex.Length == 8)
        {
            return new Color(
                Convert.ToByte(hex.Substring(0, 2), 16),
                Convert.ToByte(hex.Substring(2, 2), 16),
                Convert.ToByte(hex.Substring(4, 2), 16),
                Convert.ToByte(hex.Substring(6, 2), 16) / 255.0
            );
        }
        
        throw new ArgumentException($"Invalid hex color format: {hex}");
    }

    /// <summary>
    /// Creates a color from HSL values.
    /// </summary>
    /// <param name="h">Hue (0-360)</param>
    /// <param name="s">Saturation (0-1)</param>
    /// <param name="l">Lightness (0-1)</param>
    /// <param name="a">Alpha (0-1)</param>
    public static Color FromHsl(double h, double s, double l, double a = 1.0)
    {
        h = h % 360.0;
        s = Math.Clamp(s, 0.0, 1.0);
        l = Math.Clamp(l, 0.0, 1.0);

        double c = (1 - Math.Abs(2 * l - 1)) * s;
        double x = c * (1 - Math.Abs((h / 60.0) % 2 - 1));
        double m = l - c / 2;

        double r1, g1, b1;

        if (h < 60) { r1 = c; g1 = x; b1 = 0; }
        else if (h < 120) { r1 = x; g1 = c; b1 = 0; }
        else if (h < 180) { r1 = 0; g1 = c; b1 = x; }
        else if (h < 240) { r1 = 0; g1 = x; b1 = c; }
        else if (h < 300) { r1 = x; g1 = 0; b1 = c; }
        else { r1 = c; g1 = 0; b1 = x; }

        return new Color(
            (byte)Math.Round((r1 + m) * 255),
            (byte)Math.Round((g1 + m) * 255),
            (byte)Math.Round((b1 + m) * 255),
            a
        );
    }

    /// <summary>
    /// Returns a new color with modified alpha.
    /// </summary>
    public Color WithAlpha(double alpha) => new(R, G, B, alpha);

    /// <summary>
    /// Returns a new color with increased opacity.
    /// </summary>
    public Color Lighter(double amount = 0.1) =>
        FromHsl(GetHue(), GetSaturation(), Math.Min(1.0, GetLightness() + amount), A);

    /// <summary>
    /// Returns a new color with decreased opacity.
    /// </summary>
    public Color Darker(double amount = 0.1) =>
        FromHsl(GetHue(), GetSaturation(), Math.Max(0.0, GetLightness() - amount), A);

    private double GetHue()
    {
        double r = R / 255.0;
        double g = G / 255.0;
        double b = B / 255.0;
        
        double max = Math.Max(r, Math.Max(g, b));
        double min = Math.Min(r, Math.Min(g, b));
        double delta = max - min;

        if (delta == 0) return 0;

        double hue;
        if (max == r) hue = 60 * (((g - b) / delta) % 6);
        else if (max == g) hue = 60 * (((b - r) / delta) + 2);
        else hue = 60 * (((r - g) / delta) + 4);

        return hue < 0 ? hue + 360 : hue;
    }

    private double GetSaturation()
    {
        double l = GetLightness();
        if (l == 0 || l == 1) return 0;

        double r = R / 255.0;
        double g = G / 255.0;
        double b = B / 255.0;
        double max = Math.Max(r, Math.Max(g, b));
        double min = Math.Min(r, Math.Min(g, b));
        
        return (max - min) / (1 - Math.Abs(2 * l - 1));
    }

    private double GetLightness()
    {
        double r = R / 255.0;
        double g = G / 255.0;
        double b = B / 255.0;
        double max = Math.Max(r, Math.Max(g, b));
        double min = Math.Min(r, Math.Min(g, b));
        return (max + min) / 2;
    }

    // Common colors
    public static Color Transparent => new(0, 0, 0, 0);
    public static Color Black => new(0, 0, 0);
    public static Color White => new(255, 255, 255);
    public static Color Red => new(255, 0, 0);
    public static Color Green => new(0, 255, 0);
    public static Color Blue => new(0, 0, 255);
    public static Color Yellow => new(255, 255, 0);
    public static Color Cyan => new(0, 255, 255);
    public static Color Magenta => new(255, 0, 255);
    public static Color Gray => new(128, 128, 128);

    public bool Equals(Color other) =>
        R == other.R && G == other.G && B == other.B && A == other.A;

    public override bool Equals(object? obj) =>
        obj is Color other && Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(R, G, B, A);

    public static bool operator ==(Color left, Color right) =>
        left.Equals(right);

    public static bool operator !=(Color left, Color right) =>
        !left.Equals(right);

    public override string ToString() =>
        $"Color(R:{R}, G:{G}, B:{B}, A:{A:F2})";

    /// <summary>
    /// Converts to hex string format.
    /// </summary>
    public string ToHex() =>
        $"#{R:X2}{G:X2}{B:X2}{(A < 1.0 ? $"{(byte)(A * 255):X2}" : "")}";
}
