# ZorUI VS Code Extension

<div align="center">

![ZorUI](https://img.shields.io/badge/ZorUI-v1.0.0-blue?style=for-the-badge)
![VS Code](https://img.shields.io/badge/VS_Code-Extension-007ACC?style=for-the-badge&logo=visual-studio-code)

**Boost your productivity with ZorUI Framework in VS Code!**

</div>

---

## ✨ Features

### 🚀 Code Snippets

Over **30+ snippets** for ZorUI components:

- **Components**: `zuicomp`, `zuibtn`, `zuitext`, `zuicard`
- **Layout**: `zuivstack`, `zuihstack`, `zuigrid`, `zuiscroll`
- **Forms**: `zuitf`, `zuichk`, `zuiswitch`, `zuiradio`
- **Icons**: `zuiicon` - Insert Radix UI style icons
- **Pages**: `zuipage` - Full page layout template

### 🎯 Commands

- **ZorUI: Create Component** - Generate a new component file
- **ZorUI: Create Page** - Generate a new page file
- **ZorUI: Insert Icon** - Quick icon picker

### 🎨 Icon Library

120+ Radix UI style icons built-in:
- Navigation icons
- Action icons
- Status icons
- Social icons
- And more!

### ⚙️ Configuration

Customize the extension behavior in VS Code settings.

---

## 📦 Installation

### From Marketplace (Coming Soon)

1. Open VS Code
2. Go to Extensions (`Ctrl+Shift+X`)
3. Search for "ZorUI"
4. Click Install

### From VSIX

1. Download the `.vsix` file
2. Open VS Code
3. Go to Extensions
4. Click `...` → `Install from VSIX...`
5. Select the downloaded file

### Manual Installation

```bash
cd vscode-extension
npm install
npm run compile
npm run package
code --install-extension zorui-1.0.0.vsix
```

---

## 🚀 Usage

### Snippets

Type the prefix and press `Tab` to expand:

#### Component Snippet

```csharp
// Type: zuicomp
public class MyComponent : ZorComponent
{
    private int _state = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .AddChild(
                new Text("Hello").WithFontSize(24)
            );
    }
}
```

#### Button Snippet

```csharp
// Type: zuibtn
new Button("Click me", HandleClick)
    .Primary()
    .Medium()
```

#### VStack Snippet

```csharp
// Type: zuivstack
new VStack(spacing: 20)
    .WithPadding(EdgeInsets.All(16))
    .AddChild(child)
```

#### Icon Snippet

```csharp
// Type: zuiicon
new Icon(ZorIcon.Home)
    .WithSize(24)
    .WithColor(theme.Colors.Primary)
```

### Commands

#### Create Component

1. Press `Ctrl+Shift+P` (or `Cmd+Shift+P` on Mac)
2. Type `ZorUI: Create Component`
3. Enter component name
4. File is created with template

#### Insert Icon

1. Press `Ctrl+Shift+P`
2. Type `ZorUI: Insert Icon`
3. Select icon from list
4. Icon code is inserted at cursor

---

## 📋 Available Snippets

### Components

| Prefix | Description |
|--------|-------------|
| `zuicomp` | Create a ZorComponent |
| `zuibtn` | Create a Button |
| `zuitext` | Create a Text element |
| `zuicard` | Create a Card |
| `zuitf` | Create a TextField |
| `zuidialog` | Create a Dialog |
| `zuiicon` | Create an Icon |
| `zuitas` | Create Tabs |
| `zuistate` | Create state with setter |

### Layout

| Prefix | Description |
|--------|-------------|
| `zuivstack` | Create a VStack |
| `zuihstack` | Create an HStack |
| `zuizstack` | Create a ZStack |
| `zuicont` | Create a Container |
| `zuigrid` | Create a Grid |
| `zuiscroll` | Create a ScrollView |
| `zuispacer` | Create a Spacer |
| `zuidivider` | Create a Divider |
| `zuiwrap` | Create a Wrap |
| `zuipage` | Create a full page layout |

### Forms

| Prefix | Description |
|--------|-------------|
| `zuichk` | Create a Checkbox |
| `zuiswitch` | Create a Switch |
| `zuiradio` | Create a RadioGroup |
| `zuislider` | Create a Slider |
| `zuiform` | Create a complete form |

---

## 🎨 Icon System

### Using Icons

```csharp
using ZorUI.Icons;

// Using ZorIconComponent
var icon = new ZorIconComponent(ZorIcon.Home)
    .WithSize(24)
    .WithColor(theme.Colors.Primary);

// Using helper methods
var icon2 = ZIcon.Home()
    .Medium()
    .WithColor(Color.Blue);

// Available sizes
icon.Small();      // 16px
icon.Medium();     // 24px
icon.Large();      // 32px
icon.ExtraLarge(); // 48px
```

### Available Icon Categories

- **Navigation**: Home, Menu, Close, Chevron*, Arrow*
- **Actions**: Plus, Minus, Check, X, Edit, Trash, Search
- **User**: User, Users, Settings, Lock, Key
- **Communication**: Mail, Phone, Chat, Bell
- **Media**: Play, Pause, Stop, Volume*
- **Files**: File, Folder, Download, Upload
- **Status**: Info, Warning, Error, Success
- **Social**: Heart, Star, Bookmark, Share
- **Time**: Calendar, Clock, Timer
- **Layout**: Grid, List, Columns, Rows
- **Visibility**: Eye, EyeOff
- **Misc**: Link, Globe, Code, Moon, Sun

[See full icon list](../docs/ICONS.md)

---

## ⚙️ Configuration

### Settings

Open VS Code settings and search for "ZorUI":

```json
{
  "zorui.useFluentApi": true,
  "zorui.defaultTheme": "light"
}
```

#### Available Settings

- `zorui.useFluentApi` (boolean) - Use Fluent API style in snippets
- `zorui.defaultTheme` (string) - Default theme: `"light"` or `"dark"`

---

## 🎯 Examples

### Create a Counter Component

1. Type `zuicomp` and press Tab
2. Modify the template:

```csharp
public class Counter : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .AddChild(
                new Text($"Count: {_count}")
                    .WithFontSize(32)
                    .Bold()
            )
            .AddChild(
                new Button("Increment", () => 
                    SetState(nameof(_count), ++_count))
                    .Primary()
            );
    }
}
```

### Create a Form

Type `zuiform` and press Tab:

```csharp
new VStack(spacing: 16)
    .WithPadding(EdgeInsets.All(16))
    
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
        new Button("Submit", HandleSubmit)
            .Primary()
            .WithFullWidth()
            .Disabled(string.IsNullOrEmpty(_name))
    )
```

### Create a Page with Icon

Type `zuipage` then add icons:

```csharp
public class HomePage : ZorComponent
{
    public override ZorElement Build()
    {
        return new VStack(spacing: 0)
            .AddChild(
                new Container()
                    .WithPadding(EdgeInsets.All(16))
                    .AddChild(
                        new HStack(spacing: 12)
                            .AddChild(ZIcon.Home().Medium())
                            .AddChild(new Text("Home").Bold())
                    )
            );
    }
}
```

---

## 🔧 Development

### Setup

```bash
cd vscode-extension
npm install
```

### Compile

```bash
npm run compile
```

### Watch Mode

```bash
npm run watch
```

### Package

```bash
npm run package
```

This creates a `.vsix` file for distribution.

### Test Locally

1. Press `F5` in VS Code to launch Extension Development Host
2. Test snippets and commands
3. Debug in the Extension Development Host window

---

## 📚 Resources

- [ZorUI Documentation](../docs/)
- [ZorUI GitHub](https://github.com/zorui/zorui)
- [Icon Reference](../docs/ICONS.md)
- [Component Reference](../docs/QuickReference.md)

---

## 🤝 Contributing

Contributions are welcome!

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

---

## 📝 License

MIT License - see [LICENSE](../LICENSE)

---

## 🎉 Enjoy!

**Happy coding with ZorUI!** 🚀

If you like this extension, please ⭐ star the [repo](https://github.com/zorui/zorui)!
