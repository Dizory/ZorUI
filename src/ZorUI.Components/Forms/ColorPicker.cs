using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;

namespace ZorUI.Components.Forms;

/// <summary>
/// Color picker component for selecting colors.
/// </summary>
public class ColorPicker : ZorComponent
{
    private Color _value = Color.White;
    private bool _isOpen = false;
    private ColorPickerMode _mode = ColorPickerMode.Presets;

    public Color Value
    {
        get => _value;
        set
        {
            _value = value;
            MarkNeedsRebuild();
        }
    }

    public Action<Color>? OnChange { get; set; }

    public override ZorElement Build()
    {
        var trigger = new Container()
            .WithPadding(EdgeInsets.All(12))
            .WithBackground(_value)
            .Metadata["OnClick"] = new Action(() =>
            {
                _isOpen = !_isOpen;
                MarkNeedsRebuild();
            });

        if (!_isOpen)
        {
            return trigger;
        }

        return new VStack(spacing: 8)
            .AddChild(trigger)
            .AddChild(BuildPicker());
    }

    private ZorElement BuildPicker()
    {
        return new VStack(spacing: 12)
            .WithPadding(EdgeInsets.All(16))
            .AddChild(BuildModeTabs())
            .AddChild(_mode == ColorPickerMode.Presets 
                ? BuildPresets() 
                : BuildCustom());
    }

    private ZorElement BuildModeTabs()
    {
        return new HStack(spacing: 8)
            .AddChild(new Button("Presets", () =>
            {
                _mode = ColorPickerMode.Presets;
                MarkNeedsRebuild();
            }).Ghost())
            .AddChild(new Button("Custom", () =>
            {
                _mode = ColorPickerMode.Custom;
                MarkNeedsRebuild();
            }).Ghost());
    }

    private ZorElement BuildPresets()
    {
        var presetColors = new[]
        {
            Color.Red, Color.Green, Color.Blue,
            Color.Yellow, Color.Cyan, Color.Magenta,
            Color.Black, Color.White, Color.Gray,
            Color.FromHex("#FF6B6B"), Color.FromHex("#4ECDC4"), Color.FromHex("#45B7D1"),
            Color.FromHex("#F7B731"), Color.FromHex("#5F27CD"), Color.FromHex("#00D2D3")
        };

        var grid = new Grid(columns: 6, spacing: 8);

        foreach (var color in presetColors)
        {
            var colorBox = new Container()
                .WithPadding(EdgeInsets.All(20))
                .WithBackground(color)
                .Metadata["OnClick"] = new Action(() => SelectColor(color));

            grid.AddChild(colorBox);
        }

        return grid;
    }

    private ZorElement BuildCustom()
    {
        // Simple RGB sliders
        return new VStack(spacing: 12)
            .AddChild(new Text("RGB Sliders:"))
            .AddChild(BuildRGBSliders());
    }

    private ZorElement BuildRGBSliders()
    {
        var (r, g, b, a) = _value.ToRGBA();

        return new VStack(spacing: 8)
            .AddChild(BuildSlider("R", r, v => UpdateColor(r: (byte)v)))
            .AddChild(BuildSlider("G", g, v => UpdateColor(g: (byte)v)))
            .AddChild(BuildSlider("B", b, v => UpdateColor(b: (byte)v)))
            .AddChild(BuildSlider("A", a, v => UpdateColor(a: (byte)v)));
    }

    private ZorElement BuildSlider(string label, byte value, Action<double> onChange)
    {
        return new HStack(spacing: 8)
            .AddChild(new Text($"{label}:").WithWidth(20))
            .AddChild(new Slider(value, 0, 255)
                .WithOnChange(onChange))
            .AddChild(new Text(value.ToString()).WithWidth(30));
    }

    private void UpdateColor(byte? r = null, byte? g = null, byte? b = null, byte? a = null)
    {
        var (currentR, currentG, currentB, currentA) = _value.ToRGBA();
        _value = Color.FromRGBA(
            r ?? currentR,
            g ?? currentG,
            b ?? currentB,
            a ?? currentA
        );
        OnChange?.Invoke(_value);
        MarkNeedsRebuild();
    }

    private void SelectColor(Color color)
    {
        _value = color;
        _isOpen = false;
        OnChange?.Invoke(_value);
        MarkNeedsRebuild();
    }

    // Fluent API

    public ColorPicker WithValue(Color value)
    {
        Value = value;
        return this;
    }

    public ColorPicker WithOnChange(Action<Color> handler)
    {
        OnChange = handler;
        return this;
    }
}

public enum ColorPickerMode
{
    Presets,
    Custom
}
