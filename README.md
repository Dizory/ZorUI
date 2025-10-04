# ZorUI

<div align="center">

![ZorUI Logo](docs/logo.svg)

**Кроссплатформенный UI фреймворк для .NET, вдохновленный Radix UI**

[![.NET](https://img.shields.io/badge/.NET-8.0%20%7C%209.0-512BD4)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)]()

[Документация](docs/README.md) • [Примеры](samples/) • [API Reference](docs/api/) • [Contributing](CONTRIBUTING.md)

</div>

---

## 🚀 О проекте

**ZorUI** — это современный кроссплатформенный UI фреймворк для .NET 8/9, который предоставляет мощный и интуитивный способ создания пользовательских интерфейсов без использования XAML. Вдохновленный дизайн-системой Radix UI, ZorUI предлагает:

- ✨ **Fluent API** — чистый C# код без XAML
- 🎨 **30+ готовых компонентов** — от базовых до продвинутых
- 🌓 **Система тем** — светлая/темная темы из коробки
- ♿ **Доступность** — полная поддержка accessibility
- 🔄 **Реактивное состояние** — автоматическое обновление UI
- 🌍 **Кроссплатформенность** — Windows, Linux, macOS (готово!), Android, iOS (в разработке)

## 🌍 Поддерживаемые платформы

| Платформа | Статус | Рендерер |
|-----------|--------|----------|
| 🖥️ **Windows** | ✅ Готово | SkiaSharp |
| 🐧 **Linux** | ✅ Готово | SkiaSharp |
| 🍎 **macOS** | ✅ Готово | SkiaSharp |
| 📱 **Android** | 🔄 В разработке | MAUI + Skia |
| 🍏 **iOS** | 🔄 В разработке | MAUI + Skia |
| 🌐 **Web** | 🔮 Планируется | Blazor WASM |

**ОДИН КОД UI - ВСЕ ПЛАТФОРМЫ!** 🎉

Подробнее: [PLATFORM_GUIDE.md](PLATFORM_GUIDE.md)

## 📦 Установка

### NuGet Package Manager

```bash
dotnet add package ZorUI.Core
dotnet add package ZorUI.Components
dotnet add package ZorUI.Styling
```

### Package Manager Console

```powershell
Install-Package ZorUI.Core
Install-Package ZorUI.Components
Install-Package ZorUI.Styling
```

## 🚀 Запуск примеров

### Console приложение (для тестирования)
```bash
cd samples/BasicApp
dotnet run
```

### 🎨 Desktop GUI приложение (Windows/Linux/macOS)
```bash
cd samples/DesktopApp
dotnet run
```

**Да! Это настоящее кроссплатформенное GUI приложение!** 🎉

### Галерея всех компонентов
```bash
cd samples/ComponentGallery
dotnet run
```

## 🎯 Быстрый старт

### Создание Desktop приложения

```csharp
using ZorUI.Core;
using ZorUI.Rendering.Skia;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;

// Создайте окно (работает на Windows/Linux/macOS!)
var window = new SkiaWindow(800, 600, "My App");

// Установите UI
window.RootComponent = new MyApp();

// Покажите окно
window.Show();

// Ваш UI компонент
public class MyApp : ZorComponent
{
    private int count = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .AddChild(
                new Text($"Count: {count}")
                    .WithFontSize(32)
                    .Bold()
            )
            .AddChild(
                new Button("Increment", () => 
                {
                    SetState(nameof(count), ++count);
                })
                .Primary()
            );
    }
}
```

### Создание Console приложения

```csharp
using ZorUI.Core;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;

// Создаем приложение
var app = new ZorApplication();

// Запускаем с корневым компонентом
app.Run(new MyApp());

// Определяем компонент
public class MyApp : ZorComponent
{
    private int count = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .AddChild(
                new Text($"Count: {count}")
                    .WithFontSize(32)
                    .Bold()
            )
            .AddChild(
                new Button("Increment", () => 
                {
                    SetState(nameof(count), ++count);
                })
                .Primary()
            );
    }
}
```

## 🧩 Компоненты

### Layout Components

```csharp
// Вертикальный стек
new VStack(spacing: 16,
    new Text("Title"),
    new Text("Subtitle"),
    new Button("Action")
)

// Горизонтальный стек
new HStack(spacing: 8,
    new Icon("home"),
    new Text("Home")
)

// Z-стек (наложение)
new ZStack(
    new Image("background.jpg"),
    new Text("Overlay Text")
)
```

### Form Components

```csharp
// Текстовое поле
new TextField("Enter email...")
    .Email()
    .Required()
    .WithOnChange(email => Console.WriteLine(email))

// Чекбокс
new Checkbox("Accept terms", isChecked: false)
    .WithOnChange(checked => HandleAccept(checked))

// Переключатель
new Switch("Enable notifications", isOn: true)
    .Large()
    .WithOnChange(on => HandleToggle(on))

// Слайдер
new Slider(min: 0, max: 100, value: 50)
    .WithStep(5)
    .WithShowValue()
    .WithOnChange(value => HandleSlider(value))

// Радио группа
new RadioGroup("size")
    .AddRadio(new Radio("small", "Small"))
    .AddRadio(new Radio("medium", "Medium").Selected())
    .AddRadio(new Radio("large", "Large"))
    .WithOnValueChange(HandleSizeChange)
```

### Button Variants

```csharp
new Button("Default")

new Button("Primary").Primary()

new Button("Secondary").Secondary()

new Button("Destructive").Destructive()

new Button("Ghost").Ghost()

new Button("Link").Link()

// С иконками
new Button("Save")
    .Primary()
    .WithLeadingIcon(new Icon("save"))
    .Large()
```

### Navigation Components

```csharp
// Табы
new Tabs("home")
    .AddTab(new Tab("home", "Home")
        .WithContent(new Text("Home content")))
    .AddTab(new Tab("profile", "Profile")
        .WithContent(new Text("Profile content")))
    .WithOnTabChange(HandleTabChange)

// Меню
new Menu()
    .WithTrigger(new Button("Options"))
    .AddItem(new MenuItem("Edit", HandleEdit))
    .AddItem(new MenuItem("Delete", HandleDelete).Destructive())
    .AddSeparator()
    .AddItem(new MenuItem("Settings", HandleSettings))
```

### Overlay Components

```csharp
// Диалог
new Dialog("Confirm Action")
    .WithContent(new Text("Are you sure?"))
    .WithFooter(
        new HStack(spacing: 8,
            new Button("Cancel", dialog.Close()),
            new Button("Confirm", HandleConfirm).Destructive()
        )
    )

// Тултип
new Tooltip("Click to edit")
    .WithTrigger(new Button("Edit"))
    .WithPlacement(TooltipPlacement.Top)

// Попover
new Popover()
    .WithTrigger(new Button("More"))
    .WithContent(new VStack(spacing: 8,
        new Text("Additional options"),
        new Button("Option 1")
    ))

// Тост
ToastManager.Instance.ShowSuccess("Changes saved!");
ToastManager.Instance.ShowError("Failed to save", "Error");
```

### Data Display Components

```csharp
// Карточка
new Card()
    .WithHeader(new Text("Card Title").Bold())
    .WithContent(new Text("Card content here"))
    .WithFooter(new Button("Action"))
    .Elevated()

// Аккордеон
new Accordion()
    .Single()
    .AddItem(new AccordionItem("faq1", "What is ZorUI?")
        .WithContent(new Text("A UI framework...")))
    .AddItem(new AccordionItem("faq2", "How to install?")
        .WithContent(new Text("Use NuGet...")))

// Прогресс бар
new Progress(value: 75, max: 100)
    .Success()
    .WithShowValue()

// Аватар
new Avatar()
    .WithImage("user.jpg")
    .WithInitials("JD")
    .Large()
    .WithStatus(AvatarStatus.Online)

// Бейдж
new Badge("New")
    .Primary()
    .Small()

// Алерт
new Alert("Update available!")
    .Info()
    .Dismissible()
    .WithAction(new Button("Update"))

// Спиннер
new Spinner()
    .Large()
    .WithLabel("Loading...")
```

## 🎨 Стилизация и темы

### Использование тем

```csharp
// Светлая тема
var lightTheme = Theme.Light();

// Темная тема
var darkTheme = Theme.Dark();

// Применение темы
app.Context.Theme = darkTheme;

// Кастомная тема
var customTheme = new Theme
{
    Colors = new ColorPalette
    {
        Primary = Color.FromHex("#FF6B6B"),
        Background = Color.FromHex("#1A1A2E"),
        Foreground = Color.White
    },
    Typography = new Typography
    {
        FontFamily = "Roboto",
        FontSizeBase = 16
    }
};
```

### Стили компонентов

```csharp
var buttonStyle = new Style
{
    BackgroundColor = Color.FromHex("#3B82F6"),
    ForegroundColor = Color.White,
    BorderRadius = 8,
    Padding = EdgeInsets.Symmetric(horizontal: 16, vertical: 8)
};

new Button("Styled Button")
    .WithStyle(buttonStyle);
```

### Responsive модификаторы

```csharp
new Container()
    .WithPadding(EdgeInsets.All(16))
    .WithWidth(300)
    .WithHeight(200)
    .WithBackground(Color.FromHex("#F3F4F6"));
```

## 📚 Документация

### Структура документации

- **[Getting Started](docs/getting-started.md)** - Начало работы с ZorUI
- **[Core Concepts](docs/core-concepts.md)** - Основные концепции фреймворка
- **[Components](docs/components/)** - Полное руководство по компонентам
  - [Layout](docs/components/layout.md)
  - [Forms](docs/components/forms.md)
  - [Navigation](docs/components/navigation.md)
  - [Overlays](docs/components/overlays.md)
  - [Data Display](docs/components/data-display.md)
- **[Styling](docs/styling.md)** - Система стилей и тем
- **[State Management](docs/state-management.md)** - Управление состоянием
- **[Best Practices](docs/best-practices.md)** - Лучшие практики
- **[API Reference](docs/api/)** - Полная API документация

## 🏗️ Архитектура

```
ZorUI/
├── src/
│   ├── ZorUI.Core/              # Ядро фреймворка
│   │   ├── ZorElement.cs        # Базовый элемент
│   │   ├── ZorComponent.cs      # Базовый компонент
│   │   ├── ZorApplication.cs    # Приложение
│   │   ├── BuildContext.cs      # Контекст сборки
│   │   └── Properties/          # Общие свойства
│   │
│   ├── ZorUI.Styling/           # Система стилей
│   │   ├── Style.cs             # Стили
│   │   └── Theme.cs             # Темы
│   │
│   ├── ZorUI.Components/        # Библиотека компонентов
│   │   ├── Layout/              # Layout компоненты
│   │   ├── Primitives/          # Базовые UI элементы
│   │   ├── Forms/               # Формы
│   │   ├── Navigation/          # Навигация
│   │   ├── Overlays/            # Оверлеи
│   │   └── DataDisplay/         # Отображение данных
│   │
│   └── ZorUI.Rendering/         # Система рендеринга
│       ├── IRenderer.cs         # Интерфейс рендерера
│       └── ConsoleRenderer.cs   # Консольный рендерер
│
├── samples/                     # Примеры приложений
│   ├── BasicApp/                # Базовое приложение
│   └── ComponentGallery/        # Галерея компонентов
│
└── docs/                        # Документация
```

## 💡 Примеры

### Форма регистрации

```csharp
public class RegistrationForm : ZorComponent
{
    private string _email = "";
    private string _password = "";
    private bool _agreedToTerms = false;

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .WithPadding(24)
            .AddChild(
                new Text("Create Account")
                    .WithFontSize(28)
                    .Bold()
            )
            .AddChild(
                new TextField("Email")
                    .Email()
                    .Required()
                    .WithValue(_email)
                    .WithOnChange(v => SetState(nameof(_email), _email = v))
            )
            .AddChild(
                new TextField("Password")
                    .Password()
                    .Required()
                    .WithMinLength(8)
                    .WithValue(_password)
                    .WithOnChange(v => SetState(nameof(_password), _password = v))
            )
            .AddChild(
                new Checkbox("I agree to the Terms and Conditions", _agreedToTerms)
                    .WithOnChange(v => SetState(nameof(_agreedToTerms), _agreedToTerms = v))
            )
            .AddChild(
                new Button("Sign Up", HandleSignUp)
                    .Primary()
                    .WithFullWidth()
                    .Disabled(!IsFormValid())
            );
    }

    private bool IsFormValid() => 
        !string.IsNullOrEmpty(_email) && 
        _password.Length >= 8 && 
        _agreedToTerms;

    private void HandleSignUp()
    {
        Console.WriteLine($"Signing up with {_email}");
        ToastManager.Instance.ShowSuccess("Account created!");
    }
}
```

### Dashboard приложение

```csharp
public class Dashboard : ZorComponent
{
    private string _activeTab = "overview";

    public override ZorElement Build()
    {
        return new VStack(spacing: 0)
            .AddChild(BuildHeader())
            .AddChild(BuildTabs())
            .AddChild(BuildContent());
    }

    private ZorElement BuildHeader()
    {
        return new HStack(spacing: 16)
            .WithPadding(20)
            .WithBackground(Color.FromHex("#1F2937"))
            .AddChild(
                new Text("Dashboard")
                    .WithFontSize(24)
                    .Bold()
                    .WithColor(Color.White)
            )
            .AddChild(new Spacer())
            .AddChild(
                new Avatar()
                    .WithInitials("JD")
                    .WithStatus(AvatarStatus.Online)
            );
    }

    private ZorElement BuildTabs()
    {
        return new Tabs(_activeTab)
            .AddTab(new Tab("overview", "Overview"))
            .AddTab(new Tab("analytics", "Analytics"))
            .AddTab(new Tab("reports", "Reports"))
            .WithOnTabChange(tab => SetState(nameof(_activeTab), _activeTab = tab));
    }

    private ZorElement BuildContent()
    {
        return new VStack(spacing: 20)
            .WithPadding(20)
            .AddChild(BuildStatsRow())
            .AddChild(BuildRecentActivity());
    }

    private ZorElement BuildStatsRow()
    {
        return new HStack(spacing: 16)
            .AddChild(BuildStatCard("Total Users", "1,234", "+12%", Color.FromHex("#10B981")))
            .AddChild(BuildStatCard("Revenue", "$45.2K", "+8%", Color.FromHex("#3B82F6")))
            .AddChild(BuildStatCard("Orders", "892", "-3%", Color.FromHex("#EF4444")));
    }

    private ZorElement BuildStatCard(string title, string value, string change, Color color)
    {
        return new Card()
            .Elevated()
            .WithContent(
                new VStack(spacing: 8)
                    .AddChild(new Text(title).WithFontSize(14))
                    .AddChild(new Text(value).WithFontSize(32).Bold())
                    .AddChild(new Text(change).WithColor(color))
            );
    }

    private ZorElement BuildRecentActivity()
    {
        return new Card()
            .WithHeader(new Text("Recent Activity").Bold())
            .WithContent(
                new VStack(spacing: 12)
                    .AddChild(BuildActivityItem("User signed up", "2 min ago"))
                    .AddChild(BuildActivityItem("Order placed", "15 min ago"))
                    .AddChild(BuildActivityItem("Payment received", "1 hour ago"))
            );
    }

    private ZorElement BuildActivityItem(string text, string time)
    {
        return new HStack(spacing: 12)
            .AddChild(new Badge("•").Dot().Primary())
            .AddChild(new Text(text))
            .AddChild(new Spacer())
            .AddChild(new Text(time).WithFontSize(12));
    }
}
```

## 🛠️ Расширение

### Создание кастомного компонента

```csharp
public class CustomButton : Button
{
    private bool _isHovered = false;

    public CustomButton(string text, Action? onClick = null) 
        : base(text, onClick)
    {
    }

    public override void OnMount()
    {
        base.OnMount();
        // Инициализация при монтировании
    }

    // Добавление кастомной логики
    public CustomButton WithHoverEffect()
    {
        // Реализация hover эффекта
        return this;
    }
}
```

### Создание кастомного рендерера

```csharp
public class MyCustomRenderer : IRenderer
{
    public void Render(ZorElement root)
    {
        // Ваша логика рендеринга
    }

    public Size Measure(ZorElement element, SizeConstraints constraints)
    {
        // Измерение элементов
        return Size.Zero;
    }

    public void Layout(ZorElement element, double x, double y, double width, double height)
    {
        // Позиционирование элементов
    }

    // ... остальные методы
}
```

## 🤝 Вклад в проект

Мы приветствуем вклад в развитие ZorUI! Пожалуйста, ознакомьтесь с [CONTRIBUTING.md](CONTRIBUTING.md) для получения информации о том, как внести свой вклад.

### Как помочь:

- 🐛 Сообщить о баге
- 💡 Предложить новую фичу
- 📝 Улучшить документацию
- 🔧 Исправить баг
- ✨ Добавить новый компонент

## 📄 Лицензия

Этот проект распространяется под лицензией MIT. См. файл [LICENSE](LICENSE) для подробностей.

## 🙏 Благодарности

- **Radix UI** - вдохновение для дизайн-системы
- **SwiftUI** - вдохновение для Fluent API
- **React** - вдохновение для компонентной модели
- **.NET Community** - за потрясающие инструменты и библиотеки

## 📞 Контакты

- **GitHub**: [github.com/zorui/zorui](https://github.com/zorui/zorui)
- **Документация**: [zorui.dev](https://zorui.dev)
- **Discord**: [discord.gg/zorui](https://discord.gg/zorui)
- **Twitter**: [@ZorUIFramework](https://twitter.com/ZorUIFramework)

---

<div align="center">

**Сделано с ❤️ командой ZorUI**

[⬆ Наверх](#zorui)

</div>
