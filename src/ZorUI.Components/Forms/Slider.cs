using ZorUI.Core;

namespace ZorUI.Components.Forms;

/// <summary>
/// Slider component for selecting a numeric value from a range.
/// Similar to Radix UI Slider or HTML input[type="range"].
/// </summary>
public class Slider : ZorElement
{
    /// <summary>
    /// Current value of the slider.
    /// </summary>
    public double Value { get; set; } = 0;

    /// <summary>
    /// Minimum value.
    /// </summary>
    public double Min { get; set; } = 0;

    /// <summary>
    /// Maximum value.
    /// </summary>
    public double Max { get; set; } = 100;

    /// <summary>
    /// Step size for value increments.
    /// </summary>
    public double Step { get; set; } = 1;

    /// <summary>
    /// Whether the slider is disabled.
    /// </summary>
    public bool IsDisabled { get; set; } = false;

    /// <summary>
    /// Whether to show the current value.
    /// </summary>
    public bool ShowValue { get; set; } = false;

    /// <summary>
    /// Orientation of the slider.
    /// </summary>
    public SliderOrientation Orientation { get; set; } = SliderOrientation.Horizontal;

    /// <summary>
    /// Called when the value changes.
    /// </summary>
    public Action<double>? OnChange { get; set; }

    /// <summary>
    /// Called when the user finishes changing the value.
    /// </summary>
    public Action<double>? OnChangeEnd { get; set; }

    /// <summary>
    /// Creates a new slider.
    /// </summary>
    public Slider(double min = 0, double max = 100, double value = 0)
    {
        Min = min;
        Max = max;
        Value = Math.Clamp(value, min, max);
    }

    // Fluent API

    public Slider WithValue(double value)
    {
        Value = Math.Clamp(value, Min, Max);
        return this;
    }

    public Slider WithRange(double min, double max)
    {
        Min = min;
        Max = max;
        Value = Math.Clamp(Value, min, max);
        return this;
    }

    public Slider WithStep(double step)
    {
        Step = step;
        return this;
    }

    public Slider Disabled(bool disabled = true)
    {
        IsDisabled = disabled;
        return this;
    }

    public Slider WithShowValue(bool show = true)
    {
        ShowValue = show;
        return this;
    }

    public Slider Vertical()
    {
        Orientation = SliderOrientation.Vertical;
        return this;
    }

    public Slider WithOnChange(Action<double> onChange)
    {
        OnChange = onChange;
        return this;
    }

    public Slider WithOnChangeEnd(Action<double> onChangeEnd)
    {
        OnChangeEnd = onChangeEnd;
        return this;
    }

    /// <summary>
    /// Sets the value and triggers change event.
    /// </summary>
    public void SetValue(double value)
    {
        if (!IsDisabled)
        {
            var newValue = Math.Clamp(value, Min, Max);
            
            // Snap to step
            if (Step > 0)
            {
                newValue = Math.Round((newValue - Min) / Step) * Step + Min;
            }
            
            if (Value != newValue)
            {
                Value = newValue;
                OnChange?.Invoke(Value);
                MarkNeedsRebuild();
            }
        }
    }
}

/// <summary>
/// Slider orientation options.
/// </summary>
public enum SliderOrientation
{
    Horizontal,
    Vertical
}
