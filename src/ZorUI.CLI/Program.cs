using System.CommandLine;
using Spectre.Console;
using ZorUI.CLI.Commands;

namespace ZorUI.CLI;

/// <summary>
/// ZorUI CLI - Command-line tool for creating and managing ZorUI projects.
/// </summary>
class Program
{
    static async Task<int> Main(string[] args)
    {
        // Show banner
        ShowBanner();

        // Create root command
        var rootCommand = new RootCommand("ZorUI CLI - Cross-platform UI framework for .NET");

        // Add commands
        rootCommand.AddCommand(CreateNewCommand());
        rootCommand.AddCommand(CreateListCommand());
        rootCommand.AddCommand(CreateInfoCommand());

        // Execute
        return await rootCommand.InvokeAsync(args);
    }

    private static void ShowBanner()
    {
        AnsiConsole.Write(
            new FigletText("ZorUI CLI")
                .Centered()
                .Color(Color.Blue));

        AnsiConsole.MarkupLine("[dim]Cross-platform UI framework for .NET[/]");
        AnsiConsole.WriteLine();
    }

    private static Command CreateNewCommand()
    {
        var command = new Command("new", "Create a new ZorUI project from a template");

        var templateArg = new Argument<string>(
            "template",
            "Template to use (desktop, console, component, full)");

        var nameOption = new Option<string>(
            aliases: new[] { "--name", "-n" },
            description: "Project name",
            getDefaultValue: () => "MyZorUIApp");

        var outputOption = new Option<string>(
            aliases: new[] { "--output", "-o" },
            description: "Output directory",
            getDefaultValue: () => ".");

        command.AddArgument(templateArg);
        command.AddOption(nameOption);
        command.AddOption(outputOption);

        command.SetHandler(
            NewCommand.Handle,
            templateArg,
            nameOption,
            outputOption);

        return command;
    }

    private static Command CreateListCommand()
    {
        var command = new Command("list", "List available templates");

        command.SetHandler(ListCommand.Handle);

        return command;
    }

    private static Command CreateInfoCommand()
    {
        var command = new Command("info", "Show information about ZorUI");

        command.SetHandler(InfoCommand.Handle);

        return command;
    }
}
