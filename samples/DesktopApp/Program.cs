using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Components.Forms;
using ZorUI.Components.DataDisplay;
using ZorUI.Styling;
using ZorUI.Rendering.Skia;

namespace DesktopApp;

/// <summary>
/// Cross-platform desktop application using ZorUI with SkiaSharp renderer.
/// Works on Windows, Linux, and macOS!
/// </summary>
class Program
{
    [STAThread]
    static void Main()
    {
        // Create and show window
        var window = new SkiaWindow(900, 700, "ZorUI Desktop App - Cross Platform!");
        window.RootComponent = new MyDesktopApp();
        window.Show();
    }
}

/// <summary>
/// Main application component.
/// This exact same code works on Windows, Linux, and macOS!
/// </summary>
public class MyDesktopApp : ZorComponent
{
    // Application state
    private int _counter = 0;
    private string _name = "";
    private string _email = "";
    private bool _darkMode = false;
    private int _sliderValue = 50;

    public override ZorElement Build()
    {
        // Choose theme
        var theme = _darkMode ? Theme.Dark() : Theme.Light();

        return new VStack(spacing: theme.Spacing.Space6)
            .WithPadding(EdgeInsets.All(theme.Spacing.Space8))
            .WithBackground(theme.Colors.Background)
            .AddChild(BuildHeader(theme))
            .AddChild(BuildDivider(theme))
            .AddChild(BuildCounterSection(theme))
            .AddChild(BuildDivider(theme))
            .AddChild(BuildFormSection(theme))
            .AddChild(BuildDivider(theme))
            .AddChild(BuildFooter(theme));
    }

    private ZorElement BuildHeader(Theme theme)
    {
        return new VStack(spacing: theme.Spacing.Space3)
            .AddChild(
                new Text("🎨 ZorUI Desktop Application")
                    .WithFontSize(theme.Typography.FontSize4Xl)
                    .Bold()
            )
            .AddChild(
                new Text("Cross-platform GUI framework for .NET")
                    .WithFontSize(theme.Typography.FontSizeLg)
            )
            .AddChild(
                new HStack(spacing: theme.Spacing.Space3)
                    .AddChild(new Badge("Windows").Primary())
                    .AddChild(new Badge("Linux").Success())
                    .AddChild(new Badge("macOS").Info())
            )
            .AddChild(
                new Switch(_darkMode ? "Dark Mode 🌙" : "Light Mode ☀️", _darkMode)
                    .WithOnChange(dark =>
                    {
                        SetState(nameof(_darkMode), _darkMode = dark);
                    })
            );
    }

    private ZorElement BuildCounterSection(Theme theme)
    {
        return new Card()
            .Elevated()
            .WithContent(
                new VStack(spacing: theme.Spacing.Space4)
                    .AddChild(
                        new Text("📊 Interactive Counter")
                            .WithFontSize(theme.Typography.FontSizeXl)
                            .Bold()
                    )
                    .AddChild(
                        new HStack(spacing: theme.Spacing.Space4)
                            .AddChild(
                                new VStack(spacing: theme.Spacing.Space2)
                                    .AddChild(
                                        new Text($"Count: {_counter}")
                                            .WithFontSize(theme.Typography.FontSize3Xl)
                                            .Bold()
                                    )
                                    .AddChild(
                                        new Progress(_counter % 100, 100)
                                            .Success()
                                            .WithShowValue()
                                    )
                            )
                            .AddChild(new Spacer())
                            .AddChild(
                                new VStack(spacing: theme.Spacing.Space2)
                                    .AddChild(
                                        new Button("➕ Increment (+1)", () =>
                                        {
                                            SetState(nameof(_counter), ++_counter);
                                        })
                                        .Primary()
                                    )
                                    .AddChild(
                                        new Button("➕➕ Add 10", () =>
                                        {
                                            SetState(nameof(_counter), _counter += 10);
                                        })
                                        .Secondary()
                                    )
                                    .AddChild(
                                        new Button("🔄 Reset", () =>
                                        {
                                            SetState(nameof(_counter), _counter = 0);
                                        })
                                        .Destructive()
                                    )
                            )
                    )
            );
    }

    private ZorElement BuildFormSection(Theme theme)
    {
        var isValid = !string.IsNullOrWhiteSpace(_name) && 
                      !string.IsNullOrWhiteSpace(_email);

        return new Card()
            .Elevated()
            .WithContent(
                new VStack(spacing: theme.Spacing.Space4)
                    .AddChild(
                        new Text("📝 Form Example")
                            .WithFontSize(theme.Typography.FontSizeXl)
                            .Bold()
                    )
                    .AddChild(
                        new VStack(spacing: theme.Spacing.Space3)
                            .AddChild(new Label("Your Name:", "name"))
                            .AddChild(
                                new TextField("Enter your name...")
                                    .WithValue(_name)
                                    .WithOnChange(name =>
                                    {
                                        SetState(nameof(_name), _name = name);
                                    })
                            )
                    )
                    .AddChild(
                        new VStack(spacing: theme.Spacing.Space3)
                            .AddChild(new Label("Email:", "email"))
                            .AddChild(
                                new TextField("your.email@example.com")
                                    .Email()
                                    .WithValue(_email)
                                    .WithOnChange(email =>
                                    {
                                        SetState(nameof(_email), _email = email);
                                    })
                            )
                    )
                    .AddChild(
                        new VStack(spacing: theme.Spacing.Space3)
                            .AddChild(new Label("Preference Level:"))
                            .AddChild(
                                new Slider(0, 100, _sliderValue)
                                    .WithStep(1)
                                    .WithShowValue()
                                    .WithOnChange(value =>
                                    {
                                        SetState(nameof(_sliderValue), _sliderValue = (int)value);
                                    })
                            )
                    )
                    .AddChild(
                        new HStack(spacing: theme.Spacing.Space2)
                            .AddChild(
                                new Button("📧 Submit", () =>
                                {
                                    if (isValid)
                                    {
                                        System.Windows.Forms.MessageBox.Show(
                                            $"Form Submitted!\n\n" +
                                            $"Name: {_name}\n" +
                                            $"Email: {_email}\n" +
                                            $"Level: {_sliderValue}",
                                            "Success",
                                            System.Windows.Forms.MessageBoxButtons.OK,
                                            System.Windows.Forms.MessageBoxIcon.Information
                                        );
                                    }
                                })
                                .Primary()
                                .Disabled(!isValid)
                            )
                            .AddChild(
                                new Button("🗑️ Clear", () =>
                                {
                                    SetState(() =>
                                    {
                                        _name = "";
                                        _email = "";
                                        _sliderValue = 50;
                                    });
                                })
                                .Secondary()
                            )
                    )
            );
    }

    private ZorElement BuildFooter(Theme theme)
    {
        return new VStack(spacing: theme.Spacing.Space2)
            .AddChild(
                new Alert("✨ This is a real desktop application running with SkiaSharp!")
                    .Info()
            )
            .AddChild(
                new HStack(spacing: theme.Spacing.Space2)
                    .AddChild(
                        new Text($"Total Clicks: {_counter}")
                            .WithFontSize(theme.Typography.FontSizeSm)
                    )
                    .AddChild(new Spacer())
                    .AddChild(
                        new Text("ZorUI v1.0 | © 2024")
                            .WithFontSize(theme.Typography.FontSizeSm)
                    )
            );
    }

    private ZorElement BuildDivider(Theme theme)
    {
        return new Divider()
            .WithColor(theme.Colors.Border)
            .WithThickness(1);
    }
}
