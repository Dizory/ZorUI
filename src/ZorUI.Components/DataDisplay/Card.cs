using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;

namespace ZorUI.Components.DataDisplay;

/// <summary>
/// Card component for grouping related content.
/// Similar to Material Card or Bootstrap Card.
/// </summary>
public class Card : Container
{
    /// <summary>
    /// Header content.
    /// </summary>
    public ZorElement? Header { get; set; }

    /// <summary>
    /// Main content.
    /// </summary>
    public ZorElement? Content { get; set; }

    /// <summary>
    /// Footer content.
    /// </summary>
    public ZorElement? Footer { get; set; }

    /// <summary>
    /// Card variant.
    /// </summary>
    public CardVariant Variant { get; set; } = CardVariant.Elevated;

    /// <summary>
    /// Whether the card is clickable/interactive.
    /// </summary>
    public bool IsClickable { get; set; } = false;

    /// <summary>
    /// Called when the card is clicked (if clickable).
    /// </summary>
    public Action? OnClick { get; set; }

    /// <summary>
    /// Creates a new card.
    /// </summary>
    public Card()
    {
    }

    // Fluent API

    public Card WithHeader(ZorElement header)
    {
        Header = header;
        return this;
    }

    public Card WithContent(ZorElement content)
    {
        Content = content;
        return this;
    }

    public Card WithFooter(ZorElement footer)
    {
        Footer = footer;
        return this;
    }

    public Card WithVariant(CardVariant variant)
    {
        Variant = variant;
        return this;
    }

    public Card Elevated()
    {
        Variant = CardVariant.Elevated;
        return this;
    }

    public Card Outlined()
    {
        Variant = CardVariant.Outlined;
        return this;
    }

    public Card Filled()
    {
        Variant = CardVariant.Filled;
        return this;
    }

    public Card Clickable(Action? onClick = null)
    {
        IsClickable = true;
        if (onClick != null)
            OnClick = onClick;
        return this;
    }

    public Card WithOnClick(Action onClick)
    {
        OnClick = onClick;
        IsClickable = true;
        return this;
    }
}

/// <summary>
/// Card visual variants.
/// </summary>
public enum CardVariant
{
    /// <summary>
    /// Card with shadow elevation.
    /// </summary>
    Elevated,

    /// <summary>
    /// Card with border outline.
    /// </summary>
    Outlined,

    /// <summary>
    /// Card with filled background.
    /// </summary>
    Filled
}
