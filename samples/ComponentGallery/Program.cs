using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Components.Forms;
using ZorUI.Components.DataDisplay;
using ZorUI.Components.Navigation;
using ZorUI.Components.Overlays;
using ZorUI.Styling;
using ZorUI.Rendering;

namespace ComponentGallery;

/// <summary>
/// Component gallery application showcasing all ZorUI components.
/// This serves as both a demo and a visual testing tool.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║   ZorUI Framework - Component Gallery  ║");
        Console.WriteLine("║   Inspired by Radix UI                 ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        var app = new ZorApplication();
        app.Run(new GalleryApp());

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}

/// <summary>
/// Main gallery application component.
/// Demonstrates all available components with examples.
/// </summary>
public class GalleryApp : ZorComponent
{
    private string _activeTab = "layout";
    private bool _switchState = false;
    private bool _checkboxState = true;
    private string _radioValue = "option2";

    public override ZorElement Build()
    {
        var renderer = new ConsoleRenderer();
        
        var ui = new VStack(spacing: 16)
            .WithPadding(20);

        // Add header
        ui.AddChild(
            new VStack(spacing: 8)
                .AddChild(
                    new Text("ZorUI Component Gallery")
                        .WithFontSize(36)
                        .Bold()
                )
                .AddChild(
                    new Text("A comprehensive showcase of all available components")
                        .WithFontSize(14)
                )
                .AddChild(new Divider())
        );

        // Layout Components Section
        ui.AddChild(
            new VStack(spacing: 12)
                .AddChild(
                    new Text("Layout Components")
                        .WithFontSize(24)
                        .Bold()
                )
                .AddChild(
                    new HStack(spacing: 8)
                        .AddChild(new Text("VStack"))
                        .AddChild(new Text("•"))
                        .AddChild(new Text("HStack"))
                        .AddChild(new Text("•"))
                        .AddChild(new Text("ZStack"))
                        .AddChild(new Text("•"))
                        .AddChild(new Text("Container"))
                )
        );

        // Button Components Section
        ui.AddChild(
            new VStack(spacing: 8)
                .AddChild(
                    new Text("Buttons")
                        .WithFontSize(20)
                        .SemiBold()
                )
                .AddChild(
                    new HStack(spacing: 8)
                        .AddChild(new Button("Default"))
                        .AddChild(new Button("Primary").Primary())
                        .AddChild(new Button("Secondary").Secondary())
                        .AddChild(new Button("Destructive").Destructive())
                )
        );

        // Form Components Section
        ui.AddChild(
            new VStack(spacing: 8)
                .AddChild(
                    new Text("Form Controls")
                        .WithFontSize(20)
                        .SemiBold()
                )
                .AddChild(
                    new TextField("Enter your name...")
                        .WithValue("John Doe")
                )
                .AddChild(
                    new Checkbox("Accept terms and conditions", _checkboxState)
                        .WithOnChange(val => SetState(nameof(_checkboxState), _checkboxState = val))
                )
                .AddChild(
                    new Switch("Enable notifications", _switchState)
                        .WithOnChange(val => SetState(nameof(_switchState), _switchState = val))
                )
        );

        // Radio Group
        var radioGroup = new RadioGroup("options")
            .AddRadio(new Radio("option1", "Option 1"))
            .AddRadio(new Radio("option2", "Option 2").Selected())
            .AddRadio(new Radio("option3", "Option 3"));
        
        ui.AddChild(
            new VStack(spacing: 8)
                .AddChild(new Text("Radio Group").SemiBold())
                .AddChild(radioGroup)
        );

        // Data Display Components
        ui.AddChild(
            new VStack(spacing: 8)
                .AddChild(
                    new Text("Data Display")
                        .WithFontSize(20)
                        .SemiBold()
                )
                .AddChild(
                    new Progress(65, 100)
                        .WithShowValue()
                        .Success()
                )
                .AddChild(
                    new Badge("New")
                        .Primary()
                )
                .AddChild(
                    new Alert("This is an informational alert!")
                        .Info()
                        .Dismissible()
                )
        );

        // Tabs Example
        var tabs = new Tabs("tab1")
            .AddTab(new Tab("tab1", "Profile").WithContent(
                new Text("Profile content here")
            ))
            .AddTab(new Tab("tab2", "Settings").WithContent(
                new Text("Settings content here")
            ))
            .AddTab(new Tab("tab3", "Messages").WithContent(
                new Text("Messages content here")
            ));

        ui.AddChild(
            new VStack(spacing: 8)
                .AddChild(new Text("Tabs").WithFontSize(20).SemiBold())
                .AddChild(tabs)
        );

        // Accordion Example
        var accordion = new Accordion()
            .Single()
            .AddItem(
                new AccordionItem("item1", "What is ZorUI?")
                    .WithContent(new Text("ZorUI is a cross-platform UI framework inspired by Radix UI"))
            )
            .AddItem(
                new AccordionItem("item2", "How to use it?")
                    .WithContent(new Text("Use the fluent API to build your components"))
            );

        ui.AddChild(
            new VStack(spacing: 8)
                .AddChild(new Text("Accordion").WithFontSize(20).SemiBold())
                .AddChild(accordion)
        );

        // Render
        renderer.Render(ui);

        return ui;
    }
}
