using ZorUI.Core;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Components.Navigation;

namespace ZorUI.DevTools;

/// <summary>
/// Developer tools window for debugging ZorUI applications.
/// </summary>
public class DevToolsWindow : ZorComponent
{
    private string _selectedTab = "inspector";
    private ZorElement? _rootElement;
    private ZorElement? _selectedElement;

    public ZorElement? RootElement
    {
        get => _rootElement;
        set
        {
            _rootElement = value;
            MarkNeedsRebuild();
        }
    }

    public override ZorElement Build()
    {
        return new VStack(spacing: 0)
            .AddChild(BuildHeader())
            .AddChild(BuildTabs())
            .AddChild(BuildContent());
    }

    private ZorElement BuildHeader()
    {
        return new HStack(spacing: 12)
            .WithPadding(EdgeInsets.All(12))
            .AddChild(new Text("ZorUI DevTools").Bold().WithFontSize(18))
            .AddChild(new Spacer())
            .AddChild(new Button("Close", CloseDevTools).Ghost().Small());
    }

    private ZorElement BuildTabs()
    {
        return new Tabs(_selectedTab)
            .AddTab(new Tab("inspector", "Inspector"))
            .AddTab(new Tab("state", "State"))
            .AddTab(new Tab("performance", "Performance"))
            .AddTab(new Tab("console", "Console"))
            .WithOnTabChange(tab =>
            {
                SetState(nameof(_selectedTab), _selectedTab = tab);
            });
    }

    private ZorElement BuildContent()
    {
        return _selectedTab switch
        {
            "inspector" => BuildInspector(),
            "state" => BuildStateViewer(),
            "performance" => BuildPerformancePanel(),
            "console" => BuildConsole(),
            _ => new Text("Unknown tab")
        };
    }

    private ZorElement BuildInspector()
    {
        if (_rootElement == null)
        {
            return new Text("No root element to inspect");
        }

        return new HStack(spacing: 0)
            .AddChild(BuildElementTree())
            .AddChild(BuildElementDetails());
    }

    private ZorElement BuildElementTree()
    {
        return new VStack(spacing: 4)
            .WithPadding(EdgeInsets.All(12))
            .AddChild(new Text("Element Tree").Bold())
            .AddChild(BuildTreeNode(_rootElement!, 0));
    }

    private ZorElement BuildTreeNode(ZorElement element, int depth)
    {
        var isSelected = element == _selectedElement;
        var indent = new string(' ', depth * 2);

        var node = new HStack(spacing: 4)
            .AddChild(new Text($"{indent}▸ {element.ElementType}"))
            .Metadata["OnClick"] = new Action(() =>
            {
                _selectedElement = element;
                MarkNeedsRebuild();
            });

        if (isSelected)
        {
            node.Metadata["Highlight"] = true;
        }

        var container = new VStack(spacing: 2)
            .AddChild(node);

        foreach (var child in element.Children)
        {
            container.AddChild(BuildTreeNode(child, depth + 1));
        }

        return container;
    }

    private ZorElement BuildElementDetails()
    {
        if (_selectedElement == null)
        {
            return new Text("Select an element to view details");
        }

        return new VStack(spacing: 8)
            .WithPadding(EdgeInsets.All(12))
            .AddChild(new Text("Element Details").Bold())
            .AddChild(new Text($"Type: {_selectedElement.ElementType}"))
            .AddChild(new Text($"ID: {_selectedElement.Id}"))
            .AddChild(new Text($"Children: {_selectedElement.Children.Count}"))
            .AddChild(BuildMetadata());
    }

    private ZorElement BuildMetadata()
    {
        if (_selectedElement?.Metadata == null || _selectedElement.Metadata.Count == 0)
        {
            return new Text("No metadata");
        }

        var metadata = new VStack(spacing: 4)
            .AddChild(new Text("Metadata:").Bold());

        foreach (var (key, value) in _selectedElement.Metadata)
        {
            metadata.AddChild(new Text($"{key}: {value?.ToString() ?? "null"}"));
        }

        return metadata;
    }

    private ZorElement BuildStateViewer()
    {
        return new VStack(spacing: 8)
            .WithPadding(EdgeInsets.All(12))
            .AddChild(new Text("State Viewer").Bold())
            .AddChild(new Text("Shows component state..."));
    }

    private ZorElement BuildPerformancePanel()
    {
        return new VStack(spacing: 8)
            .WithPadding(EdgeInsets.All(12))
            .AddChild(new Text("Performance").Bold())
            .AddChild(new Text("FPS: 60"))
            .AddChild(new Text("Render time: 10ms"))
            .AddChild(new Text("Component count: 42"));
    }

    private ZorElement BuildConsole()
    {
        return new VStack(spacing: 8)
            .WithPadding(EdgeInsets.All(12))
            .AddChild(new Text("Console").Bold())
            .AddChild(new Text("Console logs will appear here..."));
    }

    private void CloseDevTools()
    {
        // Signal to close DevTools
        Metadata["ShouldClose"] = true;
    }
}

/// <summary>
/// Global DevTools instance.
/// </summary>
public static class DevTools
{
    private static DevToolsWindow? _instance;
    private static bool _isOpen = false;

    public static void Open(ZorElement rootElement)
    {
        if (!_isOpen)
        {
            _instance = new DevToolsWindow { RootElement = rootElement };
            _isOpen = true;
        }
    }

    public static void Close()
    {
        _instance = null;
        _isOpen = false;
    }

    public static void Toggle(ZorElement rootElement)
    {
        if (_isOpen)
            Close();
        else
            Open(rootElement);
    }

    public static bool IsOpen => _isOpen;

    public static DevToolsWindow? Instance => _instance;
}
