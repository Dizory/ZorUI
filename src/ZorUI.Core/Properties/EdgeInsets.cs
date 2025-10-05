namespace ZorUI.Core.Properties;

/// <summary>
/// Represents spacing or padding on the edges of a rectangle.
/// Similar to CSS padding/margin or SwiftUI EdgeInsets.
/// </summary>
public readonly struct EdgeInsets : IEquatable<EdgeInsets>
{
    /// <summary>
    /// Space from the top edge.
    /// </summary>
    public double Top { get; init; }

    /// <summary>
    /// Space from the right edge.
    /// </summary>
    public double Right { get; init; }

    /// <summary>
    /// Space from the bottom edge.
    /// </summary>
    public double Bottom { get; init; }

    /// <summary>
    /// Space from the left edge.
    /// </summary>
    public double Left { get; init; }

    /// <summary>
    /// Creates edge insets with individual values for each side.
    /// </summary>
    public EdgeInsets(double top, double right, double bottom, double left)
    {
        Top = top;
        Right = right;
        Bottom = bottom;
        Left = left;
    }

    /// <summary>
    /// Creates edge insets with the same value for all sides.
    /// </summary>
    public static EdgeInsets All(double value) => new(value, value, value, value);

    /// <summary>
    /// Creates edge insets with symmetric horizontal and vertical values.
    /// </summary>
    public static EdgeInsets Symmetric(double horizontal = 0, double vertical = 0) =>
        new(vertical, horizontal, vertical, horizontal);

    /// <summary>
    /// Creates edge insets with only horizontal values.
    /// </summary>
    public static EdgeInsets Horizontal(double value) => new(0, value, 0, value);

    /// <summary>
    /// Creates edge insets with only vertical values.
    /// </summary>
    public static EdgeInsets Vertical(double value) => new(value, 0, value, 0);

    /// <summary>
    /// Zero edge insets (no spacing).
    /// </summary>
    public static EdgeInsets Zero => new(0, 0, 0, 0);

    /// <summary>
    /// Total horizontal space (left + right).
    /// </summary>
    public double Horizontal => Left + Right;

    /// <summary>
    /// Total vertical space (top + bottom).
    /// </summary>
    public double Vertical => Top + Bottom;

    public bool Equals(EdgeInsets other) =>
        Top == other.Top && Right == other.Right && 
        Bottom == other.Bottom && Left == other.Left;

    public override bool Equals(object? obj) => 
        obj is EdgeInsets other && Equals(other);

    public override int GetHashCode() => 
        HashCode.Combine(Top, Right, Bottom, Left);

    public static bool operator ==(EdgeInsets left, EdgeInsets right) => 
        left.Equals(right);

    public static bool operator !=(EdgeInsets left, EdgeInsets right) => 
        !left.Equals(right);

    public override string ToString() => 
        $"EdgeInsets(top: {Top}, right: {Right}, bottom: {Bottom}, left: {Left})";
}
