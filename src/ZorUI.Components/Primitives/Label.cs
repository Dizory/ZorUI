using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;

namespace ZorUI.Components.Primitives;

/// <summary>
/// Label component for form inputs and accessibility.
/// Similar to Radix UI Label or HTML label.
/// </summary>
public class Label : Container
{
    /// <summary>
    /// ID of the element this label is associated with.
    /// </summary>
    public string? ForId { get; set; }

    /// <summary>
    /// Whether this label is required (shows indicator).
    /// </summary>
    public bool Required { get; set; } = false;

    /// <summary>
    /// Creates a label with text content.
    /// </summary>
    public Label(string text, string? forId = null)
    {
        AddChild(new Text(text));
        ForId = forId;
    }

    /// <summary>
    /// Creates a label with custom content.
    /// </summary>
    public Label(ZorElement content, string? forId = null)
    {
        AddChild(content);
        ForId = forId;
    }

    // Fluent API

    public Label For(string id)
    {
        ForId = id;
        return this;
    }

    public Label IsRequired(bool required = true)
    {
        Required = required;
        return this;
    }
}
