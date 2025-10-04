using ZorUI.Core;

namespace ZorUI.Components.Forms;

/// <summary>
/// Checkbox component for boolean selection.
/// Similar to Radix UI Checkbox or HTML input[type="checkbox"].
/// </summary>
public class Checkbox : ZorElement
{
    /// <summary>
    /// Whether the checkbox is checked.
    /// </summary>
    public bool IsChecked { get; set; } = false;

    /// <summary>
    /// Whether the checkbox is in indeterminate state (partially checked).
    /// </summary>
    public bool IsIndeterminate { get; set; } = false;

    /// <summary>
    /// Whether the checkbox is disabled.
    /// </summary>
    public bool IsDisabled { get; set; } = false;

    /// <summary>
    /// Whether the checkbox is required.
    /// </summary>
    public bool IsRequired { get; set; } = false;

    /// <summary>
    /// Label text for the checkbox.
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Called when the checked state changes.
    /// </summary>
    public Action<bool>? OnChange { get; set; }

    /// <summary>
    /// Creates a new checkbox.
    /// </summary>
    public Checkbox(string? label = null, bool isChecked = false)
    {
        Label = label;
        IsChecked = isChecked;
    }

    // Fluent API

    public Checkbox Checked(bool isChecked = true)
    {
        IsChecked = isChecked;
        IsIndeterminate = false;
        return this;
    }

    public Checkbox Indeterminate(bool indeterminate = true)
    {
        IsIndeterminate = indeterminate;
        return this;
    }

    public Checkbox Disabled(bool disabled = true)
    {
        IsDisabled = disabled;
        return this;
    }

    public Checkbox Required(bool required = true)
    {
        IsRequired = required;
        return this;
    }

    public Checkbox WithLabel(string label)
    {
        Label = label;
        return this;
    }

    public Checkbox WithOnChange(Action<bool> onChange)
    {
        OnChange = onChange;
        return this;
    }

    /// <summary>
    /// Toggles the checked state.
    /// </summary>
    public void Toggle()
    {
        if (!IsDisabled)
        {
            IsChecked = !IsChecked;
            IsIndeterminate = false;
            OnChange?.Invoke(IsChecked);
            MarkNeedsRebuild();
        }
    }
}
