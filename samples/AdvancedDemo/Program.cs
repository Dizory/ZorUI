using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Core.Input;
using ZorUI.Core.Validation;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Components.Forms;
using ZorUI.Components.DataDisplay;
using ZorUI.Styling;
using ZorUI.Rendering.Skia;
using ZorUI.Icons;
using ZorUI.Animations;
using ZorUI.DragDrop;
using ZorUI.DataGrid;
using ZorUI.Routing;
using ZorUI.DevTools;

namespace AdvancedDemo;

/// <summary>
/// Advanced demo showcasing all new features!
/// </summary>
class Program
{
    [STAThread]
    static void Main()
    {
        var window = new SkiaWindow(1200, 800, "ZorUI v1.5 - Advanced Features Demo");
        var router = CreateRouter();
        
        window.RootComponent = new App(router);
        
        // Keyboard shortcuts
        var shortcuts = new KeyboardShortcutManager();
        shortcuts.Register("Ctrl+D", () => DevTools.Toggle(window.RootComponent.Build()));
        
        window.Show();
    }

    static Router CreateRouter()
    {
        return new Router()
            .AddRoute("/", () => new HomePage())
            .AddRoute("/animations", () => new AnimationsPage())
            .AddRoute("/dragdrop", () => new DragDropPage())
            .AddRoute("/datagrid", () => new DataGridPage())
            .AddRoute("/forms", () => new FormsPage());
    }
}

public class App : ZorComponent
{
    private readonly Router _router;
    private bool _darkMode = false;

    public App(Router router)
    {
        _router = router;
        _router.OnNavigate += _ => MarkNeedsRebuild();
    }

    public override ZorElement Build()
    {
        var theme = _darkMode ? Theme.Dark() : Theme.Light();

        return new VStack(spacing: 0)
            .WithBackground(theme.Colors.Background)
            .AddChild(BuildHeader(theme))
            .AddChild(
                new HStack(spacing: 0)
                    .AddChild(BuildSidebar(theme))
                    .AddChild(new RouterOutlet(_router))
            );
    }

    private ZorElement BuildHeader(Theme theme)
    {
        return new HStack(spacing: 16)
            .WithPadding(EdgeInsets.All(16))
            .WithBackground(theme.Colors.Primary)
            .AddChild(
                new HStack(spacing: 12)
                    .AddChild(new ZorIconComponent(ZorIcon.Lightning)
                        .Medium()
                        .WithColor(theme.Colors.PrimaryForeground))
                    .AddChild(
                        new Text("ZorUI v1.5 Advanced Demo")
                            .Bold()
                            .WithFontSize(20)
                            .WithColor(theme.Colors.PrimaryForeground)
                    )
            )
            .AddChild(new Spacer())
            .AddChild(
                new Switch("Dark Mode", _darkMode)
                    .WithOnChange(dark => SetState(nameof(_darkMode), _darkMode = dark))
            );
    }

    private ZorElement BuildSidebar(Theme theme)
    {
        return new VStack(spacing: 8)
            .WithPadding(EdgeInsets.All(16))
            .WithBackground(theme.Colors.Muted)
            .AddChild(NavButton("/", "Home", ZorIcon.Home))
            .AddChild(NavButton("/animations", "Animations", ZorIcon.Lightning))
            .AddChild(NavButton("/dragdrop", "Drag & Drop", ZorIcon.Move))
            .AddChild(NavButton("/datagrid", "Data Grid", ZorIcon.Grid))
            .AddChild(NavButton("/forms", "Forms", ZorIcon.FileText));
    }

    private ZorElement NavButton(string path, string label, ZorIcon icon)
    {
        var isActive = _router.CurrentPath == path;

        return new Button(label, () => _router.Navigate(path))
            .WithLeadingIcon(new ZorIconComponent(icon).Small())
            .WithFullWidth()
            .If(isActive, b => b.Primary(), b => b.Ghost());
    }
}

// Pages

public class HomePage : ZorComponent
{
    public override ZorElement Build()
    {
        return new VStack(spacing: 24)
            .WithPadding(EdgeInsets.All(24))
            .AddChild(
                new Text("Welcome to ZorUI v1.5!")
                    .WithFontSize(32)
                    .Bold()
                    .Animate(Animation.FadeIn().WithDuration(500))
            )
            .AddChild(
                new Card()
                    .Elevated()
                    .WithContent(
                        new VStack(spacing: 12)
                            .AddChild(new Text("New Features:").Bold())
                            .AddChild(new Text("✅ Animations"))
                            .AddChild(new Text("✅ Drag & Drop"))
                            .AddChild(new Text("✅ DataGrid"))
                            .AddChild(new Text("✅ Routing"))
                            .AddChild(new Text("✅ Performance"))
                            .AddChild(new Text("✅ DevTools"))
                    )
                    .Animate(Animation.SlideIn(SlideDirection.Up).WithDelay(200))
            );
    }
}

public class AnimationsPage : ZorComponent
{
    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .WithPadding(EdgeInsets.All(24))
            .AddChild(new Text("Animation Examples").WithFontSize(28).Bold())
            .AddChild(
                new HStack(spacing: 16)
                    .AddChild(
                        new Card()
                            .WithContent(new Text("Fade In"))
                            .Animate(Animation.FadeIn())
                    )
                    .AddChild(
                        new Card()
                            .WithContent(new Text("Slide In"))
                            .Animate(Animation.SlideIn(SlideDirection.Left))
                    )
                    .AddChild(
                        new Card()
                            .WithContent(new Text("Spring"))
                            .Animate(Animation.Spring())
                    )
            );
    }
}

public class DragDropPage : ZorComponent
{
    private List<string> _items = new() { "Item 1", "Item 2", "Item 3" };

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .WithPadding(EdgeInsets.All(24))
            .AddChild(new Text("Drag & Drop Demo").WithFontSize(28).Bold())
            .AddChild(
                new VStack(spacing: 8)
                    .AddChildren(_items.Select(item =>
                        new Draggable(
                            new Card()
                                .WithContent(new Text(item))
                                .WithPadding(EdgeInsets.All(16))
                        )
                    ))
            );
    }
}

public class DataGridPage : ZorComponent
{
    private record User(string Name, string Email, string Role);

    private List<User> _users = new()
    {
        new("John Doe", "john@example.com", "Admin"),
        new("Jane Smith", "jane@example.com", "User"),
        new("Bob Johnson", "bob@example.com", "Moderator"),
    };

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .WithPadding(EdgeInsets.All(24))
            .AddChild(new Text("DataGrid Demo").WithFontSize(28).Bold())
            .AddChild(
                new DataGrid<User>()
                    .WithItems(_users)
                    .AddColumn("Name", u => u.Name)
                    .AddColumn("Email", u => u.Email)
                    .AddColumn("Role", u => u.Role)
                    .WithSorting()
                    .WithPagination(10)
                    .WithSelection()
            );
    }
}

public class FormsPage : ZorComponent
{
    private string _name = "";
    private string _email = "";
    private DateTime? _birthDate;
    private Color _favoriteColor = Color.Blue;

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .WithPadding(EdgeInsets.All(24))
            .AddChild(new Text("Advanced Forms").WithFontSize(28).Bold())
            .AddChild(
                new Card()
                    .Elevated()
                    .WithContent(
                        new VStack(spacing: 16)
                            .AddChild(
                                new TextField("Name")
                                    .WithValue(_name)
                                    .WithOnChange(v => SetState(nameof(_name), _name = v))
                            )
                            .AddChild(
                                new TextField("Email")
                                    .WithValue(_email)
                                    .WithOnChange(v => SetState(nameof(_email), _email = v))
                            )
                            .AddChild(
                                new DatePicker()
                                    .WithValue(_birthDate)
                                    .WithOnChange(d => SetState(nameof(_birthDate), _birthDate = d))
                            )
                            .AddChild(
                                new ColorPicker()
                                    .WithValue(_favoriteColor)
                                    .WithOnChange(c => SetState(nameof(_favoriteColor), _favoriteColor = c))
                            )
                            .AddChild(
                                new Button("Submit", HandleSubmit)
                                    .Primary()
                                    .Disabled(string.IsNullOrEmpty(_name))
                            )
                    )
            );
    }

    private void HandleSubmit()
    {
        Console.WriteLine($"Submitted: {_name}, {_email}, {_birthDate}, {_favoriteColor}");
    }
}

// Helper extensions
public static class ComponentExtensions
{
    public static T If<T>(this T component, bool condition, Func<T, T> ifTrue, Func<T, T>? ifFalse = null) 
        where T : ZorElement
    {
        return condition ? ifTrue(component) : (ifFalse?.Invoke(component) ?? component);
    }
}
