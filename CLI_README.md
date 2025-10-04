# 🎯 ZorUI CLI - Создание проектов одной командой!

## ⚡ Что это?

**ZorUI CLI** - инструмент командной строки для мгновенного создания ZorUI проектов из готовых шаблонов.

**Как `create-react-app`, но для ZorUI! 🚀**

---

## 🎬 Демо

```bash
# Установите CLI
./install-cli.sh

# Создайте проект
zorui new desktop --name MyApp

# Запустите
cd MyApp
dotnet run
```

**Готово!** Полноценное GUI приложение работает через 30 секунд! ⚡

---

## ✨ Возможности

- ✅ **4 готовых шаблона** - Desktop, Console, Component, Full
- ✅ **Одна команда** - `zorui new desktop -n MyApp`
- ✅ **Красивый UI** - С использованием Spectre.Console
- ✅ **Кроссплатформенность** - Windows, Linux, macOS
- ✅ **Интерактивная справка** - `zorui list`, `zorui info`
- ✅ **Готовый код** - Работает сразу после создания

---

## 📦 Шаблоны

### 1. `desktop` - Desktop GUI приложение

Полноценное кроссплатформенное приложение с окном.

```bash
zorui new desktop --name MyApp
```

**Что внутри:**
- SkiaSharp renderer
- Counter example
- Dark/Light theme toggle
- Ready to run!

**Платформы:** Windows, Linux, macOS

### 2. `console` - Console приложение

Простое консольное приложение для CLI инструментов.

```bash
zorui new console --name MyTool
```

**Что внутри:**
- Console renderer
- Basic UI example
- Quick start code

**Платформы:** Все

### 3. `component` - Библиотека компонентов

Создание переиспользуемых компонентов.

```bash
zorui new component --name MyComponents
```

**Что внутри:**
- Custom button example
- Custom card example
- NuGet-ready structure

**Платформы:** Library

### 4. `full` - Полное приложение

Приложение с лучшими практиками и структурой.

```bash
zorui new full --name MyFullApp
```

**Что внутри:**
- Multi-page structure
- Reusable components
- Custom styles
- Best practices

**Платформы:** Все

---

## 🚀 Установка

### Шаг 1: Установите CLI

```bash
# Linux/macOS
./install-cli.sh

# Windows
install-cli.cmd
```

### Шаг 2: Проверьте установку

```bash
zorui --version
zorui list
```

---

## 📖 Использование

### Базовое

```bash
# Создать desktop приложение
zorui new desktop --name MyApp

# Создать в определенной папке
zorui new desktop --name MyApp --output ./projects

# Сокращенная форма
zorui new desktop -n MyApp -o ./projects
```

### Посмотреть все шаблоны

```bash
zorui list
```

Вывод:

```
┌──────────────────────────────────────────────────────────┐
│              Available Templates                          │
├─────────────┬───────────┬────────────────────┬──────────┤
│  Template   │ Short Name│    Description     │ Platform │
├─────────────┼───────────┼────────────────────┼──────────┤
│ Desktop App │  desktop  │ Cross-platform GUI │ 🖥️ Desktop│
│ Console App │  console  │ CLI application    │ 💻 All   │
│ Component   │ component │ Component library  │ 📦 Library│
│ Full App    │   full    │ Full-featured app  │ 🎨 All   │
└─────────────┴───────────┴────────────────────┴──────────┘
```

### Информация о ZorUI

```bash
zorui info
```

---

## 💡 Примеры сценариев

### Создать приложение и запустить

```bash
zorui new desktop -n Calculator
cd Calculator
dotnet run
```

### Создать компонентную библиотеку

```bash
zorui new component -n MyUILibrary
cd MyUILibrary
dotnet build
```

### Создать полноценный проект

```bash
zorui new full -n MyApplication
cd MyApplication
dotnet run
```

### Создать микросервис

```bash
# Backend API
zorui new console -n MyAPI

# Desktop клиент
zorui new desktop -n MyClient

# Общая библиотека
zorui new component -n Shared
```

### Монорепозиторий

```bash
mkdir MyProject
cd MyProject

# Создать приложения
zorui new desktop -n App1 -o ./apps
zorui new desktop -n App2 -o ./apps
zorui new console -n CLI -o ./apps

# Создать библиотеки
zorui new component -n UI -o ./libs
zorui new component -n Core -o ./libs

# Создать solution
dotnet new sln
dotnet sln add apps/**/*.csproj
dotnet sln add libs/**/*.csproj
```

---

## 🛠️ Разработка CLI

### Структура проекта

```
src/ZorUI.CLI/
├── Program.cs              # Entry point
├── Commands/               # Command handlers
│   ├── NewCommand.cs      # 'new' command
│   ├── ListCommand.cs     # 'list' command
│   └── InfoCommand.cs     # 'info' command
├── Templates/              # Project templates
│   ├── DesktopTemplate.cs
│   ├── ConsoleTemplate.cs
│   ├── ComponentTemplate.cs
│   └── FullTemplate.cs
└── ZorUI.CLI.csproj
```

### Добавление своего шаблона

1. Создайте `MyTemplate.cs` в `Templates/`:

```csharp
public static class MyTemplate
{
    public static void Create(string path, string name)
    {
        Directory.CreateDirectory(path);
        
        // Создайте файлы
        File.WriteAllText(
            Path.Combine(path, $"{name}.csproj"),
            GetProjectFile(name)
        );
        
        File.WriteAllText(
            Path.Combine(path, "Program.cs"),
            GetProgramFile(name)
        );
    }

    private static string GetProjectFile(string name) => 
        $@"<Project Sdk=""Microsoft.NET.Sdk"">
            <!-- ваш .csproj -->
        </Project>";

    private static string GetProgramFile(string name) => 
        $@"// ваш Program.cs
        namespace {name};
        class Program {{ }}";
}
```

2. Добавьте в `NewCommand.cs`:

```csharp
case "mytemplate":
    MyTemplate.Create(fullPath, name);
    break;
```

3. Пересоберите и переустановите:

```bash
./install-cli.sh
```

---

## 🔧 Зависимости

CLI использует:
- **System.CommandLine** - Parsing команд
- **Spectre.Console** - Красивый UI в терминале

---

## 📚 Документация

- **[CLI_GUIDE.md](CLI_GUIDE.md)** - Полное руководство
- **[QUICK_START_CLI.md](QUICK_START_CLI.md)** - Быстрый старт
- **[docs/](docs/)** - Документация ZorUI

---

## ❓ FAQ

### Как обновить CLI?

```bash
./install-cli.sh  # Переустановит последнюю версию
```

### Как удалить CLI?

```bash
dotnet tool uninstall --global ZorUI.CLI
```

### CLI не найден после установки

Добавьте в PATH:

```bash
# Linux/macOS
export PATH="$PATH:$HOME/.dotnet/tools"

# Windows
$env:PATH += ";$HOME\.dotnet\tools"
```

### Можно ли изменить созданный проект?

Да! Все созданные файлы - обычный C# код. Редактируйте как хотите.

---

## 🎉 Итог

**ZorUI CLI делает создание проектов мгновенным!**

Вместо:
```bash
# Ручное создание (5-10 минут)
mkdir MyApp
cd MyApp
dotnet new console
# ... редактирование .csproj
# ... добавление ссылок
# ... написание базового кода
# ... настройка структуры
```

Теперь:
```bash
# С CLI (30 секунд)
zorui new desktop -n MyApp
cd MyApp
dotnet run
```

**Попробуйте прямо сейчас! 🚀**

```bash
./install-cli.sh
zorui new desktop -n MyFirstApp
cd MyFirstApp
dotnet run
```
