# 🚀 ZorUI CLI - Руководство

CLI инструмент для быстрого создания ZorUI проектов из шаблонов.

## 📦 Установка

### Вариант 1: Как .NET Global Tool (рекомендуется)

```bash
# Соберите и установите локально
cd src/ZorUI.CLI
dotnet pack -c Release
dotnet tool install --global --add-source ./bin/Release ZorUI.CLI

# Проверка установки
zorui --version
```

### Вариант 2: Из NuGet (после публикации)

```bash
dotnet tool install --global ZorUI.CLI
```

### Обновление

```bash
dotnet tool update --global ZorUI.CLI
```

### Удаление

```bash
dotnet tool uninstall --global ZorUI.CLI
```

---

## 📋 Команды

### `zorui new` - Создание нового проекта

Создает новый ZorUI проект из шаблона.

#### Синтаксис

```bash
zorui new <template> [options]
```

#### Шаблоны

| Шаблон | Описание | Платформа |
|--------|----------|-----------|
| `desktop` | Desktop GUI приложение | Windows, Linux, macOS |
| `console` | Console приложение | Все платформы |
| `component` | Библиотека компонентов | Library |
| `full` | Полноценное приложение | Все платформы |

#### Опции

- `--name, -n` - Имя проекта (по умолчанию: MyZorUIApp)
- `--output, -o` - Директория для создания (по умолчанию: текущая)

#### Примеры

```bash
# Создать desktop приложение с именем по умолчанию
zorui new desktop

# Создать desktop приложение с кастомным именем
zorui new desktop --name MyAwesomeApp

# Создать в определенной папке
zorui new desktop --name MyApp --output ./projects

# Сокращенная форма
zorui new desktop -n MyApp -o ./projects

# Console приложение
zorui new console --name MyCliTool

# Библиотека компонентов
zorui new component --name MyComponents

# Полноценное приложение
zorui new full --name MyFullApp
```

---

### `zorui list` - Список шаблонов

Показывает все доступные шаблоны.

```bash
zorui list
```

**Вывод:**

```
┌──────────────────────────────────────────────────────────────────────┐
│                      Available Templates                              │
├──────────────┬────────────┬──────────────────────────────┬───────────┤
│   Template   │ Short Name │         Description          │ Platform  │
├──────────────┼────────────┼──────────────────────────────┼───────────┤
│ Desktop App  │  desktop   │ Cross-platform desktop app   │ 🖥️ Desktop │
│ Console App  │  console   │ Simple console application   │ 💻 All    │
│ Component    │ component  │ Reusable component library   │ 📦 Library│
│ Full App     │   full     │ Full-featured application    │ 🎨 All    │
└──────────────┴────────────┴──────────────────────────────┴───────────┘
```

---

### `zorui info` - Информация о ZorUI

Показывает информацию о фреймворке.

```bash
zorui info
```

**Вывод:**

```
┌──────────────────────────────────────────┐
│             About ZorUI                   │
├──────────────────────────────────────────┤
│ ZorUI Framework                          │
│                                          │
│ Version: 1.0.0                           │
│ License: MIT                             │
│ Website: https://zorui.dev               │
│ GitHub: https://github.com/zorui/zorui   │
│                                          │
│ Supported Platforms:                     │
│   ✅ Windows (Desktop)                   │
│   ✅ Linux (Desktop)                     │
│   ✅ macOS (Desktop)                     │
│   🔄 Android (In Development)            │
│   🔄 iOS (In Development)                │
│   🔮 Web (Planned)                       │
└──────────────────────────────────────────┘
```

---

## 🎯 Быстрый старт

### 1. Установите CLI

```bash
cd src/ZorUI.CLI
dotnet pack -c Release
dotnet tool install --global --add-source ./bin/Release ZorUI.CLI
```

### 2. Создайте проект

```bash
# Desktop приложение
zorui new desktop --name MyApp

# Перейдите в папку
cd MyApp

# Запустите
dotnet run
```

### 3. Наслаждайтесь! 🎉

---

## 📝 Структура создаваемых проектов

### Desktop App

```
MyApp/
├── MyApp.csproj         # Project file
├── Program.cs           # Entry point + App component
├── README.md            # Documentation
└── .gitignore          # Git ignore file
```

### Console App

```
MyCliApp/
├── MyCliApp.csproj
├── Program.cs
├── README.md
└── .gitignore
```

### Component Library

```
MyComponents/
├── MyComponents.csproj
├── MyButton.cs          # Example component
├── README.md
```

### Full App

```
MyFullApp/
├── MyFullApp.csproj
├── Program.cs
├── Components/          # Reusable components
│   └── Header.cs
├── Pages/              # Application pages
│   └── HomePage.cs
├── Styles/             # Shared styles
│   └── AppStyles.cs
└── README.md
```

---

## 🔧 Продвинутое использование

### Создание множества проектов

```bash
# Создать несколько проектов
zorui new desktop -n App1
zorui new desktop -n App2
zorui new console -n Tool1

# Или в цикле
for name in App1 App2 App3; do
  zorui new desktop -n $name
done
```

### Интеграция с Git

```bash
# Создать проект и инициализировать Git
zorui new desktop -n MyApp
cd MyApp
git init
git add .
git commit -m "Initial commit from ZorUI CLI"
```

### Создание в подпапке

```bash
# Организация проектов
mkdir projects
zorui new desktop -n App1 -o ./projects
zorui new desktop -n App2 -o ./projects
zorui new console -n Tool1 -o ./projects
```

---

## 🎨 Кастомизация шаблонов

Если вы хотите создать свои шаблоны:

1. Откройте `src/ZorUI.CLI/Templates/`
2. Создайте новый класс, например `MyTemplate.cs`
3. Реализуйте метод `Create(string path, string name)`
4. Добавьте в `NewCommand.cs`

Пример:

```csharp
public static class MyTemplate
{
    public static void Create(string path, string name)
    {
        Directory.CreateDirectory(path);
        // Создайте файлы проекта
        File.WriteAllText(Path.Combine(path, $"{name}.csproj"), GetProjectFile(name));
        File.WriteAllText(Path.Combine(path, "Program.cs"), GetProgramFile(name));
    }

    private static string GetProjectFile(string name) => "...";
    private static string GetProgramFile(string name) => "...";
}
```

---

## 🐛 Устранение проблем

### CLI не найден после установки

```bash
# Убедитесь что .NET tools в PATH
export PATH="$PATH:$HOME/.dotnet/tools"

# Или для Windows (PowerShell)
$env:PATH += ";$HOME\.dotnet\tools"
```

### Ошибка при установке

```bash
# Удалите старую версию
dotnet tool uninstall --global ZorUI.CLI

# Очистите NuGet кэш
dotnet nuget locals all --clear

# Установите заново
cd src/ZorUI.CLI
dotnet pack -c Release
dotnet tool install --global --add-source ./bin/Release ZorUI.CLI
```

### Шаблоны не работают

Убедитесь что:
1. Установлен .NET 8.0 SDK
2. ZorUI пакеты доступны (локально или на NuGet)
3. Права на запись в целевую директорию

---

## 📚 Примеры сценариев

### Создание микросервиса

```bash
# API + Desktop клиент
zorui new console -n MyAPI
zorui new desktop -n MyClient
```

### Создание компонентной библиотеки

```bash
# Библиотека + тестовое приложение
zorui new component -n MyUILib
zorui new desktop -n MyUILib.Demo
```

### Монорепозиторий

```bash
mkdir MyProject
cd MyProject

# Создаем несколько приложений
zorui new desktop -n Desktop -o ./apps
zorui new console -n CLI -o ./apps
zorui new component -n Shared -o ./libs

# Инициализируем solution
dotnet new sln -n MyProject
dotnet sln add apps/Desktop/*.csproj
dotnet sln add apps/CLI/*.csproj
dotnet sln add libs/Shared/*.csproj
```

---

## 🚀 Что дальше?

После создания проекта:

1. **Запустите:**
   ```bash
   cd MyApp
   dotnet run
   ```

2. **Изучите код** в созданных файлах

3. **Кастомизируйте:**
   - Добавьте свои компоненты
   - Измените темы
   - Добавьте логику

4. **Соберите для других платформ:**
   ```bash
   # Windows
   dotnet publish -c Release -r win-x64 --self-contained
   
   # Linux
   dotnet publish -c Release -r linux-x64 --self-contained
   
   # macOS
   dotnet publish -c Release -r osx-x64 --self-contained
   ```

5. **Прочитайте документацию:**
   - [Getting Started](docs/getting-started.md)
   - [Components](docs/components/)
   - [Best Practices](docs/best-practices.md)

---

## 📞 Поддержка

- 📖 [Документация](docs/)
- 💬 [Discord](https://discord.gg/zorui)
- 🐛 [GitHub Issues](https://github.com/zorui/zorui/issues)
- ✉️ support@zorui.dev

---

## 🎉 Готово!

Теперь вы можете создавать ZorUI проекты одной командой!

```bash
zorui new desktop -n MyAwesomeApp
cd MyAwesomeApp
dotnet run
```

**Приятной разработки! 🚀**
