using ZorUI.Core;
using ZorUI.Components.Layout;

namespace ZorUI.Components.DataDisplay;

/// <summary>
/// Accordion component for collapsible content sections.
/// Similar to Radix UI Accordion or Bootstrap Accordion.
/// </summary>
public class Accordion : Container
{
    /// <summary>
    /// Type of accordion (single or multiple items can be open).
    /// </summary>
    public AccordionType Type { get; set; } = AccordionType.Single;

    /// <summary>
    /// Whether items can be collapsed (false = always one item open).
    /// </summary>
    public bool Collapsible { get; set; } = true;

    /// <summary>
    /// Currently open item values.
    /// </summary>
    public HashSet<string> OpenItems { get; } = new();

    /// <summary>
    /// Accordion items.
    /// </summary>
    public List<AccordionItem> Items { get; } = new();

    /// <summary>
    /// Called when an item is opened or closed.
    /// </summary>
    public Action<string, bool>? OnItemToggle { get; set; }

    /// <summary>
    /// Creates a new accordion.
    /// </summary>
    public Accordion()
    {
    }

    /// <summary>
    /// Adds an accordion item.
    /// </summary>
    public Accordion AddItem(AccordionItem item)
    {
        Items.Add(item);
        AddChild(item);
        
        if (item.IsOpen)
        {
            OpenItems.Add(item.Value);
        }
        
        return this;
    }

    /// <summary>
    /// Toggles an item open/closed.
    /// </summary>
    public void ToggleItem(string value)
    {
        var item = Items.FirstOrDefault(i => i.Value == value);
        if (item == null || item.IsDisabled) return;

        bool isOpen = OpenItems.Contains(value);

        if (Type == AccordionType.Single)
        {
            // Close all other items
            OpenItems.Clear();
            
            if (!isOpen || Collapsible)
            {
                if (!isOpen)
                    OpenItems.Add(value);
            }
            else
            {
                OpenItems.Add(value);
            }
        }
        else
        {
            if (isOpen)
                OpenItems.Remove(value);
            else
                OpenItems.Add(value);
        }

        // Update item states
        foreach (var i in Items)
        {
            i.IsOpen = OpenItems.Contains(i.Value);
            i.MarkNeedsRebuild();
        }

        OnItemToggle?.Invoke(value, OpenItems.Contains(value));
        MarkNeedsRebuild();
    }

    // Fluent API

    public Accordion Single()
    {
        Type = AccordionType.Single;
        return this;
    }

    public Accordion Multiple()
    {
        Type = AccordionType.Multiple;
        return this;
    }

    public Accordion WithCollapsible(bool collapsible = true)
    {
        Collapsible = collapsible;
        return this;
    }

    public Accordion WithOnItemToggle(Action<string, bool> onItemToggle)
    {
        OnItemToggle = onItemToggle;
        return this;
    }
}

/// <summary>
/// Individual accordion item.
/// </summary>
public class AccordionItem : Container
{
    /// <summary>
    /// Unique value identifier for this item.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Trigger content (header).
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Content to display when expanded.
    /// </summary>
    public ZorElement? Content { get; set; }

    /// <summary>
    /// Whether this item is currently open.
    /// </summary>
    public bool IsOpen { get; set; } = false;

    /// <summary>
    /// Whether this item is disabled.
    /// </summary>
    public bool IsDisabled { get; set; } = false;

    /// <summary>
    /// Creates a new accordion item.
    /// </summary>
    public AccordionItem(string value, string title)
    {
        Value = value;
        Title = title;
    }

    // Fluent API

    public AccordionItem WithContent(ZorElement content)
    {
        Content = content;
        return this;
    }

    public AccordionItem Open(bool open = true)
    {
        IsOpen = open;
        return this;
    }

    public AccordionItem Disabled(bool disabled = true)
    {
        IsDisabled = disabled;
        return this;
    }
}

/// <summary>
/// Accordion type options.
/// </summary>
public enum AccordionType
{
    /// <summary>
    /// Only one item can be open at a time.
    /// </summary>
    Single,

    /// <summary>
    /// Multiple items can be open simultaneously.
    /// </summary>
    Multiple
}
