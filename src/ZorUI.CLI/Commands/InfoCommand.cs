using Spectre.Console;

namespace ZorUI.CLI.Commands;

/// <summary>
/// Handles the 'info' command for showing information about ZorUI.
/// </summary>
public static class InfoCommand
{
    public static void Handle()
    {
        var grid = new Grid()
            .AddColumn()
            .AddRow("[bold blue]ZorUI Framework[/]")
            .AddRow("")
            .AddRow("[dim]Version:[/] 1.0.0")
            .AddRow("[dim]License:[/] MIT")
            .AddRow("[dim]Website:[/] https://zorui.dev")
            .AddRow("[dim]GitHub:[/] https://github.com/zorui/zorui")
            .AddRow("")
            .AddRow("[yellow]Supported Platforms:[/]")
            .AddRow("  ✅ Windows (Desktop)")
            .AddRow("  ✅ Linux (Desktop)")
            .AddRow("  ✅ macOS (Desktop)")
            .AddRow("  🔄 Android (In Development)")
            .AddRow("  🔄 iOS (In Development)")
            .AddRow("  🔮 Web (Planned)")
            .AddRow("")
            .AddRow("[yellow]Features:[/]")
            .AddRow("  • Fluent API - No XAML required")
            .AddRow("  • 30+ Components - Rich component library")
            .AddRow("  • Reactive State - Automatic UI updates")
            .AddRow("  • Cross-platform - One code, all platforms")
            .AddRow("  • Theming - Light/dark themes out of the box")
            .AddRow("")
            .AddRow("[dim]For more information, visit the documentation:[/]")
            .AddRow("[link]https://github.com/zorui/zorui/docs[/]");

        var panel = new Panel(grid)
        {
            Header = new PanelHeader(" About ZorUI ", Justify.Center),
            Border = BoxBorder.Rounded,
            BorderStyle = new Style(Color.Blue),
            Padding = new Padding(2)
        };

        AnsiConsole.Write(panel);
    }
}
