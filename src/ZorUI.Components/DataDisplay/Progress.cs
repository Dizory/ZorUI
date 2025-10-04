using ZorUI.Core;
using ZorUI.Core.Properties;

namespace ZorUI.Components.DataDisplay;

/// <summary>
/// Progress bar component for indicating progress or loading state.
/// Similar to Radix UI Progress or Material LinearProgress.
/// </summary>
public class Progress : ZorElement
{
    /// <summary>
    /// Current progress value (0-100).
    /// </summary>
    public double Value { get; set; } = 0;

    /// <summary>
    /// Maximum value (default 100).
    /// </summary>
    public double Max { get; set; } = 100;

    /// <summary>
    /// Whether this is an indeterminate progress (loading spinner).
    /// </summary>
    public bool IsIndeterminate { get; set; } = false;

    /// <summary>
    /// Size of the progress bar.
    /// </summary>
    public ProgressSize Size { get; set; } = ProgressSize.Medium;

    /// <summary>
    /// Variant/color of the progress bar.
    /// </summary>
    public ProgressVariant Variant { get; set; } = ProgressVariant.Primary;

    /// <summary>
    /// Whether to show the percentage text.
    /// </summary>
    public bool ShowValue { get; set; } = false;

    /// <summary>
    /// Custom label text (overrides percentage).
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Creates a new progress bar.
    /// </summary>
    public Progress(double value = 0, double max = 100)
    {
        Value = Math.Clamp(value, 0, max);
        Max = max;
    }

    /// <summary>
    /// Gets the progress as a percentage (0-100).
    /// </summary>
    public double Percentage => Max > 0 ? (Value / Max) * 100 : 0;

    // Fluent API

    public Progress WithValue(double value)
    {
        Value = Math.Clamp(value, 0, Max);
        MarkNeedsRebuild();
        return this;
    }

    public Progress WithMax(double max)
    {
        Max = max;
        Value = Math.Clamp(Value, 0, max);
        return this;
    }

    public Progress Indeterminate(bool indeterminate = true)
    {
        IsIndeterminate = indeterminate;
        return this;
    }

    public Progress WithSize(ProgressSize size)
    {
        Size = size;
        return this;
    }

    public Progress Small()
    {
        Size = ProgressSize.Small;
        return this;
    }

    public Progress Large()
    {
        Size = ProgressSize.Large;
        return this;
    }

    public Progress WithVariant(ProgressVariant variant)
    {
        Variant = variant;
        return this;
    }

    public Progress Primary()
    {
        Variant = ProgressVariant.Primary;
        return this;
    }

    public Progress Success()
    {
        Variant = ProgressVariant.Success;
        return this;
    }

    public Progress Warning()
    {
        Variant = ProgressVariant.Warning;
        return this;
    }

    public Progress Error()
    {
        Variant = ProgressVariant.Error;
        return this;
    }

    public Progress WithShowValue(bool show = true)
    {
        ShowValue = show;
        return this;
    }

    public Progress WithLabel(string label)
    {
        Label = label;
        return this;
    }

    /// <summary>
    /// Increments the progress value.
    /// </summary>
    public void Increment(double amount = 1)
    {
        WithValue(Value + amount);
    }
}

/// <summary>
/// Progress bar size options.
/// </summary>
public enum ProgressSize
{
    Small,
    Medium,
    Large
}

/// <summary>
/// Progress bar variants.
/// </summary>
public enum ProgressVariant
{
    Primary,
    Secondary,
    Success,
    Warning,
    Error
}
