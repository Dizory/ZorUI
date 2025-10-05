using ZorUI.Core;
using ZorUI.Components.Layout;

namespace ZorUI.Components.Navigation;

/// <summary>
/// Tabs component for organizing content into switchable panels.
/// Similar to Radix UI Tabs or Material Tabs.
/// </summary>
public class Tabs : Container
{
    /// <summary>
    /// Currently active tab value.
    /// </summary>
    public string ActiveTab { get; set; }

    /// <summary>
    /// Orientation of the tabs.
    /// </summary>
    public TabsOrientation Orientation { get; set; } = TabsOrientation.Horizontal;

    /// <summary>
    /// Variant of the tabs.
    /// </summary>
    public TabsVariant Variant { get; set; } = TabsVariant.Default;

    /// <summary>
    /// List of tab items.
    /// </summary>
    public List<Tab> TabItems { get; } = new();

    /// <summary>
    /// Called when the active tab changes.
    /// </summary>
    public Action<string>? OnTabChange { get; set; }

    /// <summary>
    /// Creates a new tabs container.
    /// </summary>
    public Tabs(string defaultTab = "")
    {
        ActiveTab = defaultTab;
    }

    /// <summary>
    /// Adds a tab to the container.
    /// </summary>
    public Tabs AddTab(Tab tab)
    {
        TabItems.Add(tab);
        AddChild(tab);
        
        // Set first tab as active if none selected
        if (string.IsNullOrEmpty(ActiveTab) && TabItems.Count == 1)
        {
            ActiveTab = tab.Value;
        }
        
        return this;
    }

    /// <summary>
    /// Switches to a tab by value.
    /// </summary>
    public void SwitchTo(string tabValue)
    {
        if (ActiveTab != tabValue)
        {
            var tab = TabItems.FirstOrDefault(t => t.Value == tabValue);
            if (tab != null && !tab.IsDisabled)
            {
                ActiveTab = tabValue;
                OnTabChange?.Invoke(tabValue);
                MarkNeedsRebuild();
            }
        }
    }

    // Fluent API

    public Tabs WithOrientation(TabsOrientation orientation)
    {
        Orientation = orientation;
        return this;
    }

    public Tabs WithVariant(TabsVariant variant)
    {
        Variant = variant;
        return this;
    }

    public Tabs Vertical()
    {
        Orientation = TabsOrientation.Vertical;
        return this;
    }

    public Tabs WithOnTabChange(Action<string> onTabChange)
    {
        OnTabChange = onTabChange;
        return this;
    }
}

/// <summary>
/// Individual tab item.
/// </summary>
public class Tab : Container
{
    /// <summary>
    /// Unique value identifier for this tab.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Label text for the tab.
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// Content to display when this tab is active.
    /// </summary>
    public ZorElement? Content { get; set; }

    /// <summary>
    /// Whether this tab is disabled.
    /// </summary>
    public bool IsDisabled { get; set; } = false;

    /// <summary>
    /// Icon to display in the tab (optional).
    /// </summary>
    public ZorElement? Icon { get; set; }

    /// <summary>
    /// Creates a new tab.
    /// </summary>
    public Tab(string value, string label)
    {
        Value = value;
        Label = label;
    }

    // Fluent API

    public Tab WithContent(ZorElement content)
    {
        Content = content;
        return this;
    }

    public Tab Disabled(bool disabled = true)
    {
        IsDisabled = disabled;
        return this;
    }

    public Tab WithIcon(ZorElement icon)
    {
        Icon = icon;
        return this;
    }
}

/// <summary>
/// Tabs orientation options.
/// </summary>
public enum TabsOrientation
{
    Horizontal,
    Vertical
}

/// <summary>
/// Tabs visual variants.
/// </summary>
public enum TabsVariant
{
    Default,
    Pills,
    Underlined,
    Bordered
}
