# ⚡ Быстрый старт с ZorUI CLI

Создавайте проекты одной командой!

## 🚀 Установка CLI (30 секунд)

```bash
# Linux/macOS
./install-cli.sh

# Windows
install-cli.cmd
```

**Готово!** Команда `zorui` теперь доступна глобально.

---

## 🎯 Создание первого проекта (1 минута)

### Desktop приложение

```bash
zorui new desktop --name MyApp
cd MyApp
dotnet run
```

**Результат:** Полноценное GUI приложение запустилось! 🎉

### Console приложение

```bash
zorui new console --name MyTool
cd MyTool
dotnet run
```

### Библиотека компонентов

```bash
zorui new component --name MyComponents
```

### Полное приложение

```bash
zorui new full --name MyFullApp
cd MyFullApp
dotnet run
```

---

## 📋 Все команды

```bash
# Создать проект
zorui new <template> --name <ProjectName>

# Список шаблонов
zorui list

# Информация о ZorUI
zorui info

# Справка
zorui --help
```

---

## 🎨 Доступные шаблоны

| Команда | Что создает |
|---------|-------------|
| `zorui new desktop -n MyApp` | Desktop GUI приложение (Windows/Linux/macOS) |
| `zorui new console -n MyTool` | Console приложение |
| `zorui new component -n MyLib` | Библиотека компонентов |
| `zorui new full -n MyFullApp` | Полноценное приложение с примерами |

---

## 💡 Примеры

### Создать и запустить приложение

```bash
zorui new desktop --name Calculator
cd Calculator
dotnet run
```

### Создать несколько проектов

```bash
# API
zorui new console --name MyAPI

# Desktop клиент
zorui new desktop --name MyClient

# Общая библиотека
zorui new component --name Shared
```

### С кастомной папкой

```bash
zorui new desktop --name App1 --output ./projects
zorui new desktop --name App2 --output ./projects
```

---

## 🔧 Сборка приложения

После создания проекта:

```bash
# Запустить
dotnet run

# Собрать Release
dotnet build -c Release

# Создать исполняемый файл
# Windows
dotnet publish -c Release -r win-x64 --self-contained

# Linux
dotnet publish -c Release -r linux-x64 --self-contained

# macOS
dotnet publish -c Release -r osx-x64 --self-contained
```

---

## 📚 Что дальше?

- **[CLI_GUIDE.md](CLI_GUIDE.md)** - Полное руководство по CLI
- **[docs/](docs/)** - Документация ZorUI
- **[samples/](samples/)** - Примеры приложений

---

## ❓ Проблемы?

### CLI не найден

```bash
# Добавьте в PATH (Linux/macOS)
export PATH="$PATH:$HOME/.dotnet/tools"

# Windows PowerShell
$env:PATH += ";$HOME\.dotnet\tools"
```

### Переустановка

```bash
# Удалите
dotnet tool uninstall --global ZorUI.CLI

# Установите заново
./install-cli.sh  # или install-cli.cmd
```

---

## 🎉 Готово!

```bash
zorui new desktop --name MyApp
cd MyApp
dotnet run
```

**Приятной разработки! 🚀**
