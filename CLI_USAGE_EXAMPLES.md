# 💡 ZorUI CLI - Примеры использования

Практические примеры использования ZorUI CLI.

---

## 🚀 Базовое использование

### Создать desktop приложение

```bash
# Базовая команда
zorui new desktop --name MyApp

# С кастомной папкой
zorui new desktop --name MyApp --output ./projects

# Сокращенная форма
zorui new desktop -n MyApp -o ./projects
```

### Создать console приложение

```bash
zorui new console --name MyTool
```

### Создать библиотеку компонентов

```bash
zorui new component --name MyUIComponents
```

### Создать полноценное приложение

```bash
zorui new full --name MyFullApp
```

---

## 🎯 Реальные сценарии

### Сценарий 1: Простое приложение

```bash
# Создать калькулятор
zorui new desktop --name Calculator

# Перейти и запустить
cd Calculator
dotnet run

# Собрать для Windows
dotnet publish -c Release -r win-x64 --self-contained
```

### Сценарий 2: Проект с несколькими приложениями

```bash
# Создать папку проекта
mkdir MyProject
cd MyProject

# Desktop клиент
zorui new desktop --name Client --output ./apps

# Console API
zorui new console --name API --output ./apps

# Общая библиотека
zorui new component --name Shared --output ./libs

# Создать solution
dotnet new sln --name MyProject
dotnet sln add apps/**/*.csproj
dotnet sln add libs/**/*.csproj

# Запустить клиент
cd apps/Client
dotnet run
```

### Сценарий 3: Быстрое прототипирование

```bash
# Создать быстрый прототип
zorui new desktop --name Prototype

cd Prototype

# Запустить
dotnet run

# Экспериментировать с кодом
code Program.cs  # или vim, notepad++, и т.д.

# Пересобрать и запустить
dotnet run
```

### Сценарий 4: Компонентная библиотека

```bash
# Создать библиотеку
zorui new component --name MyUIKit

cd MyUIKit

# Добавить свои компоненты
# ... редактируем файлы

# Собрать
dotnet build

# Упаковать в NuGet
dotnet pack -c Release

# Использовать в другом проекте
zorui new desktop --name TestApp
cd TestApp
dotnet add reference ../MyUIKit/MyUIKit.csproj
```

### Сценарий 5: Учебный проект

```bash
# Создать несколько примеров для обучения
zorui new desktop --name Example1_Basics
zorui new desktop --name Example2_Forms
zorui new desktop --name Example3_Advanced
zorui new full --name Example4_BestPractices

# Каждый пример показывает разные концепции
```

---

## 🔧 Продвинутые примеры

### Монорепозиторий с микросервисами

```bash
mkdir MicroserviceApp
cd MicroserviceApp

# Backend сервисы
zorui new console --name AuthService -o ./services
zorui new console --name DataService -o ./services
zorui new console --name ApiGateway -o ./services

# Frontend клиенты
zorui new desktop --name AdminPanel -o ./clients
zorui new desktop --name UserApp -o ./clients

# Общие библиотеки
zorui new component --name CoreLib -o ./shared
zorui new component --name UILib -o ./shared

# Solution
dotnet new sln --name MicroserviceApp
find . -name "*.csproj" -exec dotnet sln add {} \;

# Запустить все (в разных терминалах)
cd services/AuthService && dotnet run &
cd services/DataService && dotnet run &
cd clients/AdminPanel && dotnet run
```

### Создание UI Kit

```bash
# Библиотека компонентов
zorui new component --name MyDesignSystem

cd MyDesignSystem

# Добавляем файлы
cat > CustomButton.cs << 'EOF'
using ZorUI.Components.Primitives;

public class CustomButton : Button
{
    public CustomButton(string text) : base(text)
    {
        // Ваша кастомизация
    }

    public CustomButton Branded()
    {
        WithStyle(MyStyles.BrandedButton);
        return this;
    }
}
EOF

# Собираем
dotnet build

# Используем в других проектах
zorui new desktop --name Demo
cd Demo
dotnet add reference ../MyDesignSystem/MyDesignSystem.csproj
```

### CI/CD Integration

```bash
# .github/workflows/build.yml
name: Build

on: [push]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      
      - name: Setup .NET
        uses: actions/setup-dotnet@v1
        with:
          dotnet-version: 8.0.x
      
      - name: Install ZorUI CLI
        run: |
          chmod +x install-cli.sh
          ./install-cli.sh
      
      - name: Create test project
        run: |
          zorui new desktop --name TestApp
          cd TestApp
          dotnet build
```

---

## 🎨 Кастомизация после создания

### Изменить тему

```csharp
// В Program.cs
var theme = new Theme
{
    Colors = new ColorPalette
    {
        Primary = Color.FromHex("#FF6B6B"),
        Background = Color.FromHex("#F5F5F5")
    }
};

// Применить
app.Context.Theme = theme;
```

### Добавить страницы

```bash
# Создать структуру
mkdir Pages
cat > Pages/HomePage.cs << 'EOF'
using ZorUI.Core;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;

public class HomePage : ZorComponent
{
    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .AddChild(new Text("Home Page").WithFontSize(32));
    }
}
EOF

# Использовать в Program.cs
window.RootComponent = new HomePage();
```

### Добавить навигацию

```csharp
public class App : ZorComponent
{
    private string _currentPage = "home";

    public override ZorElement Build()
    {
        return new VStack()
            .AddChild(BuildNavigation())
            .AddChild(BuildCurrentPage());
    }

    private ZorElement BuildNavigation()
    {
        return new Tabs(_currentPage)
            .AddTab(new Tab("home", "Home"))
            .AddTab(new Tab("settings", "Settings"))
            .WithOnTabChange(page => 
                SetState(nameof(_currentPage), _currentPage = page));
    }

    private ZorElement BuildCurrentPage()
    {
        return _currentPage switch
        {
            "home" => new HomePage(),
            "settings" => new SettingsPage(),
            _ => new Text("404")
        };
    }
}
```

---

## 🔍 Советы и трюки

### 1. Быстрое создание и запуск

```bash
# Одной строкой
zorui new desktop -n QuickTest && cd QuickTest && dotnet run
```

### 2. Создание в текущей папке

```bash
# Без создания подпапки
mkdir MyApp && cd MyApp
zorui new desktop -n MyApp -o .
```

### 3. Пакетное создание

```bash
# Bash
for app in App1 App2 App3; do
  zorui new desktop -n $app
done

# PowerShell
@("App1", "App2", "App3") | ForEach-Object {
  zorui new desktop -n $_
}
```

### 4. Быстрое тестирование идеи

```bash
# Создать временный проект
zorui new desktop -n temp_test
cd temp_test
dotnet run
cd ..
rm -rf temp_test  # Удалить после
```

### 5. Создание из скрипта

```bash
#!/bin/bash
# create-my-app.sh

APP_NAME=${1:-MyApp}

echo "Creating $APP_NAME..."
zorui new desktop -n "$APP_NAME"

cd "$APP_NAME"

echo "Installing additional packages..."
dotnet add package Newtonsoft.Json

echo "Done! Run with: cd $APP_NAME && dotnet run"
```

---

## 📊 Сравнение времени

| Задача | Без CLI | С CLI |
|--------|---------|-------|
| Создание проекта | 5-10 мин | 5 сек |
| Настройка .csproj | 2-3 мин | 0 сек |
| Написание базового кода | 3-5 мин | 0 сек |
| Создание структуры | 2-3 мин | 0 сек |
| Готовность к работе | 10-20 мин | **30 сек** ⚡ |

**Экономия времени: 95%!**

---

## ✅ Best Practices с CLI

### 1. Используйте осмысленные имена

```bash
# ✅ Хорошо
zorui new desktop --name UserManagementApp
zorui new console --name DataMigrationTool

# ❌ Плохо
zorui new desktop --name app1
zorui new console --name test
```

### 2. Организуйте структуру

```bash
# ✅ Хорошо: Организованная структура
mkdir MyProject
cd MyProject
zorui new desktop -n Client -o ./apps
zorui new console -n API -o ./apps
zorui new component -n Shared -o ./libs

# ❌ Плохо: Всё в одной папке
zorui new desktop -n Client
zorui new console -n API
zorui new component -n Shared
```

### 3. Используйте правильный шаблон

```bash
# ✅ Desktop для GUI
zorui new desktop -n MyGUIApp

# ✅ Console для CLI инструментов
zorui new console -n MyTool

# ✅ Component для библиотек
zorui new component -n MyLib

# ✅ Full для сложных проектов
zorui new full -n MyComplexApp
```

---

## 🎉 Заключение

**ZorUI CLI делает создание проектов мгновенным!**

**Основные команды:**

```bash
# Установка
./install-cli.sh

# Создание
zorui new desktop --name MyApp

# Запуск
cd MyApp && dotnet run
```

**Попробуйте прямо сейчас! 🚀**

---

## 📚 См. также

- [CLI_GUIDE.md](CLI_GUIDE.md) - Полное руководство
- [QUICK_START_CLI.md](QUICK_START_CLI.md) - Быстрый старт
- [START_HERE.md](START_HERE.md) - Главная страница
