# ZorUI - Quick Start (5 минут)

Быстрый старт для тех, кто хочет немедленно начать разработку с ZorUI.

## 1️⃣ Установка (30 секунд)

```bash
# Создайте новый проект
dotnet new console -n MyZorApp
cd MyZorApp

# Добавьте пакеты ZorUI
dotnet add package ZorUI.Core
dotnet add package ZorUI.Components
dotnet add package ZorUI.Styling
dotnet add package ZorUI.Rendering
```

## 2️⃣ Hello World (1 минута)

Замените содержимое `Program.cs`:

```csharp
using ZorUI.Core;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Rendering;

var app = new ZorApplication();
app.Run(new HelloWorld());

Console.ReadKey();

public class HelloWorld : ZorComponent
{
    public override ZorElement Build()
    {
        var renderer = new ConsoleRenderer();
        
        var ui = new VStack(spacing: 16)
            .WithPadding(20)
            .AddChild(
                new Text("Hello, ZorUI! 👋")
                    .WithFontSize(32)
                    .Bold()
            )
            .AddChild(
                new Text("Your first ZorUI application!")
                    .WithFontSize(16)
            );
        
        renderer.Render(ui);
        return ui;
    }
}
```

Запустите:
```bash
dotnet run
```

## 3️⃣ Интерактивное приложение (2 минуты)

Создайте счетчик с кнопками:

```csharp
using ZorUI.Core;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Rendering;

var app = new ZorApplication();
app.Run(new CounterApp());

Console.ReadKey();

public class CounterApp : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        var renderer = new ConsoleRenderer();
        
        var ui = new VStack(spacing: 20)
            .WithPadding(20)
            .AddChild(
                new Text($"🔢 Count: {_count}")
                    .WithFontSize(28)
                    .Bold()
            )
            .AddChild(
                new HStack(spacing: 10)
                    .AddChild(
                        new Button("➖ Decrement", () => 
                        {
                            SetState(nameof(_count), --_count);
                            Console.WriteLine($"\nCount: {_count}");
                        })
                        .Secondary()
                    )
                    .AddChild(
                        new Button("➕ Increment", () => 
                        {
                            SetState(nameof(_count), ++_count);
                            Console.WriteLine($"\nCount: {_count}");
                        })
                        .Primary()
                    )
                    .AddChild(
                        new Button("🔄 Reset", () => 
                        {
                            SetState(nameof(_count), _count = 0);
                            Console.WriteLine($"\nCount reset!");
                        })
                        .Destructive()
                    )
            );

        renderer.Render(ui);
        return ui;
    }
}
```

## 4️⃣ Добавьте стили (1 минута)

```csharp
using ZorUI.Core.Properties;
using ZorUI.Styling;

// В методе Build():
var theme = Theme.Dark(); // или Theme.Light()

var ui = new VStack(spacing: 20)
    .WithPadding(20)
    .WithBackground(theme.Colors.Background)
    .AddChild(
        new Text($"Count: {_count}")
            .WithFontSize(28)
            .Bold()
            .WithColor(theme.Colors.Primary)
    );
```

## 5️⃣ Добавьте форму (1 минута)

```csharp
using ZorUI.Components.Forms;

public class FormApp : ZorComponent
{
    private string _name = "";
    private bool _agreed = false;

    public override ZorElement Build()
    {
        var renderer = new ConsoleRenderer();
        
        var ui = new VStack(spacing: 16)
            .WithPadding(20)
            .AddChild(
                new TextField("Enter your name")
                    .WithValue(_name)
                    .WithOnChange(value => 
                    {
                        SetState(nameof(_name), _name = value);
                        Console.WriteLine($"Name: {value}");
                    })
            )
            .AddChild(
                new Checkbox("I agree to terms", _agreed)
                    .WithOnChange(value => 
                    {
                        SetState(nameof(_agreed), _agreed = value);
                        Console.WriteLine($"Agreed: {value}");
                    })
            )
            .AddChild(
                new Button("Submit", () => 
                {
                    Console.WriteLine($"\nSubmitted: {_name}, Agreed: {_agreed}");
                })
                .Primary()
                .Disabled(string.IsNullOrEmpty(_name) || !_agreed)
            );

        renderer.Render(ui);
        return ui;
    }
}
```

## 🎯 Что дальше?

### Изучите документацию
- [📖 Getting Started](docs/getting-started.md) - Подробное руководство
- [🎨 Components](docs/components/) - Все компоненты
- [💅 Styling](docs/styling.md) - Стилизация
- [📚 Quick Reference](docs/QuickReference.md) - Быстрая справка

### Посмотрите примеры
```bash
# Базовое приложение
cd samples/BasicApp
dotnet run

# Галерея компонентов
cd samples/ComponentGallery
dotnet run
```

### Популярные компоненты

#### Layout
```csharp
new VStack(spacing: 16, ...)  // Вертикальный стек
new HStack(spacing: 8, ...)   // Горизонтальный стек
new Grid(columns: 3, ...)     // Сетка
```

#### UI
```csharp
new Text("Hello").Bold().WithFontSize(24)
new Button("Click", onClick).Primary()
new Image("photo.jpg").Circle()
```

#### Forms
```csharp
new TextField("Placeholder")
new Checkbox("Label", isChecked)
new Switch("Enable", isOn)
new Slider(min: 0, max: 100, value: 50)
```

#### Data Display
```csharp
new Card().WithHeader(...).WithContent(...)
new Progress(75, 100).Success()
new Avatar().WithInitials("JD")
new Badge("New").Primary()
```

## 💡 Советы

1. **Используйте Fluent API** - методы можно цеплять цепочкой
2. **State в полях** - используйте `private` поля для состояния
3. **SetState** для обновления - автоматический rebuild
4. **Тема** - используйте `Theme.Light()` или `Theme.Dark()`
5. **Spacing** - используйте `theme.Spacing.SpaceX` для консистентности

## ❓ Проблемы?

- 📖 [Документация](docs/)
- 💬 [Discord](https://discord.gg/zorui)
- 🐛 [Issues](https://github.com/zorui/zorui/issues)
- ✉️ support@zorui.dev

## 🚀 Готовы к большему?

Создайте полноценное приложение:
- Todo List
- Dashboard
- Form с валидацией
- Chat приложение

Примеры скоро будут добавлены!

---

**Создано с ❤️ ZorUI Team**
