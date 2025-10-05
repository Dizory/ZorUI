namespace ZorUI.CLI.Templates;

/// <summary>
/// Template for creating full-featured applications.
/// </summary>
public static class FullTemplate
{
    public static void Create(string path, string name)
    {
        Directory.CreateDirectory(path);
        Directory.CreateDirectory(Path.Combine(path, "Components"));
        Directory.CreateDirectory(Path.Combine(path, "Pages"));
        Directory.CreateDirectory(Path.Combine(path, "Styles"));

        File.WriteAllText(Path.Combine(path, $"{name}.csproj"), GetProjectFile(name));
        File.WriteAllText(Path.Combine(path, "Program.cs"), GetProgramFile(name));
        File.WriteAllText(Path.Combine(path, "Pages", "HomePage.cs"), GetHomePageFile(name));
        File.WriteAllText(Path.Combine(path, "Components", "Header.cs"), GetHeaderFile(name));
        File.WriteAllText(Path.Combine(path, "Styles", "AppStyles.cs"), GetStylesFile(name));
        File.WriteAllText(Path.Combine(path, "README.md"), GetReadmeFile(name));
    }

    private static string GetProjectFile(string name)
    {
        return $@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework>
    <LangVersion>latest</LangVersion>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <UseWindowsForms>true</UseWindowsForms>
    <RootNamespace>{name}</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include=""ZorUI.Core"" Version=""1.0.0"" />
    <PackageReference Include=""ZorUI.Components"" Version=""1.0.0"" />
    <PackageReference Include=""ZorUI.Styling"" Version=""1.0.0"" />
    <PackageReference Include=""ZorUI.Rendering.Skia"" Version=""1.0.0"" />
  </ItemGroup>

</Project>
";
    }

    private static string GetProgramFile(string name)
    {
        return $@"using ZorUI.Rendering.Skia;
using {name}.Pages;

namespace {name};

class Program
{{
    [STAThread]
    static void Main()
    {{
        var window = new SkiaWindow(1000, 700, ""{name}"");
        window.RootComponent = new HomePage();
        window.Show();
    }}
}}
";
    }

    private static string GetHomePageFile(string name)
    {
        return $@"using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Components.Forms;
using ZorUI.Components.DataDisplay;
using ZorUI.Styling;
using {name}.Components;
using {name}.Styles;

namespace {name}.Pages;

public class HomePage : ZorComponent
{{
    private int _counter = 0;
    private bool _darkMode = false;
    private string _name = """";

    public override ZorElement Build()
    {{
        var theme = _darkMode ? Theme.Dark() : Theme.Light();

        return new VStack(spacing: 0)
            .WithBackground(theme.Colors.Background)
            .AddChild(new Header(_darkMode, isDark => 
                SetState(nameof(_darkMode), _darkMode = isDark)))
            .AddChild(BuildContent(theme));
    }}

    private ZorElement BuildContent(Theme theme)
    {{
        return new VStack(spacing: theme.Spacing.Space6)
            .WithPadding(EdgeInsets.All(theme.Spacing.Space8))
            .AddChild(BuildWelcomeSection(theme))
            .AddChild(BuildCounterSection(theme))
            .AddChild(BuildFormSection(theme));
    }}

    private ZorElement BuildWelcomeSection(Theme theme)
    {{
        return new Card()
            .Elevated()
            .WithContent(
                new VStack(spacing: theme.Spacing.Space3)
                    .AddChild(
                        new Text(""Welcome to {name}!"")
                            .WithFontSize(theme.Typography.FontSize3Xl)
                            .Bold()
                    )
                    .AddChild(
                        new Text(""A full-featured ZorUI application"")
                            .WithFontSize(theme.Typography.FontSizeLg)
                    )
            );
    }}

    private ZorElement BuildCounterSection(Theme theme)
    {{
        return new Card()
            .Elevated()
            .WithContent(
                new VStack(spacing: theme.Spacing.Space4)
                    .AddChild(
                        new Text(""Counter Demo"")
                            .WithFontSize(theme.Typography.FontSizeXl)
                            .Bold()
                    )
                    .AddChild(
                        new Text($""Value: {{_counter}}"")
                            .WithFontSize(theme.Typography.FontSize2Xl)
                    )
                    .AddChild(
                        new Progress(_counter % 100, 100)
                            .Success()
                            .WithShowValue()
                    )
                    .AddChild(
                        new HStack(spacing: theme.Spacing.Space2)
                            .AddChild(
                                new Button(""Increment"", () => 
                                    SetState(nameof(_counter), ++_counter))
                                    .Primary()
                            )
                            .AddChild(
                                new Button(""Reset"", () => 
                                    SetState(nameof(_counter), _counter = 0))
                                    .Secondary()
                            )
                    )
            );
    }}

    private ZorElement BuildFormSection(Theme theme)
    {{
        return new Card()
            .Elevated()
            .WithContent(
                new VStack(spacing: theme.Spacing.Space4)
                    .AddChild(
                        new Text(""Form Example"")
                            .WithFontSize(theme.Typography.FontSizeXl)
                            .Bold()
                    )
                    .AddChild(
                        new TextField(""Enter your name..."")
                            .WithValue(_name)
                            .WithOnChange(n => SetState(nameof(_name), _name = n))
                    )
                    .AddChild(
                        new Button(""Submit"", () => 
                            System.Windows.Forms.MessageBox.Show($""Hello, {{_name}}!""))
                            .Primary()
                            .Disabled(string.IsNullOrWhiteSpace(_name))
                    )
            );
    }}
}}
";
    }

    private static string GetHeaderFile(string name)
    {
        return $@"using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Components.Forms;
using ZorUI.Styling;

namespace {name}.Components;

public class Header : ZorComponent
{{
    private readonly bool _isDark;
    private readonly Action<bool> _onThemeChange;

    public Header(bool isDark, Action<bool> onThemeChange)
    {{
        _isDark = isDark;
        _onThemeChange = onThemeChange;
    }}

    public override ZorElement Build()
    {{
        var theme = _isDark ? Theme.Dark() : Theme.Light();

        return new HStack(spacing: theme.Spacing.Space4)
            .WithPadding(EdgeInsets.All(theme.Spacing.Space4))
            .WithBackground(theme.Colors.Primary)
            .AddChild(
                new Text(""{name}"")
                    .WithFontSize(theme.Typography.FontSizeXl)
                    .Bold()
                    .WithColor(theme.Colors.PrimaryForeground)
            )
            .AddChild(new Spacer())
            .AddChild(
                new Switch(""Dark Mode"", _isDark)
                    .WithOnChange(_onThemeChange)
            );
    }}
}}
";
    }

    private static string GetStylesFile(string name)
    {
        return $@"using ZorUI.Core.Properties;
using ZorUI.Styling;

namespace {name}.Styles;

public static class AppStyles
{{
    public static Style PrimaryButton => new Style
    {{
        BackgroundColor = Color.FromHex(""#3B82F6""),
        ForegroundColor = Color.White,
        BorderRadius = 8,
        Padding = EdgeInsets.Symmetric(horizontal: 16, vertical: 8)
    }};

    public static Style Card => new Style
    {{
        BackgroundColor = Color.White,
        BorderRadius = 12,
        Padding = EdgeInsets.All(16),
        BorderWidth = 1,
        BorderColor = Color.FromHex(""#E5E7EB"")
    }};
}}
";
    }

    private static string GetReadmeFile(string name)
    {
        return $@"# {name}

Full-featured ZorUI application with best practices.

## Structure

```
{name}/
├── Components/      # Reusable UI components
├── Pages/          # Application pages
├── Styles/         # Shared styles
└── Program.cs      # Entry point
```

## Running

```bash
dotnet run
```

## Features

- ✅ Multi-page structure
- ✅ Reusable components
- ✅ Theme system
- ✅ Custom styles
- ✅ Best practices

## Customization

Edit files in:
- `Pages/` for new pages
- `Components/` for new components
- `Styles/` for styling
";
    }
}
