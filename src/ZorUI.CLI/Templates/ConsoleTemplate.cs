namespace ZorUI.CLI.Templates;

/// <summary>
/// Template for creating console applications.
/// </summary>
public static class ConsoleTemplate
{
    public static void Create(string path, string name)
    {
        Directory.CreateDirectory(path);

        File.WriteAllText(Path.Combine(path, $"{name}.csproj"), GetProjectFile(name));
        File.WriteAllText(Path.Combine(path, "Program.cs"), GetProgramFile(name));
        File.WriteAllText(Path.Combine(path, "README.md"), GetReadmeFile(name));
        File.WriteAllText(Path.Combine(path, ".gitignore"), GetGitignoreFile());
    }

    private static string GetProjectFile(string name)
    {
        return $@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <LangVersion>latest</LangVersion>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <RootNamespace>{name}</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include=""ZorUI.Core"" Version=""1.0.0"" />
    <PackageReference Include=""ZorUI.Components"" Version=""1.0.0"" />
    <PackageReference Include=""ZorUI.Styling"" Version=""1.0.0"" />
    <PackageReference Include=""ZorUI.Rendering"" Version=""1.0.0"" />
  </ItemGroup>

</Project>
";
    }

    private static string GetProgramFile(string name)
    {
        return $@"using ZorUI.Core;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Components.Forms;
using ZorUI.Rendering;

namespace {name};

class Program
{{
    static void Main(string[] args)
    {{
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        var app = new ZorApplication();
        app.Run(new App());

        Console.WriteLine(""\nPress any key to exit..."");
        Console.ReadKey();
    }}
}}

public class App : ZorComponent
{{
    private int _count = 0;
    private string _name = """";

    public override ZorElement Build()
    {{
        var renderer = new ConsoleRenderer();
        
        var ui = new VStack(spacing: 16)
            .WithPadding(20)
            .AddChild(
                new Text(""Welcome to {name}!"")
                    .WithFontSize(32)
                    .Bold()
            )
            .AddChild(
                new Text($""Count: {{_count}}"")
                    .WithFontSize(24)
            )
            .AddChild(
                new HStack(spacing: 8)
                    .AddChild(
                        new Button(""Increment"", () => 
                        {{
                            SetState(nameof(_count), ++_count);
                            Console.WriteLine($""\\nCount: {{_count}}"");
                        }})
                    )
                    .AddChild(
                        new Button(""Reset"", () => 
                        {{
                            SetState(nameof(_count), _count = 0);
                            Console.WriteLine(""\\nReset!"");
                        }})
                    )
            );

        renderer.Render(ui);
        return ui;
    }}
}}
";
    }

    private static string GetReadmeFile(string name)
    {
        return $@"# {name}

ZorUI console application.

## Running

```bash
dotnet run
```
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
