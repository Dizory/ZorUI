using ZorUI.Core;

namespace ZorUI.Components.Forms;

/// <summary>
/// Radio button component for single selection within a group.
/// Similar to Radix UI Radio or HTML input[type="radio"].
/// </summary>
public class Radio : ZorElement
{
    /// <summary>
    /// Value of this radio button.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Whether this radio button is selected.
    /// </summary>
    public bool IsSelected { get; set; } = false;

    /// <summary>
    /// Whether this radio button is disabled.
    /// </summary>
    public bool IsDisabled { get; set; } = false;

    /// <summary>
    /// Label text for the radio button.
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Name of the radio group this button belongs to.
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// Called when this radio button is selected.
    /// </summary>
    public Action<string>? OnSelect { get; set; }

    /// <summary>
    /// Creates a new radio button.
    /// </summary>
    public Radio(string value, string? label = null)
    {
        Value = value;
        Label = label ?? value;
    }

    // Fluent API

    public Radio Selected(bool selected = true)
    {
        IsSelected = selected;
        return this;
    }

    public Radio Disabled(bool disabled = true)
    {
        IsDisabled = disabled;
        return this;
    }

    public Radio WithLabel(string label)
    {
        Label = label;
        return this;
    }

    public Radio InGroup(string groupName)
    {
        GroupName = groupName;
        return this;
    }

    public Radio WithOnSelect(Action<string> onSelect)
    {
        OnSelect = onSelect;
        return this;
    }

    /// <summary>
    /// Selects this radio button.
    /// </summary>
    public void Select()
    {
        if (!IsDisabled && !IsSelected)
        {
            IsSelected = true;
            OnSelect?.Invoke(Value);
            MarkNeedsRebuild();
        }
    }
}

/// <summary>
/// Radio group container that manages a collection of radio buttons.
/// Ensures only one radio button is selected at a time.
/// </summary>
public class RadioGroup : ZorElement
{
    /// <summary>
    /// Currently selected value.
    /// </summary>
    public string? SelectedValue { get; set; }

    /// <summary>
    /// Name of this radio group.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Whether the group is disabled.
    /// </summary>
    public bool IsDisabled { get; set; } = false;

    /// <summary>
    /// Called when the selected value changes.
    /// </summary>
    public Action<string>? OnValueChange { get; set; }

    /// <summary>
    /// Radio buttons in this group.
    /// </summary>
    public List<Radio> Radios { get; } = new();

    /// <summary>
    /// Creates a new radio group.
    /// </summary>
    public RadioGroup(string? name = null)
    {
        Name = name;
    }

    /// <summary>
    /// Adds a radio button to this group.
    /// </summary>
    public RadioGroup AddRadio(Radio radio)
    {
        radio.GroupName = Name;
        radio.OnSelect = SelectValue;
        radio.IsDisabled = IsDisabled || radio.IsDisabled;
        Radios.Add(radio);
        AddChild(radio);
        return this;
    }

    /// <summary>
    /// Selects a value in the group.
    /// </summary>
    public void SelectValue(string value)
    {
        if (SelectedValue != value)
        {
            SelectedValue = value;
            
            // Update all radios in the group
            foreach (var radio in Radios)
            {
                radio.IsSelected = radio.Value == value;
                radio.MarkNeedsRebuild();
            }
            
            OnValueChange?.Invoke(value);
            MarkNeedsRebuild();
        }
    }

    // Fluent API

    public RadioGroup Disabled(bool disabled = true)
    {
        IsDisabled = disabled;
        foreach (var radio in Radios)
        {
            radio.IsDisabled = disabled;
        }
        return this;
    }

    public RadioGroup WithOnValueChange(Action<string> onValueChange)
    {
        OnValueChange = onValueChange;
        return this;
    }
}
