using ZorUI.Core;
using ZorUI.Core.Properties;

namespace ZorUI.Components.Primitives;

/// <summary>
/// Image element for displaying images.
/// Similar to HTML img or SwiftUI Image.
/// </summary>
public class Image : ZorElement
{
    /// <summary>
    /// Source path or URL of the image.
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// Alternative text for accessibility.
    /// </summary>
    public string? AltText { get; set; }

    /// <summary>
    /// Width of the image.
    /// </summary>
    public double? Width { get; set; }

    /// <summary>
    /// Height of the image.
    /// </summary>
    public double? Height { get; set; }

    /// <summary>
    /// How the image should be scaled/fitted.
    /// </summary>
    public ImageFit Fit { get; set; } = ImageFit.Cover;

    /// <summary>
    /// Border radius for rounded images.
    /// </summary>
    public double? BorderRadius { get; set; }

    /// <summary>
    /// Opacity of the image (0.0 to 1.0).
    /// </summary>
    public double Opacity { get; set; } = 1.0;

    /// <summary>
    /// Creates a new image element.
    /// </summary>
    public Image(string source, string? altText = null)
    {
        Source = source;
        AltText = altText;
    }

    // Fluent API

    public Image WithSize(double width, double height)
    {
        Width = width;
        Height = height;
        return this;
    }

    public Image WithWidth(double width)
    {
        Width = width;
        return this;
    }

    public Image WithHeight(double height)
    {
        Height = height;
        return this;
    }

    public Image WithFit(ImageFit fit)
    {
        Fit = fit;
        return this;
    }

    public Image Rounded(double radius)
    {
        BorderRadius = radius;
        return this;
    }

    public Image Circle()
    {
        BorderRadius = 9999;
        return this;
    }

    public Image WithOpacity(double opacity)
    {
        Opacity = Math.Clamp(opacity, 0.0, 1.0);
        return this;
    }

    public Image WithAltText(string altText)
    {
        AltText = altText;
        return this;
    }
}

/// <summary>
/// Image fitting modes.
/// </summary>
public enum ImageFit
{
    /// <summary>
    /// Scale to fill entire area, may crop.
    /// </summary>
    Cover,

    /// <summary>
    /// Scale to fit within area, may have empty space.
    /// </summary>
    Contain,

    /// <summary>
    /// Fill area completely, may distort.
    /// </summary>
    Fill,

    /// <summary>
    /// Keep original size.
    /// </summary>
    None,

    /// <summary>
    /// Scale down if larger, otherwise keep original.
    /// </summary>
    ScaleDown
}
