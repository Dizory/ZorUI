using ZorUI.Core.Properties;

namespace ZorUI.Styling;

/// <summary>
/// Represents a collection of visual properties that can be applied to elements.
/// Similar to CSS styles or SwiftUI ViewModifiers.
/// </summary>
public class Style
{
    private readonly Dictionary<string, object?> _properties = new();

    /// <summary>
    /// Gets or sets a style property.
    /// </summary>
    public object? this[string key]
    {
        get => _properties.TryGetValue(key, out var value) ? value : null;
        set => _properties[key] = value;
    }

    /// <summary>
    /// Gets a strongly-typed property value.
    /// </summary>
    public T? Get<T>(string key, T? defaultValue = default)
    {
        if (_properties.TryGetValue(key, out var value) && value is T typedValue)
        {
            return typedValue;
        }
        return defaultValue;
    }

    /// <summary>
    /// Sets a property value.
    /// </summary>
    public Style Set<T>(string key, T value)
    {
        _properties[key] = value;
        return this;
    }

    /// <summary>
    /// Merges another style into this one.
    /// Properties from the other style override this one's properties.
    /// </summary>
    public Style Merge(Style other)
    {
        foreach (var kvp in other._properties)
        {
            _properties[kvp.Key] = kvp.Value;
        }
        return this;
    }

    /// <summary>
    /// Creates a copy of this style.
    /// </summary>
    public Style Clone()
    {
        var clone = new Style();
        foreach (var kvp in _properties)
        {
            clone._properties[kvp.Key] = kvp.Value;
        }
        return clone;
    }

    // Common style properties
    public Color? BackgroundColor
    {
        get => Get<Color?>(nameof(BackgroundColor));
        set => Set(nameof(BackgroundColor), value);
    }

    public Color? ForegroundColor
    {
        get => Get<Color?>(nameof(ForegroundColor));
        set => Set(nameof(ForegroundColor), value);
    }

    public Color? BorderColor
    {
        get => Get<Color?>(nameof(BorderColor));
        set => Set(nameof(BorderColor), value);
    }

    public double? BorderWidth
    {
        get => Get<double?>(nameof(BorderWidth));
        set => Set(nameof(BorderWidth), value);
    }

    public double? BorderRadius
    {
        get => Get<double?>(nameof(BorderRadius));
        set => Set(nameof(BorderRadius), value);
    }

    public EdgeInsets? Padding
    {
        get => Get<EdgeInsets?>(nameof(Padding));
        set => Set(nameof(Padding), value);
    }

    public EdgeInsets? Margin
    {
        get => Get<EdgeInsets?>(nameof(Margin));
        set => Set(nameof(Margin), value);
    }

    public double? Width
    {
        get => Get<double?>(nameof(Width));
        set => Set(nameof(Width), value);
    }

    public double? Height
    {
        get => Get<double?>(nameof(Height));
        set => Set(nameof(Height), value);
    }

    public double? MinWidth
    {
        get => Get<double?>(nameof(MinWidth));
        set => Set(nameof(MinWidth), value);
    }

    public double? MaxWidth
    {
        get => Get<double?>(nameof(MaxWidth));
        set => Set(nameof(MaxWidth), value);
    }

    public double? MinHeight
    {
        get => Get<double?>(nameof(MinHeight));
        set => Set(nameof(MinHeight), value);
    }

    public double? MaxHeight
    {
        get => Get<double?>(nameof(MaxHeight));
        set => Set(nameof(MaxHeight), value);
    }

    public double? Opacity
    {
        get => Get<double?>(nameof(Opacity));
        set => Set(nameof(Opacity), value);
    }

    public double? FontSize
    {
        get => Get<double?>(nameof(FontSize));
        set => Set(nameof(FontSize), value);
    }

    public FontWeight? FontWeight
    {
        get => Get<FontWeight?>(nameof(FontWeight));
        set => Set(nameof(FontWeight), value);
    }

    public string? FontFamily
    {
        get => Get<string?>(nameof(FontFamily));
        set => Set(nameof(FontFamily), value);
    }
}

/// <summary>
/// Font weight options.
/// </summary>
public enum FontWeight
{
    Thin = 100,
    ExtraLight = 200,
    Light = 300,
    Normal = 400,
    Medium = 500,
    SemiBold = 600,
    Bold = 700,
    ExtraBold = 800,
    Black = 900
}
