# ZorUI Quick Reference

Краткая справка по всем компонентам и их API.

## Layout Components

### VStack
```csharp
new VStack(spacing: 16,
    child1,
    child2
)
.WithPadding(20)
.WithAlignment(HorizontalAlignment.Center)
```

### HStack
```csharp
new HStack(spacing: 8,
    child1,
    child2
)
.WithAlignment(VerticalAlignment.Center)
```

### ZStack
```csharp
new ZStack(
    background,
    foreground
)
.WithAlignment(Alignment.Center)
```

### Container
```csharp
new Container(children...)
    .WithPadding(EdgeInsets.All(16))
    .WithMargin(EdgeInsets.Symmetric(horizontal: 8))
    .WithWidth(300)
    .WithHeight(200)
    .WithBackground(Color.Blue)
```

### Spacer
```csharp
new Spacer()                        // Flexible
new Spacer(minLength: 20)           // With min size
FixedSpacer.Horizontal(50)          // Fixed horizontal
FixedSpacer.Vertical(30)            // Fixed vertical
```

### Divider
```csharp
new Divider()
    .WithThickness(2)
    .WithColor(Color.Gray)
    .WithLength(100)
```

## Primitives

### Text
```csharp
new Text("Hello World")
    .WithFontSize(24)
    .Bold()
    .SemiBold()
    .Light()
    .WithColor(Color.Blue)
    .WithAlignment(TextAlignment.Center)
    .Underline()
    .Strikethrough()
    .WithMaxLines(2)
    .NoWrap()
```

### Button
```csharp
new Button("Click me", onClick)
    .Primary()
    .Secondary()
    .Destructive()
    .Ghost()
    .Link()
    .Small()
    .Large()
    .WithFullWidth()
    .Disabled(true)
    .Loading(true)
    .WithLeadingIcon(icon)
    .WithTrailingIcon(icon)
```

### Image
```csharp
new Image("path/to/image.jpg")
    .WithSize(200, 150)
    .WithFit(ImageFit.Cover)
    .Rounded(8)
    .Circle()
    .WithOpacity(0.8)
    .WithAltText("Description")
```

### Icon
```csharp
new Icon("home")
    .WithSize(24)
    .Small()      // 16px
    .Medium()     // 24px
    .Large()      // 32px
    .WithColor(Color.Blue)
    .FromSet("material")
```

### Label
```csharp
new Label("Email", forId: "email-input")
    .IsRequired()
```

## Form Components

### TextField
```csharp
new TextField("Placeholder")
    .WithValue("initial value")
    .Email()
    .Password()
    .Number()
    .Required()
    .Disabled()
    .ReadOnly()
    .WithError("Error message")
    .WithMaxLength(100)
    .WithOnChange(value => { })
    .WithLeadingIcon(icon)
    .WithTrailingIcon(icon)
```

### Checkbox
```csharp
new Checkbox("Label", isChecked: false)
    .Checked()
    .Indeterminate()
    .Disabled()
    .Required()
    .WithOnChange(checked => { })
```

### Radio & RadioGroup
```csharp
new RadioGroup("size")
    .AddRadio(new Radio("small", "Small"))
    .AddRadio(new Radio("medium", "Medium").Selected())
    .AddRadio(new Radio("large", "Large"))
    .Disabled()
    .WithOnValueChange(value => { })
```

### Switch
```csharp
new Switch("Enable", isOn: false)
    .On()
    .Off()
    .Small()
    .Large()
    .Disabled()
    .WithOnChange(on => { })
```

### Slider
```csharp
new Slider(min: 0, max: 100, value: 50)
    .WithStep(5)
    .WithShowValue()
    .Vertical()
    .Disabled()
    .WithOnChange(value => { })
    .WithOnChangeEnd(value => { })
```

## Navigation

### Tabs
```csharp
new Tabs(defaultTab: "home")
    .AddTab(new Tab("home", "Home")
        .WithContent(content)
        .WithIcon(icon))
    .AddTab(new Tab("profile", "Profile")
        .Disabled())
    .Vertical()
    .WithVariant(TabsVariant.Pills)
    .WithOnTabChange(tab => { })
```

### Menu
```csharp
new Menu()
    .WithTrigger(button)
    .AddItem(new MenuItem("Edit", onClick)
        .WithIcon(icon)
        .WithShortcut("Ctrl+E")
        .Disabled())
    .AddSeparator()
    .AddItem(new MenuItem("Delete")
        .Selected())
    .WithOnOpen(() => { })
    .WithOnClose(() => { })
```

## Overlays

### Dialog
```csharp
new Dialog("Title")
    .WithDescription("Description")
    .WithContent(content)
    .WithFooter(footer)
    .WithCloseOnOutsideClick()
    .WithCloseOnEscape()
    .WithShowCloseButton()
    .WithOnOpen(() => { })
    .WithOnClose(() => { })

dialog.Open()
dialog.Close()
dialog.Toggle()
```

### AlertDialog
```csharp
new AlertDialog("Warning")
    .Warning()
    .Error()
    .Success()
    .Info()
    .WithContent(content)
    .WithFooter(buttons)
```

### Popover
```csharp
new Popover()
    .WithTrigger(button)
    .WithContent(content)
    .WithPlacement(PopoverPlacement.Bottom)
    .WithOffset(8)
    .WithCloseOnOutsideClick()
    .WithCloseOnContentClick()
```

### Tooltip
```csharp
new Tooltip("Helpful text")
    .WithTrigger(element)
    .WithPlacement(TooltipPlacement.Top)
    .WithShowDelay(700)
    .WithHideDelay(0)
```

### Toast
```csharp
// Using ToastManager
ToastManager.Instance.ShowSuccess("Saved!")
ToastManager.Instance.ShowError("Failed!", "Error")
ToastManager.Instance.ShowWarning("Be careful")
ToastManager.Instance.ShowInfo("FYI")

// Manual toast
new Toast("Message", "Title")
    .Success()
    .Error()
    .Warning()
    .Info()
    .WithDuration(5000)
    .Persistent()
    .WithAction(button)
    .WithOnClose(() => { })
```

## Data Display

### Card
```csharp
new Card()
    .WithHeader(header)
    .WithContent(content)
    .WithFooter(footer)
    .Elevated()
    .Outlined()
    .Filled()
    .Clickable(onClick)
```

### Accordion
```csharp
new Accordion()
    .Single()
    .Multiple()
    .WithCollapsible()
    .AddItem(new AccordionItem("id", "Title")
        .WithContent(content)
        .Open()
        .Disabled())
    .WithOnItemToggle((id, isOpen) => { })
```

### Progress
```csharp
new Progress(value: 75, max: 100)
    .Indeterminate()
    .Small()
    .Large()
    .Primary()
    .Success()
    .Warning()
    .Error()
    .WithShowValue()
    .WithLabel("Loading...")
```

### Avatar
```csharp
new Avatar()
    .WithImage("user.jpg", "Alt text")
    .WithInitials("JD")
    .WithFallbackIcon(icon)
    .Small()
    .Large()
    .ExtraLarge()
    .WithCustomSize(48)
    .Circle()
    .Square()
    .Rounded()
    .WithStatus(AvatarStatus.Online)
```

### Badge
```csharp
new Badge("New")
    .Dot()
    .Primary()
    .Secondary()
    .Success()
    .Warning()
    .Error()
    .Info()
    .Small()
    .Large()
    .AttachTo(element)
```

### Spinner
```csharp
new Spinner()
    .Small()
    .Large()
    .WithCustomSize(32)
    .WithColor(Color.Blue)
    .WithThickness(3)
    .WithLabel("Loading...")
```

### Alert
```csharp
new Alert("Message", "Title")
    .Info()
    .Success()
    .Warning()
    .Error()
    .Dismissible()
    .WithIcon(icon)
    .WithAction(button)
    .WithOnDismiss(() => { })

alert.Dismiss()
```

## Styling

### Colors
```csharp
// Predefined
Color.Red
Color.Blue
Color.Green
Color.White
Color.Black
Color.Transparent

// From hex
Color.FromHex("#3B82F6")
Color.FromHex("#FF5733AA")  // With alpha

// From RGB
Color.FromRgb(59, 130, 246)

// From HSL
Color.FromHsl(215, 0.92, 0.6)

// Modifiers
color.WithAlpha(0.5)
color.Lighter(0.1)
color.Darker(0.1)
```

### EdgeInsets
```csharp
EdgeInsets.All(16)
EdgeInsets.Symmetric(horizontal: 16, vertical: 8)
EdgeInsets.Horizontal(16)
EdgeInsets.Vertical(8)
new EdgeInsets(top: 10, right: 15, bottom: 10, left: 15)
```

### Size
```csharp
new Size(width: 200, height: 100)
Size.Square(100)
Size.Zero
Size.Infinite
```

### Theme
```csharp
// Predefined themes
var light = Theme.Light()
var dark = Theme.Dark()

// Custom theme
var theme = new Theme
{
    Colors = new ColorPalette
    {
        Primary = Color.FromHex("#3B82F6"),
        Background = Color.White,
        // ... more colors
    },
    Typography = new Typography
    {
        FontFamily = "Inter",
        FontSizeBase = 16,
        // ... more settings
    },
    Spacing = new SpacingScale
    {
        BaseUnit = 4
    }
}

// Apply theme
app.Context.Theme = theme
```

### Style
```csharp
var style = new Style
{
    BackgroundColor = Color.Blue,
    ForegroundColor = Color.White,
    BorderRadius = 8,
    BorderWidth = 1,
    BorderColor = Color.Gray,
    Padding = EdgeInsets.All(16),
    Margin = EdgeInsets.All(8),
    Opacity = 0.9,
    FontSize = 16,
    FontWeight = FontWeight.Bold
}

element.WithStyle(style)
```

## Component Lifecycle

```csharp
public class MyComponent : ZorComponent
{
    public override void OnMount()
    {
        // Called when component is added to tree
    }

    protected override void BeforeBuild()
    {
        // Called before Build()
    }

    public override ZorElement Build()
    {
        // Return UI tree
        return new VStack();
    }

    protected override void AfterBuild()
    {
        // Called after Build()
    }

    public override void OnUpdate()
    {
        // Called when properties change
    }

    public override void OnUnmount()
    {
        // Called when component is removed
    }
}
```

## State Management

```csharp
public class MyComponent : ZorComponent
{
    // Field for state
    private int _count = 0;

    // Update state
    SetState(nameof(_count), ++_count);

    // Or with action
    SetState(() => 
    {
        _count++;
        // Multiple updates
    });

    // Get state
    var value = GetState<int>(nameof(_count), defaultValue: 0);
}
```

## Common Patterns

### Conditional Rendering
```csharp
var ui = new VStack();

if (condition)
{
    ui.AddChild(new Text("Shown when true"));
}
else
{
    ui.AddChild(new Text("Shown when false"));
}
```

### Lists
```csharp
var list = new VStack(spacing: 8);

foreach (var item in items)
{
    list.AddChild(
        new Text(item.Name)
    );
}
```

### Form with Validation
```csharp
public class FormExample : ZorComponent
{
    private string _email = "";
    private string? _error;

    private void Validate()
    {
        _error = IsValidEmail(_email) 
            ? null 
            : "Invalid email";
        SetState(() => { });
    }

    public override ZorElement Build()
    {
        return new TextField("Email")
            .WithValue(_email)
            .WithError(_error)
            .WithOnChange(email =>
            {
                _email = email;
                Validate();
            });
    }
}
```
