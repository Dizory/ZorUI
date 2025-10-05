# 🚀 НАЧНИТЕ ЗДЕСЬ - ZorUI Framework

## ✨ Что такое ZorUI?

**ZorUI** - это кроссплатформенный UI фреймворк для .NET, вдохновленный Radix UI.

### Ключевые особенности:

- ✅ **Без XAML** - Только чистый C# с Fluent API
- ✅ **Кроссплатформенность** - Windows, Linux, macOS (готово!), Android/iOS (в разработке)
- ✅ **30+ компонентов** - Buttons, Forms, Cards, Dialogs, и многое другое
- ✅ **CLI инструмент** - Создавайте проекты одной командой
- ✅ **Реактивное состояние** - Автообновление UI
- ✅ **Темы** - Светлая/темная из коробки

---

## ⚡ Быстрый старт (3 минуты)

### Вариант 1: С CLI инструментом (РЕКОМЕНДУЕТСЯ!)

```bash
# 1. Установите CLI
./install-cli.sh          # Linux/macOS
# или
install-cli.cmd           # Windows

# 2. Создайте проект
zorui new desktop --name MyApp

# 3. Запустите
cd MyApp
dotnet run
```

**Готово! Полноценное GUI приложение работает! 🎉**

### Вариант 2: Запустите готовые примеры

```bash
# Desktop GUI приложение
cd samples/DesktopApp
dotnet run

# Или простой console пример
cd samples/BasicApp
dotnet run
```

---

## 📚 Документация

### 🎯 Начало работы
- **[QUICK_START_CLI.md](QUICK_START_CLI.md)** ⭐ - Быстрый старт с CLI
- **[CLI_GUIDE.md](CLI_GUIDE.md)** - Полное руководство по CLI
- **[HOW_TO_USE.md](HOW_TO_USE.md)** - Как создать проект вручную
- **[GETTING_STARTED_QUICK.md](GETTING_STARTED_QUICK.md)** - Старт за 5 минут

### 🌍 Платформы
- **[PLATFORM_GUIDE.md](PLATFORM_GUIDE.md)** ⭐ - Windows/Linux/macOS/Android/iOS
- **[PLATFORM_SUPPORT.md](PLATFORM_SUPPORT.md)** - Детали поддержки
- **[FINAL_ANSWER.md](FINAL_ANSWER.md)** - Сводка по платформам

### 📖 Основная документация
- **[docs/getting-started.md](docs/getting-started.md)** - Детальное руководство
- **[docs/QuickReference.md](docs/QuickReference.md)** - API справка
- **[docs/core-concepts.md](docs/core-concepts.md)** - Основные концепции
- **[docs/styling.md](docs/styling.md)** - Стилизация
- **[docs/best-practices.md](docs/best-practices.md)** - Лучшие практики

### 📁 Примеры
- **[samples/DesktopApp/](samples/DesktopApp/)** - Desktop GUI
- **[samples/BasicApp/](samples/BasicApp/)** - Простой счетчик
- **[samples/ComponentGallery/](samples/ComponentGallery/)** - Все компоненты
- **[MyZorApp/](MyZorApp/)** - Демо приложение

---

## 🎯 Какой способ выбрать?

### Для начинающих:

```bash
# 1. Установите CLI
./install-cli.sh

# 2. Создайте проект
zorui new desktop --name MyFirstApp

# 3. Готово!
cd MyFirstApp
dotnet run
```

**Читайте:** [QUICK_START_CLI.md](QUICK_START_CLI.md)

### Для изучения:

```bash
# Запустите готовые примеры
cd samples/DesktopApp
dotnet run
```

**Читайте:** [docs/getting-started.md](docs/getting-started.md)

### Для опытных:

```bash
# Создайте вручную с нужной структурой
mkdir MyApp
dotnet new console
# ... настройка
```

**Читайте:** [HOW_TO_USE.md](HOW_TO_USE.md)

---

## 💻 Пример кода

```csharp
using ZorUI.Core;
using ZorUI.Rendering.Skia;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;

// Создайте окно (работает на Win/Linux/Mac!)
var window = new SkiaWindow(800, 600, "My App");

// Ваш UI компонент
window.RootComponent = new MyApp();

// Покажите окно
window.Show();

// Компонент с состоянием
public class MyApp : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .WithPadding(20)
            .AddChild(
                new Text($"Счетчик: {_count}")
                    .WithFontSize(32)
                    .Bold()
            )
            .AddChild(
                new Button("Нажми!", () => 
                {
                    SetState(nameof(_count), ++_count);
                })
                .Primary()
            );
    }
}
```

**Этот код работает на Windows, Linux И macOS!** 🎉

---

## 🎨 Что можно создать?

- ✅ Desktop приложения (Windows/Linux/macOS)
- ✅ CLI инструменты
- ✅ Компонентные библиотеки
- 🔄 Мобильные приложения (скоро)
- 🔮 Web приложения (планируется)

---

## 📊 Структура проекта

```
ZorUI/
├── 🚀 install-cli.sh         ⭐ УСТАНОВИТЕ CLI!
├── 🚀 install-cli.cmd        ⭐ (Windows)
│
├── 📂 samples/               ⭐ ПРИМЕРЫ
│   ├── DesktopApp/          (Полноценное GUI!)
│   ├── BasicApp/            (Простой счетчик)
│   └── ComponentGallery/    (Все компоненты)
│
├── 📂 src/
│   ├── ZorUI.Core/          (Ядро)
│   ├── ZorUI.Components/    (30+ компонентов)
│   ├── ZorUI.Styling/       (Темы)
│   ├── ZorUI.Rendering/     (Console renderer)
│   ├── ZorUI.Rendering.Skia/ (GUI renderer)
│   └── ZorUI.CLI/           ⭐ (CLI инструмент)
│
├── 📂 docs/                 (Документация)
│
├── 📄 START_HERE.md         ⭐⭐⭐ ВЫ ЗДЕСЬ!
├── 📄 QUICK_START_CLI.md    (Быстрый старт CLI)
├── 📄 CLI_GUIDE.md          (Руководство CLI)
├── 📄 PLATFORM_GUIDE.md     (Платформы)
└── 📄 README.md             (О проекте)
```

---

## 🎯 Следующие шаги

### 1. Установите CLI

```bash
./install-cli.sh
```

### 2. Создайте проект

```bash
zorui new desktop --name MyApp
```

### 3. Запустите

```bash
cd MyApp
dotnet run
```

### 4. Изучите
- Посмотрите код в `MyApp/Program.cs`
- Прочитайте [docs/QuickReference.md](docs/QuickReference.md)
- Экспериментируйте!

---

## 📞 Поддержка

- 📖 [Документация](docs/)
- 💬 [Discord](https://discord.gg/zorui)
- 🐛 [GitHub](https://github.com/zorui/zorui)
- ✉️ support@zorui.dev

---

## ✅ Checklist для старта

- [ ] Установил CLI (`./install-cli.sh`)
- [ ] Создал проект (`zorui new desktop -n MyApp`)
- [ ] Запустил (`dotnet run`)
- [ ] Посмотрел код
- [ ] Прочитал [QuickReference.md](docs/QuickReference.md)
- [ ] Готов создавать! 🎉

---

<div align="center">

# 🎉 Готово! Начинайте создавать!

```bash
./install-cli.sh
zorui new desktop --name MyFirstApp
cd MyFirstApp
dotnet run
```

**Приятной разработки! 🚀**

</div>
