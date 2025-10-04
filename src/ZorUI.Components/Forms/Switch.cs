using ZorUI.Core;

namespace ZorUI.Components.Forms;

/// <summary>
/// Toggle switch component for boolean on/off states.
/// Similar to Radix UI Switch or iOS/Material toggle switch.
/// </summary>
public class Switch : ZorElement
{
    /// <summary>
    /// Whether the switch is on (true) or off (false).
    /// </summary>
    public bool IsOn { get; set; } = false;

    /// <summary>
    /// Whether the switch is disabled.
    /// </summary>
    public bool IsDisabled { get; set; } = false;

    /// <summary>
    /// Label text for the switch.
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Size of the switch.
    /// </summary>
    public SwitchSize Size { get; set; } = SwitchSize.Medium;

    /// <summary>
    /// Called when the switch state changes.
    /// </summary>
    public Action<bool>? OnChange { get; set; }

    /// <summary>
    /// Creates a new switch.
    /// </summary>
    public Switch(string? label = null, bool isOn = false)
    {
        Label = label;
        IsOn = isOn;
    }

    // Fluent API

    public Switch On(bool on = true)
    {
        IsOn = on;
        return this;
    }

    public Switch Off()
    {
        IsOn = false;
        return this;
    }

    public Switch Disabled(bool disabled = true)
    {
        IsDisabled = disabled;
        return this;
    }

    public Switch WithLabel(string label)
    {
        Label = label;
        return this;
    }

    public Switch WithSize(SwitchSize size)
    {
        Size = size;
        return this;
    }

    public Switch Small()
    {
        Size = SwitchSize.Small;
        return this;
    }

    public Switch Large()
    {
        Size = SwitchSize.Large;
        return this;
    }

    public Switch WithOnChange(Action<bool> onChange)
    {
        OnChange = onChange;
        return this;
    }

    /// <summary>
    /// Toggles the switch state.
    /// </summary>
    public void Toggle()
    {
        if (!IsDisabled)
        {
            IsOn = !IsOn;
            OnChange?.Invoke(IsOn);
            MarkNeedsRebuild();
        }
    }
}

/// <summary>
/// Switch size options.
/// </summary>
public enum SwitchSize
{
    Small,
    Medium,
    Large
}
