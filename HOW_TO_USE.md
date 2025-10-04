# 🚀 Как использовать ZorUI

Полное руководство по созданию проектов с ZorUI Framework.

## 📋 Содержание

1. [Быстрый старт с готовым примером](#1-быстрый-старт-с-готовым-примером)
2. [Создание проекта с локальными ссылками](#2-создание-проекта-с-локальными-ссылками)
3. [Использование NuGet пакетов](#3-использование-nuget-пакетов)
4. [Структура проекта](#4-структура-проекта)
5. [Примеры кода](#5-примеры-кода)

---

## 1. 🎯 Быстрый старт с готовым примером

Самый быстрый способ начать - используйте готовый пример!

### Вариант A: Готовое демо-приложение

```bash
# Запустите готовое приложение MyZorApp
cd MyZorApp
dotnet run
```

Это приложение демонстрирует:
- ✅ Счетчик с кнопками
- ✅ Форма ввода с валидацией
- ✅ Переключение темной/светлой темы
- ✅ Progress bar
- ✅ Cards, Badges, Alerts

### Вариант B: Базовое приложение

```bash
# Простой счетчик
cd samples/BasicApp
dotnet run
```

### Вариант C: Галерея всех компонентов

```bash
# Все компоненты в одном месте
cd samples/ComponentGallery
dotnet run
```

---

## 2. 🔨 Создание проекта с локальными ссылками

Этот способ лучше всего подходит для **разработки и экспериментов**.

### Шаг 1: Создайте проект

```bash
# Создайте папку
mkdir MyAwesomeApp
cd MyAwesomeApp

# Инициализируйте консольный проект
dotnet new console
```

### Шаг 2: Добавьте ссылки на ZorUI

```bash
# Добавьте ProjectReference на библиотеки ZorUI
dotnet add reference ../src/ZorUI.Core/ZorUI.Core.csproj
dotnet add reference ../src/ZorUI.Components/ZorUI.Components.csproj
dotnet add reference ../src/ZorUI.Styling/ZorUI.Styling.csproj
dotnet add reference ../src/ZorUI.Rendering/ZorUI.Rendering.csproj
```

Или вручную отредактируйте `.csproj`:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\src\ZorUI.Core\ZorUI.Core.csproj" />
    <ProjectReference Include="..\src\ZorUI.Components\ZorUI.Components.csproj" />
    <ProjectReference Include="..\src\ZorUI.Styling\ZorUI.Styling.csproj" />
    <ProjectReference Include="..\src\ZorUI.Rendering\ZorUI.Rendering.csproj" />
  </ItemGroup>
</Project>
```

### Шаг 3: Напишите код

Создайте `Program.cs`:

```csharp
using ZorUI.Core;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Rendering;

var app = new ZorApplication();
app.Run(new MyApp());
Console.ReadKey();

public class MyApp : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        var renderer = new ConsoleRenderer();
        
        var ui = new VStack(spacing: 20)
            .WithPadding(20)
            .AddChild(new Text($"Count: {_count}").WithFontSize(32))
            .AddChild(
                new Button("Increment", () => 
                {
                    SetState(nameof(_count), ++_count);
                })
                .Primary()
            );
        
        renderer.Render(ui);
        return ui;
    }
}
```

### Шаг 4: Запустите

```bash
dotnet run
```

---

## 3. 📦 Использование NuGet пакетов

Этот способ подходит для **продакшн приложений**.

### Шаг 1: Соберите NuGet пакеты

```bash
# Linux/Mac
chmod +x build-packages.sh
./build-packages.sh

# Windows
build-packages.cmd
```

Пакеты будут созданы в папке `./nupkgs/`

### Шаг 2: Создайте новый проект

```bash
mkdir MyProductionApp
cd MyProductionApp
dotnet new console
```

### Шаг 3: Добавьте пакеты из локальной папки

```bash
# Добавьте источник пакетов
dotnet nuget add source ../nupkgs --name ZorUILocal

# Установите пакеты
dotnet add package ZorUI.Core --version 1.0.0 --source ZorUILocal
dotnet add package ZorUI.Components --version 1.0.0 --source ZorUILocal
dotnet add package ZorUI.Styling --version 1.0.0 --source ZorUILocal
dotnet add package ZorUI.Rendering --version 1.0.0 --source ZorUILocal
```

Или установите напрямую:

```bash
dotnet add package ZorUI.Core --source ../nupkgs
dotnet add package ZorUI.Components --source ../nupkgs
dotnet add package ZorUI.Styling --source ../nupkgs
dotnet add package ZorUI.Rendering --source ../nupkgs
```

### Шаг 4: Публикация на NuGet.org (опционально)

Если хотите опубликовать на NuGet.org:

1. Зарегистрируйтесь на [nuget.org](https://www.nuget.org/)
2. Создайте API ключ
3. Опубликуйте:

```bash
dotnet nuget push ./nupkgs/*.nupkg \
  --source https://api.nuget.org/v3/index.json \
  --api-key YOUR_API_KEY
```

После публикации любой сможет установить:

```bash
dotnet add package ZorUI.Core
dotnet add package ZorUI.Components
dotnet add package ZorUI.Styling
dotnet add package ZorUI.Rendering
```

---

## 4. 📁 Структура проекта

### Минимальная структура

```
MyApp/
├── Program.cs          # Точка входа
└── MyApp.csproj       # Конфигурация проекта
```

### Рекомендуемая структура

```
MyApp/
├── Components/         # Ваши компоненты
│   ├── HomePage.cs
│   ├── ProfilePage.cs
│   └── Common/
│       └── Header.cs
├── Styles/            # Стили и темы
│   └── AppStyles.cs
├── Models/            # Модели данных
│   └── User.cs
├── Services/          # Бизнес-логика
│   └── ApiService.cs
├── Program.cs         # Точка входа
└── MyApp.csproj      # Конфигурация
```

---

## 5. 💡 Примеры кода

### Минимальный пример

```csharp
using ZorUI.Core;
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
        var ui = new Text("Hello, ZorUI!").WithFontSize(32);
        renderer.Render(ui);
        return ui;
    }
}
```

### Счетчик с состоянием

```csharp
public class Counter : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        var renderer = new ConsoleRenderer();
        
        var ui = new VStack(spacing: 16)
            .AddChild(new Text($"Count: {_count}"))
            .AddChild(
                new Button("Increment", () => 
                    SetState(nameof(_count), ++_count))
            );
        
        renderer.Render(ui);
        return ui;
    }
}
```

### Форма с валидацией

```csharp
public class LoginForm : ZorComponent
{
    private string _email = "";
    private string _password = "";

    private bool IsValid() => 
        !string.IsNullOrEmpty(_email) && 
        _password.Length >= 8;

    public override ZorElement Build()
    {
        var renderer = new ConsoleRenderer();
        
        var ui = new VStack(spacing: 16)
            .AddChild(
                new TextField("Email")
                    .Email()
                    .WithValue(_email)
                    .WithOnChange(v => SetState(nameof(_email), _email = v))
            )
            .AddChild(
                new TextField("Password")
                    .Password()
                    .WithValue(_password)
                    .WithOnChange(v => SetState(nameof(_password), _password = v))
            )
            .AddChild(
                new Button("Login", HandleLogin)
                    .Primary()
                    .Disabled(!IsValid())
            );
        
        renderer.Render(ui);
        return ui;
    }

    private void HandleLogin()
    {
        Console.WriteLine($"Logging in: {_email}");
    }
}
```

### Использование тем

```csharp
public class ThemedApp : ZorComponent
{
    private bool _isDark = false;

    public override ZorElement Build()
    {
        var theme = _isDark ? Theme.Dark() : Theme.Light();
        var renderer = new ConsoleRenderer();
        
        var ui = new VStack(spacing: theme.Spacing.Space4)
            .WithPadding(EdgeInsets.All(theme.Spacing.Space6))
            .AddChild(
                new Text("Themed App")
                    .WithFontSize(theme.Typography.FontSize2Xl)
                    .Bold()
            )
            .AddChild(
                new Switch("Dark Mode", _isDark)
                    .WithOnChange(dark => 
                        SetState(nameof(_isDark), _isDark = dark))
            );
        
        renderer.Render(ui);
        return ui;
    }
}
```

---

## 🎯 Что дальше?

### Документация
- [📖 Getting Started](docs/getting-started.md)
- [🎨 Components Guide](docs/components/)
- [💅 Styling Guide](docs/styling.md)
- [📚 Quick Reference](docs/QuickReference.md)
- [🏆 Best Practices](docs/best-practices.md)

### Примеры
- [BasicApp](samples/BasicApp/) - Простой счетчик
- [ComponentGallery](samples/ComponentGallery/) - Все компоненты
- [MyZorApp](MyZorApp/) - Полноценное демо

### Сообщество
- 💬 [Discord](https://discord.gg/zorui)
- 🐙 [GitHub](https://github.com/zorui/zorui)
- 🐦 [Twitter](https://twitter.com/ZorUIFramework)

---

## ❓ FAQ

### Как обновить ZorUI?

**С локальными ссылками:**
```bash
# Просто пересоберите проект
dotnet build
```

**С NuGet пакетами:**
```bash
# Пересоздайте пакеты
./build-packages.sh

# Обновите в проекте
dotnet add package ZorUI.Core --version 1.0.1 --source ../nupkgs
```

### Можно ли использовать в продакшене?

Да! Фреймворк полностью готов к использованию. Для продакшена рекомендуется:
1. Использовать NuGet пакеты
2. Версионировать зависимости
3. Добавить unit тесты
4. Настроить CI/CD

### Как добавить свои компоненты?

```csharp
public class MyCustomButton : Button
{
    public MyCustomButton(string text) : base(text)
    {
        // Ваша кастомизация
    }
    
    public override ZorElement Build()
    {
        // Ваша логика
        return base.Build();
    }
}
```

### Где искать помощь?

1. 📖 Прочитайте [документацию](docs/)
2. 👀 Посмотрите [примеры](samples/)
3. 💬 Задайте вопрос в [Discord](https://discord.gg/zorui)
4. 🐛 Создайте [Issue](https://github.com/zorui/zorui/issues)

---

**Удачи в создании с ZorUI! 🚀**
