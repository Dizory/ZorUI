using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Styling;

namespace ZorUI.Components.Primitives;

/// <summary>
/// Text element for displaying text content.
/// Similar to SwiftUI Text or HTML span/p.
/// </summary>
public class Text : ZorElement
{
    /// <summary>
    /// Text content to display.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Font size in points.
    /// </summary>
    public double? FontSize { get; set; }

    /// <summary>
    /// Font weight.
    /// </summary>
    public FontWeight? FontWeight { get; set; }

    /// <summary>
    /// Font family name.
    /// </summary>
    public string? FontFamily { get; set; }

    /// <summary>
    /// Text color.
    /// </summary>
    public Color? Color { get; set; }

    /// <summary>
    /// Line height multiplier.
    /// </summary>
    public double? LineHeight { get; set; }

    /// <summary>
    /// Letter spacing.
    /// </summary>
    public double? LetterSpacing { get; set; }

    /// <summary>
    /// Text alignment.
    /// </summary>
    public TextAlignment Alignment { get; set; } = TextAlignment.Start;

    /// <summary>
    /// Whether text should wrap to multiple lines.
    /// </summary>
    public bool WordWrap { get; set; } = true;

    /// <summary>
    /// Maximum number of lines (null for unlimited).
    /// </summary>
    public int? MaxLines { get; set; }

    /// <summary>
    /// Text decoration (underline, strikethrough, etc.).
    /// </summary>
    public TextDecoration? Decoration { get; set; }

    /// <summary>
    /// Creates a new text element.
    /// </summary>
    public Text(string content)
    {
        Content = content;
    }

    // Fluent API methods

    public Text WithFontSize(double size)
    {
        FontSize = size;
        return this;
    }

    public Text Bold()
    {
        FontWeight = Styling.FontWeight.Bold;
        return this;
    }

    public Text SemiBold()
    {
        FontWeight = Styling.FontWeight.SemiBold;
        return this;
    }

    public Text Light()
    {
        FontWeight = Styling.FontWeight.Light;
        return this;
    }

    public Text WithFontWeight(FontWeight weight)
    {
        FontWeight = weight;
        return this;
    }

    public Text WithColor(Color color)
    {
        Color = color;
        return this;
    }

    public Text WithFontFamily(string family)
    {
        FontFamily = family;
        return this;
    }

    public Text WithLineHeight(double lineHeight)
    {
        LineHeight = lineHeight;
        return this;
    }

    public Text WithAlignment(TextAlignment alignment)
    {
        Alignment = alignment;
        return this;
    }

    public Text Underline()
    {
        Decoration = TextDecoration.Underline;
        return this;
    }

    public Text Strikethrough()
    {
        Decoration = TextDecoration.Strikethrough;
        return this;
    }

    public Text WithMaxLines(int maxLines)
    {
        MaxLines = maxLines;
        return this;
    }

    public Text NoWrap()
    {
        WordWrap = false;
        return this;
    }
}

/// <summary>
/// Text alignment options.
/// </summary>
public enum TextAlignment
{
    Start,
    Center,
    End,
    Justify
}

/// <summary>
/// Text decoration options.
/// </summary>
public enum TextDecoration
{
    None,
    Underline,
    Overline,
    Strikethrough
}
