# 🎨 ZorUI Icons - Radix UI Style

Complete icon reference for ZorUI framework.

---

## 📦 Installation

Icons are part of the `ZorUI.Icons` package:

```bash
dotnet add package ZorUI.Icons
```

Or reference the project:

```bash
dotnet add reference ../src/ZorUI.Icons/ZorUI.Icons.csproj
```

---

## 🚀 Usage

### Basic Usage

```csharp
using ZorUI.Icons;

// Create an icon
var icon = new ZorIconComponent(ZorIcon.Home);

// With customization
var icon2 = new ZorIconComponent(ZorIcon.Search)
    .WithSize(24)
    .WithColor(Color.Blue)
    .WithStrokeWidth(2);
```

### Using Helper Methods

```csharp
using ZorUI.Icons;

// Quick icon creation
var homeIcon = ZIcon.Home();
var menuIcon = ZIcon.Menu();
var searchIcon = ZIcon.Search();

// With chaining
var icon = ZIcon.Settings()
    .Large()
    .WithColor(theme.Colors.Primary);
```

### Size Presets

```csharp
icon.Small();      // 16px
icon.Medium();     // 24px (default)
icon.Large();      // 32px
icon.ExtraLarge(); // 48px

// Or custom size
icon.WithSize(20);
```

---

## 📋 Complete Icon List

### 🧭 Navigation (18 icons)

| Icon | Enum | Usage |
|------|------|-------|
| 🏠 Home | `ZorIcon.Home` | `ZIcon.Home()` |
| ☰ Menu | `ZorIcon.Menu` | `ZIcon.Menu()` |
| ☰ Menu Alt | `ZorIcon.MenuAlt` | `new ZorIconComponent(ZorIcon.MenuAlt)` |
| ✕ Close | `ZorIcon.Close` | `ZIcon.Close()` |
| ‹ Chevron Left | `ZorIcon.ChevronLeft` | `new ZorIconComponent(ZorIcon.ChevronLeft)` |
| › Chevron Right | `ZorIcon.ChevronRight` | `new ZorIconComponent(ZorIcon.ChevronRight)` |
| ∧ Chevron Up | `ZorIcon.ChevronUp` | `new ZorIconComponent(ZorIcon.ChevronUp)` |
| ∨ Chevron Down | `ZorIcon.ChevronDown` | `new ZorIconComponent(ZorIcon.ChevronDown)` |
| « Double Arrow Left | `ZorIcon.DoubleArrowLeft` | `new ZorIconComponent(ZorIcon.DoubleArrowLeft)` |
| » Double Arrow Right | `ZorIcon.DoubleArrowRight` | `new ZorIconComponent(ZorIcon.DoubleArrowRight)` |
| ⇧ Double Arrow Up | `ZorIcon.DoubleArrowUp` | `new ZorIconComponent(ZorIcon.DoubleArrowUp)` |
| ⇩ Double Arrow Down | `ZorIcon.DoubleArrowDown` | `new ZorIconComponent(ZorIcon.DoubleArrowDown)` |
| ← Arrow Left | `ZorIcon.ArrowLeft` | `new ZorIconComponent(ZorIcon.ArrowLeft)` |
| → Arrow Right | `ZorIcon.ArrowRight` | `new ZorIconComponent(ZorIcon.ArrowRight)` |
| ↑ Arrow Up | `ZorIcon.ArrowUp` | `new ZorIconComponent(ZorIcon.ArrowUp)` |
| ↓ Arrow Down | `ZorIcon.ArrowDown` | `new ZorIconComponent(ZorIcon.ArrowDown)` |

### ⚡ Actions (16 icons)

| Icon | Enum | Usage |
|------|------|-------|
| + Plus | `ZorIcon.Plus` | `ZIcon.Plus()` |
| − Minus | `ZorIcon.Minus` | `new ZorIconComponent(ZorIcon.Minus)` |
| ✓ Check | `ZorIcon.Check` | `ZIcon.Check()` |
| ✕ X | `ZorIcon.X` | `new ZorIconComponent(ZorIcon.X)` |
| ✎ Edit | `ZorIcon.Edit` | `new ZorIconComponent(ZorIcon.Edit)` |
| 🗑 Trash | `ZorIcon.Trash` | `new ZorIconComponent(ZorIcon.Trash)` |
| 📄 Copy | `ZorIcon.Copy` | `new ZorIconComponent(ZorIcon.Copy)` |
| 📋 Paste | `ZorIcon.Paste` | `new ZorIconComponent(ZorIcon.Paste)` |
| ✂ Cut | `ZorIcon.Cut` | `new ZorIconComponent(ZorIcon.Cut)` |
| 💾 Save | `ZorIcon.Save` | `new ZorIconComponent(ZorIcon.Save)` |
| ⬇ Download | `ZorIcon.Download` | `new ZorIconComponent(ZorIcon.Download)` |
| ⬆ Upload | `ZorIcon.Upload` | `new ZorIconComponent(ZorIcon.Upload)` |
| ↻ Refresh | `ZorIcon.Refresh` | `new ZorIconComponent(ZorIcon.Refresh)` |
| 🔍 Search | `ZorIcon.Search` | `ZIcon.Search()` |
| 🔽 Filter | `ZorIcon.Filter` | `new ZorIconComponent(ZorIcon.Filter)` |
| ⇅ Sort | `ZorIcon.Sort` | `new ZorIconComponent(ZorIcon.Sort)` |

### 👤 User & Account (9 icons)

| Icon | Enum | Usage |
|------|------|-------|
| 👤 User | `ZorIcon.User` | `ZIcon.User()` |
| 👤+ User Plus | `ZorIcon.UserPlus` | `new ZorIconComponent(ZorIcon.UserPlus)` |
| 👥 Users | `ZorIcon.Users` | `new ZorIconComponent(ZorIcon.Users)` |
| 👤 Account | `ZorIcon.Account` | `new ZorIconComponent(ZorIcon.Account)` |
| ⚙ Settings | `ZorIcon.Settings` | `ZIcon.Settings()` |
| ⚙ Gear | `ZorIcon.Gear` | `new ZorIconComponent(ZorIcon.Gear)` |
| 🔒 Lock | `ZorIcon.Lock` | `new ZorIconComponent(ZorIcon.Lock)` |
| 🔓 Unlock | `ZorIcon.Unlock` | `new ZorIconComponent(ZorIcon.Unlock)` |
| 🔑 Key | `ZorIcon.Key` | `new ZorIconComponent(ZorIcon.Key)` |

### 💬 Communication (7 icons)

| Icon | Enum | Usage |
|------|------|-------|
| ✉ Mail | `ZorIcon.Mail` | `new ZorIconComponent(ZorIcon.Mail)` |
| 📧 Mail Open | `ZorIcon.MailOpen` | `new ZorIconComponent(ZorIcon.MailOpen)` |
| 📞 Phone | `ZorIcon.Phone` | `new ZorIconComponent(ZorIcon.Phone)` |
| 💬 Chat | `ZorIcon.Chat` | `new ZorIconComponent(ZorIcon.Chat)` |
| 💬 Message | `ZorIcon.Message` | `new ZorIconComponent(ZorIcon.Message)` |
| 🔔 Bell | `ZorIcon.Bell` | `new ZorIconComponent(ZorIcon.Bell)` |
| 🔕 Bell Off | `ZorIcon.BellOff` | `new ZorIconComponent(ZorIcon.BellOff)` |

### 🎵 Media (11 icons)

| Icon | Enum | Usage |
|------|------|-------|
| ▶ Play | `ZorIcon.Play` | `new ZorIconComponent(ZorIcon.Play)` |
| ⏸ Pause | `ZorIcon.Pause` | `new ZorIconComponent(ZorIcon.Pause)` |
| ⏹ Stop | `ZorIcon.Stop` | `new ZorIconComponent(ZorIcon.Stop)` |
| ⏮ Previous | `ZorIcon.Previous` | `new ZorIconComponent(ZorIcon.Previous)` |
| ⏭ Next | `ZorIcon.Next` | `new ZorIconComponent(ZorIcon.Next)` |
| 🔊 Volume Up | `ZorIcon.VolumeUp` | `new ZorIconComponent(ZorIcon.VolumeUp)` |
| 🔉 Volume Down | `ZorIcon.VolumeDown` | `new ZorIconComponent(ZorIcon.VolumeDown)` |
| 🔇 Volume Mute | `ZorIcon.VolumeMute` | `new ZorIconComponent(ZorIcon.VolumeMute)` |
| 🖼 Image | `ZorIcon.Image` | `new ZorIconComponent(ZorIcon.Image)` |
| 🎥 Video | `ZorIcon.Video` | `new ZorIconComponent(ZorIcon.Video)` |
| 🎵 Music | `ZorIcon.Music` | `new ZorIconComponent(ZorIcon.Music)` |

### 📁 Files & Folders (6 icons)

| Icon | Enum | Usage |
|------|------|-------|
| 📄 File | `ZorIcon.File` | `new ZorIconComponent(ZorIcon.File)` |
| 📄 File Text | `ZorIcon.FileText` | `new ZorIconComponent(ZorIcon.FileText)` |
| 📄+ File Plus | `ZorIcon.FilePlus` | `new ZorIconComponent(ZorIcon.FilePlus)` |
| 📁 Folder | `ZorIcon.Folder` | `new ZorIconComponent(ZorIcon.Folder)` |
| 📂 Folder Open | `ZorIcon.FolderOpen` | `new ZorIconComponent(ZorIcon.FolderOpen)` |
| 📁+ Folder Plus | `ZorIcon.FolderPlus` | `new ZorIconComponent(ZorIcon.FolderPlus)` |

### ⚠ Status & Alerts (10 icons)

| Icon | Enum | Usage |
|------|------|-------|
| ℹ Info | `ZorIcon.Info` | `new ZorIconComponent(ZorIcon.Info)` |
| ⓘ Info Circled | `ZorIcon.InfoCircled` | `new ZorIconComponent(ZorIcon.InfoCircled)` |
| ⚠ Warning | `ZorIcon.Warning` | `new ZorIconComponent(ZorIcon.Warning)` |
| ⚠ Warning Circled | `ZorIcon.WarningCircled` | `new ZorIconComponent(ZorIcon.WarningCircled)` |
| ✕ Error | `ZorIcon.Error` | `new ZorIconComponent(ZorIcon.Error)` |
| ⓧ Error Circled | `ZorIcon.ErrorCircled` | `new ZorIconComponent(ZorIcon.ErrorCircled)` |
| ✓ Success | `ZorIcon.Success` | `new ZorIconComponent(ZorIcon.Success)` |
| ✓ Success Circled | `ZorIcon.SuccessCircled` | `new ZorIconComponent(ZorIcon.SuccessCircled)` |
| ? Question | `ZorIcon.Question` | `new ZorIconComponent(ZorIcon.Question)` |
| ? Question Circled | `ZorIcon.QuestionCircled` | `new ZorIconComponent(ZorIcon.QuestionCircled)` |

### 💙 Social & Engagement (8 icons)

| Icon | Enum | Usage |
|------|------|-------|
| ♡ Heart | `ZorIcon.Heart` | `new ZorIconComponent(ZorIcon.Heart)` |
| ♥ Heart Filled | `ZorIcon.HeartFilled` | `new ZorIconComponent(ZorIcon.HeartFilled)` |
| ☆ Star | `ZorIcon.Star` | `new ZorIconComponent(ZorIcon.Star)` |
| ★ Star Filled | `ZorIcon.StarFilled` | `new ZorIconComponent(ZorIcon.StarFilled)` |
| 🔖 Bookmark | `ZorIcon.Bookmark` | `new ZorIconComponent(ZorIcon.Bookmark)` |
| 🔖 Bookmark Filled | `ZorIcon.BookmarkFilled` | `new ZorIconComponent(ZorIcon.BookmarkFilled)` |
| 🔗 Share | `ZorIcon.Share` | `new ZorIconComponent(ZorIcon.Share)` |
| 👍 Thumbs Up | `ZorIcon.ThumbsUp` | `new ZorIconComponent(ZorIcon.ThumbsUp)` |
| 👎 Thumbs Down | `ZorIcon.ThumbsDown` | `new ZorIconComponent(ZorIcon.ThumbsDown)` |

### 📅 Time & Calendar (4 icons)

| Icon | Enum | Usage |
|------|------|-------|
| 📅 Calendar | `ZorIcon.Calendar` | `new ZorIconComponent(ZorIcon.Calendar)` |
| 🕐 Clock | `ZorIcon.Clock` | `new ZorIconComponent(ZorIcon.Clock)` |
| ⏱ Timer | `ZorIcon.Timer` | `new ZorIconComponent(ZorIcon.Timer)` |
| ⏱ Stopwatch | `ZorIcon.Stopwatch` | `new ZorIconComponent(ZorIcon.Stopwatch)` |

### 📐 Layout & View (6 icons)

| Icon | Enum | Usage |
|------|------|-------|
| ▦ Grid | `ZorIcon.Grid` | `new ZorIconComponent(ZorIcon.Grid)` |
| ☰ List | `ZorIcon.List` | `new ZorIconComponent(ZorIcon.List)` |
| ⫴ Columns | `ZorIcon.Columns` | `new ZorIconComponent(ZorIcon.Columns)` |
| ☰ Rows | `ZorIcon.Rows` | `new ZorIconComponent(ZorIcon.Rows)` |
| 📐 Layout | `ZorIcon.Layout` | `new ZorIconComponent(ZorIcon.Layout)` |
| ⫸ Sidebar | `ZorIcon.Sidebar` | `new ZorIconComponent(ZorIcon.Sidebar)` |

### 👁 Visibility (3 icons)

| Icon | Enum | Usage |
|------|------|-------|
| 👁 Eye | `ZorIcon.Eye` | `new ZorIconComponent(ZorIcon.Eye)` |
| 🚫 Eye Off | `ZorIcon.EyeOff` | `new ZorIconComponent(ZorIcon.EyeOff)` |
| 👁 Eye Open | `ZorIcon.EyeOpen` | `new ZorIconComponent(ZorIcon.EyeOpen)` |

### 📝 Text Formatting (8 icons)

| Icon | Enum | Usage |
|------|------|-------|
| **B** Bold | `ZorIcon.Bold` | `new ZorIconComponent(ZorIcon.Bold)` |
| *I* Italic | `ZorIcon.Italic` | `new ZorIconComponent(ZorIcon.Italic)` |
| <u>U</u> Underline | `ZorIcon.Underline` | `new ZorIconComponent(ZorIcon.Underline)` |
| ~~S~~ Strikethrough | `ZorIcon.Strikethrough` | `new ZorIconComponent(ZorIcon.Strikethrough)` |
| ⫷ Align Left | `ZorIcon.AlignLeft` | `new ZorIconComponent(ZorIcon.AlignLeft)` |
| ⫶ Align Center | `ZorIcon.AlignCenter` | `new ZorIconComponent(ZorIcon.AlignCenter)` |
| ⫸ Align Right | `ZorIcon.AlignRight` | `new ZorIconComponent(ZorIcon.AlignRight)` |
| ⫹ Align Justify | `ZorIcon.AlignJustify` | `new ZorIconComponent(ZorIcon.AlignJustify)` |

### 🔗 Miscellaneous (30+ icons)

| Icon | Enum | Usage |
|------|------|-------|
| 🔗 Link | `ZorIcon.Link` | `new ZorIconComponent(ZorIcon.Link)` |
| 🔗 Link Break | `ZorIcon.LinkBreak` | `new ZorIconComponent(ZorIcon.LinkBreak)` |
| ⎋ External | `ZorIcon.External` | `new ZorIconComponent(ZorIcon.External)` |
| 📌 Pin | `ZorIcon.Pin` | `new ZorIconComponent(ZorIcon.Pin)` |
| 📌 Pin Filled | `ZorIcon.PinFilled` | `new ZorIconComponent(ZorIcon.PinFilled)` |
| 🏷 Tag | `ZorIcon.Tag` | `new ZorIconComponent(ZorIcon.Tag)` |
| 🚩 Flag | `ZorIcon.Flag` | `new ZorIconComponent(ZorIcon.Flag)` |
| 🏆 Award | `ZorIcon.Award` | `new ZorIconComponent(ZorIcon.Award)` |
| 🛡 Shield | `ZorIcon.Shield` | `new ZorIconComponent(ZorIcon.Shield)` |
| ⚡ Lightning | `ZorIcon.Lightning` | `new ZorIconComponent(ZorIcon.Lightning)` |
| 🌙 Moon | `ZorIcon.Moon` | `new ZorIconComponent(ZorIcon.Moon)` |
| ☀ Sun | `ZorIcon.Sun` | `new ZorIconComponent(ZorIcon.Sun)` |
| ☁ Cloud | `ZorIcon.Cloud` | `new ZorIconComponent(ZorIcon.Cloud)` |
| 📶 Wifi | `ZorIcon.Wifi` | `new ZorIconComponent(ZorIcon.Wifi)` |
| 📵 Wifi Off | `ZorIcon.WifiOff` | `new ZorIconComponent(ZorIcon.WifiOff)` |
| 🔋 Battery | `ZorIcon.Battery` | `new ZorIconComponent(ZorIcon.Battery)` |
| 🪫 Battery Low | `ZorIcon.BatteryLow` | `new ZorIconComponent(ZorIcon.BatteryLow)` |
| 🌐 Globe | `ZorIcon.Globe` | `new ZorIconComponent(ZorIcon.Globe)` |
| 🗺 Map | `ZorIcon.Map` | `new ZorIconComponent(ZorIcon.Map)` |
| 📍 Location | `ZorIcon.Location` | `new ZorIconComponent(ZorIcon.Location)` |
| 🧭 Compass | `ZorIcon.Compass` | `new ZorIconComponent(ZorIcon.Compass)` |

### 💻 Development (7 icons)

| Icon | Enum | Usage |
|------|------|-------|
| 💻 Code | `ZorIcon.Code` | `new ZorIconComponent(ZorIcon.Code)` |
| >_ Terminal | `ZorIcon.Terminal` | `new ZorIconComponent(ZorIcon.Terminal)` |
| 🐛 Bug | `ZorIcon.Bug` | `new ZorIconComponent(ZorIcon.Bug)` |
| 🔀 Git | `ZorIcon.Git` | `new ZorIconComponent(ZorIcon.Git)` |
| 🌿 Git Branch | `ZorIcon.GitBranch` | `new ZorIconComponent(ZorIcon.GitBranch)` |
| • Git Commit | `ZorIcon.GitCommit` | `new ZorIconComponent(ZorIcon.GitCommit)` |
| 📦 Package | `ZorIcon.Package` | `new ZorIconComponent(ZorIcon.Package)` |
| 📦 Box | `ZorIcon.Box` | `new ZorIconComponent(ZorIcon.Box)` |

---

## 💡 Examples

### Simple Icon

```csharp
var homeIcon = new ZorIconComponent(ZorIcon.Home);
```

### Styled Icon

```csharp
var settingsIcon = new ZorIconComponent(ZorIcon.Settings)
    .WithSize(32)
    .WithColor(Color.FromHex("#3B82F6"))
    .WithStrokeWidth(2.5);
```

### Icon in Button

```csharp
new Button("Settings", OpenSettings)
    .WithLeadingIcon(
        new ZorIconComponent(ZorIcon.Settings)
            .Medium()
            .WithColor(theme.Colors.PrimaryForeground)
    )
    .Primary();
```

### Icon Navigation

```csharp
new HStack(spacing: 24)
    .AddChild(
        new Button("", () => GoBack())
            .Ghost()
            .WithLeadingIcon(ZIcon.ChevronLeft())
    )
    .AddChild(new Text("Back"))
    .AddChild(new Spacer())
    .AddChild(
        new Button("", () => OpenMenu())
            .Ghost()
            .WithLeadingIcon(ZIcon.Menu())
    );
```

### Status Icons

```csharp
// Success message
new HStack(spacing: 8)
    .AddChild(
        new ZorIconComponent(ZorIcon.SuccessCircled)
            .WithColor(theme.Colors.Success)
    )
    .AddChild(new Text("Success!"));

// Error message
new HStack(spacing: 8)
    .AddChild(
        new ZorIconComponent(ZorIcon.ErrorCircled)
            .WithColor(theme.Colors.Error)
    )
    .AddChild(new Text("Error occurred"));
```

---

## 🎨 Customization

### Colors

```csharp
icon.WithColor(Color.Blue);
icon.WithColor(Color.FromHex("#FF6B6B"));
icon.WithColor(theme.Colors.Primary);
```

### Sizes

```csharp
icon.Small();           // 16px
icon.Medium();          // 24px
icon.Large();           // 32px
icon.ExtraLarge();      // 48px
icon.WithSize(20);      // Custom
```

### Stroke

```csharp
icon.WithStrokeWidth(1);    // Thin
icon.WithStrokeWidth(2);    // Default
icon.WithStrokeWidth(3);    // Bold
```

### Fill

```csharp
icon.Filled();              // Filled version
icon.Filled(false);         // Outline version
```

---

## 📚 Resources

- [Component Reference](QuickReference.md)
- [VS Code Extension](../vscode-extension/README.md)
- [GitHub Repository](https://github.com/zorui/zorui)

---

## 🎉 Total: 120+ Icons!

All icons are styled like Radix UI and ready to use in your ZorUI applications!
