using Spectre.Console;
using ZorUI.CLI.Templates;

namespace ZorUI.CLI.Commands;

/// <summary>
/// Handles the 'new' command for creating projects.
/// </summary>
public static class NewCommand
{
    public static void Handle(string template, string name, string output)
    {
        AnsiConsole.Status()
            .Start("Creating project...", ctx =>
            {
                ctx.Spinner(Spinner.Known.Dots);
                ctx.Status($"Creating {template} project '{name}'...");

                try
                {
                    var fullPath = Path.Combine(output, name);

                    // Create project based on template
                    switch (template.ToLower())
                    {
                        case "desktop":
                            DesktopTemplate.Create(fullPath, name);
                            break;

                        case "console":
                            ConsoleTemplate.Create(fullPath, name);
                            break;

                        case "component":
                            ComponentTemplate.Create(fullPath, name);
                            break;

                        case "full":
                            FullTemplate.Create(fullPath, name);
                            break;

                        default:
                            AnsiConsole.MarkupLine($"[red]Unknown template: {template}[/]");
                            AnsiConsole.MarkupLine("[yellow]Available templates: desktop, console, component, full[/]");
                            return;
                    }

                    ctx.Status("Project created successfully!");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
                    return;
                }
            });

        // Show success message
        ShowSuccessMessage(template, name, output);
    }

    private static void ShowSuccessMessage(string template, string name, string output)
    {
        var panel = new Panel(
            new Markup($"[green]✓[/] Project '[cyan]{name}[/]' created successfully!\n\n" +
                      $"[dim]Template:[/] {template}\n" +
                      $"[dim]Location:[/] {Path.Combine(output, name)}\n\n" +
                      $"[yellow]Next steps:[/]\n" +
                      $"  cd {name}\n" +
                      $"  dotnet run"))
        {
            Header = new PanelHeader("Success!", Justify.Center),
            Border = BoxBorder.Rounded,
            BorderStyle = new Style(Color.Green)
        };

        AnsiConsole.Write(panel);
    }
}
