namespace ZorUI.Core.Properties;

/// <summary>
/// Represents a two-dimensional size with width and height.
/// </summary>
public readonly struct Size : IEquatable<Size>
{
    /// <summary>
    /// Width of the size.
    /// </summary>
    public double Width { get; init; }

    /// <summary>
    /// Height of the size.
    /// </summary>
    public double Height { get; init; }

    /// <summary>
    /// Creates a new size with specified width and height.
    /// </summary>
    public Size(double width, double height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Creates a square size with the same width and height.
    /// </summary>
    public static Size Square(double value) => new(value, value);

    /// <summary>
    /// Zero size (0x0).
    /// </summary>
    public static Size Zero => new(0, 0);

    /// <summary>
    /// Infinite size (used for unconstrained layouts).
    /// </summary>
    public static Size Infinite => new(double.PositiveInfinity, double.PositiveInfinity);

    /// <summary>
    /// Checks if this size is zero.
    /// </summary>
    public bool IsZero => Width == 0 && Height == 0;

    /// <summary>
    /// Checks if this size is infinite.
    /// </summary>
    public bool IsInfinite => double.IsInfinity(Width) || double.IsInfinity(Height);

    public bool Equals(Size other) => 
        Width == other.Width && Height == other.Height;

    public override bool Equals(object? obj) => 
        obj is Size other && Equals(other);

    public override int GetHashCode() => 
        HashCode.Combine(Width, Height);

    public static bool operator ==(Size left, Size right) => 
        left.Equals(right);

    public static bool operator !=(Size left, Size right) => 
        !left.Equals(right);

    public override string ToString() => 
        $"Size({Width}x{Height})";
}

/// <summary>
/// Represents constraints for sizing elements.
/// Similar to Flutter's BoxConstraints.
/// </summary>
public readonly struct SizeConstraints
{
    /// <summary>
    /// Minimum width.
    /// </summary>
    public double MinWidth { get; init; }

    /// <summary>
    /// Maximum width.
    /// </summary>
    public double MaxWidth { get; init; }

    /// <summary>
    /// Minimum height.
    /// </summary>
    public double MinHeight { get; init; }

    /// <summary>
    /// Maximum height.
    /// </summary>
    public double MaxHeight { get; init; }

    /// <summary>
    /// Creates size constraints with specified min and max values.
    /// </summary>
    public SizeConstraints(double minWidth = 0, double maxWidth = double.PositiveInfinity,
                          double minHeight = 0, double maxHeight = double.PositiveInfinity)
    {
        MinWidth = minWidth;
        MaxWidth = maxWidth;
        MinHeight = minHeight;
        MaxHeight = maxHeight;
    }

    /// <summary>
    /// Creates tight constraints (fixed size).
    /// </summary>
    public static SizeConstraints Tight(Size size) =>
        new(size.Width, size.Width, size.Height, size.Height);

    /// <summary>
    /// Creates loose constraints (from zero to max).
    /// </summary>
    public static SizeConstraints Loose(Size maxSize) =>
        new(0, maxSize.Width, 0, maxSize.Height);

    /// <summary>
    /// Unconstrained size (infinite).
    /// </summary>
    public static SizeConstraints Unconstrained =>
        new(0, double.PositiveInfinity, 0, double.PositiveInfinity);

    /// <summary>
    /// Constrains a size to fit within these constraints.
    /// </summary>
    public Size Constrain(Size size) =>
        new(
            Math.Clamp(size.Width, MinWidth, MaxWidth),
            Math.Clamp(size.Height, MinHeight, MaxHeight)
        );

    /// <summary>
    /// Checks if the constraints are tight (fixed size).
    /// </summary>
    public bool IsTight => MinWidth == MaxWidth && MinHeight == MaxHeight;

    /// <summary>
    /// Gets the biggest size that satisfies these constraints.
    /// </summary>
    public Size Biggest => new(MaxWidth, MaxHeight);

    /// <summary>
    /// Gets the smallest size that satisfies these constraints.
    /// </summary>
    public Size Smallest => new(MinWidth, MinHeight);
}
