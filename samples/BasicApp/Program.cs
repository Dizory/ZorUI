using ZorUI.Core;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Components.Forms;
using ZorUI.Styling;
using ZorUI.Rendering;

namespace BasicApp;

/// <summary>
/// Basic example application demonstrating ZorUI framework.
/// This shows the Fluent API approach for building UIs.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to ZorUI Framework!");
        Console.WriteLine("A cross-platform UI framework inspired by Radix UI\n");

        // Create and run the application
        var app = new ZorApplication();
        app.Run(new CounterApp());

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}

/// <summary>
/// Simple counter application component.
/// Demonstrates stateful components and event handling.
/// </summary>
public class CounterApp : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        // Create a console renderer for this demo
        var renderer = new ConsoleRenderer();
        
        // Build the UI using Fluent API
        var ui = new VStack(spacing: 20)
            .WithPadding(20)
            .AddChild(
                new Text("Counter Application")
                    .WithFontSize(32)
                    .Bold()
            )
            .AddChild(
                new Text($"Count: {_count}")
                    .WithFontSize(24)
            )
            .AddChild(
                new HStack(spacing: 10)
                    .AddChild(
                        new Button("Decrement", () => Decrement())
                            .Secondary()
                    )
                    .AddChild(
                        new Button("Increment", () => Increment())
                            .Primary()
                    )
                    .AddChild(
                        new Button("Reset", () => Reset())
                            .Destructive()
                    )
            )
            .AddChild(
                new Divider()
            )
            .AddChild(
                new Text("This is a demo of ZorUI's Fluent API approach!")
                    .WithFontSize(14)
            );

        // Render the UI
        renderer.Render(ui);

        return ui;
    }

    private void Increment()
    {
        SetState(nameof(_count), ++_count);
        Console.WriteLine($"\n[Event] Count incremented to {_count}");
    }

    private void Decrement()
    {
        SetState(nameof(_count), --_count);
        Console.WriteLine($"\n[Event] Count decremented to {_count}");
    }

    private void Reset()
    {
        SetState(nameof(_count), _count = 0);
        Console.WriteLine($"\n[Event] Count reset to 0");
    }
}
