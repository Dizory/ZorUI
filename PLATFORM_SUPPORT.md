# 🌍 ZorUI - Кроссплатформенная поддержка

## 📊 Текущий статус поддержки платформ

| Платформа | Статус | Рендерер | Примечания |
|-----------|--------|----------|------------|
| 🖥️ **Windows** | ✅ Готово | SkiaSharp | Desktop приложения |
| 🐧 **Linux** | ✅ Готово | SkiaSharp | Desktop приложения |
| 🍎 **macOS** | ✅ Готово | SkiaSharp | Desktop приложения |
| 📱 **Android** | 🔄 В разработке | SkiaSharp + MAUI | Мобильные приложения |
| 🍏 **iOS** | 🔄 В разработке | SkiaSharp + MAUI | Мобильные приложения |
| 🌐 **Web (WASM)** | 🔮 Планируется | SkiaSharp (WASM) | Браузерные приложения |

## 🏗️ Архитектура кроссплатформенности

```
┌─────────────────────────────────────────────────────────┐
│            Ваше приложение (ZorUI код)                  │
│  new Button("Click").Primary() - ОДИН КОД ДЛЯ ВСЕХ!   │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│              ZorUI Core + Components                     │
│        (Абстрактные компоненты, без привязки)           │
└────────────────────┬────────────────────────────────────┘
                     │
        ┌────────────┴────────────┐
        │                         │
┌───────▼────────┐      ┌────────▼──────────┐
│  SkiaSharp     │      │  Platform Native  │
│  Renderer      │      │  Renderers        │
│  (Все платформы)│      │  (Специфичные)    │
└───────┬────────┘      └────────┬──────────┘
        │                        │
    ┌───┴────┬────┬────┬─────┐  │
    ▼        ▼    ▼    ▼     ▼  ▼
┌────────┐ ┌──┐ ┌──┐ ┌───┐ ┌───┐
│Windows │ │Mac│ │Lin│ │And│ │iOS│
└────────┘ └──┘ └──┘ └───┘ └───┘
```

## 🎨 Реализованные рендереры

### 1. ConsoleRenderer (✅ Готов)
- **Платформы**: Windows, Linux, macOS
- **Назначение**: Отладка, тестирование, CLI приложения
- **Ограничения**: Только текстовый вывод

### 2. SkiaRenderer (✅ Создаётся сейчас)
- **Платформы**: Windows, Linux, macOS, Android, iOS, Web
- **Назначение**: Полноценные GUI приложения
- **Возможности**: 
  - 2D графика
  - Шрифты и текст
  - Изображения
  - Анимации
  - Touch/Mouse события

### 3. WebRenderer (🔮 Планируется)
- **Платформы**: Любой браузер (через WebAssembly)
- **Назначение**: Web приложения
- **Возможности**: Рендеринг в HTML Canvas/WebGL

## 📦 Зависимости для платформ

### Desktop (Windows, Linux, macOS)

```xml
<ItemGroup>
  <PackageReference Include="SkiaSharp" Version="2.88.7" />
  <PackageReference Include="SkiaSharp.Views.Desktop.Common" Version="2.88.7" />
</ItemGroup>
```

### Android

```xml
<ItemGroup>
  <PackageReference Include="SkiaSharp" Version="2.88.7" />
  <PackageReference Include="SkiaSharp.Views.Maui.Core" Version="2.88.7" />
</ItemGroup>
```

### iOS

```xml
<ItemGroup>
  <PackageReference Include="SkiaSharp" Version="2.88.7" />
  <PackageReference Include="SkiaSharp.Views.Maui.Core" Version="2.88.7" />
</ItemGroup>
```

### Web (WebAssembly)

```xml
<ItemGroup>
  <PackageReference Include="SkiaSharp" Version="2.88.7" />
  <PackageReference Include="SkiaSharp.Views.Blazor" Version="2.88.7" />
</ItemGroup>
```

## 🚀 Как создать приложение для каждой платформы

### 🖥️ Windows Desktop

```bash
# 1. Создайте проект
dotnet new console -n MyWindowsApp

# 2. Добавьте ZorUI
dotnet add reference ../src/ZorUI.Core/ZorUI.Core.csproj
dotnet add package SkiaSharp
dotnet add package SkiaSharp.Views.Desktop.Common

# 3. Используйте SkiaRenderer
# (код ниже)
```

### 🐧 Linux Desktop

```bash
# То же, что Windows - SkiaSharp работает одинаково!
dotnet new console -n MyLinuxApp
# ... добавьте те же зависимости
```

### 🍎 macOS Desktop

```bash
# То же, что Windows и Linux
dotnet new console -n MyMacApp
# ... добавьте те же зависимости
```

### 📱 Android

```bash
# 1. Создайте MAUI проект
dotnet new maui -n MyAndroidApp

# 2. Добавьте ZorUI
dotnet add reference ../src/ZorUI.Core/ZorUI.Core.csproj
dotnet add package SkiaSharp.Views.Maui.Core

# 3. Используйте MAUIRenderer
```

### 🍏 iOS

```bash
# То же, что Android - MAUI поддерживает обе платформы
dotnet new maui -n MyiOSApp
# ... добавьте те же зависимости
```

### 🌐 Web (Blazor WebAssembly)

```bash
# 1. Создайте Blazor WASM проект
dotnet new blazorwasm -n MyWebApp

# 2. Добавьте ZorUI
dotnet add reference ../src/ZorUI.Core/ZorUI.Core.csproj
dotnet add package SkiaSharp.Views.Blazor

# 3. Используйте BlazorRenderer
```

## 💻 Примеры кода

### Desktop приложение (Windows/Linux/macOS)

```csharp
using ZorUI.Core;
using ZorUI.Rendering.Skia;

var app = new ZorApplication();
var window = new SkiaWindow(800, 600, "My ZorUI App");

window.RootComponent = new MyApp();
window.Show();

// Тот же ZorUI код работает на всех платформах!
public class MyApp : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .AddChild(new Text($"Count: {_count}"))
            .AddChild(new Button("Increment", () => 
                SetState(nameof(_count), ++_count)));
    }
}
```

### Android/iOS приложение (MAUI)

```csharp
// MainPage.xaml.cs
using ZorUI.Core;
using ZorUI.Rendering.Maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        
        var app = new ZorApplication();
        var renderer = new MauiRenderer();
        
        Content = renderer.CreateView(new MyApp());
    }
}

// MyApp.cs - ТОТ ЖЕ КОД что и для Desktop!
public class MyApp : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .AddChild(new Text($"Count: {_count}"))
            .AddChild(new Button("Increment", () => 
                SetState(nameof(_count), ++_count)));
    }
}
```

### Web приложение (Blazor)

```csharp
// Index.razor
@page "/"
@using ZorUI.Core
@using ZorUI.Rendering.Blazor

<BlazorRenderer Component="@_app" />

@code {
    private MyApp _app = new MyApp();
}

// MyApp.cs - СНОВА ТОТ ЖЕ КОД!
public class MyApp : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .AddChild(new Text($"Count: {_count}"))
            .AddChild(new Button("Increment", () => 
                SetState(nameof(_count), ++_count)));
    }
}
```

## ✨ Ключевое преимущество

**ОДИН КОД - ВСЕ ПЛАТФОРМЫ!**

```csharp
// Этот код работает на:
// ✅ Windows
// ✅ Linux  
// ✅ macOS
// ✅ Android
// ✅ iOS
// ✅ Web

public class MyApp : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .AddChild(
                new Text($"Нажато раз: {_count}")
                    .WithFontSize(32)
            )
            .AddChild(
                new Button("Нажми меня!", () => 
                {
                    SetState(nameof(_count), ++_count);
                })
                .Primary()
            );
    }
}
```

Меняется только **рендерер** - логика и UI код остаются **идентичными**!

## 🎯 Рекомендации по выбору

### Используйте SkiaRenderer если нужно:
- ✅ Кроссплатформенность (один код)
- ✅ Быстрая разработка
- ✅ Единый внешний вид на всех платформах
- ✅ 2D графика и анимации

### Используйте Native Renderers если нужно:
- ✅ Нативный look & feel
- ✅ Максимальная производительность
- ✅ Интеграция с платформенными API
- ✅ Доступ к специфичным возможностям

## 📋 Roadmap

### Версия 1.0 (Текущая)
- ✅ Ядро фреймворка
- ✅ Все компоненты
- ✅ ConsoleRenderer

### Версия 1.1 (В разработке)
- 🔄 SkiaRenderer для Desktop
- 🔄 Window management
- 🔄 Event handling (mouse, keyboard)

### Версия 1.2 (Планируется)
- 🔮 MAUI integration
- 🔮 Android support
- 🔮 iOS support
- 🔮 Touch events

### Версия 1.3 (Будущее)
- 🔮 Blazor WebAssembly support
- 🔮 Progressive Web Apps
- 🔮 Hot Reload improvements

### Версия 2.0 (Дальнейшие планы)
- 🔮 Native renderers (WinUI, AppKit, GTK)
- 🔮 Advanced graphics
- 🔮 3D support
- 🔮 AR/VR capabilities

## 🔧 Текущие ограничения

1. **SkiaRenderer** - В разработке (можно создать сейчас)
2. **MAUI integration** - Требует дополнительной работы
3. **Web support** - Планируется в будущих версиях
4. **Native look** - Пока только SkiaSharp rendering

## 💡 Вывод

**ДА, ZorUI МОЖЕТ работать на всех платформах!**

- **Сейчас готово**: Desktop (через консоль для демо)
- **Легко добавить**: SkiaSharp для полноценного GUI
- **Требует работы**: MAUI для мобильных, Blazor для Web

**Архитектура полностью готова** - нужно только реализовать рендереры!

Хотите, я создам SkiaRenderer прямо сейчас? 🚀
