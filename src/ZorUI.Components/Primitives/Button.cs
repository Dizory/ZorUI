using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;

namespace ZorUI.Components.Primitives;

/// <summary>
/// Interactive button component.
/// Similar to Radix UI Button or HTML button.
/// </summary>
public class Button : Container
{
    /// <summary>
    /// Button variant (visual style).
    /// </summary>
    public ButtonVariant Variant { get; set; } = ButtonVariant.Default;

    /// <summary>
    /// Button size.
    /// </summary>
    public ButtonSize Size { get; set; } = ButtonSize.Medium;

    /// <summary>
    /// Whether the button is disabled.
    /// </summary>
    public bool IsDisabled { get; set; } = false;

    /// <summary>
    /// Whether the button is in loading state.
    /// </summary>
    public bool IsLoading { get; set; } = false;

    /// <summary>
    /// Whether the button takes full width of its container.
    /// </summary>
    public bool FullWidth { get; set; } = false;

    /// <summary>
    /// Click event handler.
    /// </summary>
    public Action? OnClick { get; set; }

    /// <summary>
    /// Press event handler (mouse/touch down).
    /// </summary>
    public Action? OnPress { get; set; }

    /// <summary>
    /// Release event handler (mouse/touch up).
    /// </summary>
    public Action? OnRelease { get; set; }

    /// <summary>
    /// Icon to display before content.
    /// </summary>
    public ZorElement? LeadingIcon { get; set; }

    /// <summary>
    /// Icon to display after content.
    /// </summary>
    public ZorElement? TrailingIcon { get; set; }

    /// <summary>
    /// Creates a button with text content.
    /// </summary>
    public Button(string text, Action? onClick = null)
    {
        AddChild(new Text(text));
        OnClick = onClick;
    }

    /// <summary>
    /// Creates a button with custom content.
    /// </summary>
    public Button(ZorElement content, Action? onClick = null)
    {
        AddChild(content);
        OnClick = onClick;
    }

    // Fluent API

    public Button Primary()
    {
        Variant = ButtonVariant.Primary;
        return this;
    }

    public Button Secondary()
    {
        Variant = ButtonVariant.Secondary;
        return this;
    }

    public Button Destructive()
    {
        Variant = ButtonVariant.Destructive;
        return this;
    }

    public Button Ghost()
    {
        Variant = ButtonVariant.Ghost;
        return this;
    }

    public Button Link()
    {
        Variant = ButtonVariant.Link;
        return this;
    }

    public Button WithVariant(ButtonVariant variant)
    {
        Variant = variant;
        return this;
    }

    public Button Small()
    {
        Size = ButtonSize.Small;
        return this;
    }

    public Button Large()
    {
        Size = ButtonSize.Large;
        return this;
    }

    public Button WithSize(ButtonSize size)
    {
        Size = size;
        return this;
    }

    public Button Disabled(bool disabled = true)
    {
        IsDisabled = disabled;
        return this;
    }

    public Button Loading(bool loading = true)
    {
        IsLoading = loading;
        return this;
    }

    public Button WithFullWidth(bool fullWidth = true)
    {
        FullWidth = fullWidth;
        return this;
    }

    public Button WithLeadingIcon(ZorElement icon)
    {
        LeadingIcon = icon;
        return this;
    }

    public Button WithTrailingIcon(ZorElement icon)
    {
        TrailingIcon = icon;
        return this;
    }

    public Button WithOnClick(Action onClick)
    {
        OnClick = onClick;
        return this;
    }
}

/// <summary>
/// Button visual variants.
/// </summary>
public enum ButtonVariant
{
    /// <summary>
    /// Default button style.
    /// </summary>
    Default,

    /// <summary>
    /// Primary action button (emphasized).
    /// </summary>
    Primary,

    /// <summary>
    /// Secondary action button.
    /// </summary>
    Secondary,

    /// <summary>
    /// Destructive action button (e.g., delete).
    /// </summary>
    Destructive,

    /// <summary>
    /// Ghost button (minimal styling).
    /// </summary>
    Ghost,

    /// <summary>
    /// Link-styled button.
    /// </summary>
    Link,

    /// <summary>
    /// Outlined button.
    /// </summary>
    Outline
}

/// <summary>
/// Button size options.
/// </summary>
public enum ButtonSize
{
    Small,
    Medium,
    Large
}
