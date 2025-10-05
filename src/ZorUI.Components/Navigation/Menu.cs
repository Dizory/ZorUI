using ZorUI.Core;
using ZorUI.Components.Layout;

namespace ZorUI.Components.Navigation;

/// <summary>
/// Menu component for displaying a list of options.
/// Similar to Radix UI DropdownMenu or ContextMenu.
/// </summary>
public class Menu : Container
{
    /// <summary>
    /// Whether the menu is currently open.
    /// </summary>
    public bool IsOpen { get; set; } = false;

    /// <summary>
    /// Trigger element that opens the menu.
    /// </summary>
    public ZorElement? Trigger { get; set; }

    /// <summary>
    /// Menu items.
    /// </summary>
    public List<MenuItem> Items { get; } = new();

    /// <summary>
    /// Called when the menu opens.
    /// </summary>
    public Action? OnOpen { get; set; }

    /// <summary>
    /// Called when the menu closes.
    /// </summary>
    public Action? OnClose { get; set; }

    /// <summary>
    /// Creates a new menu.
    /// </summary>
    public Menu()
    {
    }

    /// <summary>
    /// Adds a menu item.
    /// </summary>
    public Menu AddItem(MenuItem item)
    {
        Items.Add(item);
        AddChild(item);
        return this;
    }

    /// <summary>
    /// Adds a separator.
    /// </summary>
    public Menu AddSeparator()
    {
        Items.Add(new MenuSeparator());
        return this;
    }

    /// <summary>
    /// Opens the menu.
    /// </summary>
    public void Open()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            OnOpen?.Invoke();
            MarkNeedsRebuild();
        }
    }

    /// <summary>
    /// Closes the menu.
    /// </summary>
    public void Close()
    {
        if (IsOpen)
        {
            IsOpen = false;
            OnClose?.Invoke();
            MarkNeedsRebuild();
        }
    }

    /// <summary>
    /// Toggles the menu.
    /// </summary>
    public void Toggle()
    {
        if (IsOpen)
            Close();
        else
            Open();
    }

    // Fluent API

    public Menu WithTrigger(ZorElement trigger)
    {
        Trigger = trigger;
        return this;
    }

    public Menu WithOnOpen(Action onOpen)
    {
        OnOpen = onOpen;
        return this;
    }

    public Menu WithOnClose(Action onClose)
    {
        OnClose = onClose;
        return this;
    }
}

/// <summary>
/// Individual menu item.
/// </summary>
public class MenuItem : Container
{
    /// <summary>
    /// Label text for the menu item.
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// Icon for the menu item (optional).
    /// </summary>
    public ZorElement? Icon { get; set; }

    /// <summary>
    /// Shortcut text to display (optional).
    /// </summary>
    public string? Shortcut { get; set; }

    /// <summary>
    /// Whether this item is disabled.
    /// </summary>
    public bool IsDisabled { get; set; } = false;

    /// <summary>
    /// Whether this item is currently selected/active.
    /// </summary>
    public bool IsSelected { get; set; } = false;

    /// <summary>
    /// Submenu items (for nested menus).
    /// </summary>
    public List<MenuItem>? SubItems { get; set; }

    /// <summary>
    /// Called when this item is clicked.
    /// </summary>
    public Action? OnClick { get; set; }

    /// <summary>
    /// Creates a new menu item.
    /// </summary>
    public MenuItem(string label, Action? onClick = null)
    {
        Label = label;
        OnClick = onClick;
    }

    // Fluent API

    public MenuItem WithIcon(ZorElement icon)
    {
        Icon = icon;
        return this;
    }

    public MenuItem WithShortcut(string shortcut)
    {
        Shortcut = shortcut;
        return this;
    }

    public MenuItem Disabled(bool disabled = true)
    {
        IsDisabled = disabled;
        return this;
    }

    public MenuItem Selected(bool selected = true)
    {
        IsSelected = selected;
        return this;
    }

    public MenuItem WithSubItems(params MenuItem[] items)
    {
        SubItems ??= new List<MenuItem>();
        SubItems.AddRange(items);
        return this;
    }

    public MenuItem WithOnClick(Action onClick)
    {
        OnClick = onClick;
        return this;
    }
}

/// <summary>
/// Menu separator for grouping menu items.
/// </summary>
public class MenuSeparator : MenuItem
{
    public MenuSeparator() : base("")
    {
    }
}
