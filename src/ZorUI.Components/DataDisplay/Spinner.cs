using ZorUI.Core;
using ZorUI.Core.Properties;

namespace ZorUI.Components.DataDisplay;

/// <summary>
/// Loading spinner component for indicating loading state.
/// Similar to Material CircularProgress or Bootstrap Spinner.
/// </summary>
public class Spinner : ZorElement
{
    /// <summary>
    /// Size of the spinner.
    /// </summary>
    public SpinnerSize Size { get; set; } = SpinnerSize.Medium;

    /// <summary>
    /// Custom size in pixels (overrides Size).
    /// </summary>
    public double? CustomSize { get; set; }

    /// <summary>
    /// Color of the spinner.
    /// </summary>
    public Color? Color { get; set; }

    /// <summary>
    /// Thickness of the spinner line.
    /// </summary>
    public double Thickness { get; set; } = 3;

    /// <summary>
    /// Label text for accessibility.
    /// </summary>
    public string Label { get; set; } = "Loading...";

    /// <summary>
    /// Creates a new spinner.
    /// </summary>
    public Spinner()
    {
    }

    // Fluent API

    public Spinner WithSize(SpinnerSize size)
    {
        Size = size;
        CustomSize = null;
        return this;
    }

    public Spinner WithCustomSize(double size)
    {
        CustomSize = size;
        return this;
    }

    public Spinner Small()
    {
        Size = SpinnerSize.Small;
        return this;
    }

    public Spinner Large()
    {
        Size = SpinnerSize.Large;
        return this;
    }

    public Spinner WithColor(Color color)
    {
        Color = color;
        return this;
    }

    public Spinner WithThickness(double thickness)
    {
        Thickness = thickness;
        return this;
    }

    public Spinner WithLabel(string label)
    {
        Label = label;
        return this;
    }
}

/// <summary>
/// Spinner size options.
/// </summary>
public enum SpinnerSize
{
    Small,    // 16px
    Medium,   // 24px
    Large     // 40px
}
