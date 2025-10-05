using Spectre.Console;

namespace ZorUI.CLI.Commands;

/// <summary>
/// Handles the 'list' command for showing available templates.
/// </summary>
public static class ListCommand
{
    public static void Handle()
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Blue)
            .AddColumn(new TableColumn("[bold]Template[/]").Centered())
            .AddColumn(new TableColumn("[bold]Short Name[/]").Centered())
            .AddColumn(new TableColumn("[bold]Description[/]"))
            .AddColumn(new TableColumn("[bold]Platform[/]").Centered());

        table.AddRow(
            "[cyan]Desktop App[/]",
            "[yellow]desktop[/]",
            "Cross-platform desktop application with GUI (Windows, Linux, macOS)",
            "🖥️ Desktop"
        );

        table.AddRow(
            "[cyan]Console App[/]",
            "[yellow]console[/]",
            "Simple console application for testing and CLI tools",
            "💻 All"
        );

        table.AddRow(
            "[cyan]Component Library[/]",
            "[yellow]component[/]",
            "Create reusable component library",
            "📦 Library"
        );

        table.AddRow(
            "[cyan]Full Application[/]",
            "[yellow]full[/]",
            "Full-featured application with examples and best practices",
            "🎨 All"
        );

        AnsiConsole.Write(
            new Panel(table)
            {
                Header = new PanelHeader(" Available Templates ", Justify.Center),
                Border = BoxBorder.Double
            });

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Usage: zorui new <template> --name <ProjectName>[/]");
        AnsiConsole.MarkupLine("[dim]Example: zorui new desktop --name MyApp[/]");
    }
}
