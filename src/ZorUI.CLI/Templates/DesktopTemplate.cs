namespace ZorUI.CLI.Templates;

/// <summary>
/// Template for creating desktop applications.
/// </summary>
public static class DesktopTemplate
{
    public static void Create(string path, string name)
    {
        Directory.CreateDirectory(path);

        // Create .csproj
        File.WriteAllText(Path.Combine(path, $"{name}.csproj"), GetProjectFile(name));

        // Create Program.cs
        File.WriteAllText(Path.Combine(path, "Program.cs"), GetProgramFile(name));

        // Create README.md
        File.WriteAllText(Path.Combine(path, "README.md"), GetReadmeFile(name));

        // Create .gitignore
        File.WriteAllText(Path.Combine(path, ".gitignore"), GetGitignoreFile());
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
    <!-- Install ZorUI packages from NuGet -->
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
        return $@"using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Components.Forms;
using ZorUI.Styling;
using ZorUI.Rendering.Skia;

namespace {name};

/// <summary>
/// Main entry point for the application.
/// </summary>
class Program
{{
    [STAThread]
    static void Main()
    {{
        // Create window
        var window = new SkiaWindow(800, 600, ""{name}"");
        
        // Set root component
        window.RootComponent = new App();
        
        // Show window
        window.Show();
    }}
}}

/// <summary>
/// Main application component.
/// </summary>
public class App : ZorComponent
{{
    private int _counter = 0;
    private bool _darkMode = false;

    public override ZorElement Build()
    {{
        var theme = _darkMode ? Theme.Dark() : Theme.Light();

        return new VStack(spacing: theme.Spacing.Space6)
            .WithPadding(EdgeInsets.All(theme.Spacing.Space8))
            .WithBackground(theme.Colors.Background)
            .AddChild(
                new Text(""Welcome to {name}!"")
                    .WithFontSize(theme.Typography.FontSize3Xl)
                    .Bold()
            )
            .AddChild(
                new Switch(""Dark Mode"", _darkMode)
                    .WithOnChange(dark => SetState(nameof(_darkMode), _darkMode = dark))
            )
            .AddChild(new Divider().WithColor(theme.Colors.Border))
            .AddChild(
                new VStack(spacing: theme.Spacing.Space4)
                    .AddChild(
                        new Text($""Counter: {{_counter}}"")
                            .WithFontSize(theme.Typography.FontSize2Xl)
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
}}
";
    }

    private static string GetReadmeFile(string name)
    {
        return $@"# {name}

ZorUI desktop application.

## Running

```bash
dotnet run
```

## Building

```bash
# Windows
dotnet publish -c Release -r win-x64 --self-contained

# Linux
dotnet publish -c Release -r linux-x64 --self-contained

# macOS
dotnet publish -c Release -r osx-x64 --self-contained
```

## Documentation

- [ZorUI Documentation](https://github.com/zorui/zorui/docs)
- [Component Reference](https://github.com/zorui/zorui/docs/QuickReference.md)
";
    }

    private static string GetGitignoreFile()
    {
        return @"bin/
obj/
.vs/
*.user
*.suo
";
    }
}
