namespace ZorUI.CLI.Templates;

/// <summary>
/// Template for creating component libraries.
/// </summary>
public static class ComponentTemplate
{
    public static void Create(string path, string name)
    {
        Directory.CreateDirectory(path);

        File.WriteAllText(Path.Combine(path, $"{name}.csproj"), GetProjectFile(name));
        File.WriteAllText(Path.Combine(path, "MyButton.cs"), GetComponentFile(name));
        File.WriteAllText(Path.Combine(path, "README.md"), GetReadmeFile(name));
    }

    private static string GetProjectFile(string name)
    {
        return $@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <LangVersion>latest</LangVersion>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <RootNamespace>{name}</RootNamespace>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include=""ZorUI.Core"" Version=""1.0.0"" />
    <PackageReference Include=""ZorUI.Components"" Version=""1.0.0"" />
    <PackageReference Include=""ZorUI.Styling"" Version=""1.0.0"" />
  </ItemGroup>

</Project>
";
    }

    private static string GetComponentFile(string name)
    {
        return $@"using ZorUI.Core;
using ZorUI.Components.Primitives;
using ZorUI.Components.Layout;
using ZorUI.Core.Properties;

namespace {name};

/// <summary>
/// Custom button component with additional features.
/// </summary>
public class MyButton : Button
{{
    private bool _isHovered = false;

    public MyButton(string text, Action? onClick = null) 
        : base(text, onClick)
    {{
    }}

    /// <summary>
    /// Adds a custom hover effect.
    /// </summary>
    public MyButton WithHoverEffect()
    {{
        // Add your custom hover logic here
        return this;
    }}

    /// <summary>
    /// Adds an icon to the button.
    /// </summary>
    public MyButton WithIcon(string iconName)
    {{
        WithLeadingIcon(new Icon(iconName));
        return this;
    }}
}}

/// <summary>
/// Custom card component with additional styling.
/// </summary>
public class MyCard : Container
{{
    public MyCard(params ZorElement[] children) : base(children)
    {{
        // Set default styling
        WithPadding(EdgeInsets.All(16));
        WithBackground(Color.White);
    }}

    /// <summary>
    /// Sets the card to elevated style.
    /// </summary>
    public MyCard Elevated()
    {{
        // Add shadow/elevation effect
        return this;
    }}

    /// <summary>
    /// Sets the card header.
    /// </summary>
    public MyCard WithHeader(string title)
    {{
        // Add header to top
        return this;
    }}
}}
";
    }

    private static string GetReadmeFile(string name)
    {
        return $@"# {name}

Custom component library for ZorUI.

## Usage

```csharp
using {name};

// Use your custom components
var button = new MyButton(""Click me"")
    .WithIcon(""home"")
    .WithHoverEffect()
    .Primary();

var card = new MyCard()
    .WithHeader(""Title"")
    .Elevated();
```

## Building

```bash
dotnet build
```

## Publishing as NuGet Package

```bash
dotnet pack -c Release
```
";
    }
}
