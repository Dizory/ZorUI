# 📁 ZorUI Framework - Обзор файлов

## 📊 Статистика

- **C# файлов:** 50+
- **Markdown файлов:** 25+
- **Проектов:** 7
- **Строк кода:** ~12,000+
- **Строк документации:** ~8,000+

---

## 🗂️ Структура файлов

### 🚀 Корневые файлы (быстрый старт)

| Файл | Назначение | Начать? |
|------|-----------|---------|
| **START_HERE.md** | Главная точка входа | ⭐⭐⭐ |
| **INDEX.md** | Навигация по документации | ⭐⭐⭐ |
| **QUICK_START_CLI.md** | CLI за 3 минуты | ⭐⭐ |
| **READY_TO_USE.md** | Готово к использованию | ⭐⭐ |
| **YOUR_ZORUI_FRAMEWORK.md** | Ваш фреймворк готов | ⭐ |
| README.md | О проекте | ⭐ |
| COMPLETE.md | Проект завершен | ℹ️ |

### 🛠️ CLI и инструменты

| Файл | Назначение |
|------|-----------|
| **install-cli.sh** | Установка CLI (Linux/Mac) |
| **install-cli.cmd** | Установка CLI (Windows) |
| CLI_GUIDE.md | Полное руководство CLI |
| CLI_README.md | О CLI инструменте |
| CLI_USAGE_EXAMPLES.md | Примеры использования CLI |
| build-packages.sh | Сборка NuGet пакетов |
| build-packages.cmd | Сборка NuGet (Windows) |

### 🌍 Платформы

| Файл | Назначение |
|------|-----------|
| PLATFORM_GUIDE.md | Руководство по платформам |
| PLATFORM_SUPPORT.md | Детали поддержки |
| FINAL_ANSWER.md | Сводка по платформам |

### 📖 Руководства

| Файл | Назначение |
|------|-----------|
| HOW_TO_USE.md | Как использовать фреймворк |
| GETTING_STARTED_QUICK.md | Старт за 5 минут |
| PROJECT_SUMMARY.md | Сводка проекта |

### 📋 Информационные

| Файл | Назначение |
|------|-----------|
| CHANGELOG.md | История изменений |
| CONTRIBUTING.md | Как помочь |
| LICENSE | Лицензия MIT |
| .gitignore | Git ignore |
| .editorconfig | Стиль кода |

---

## 📂 src/ (исходный код)

### ZorUI.Core/
```
ZorUI.Core/
├── ZorElement.cs              # Базовый элемент
├── ZorComponent.cs            # Базовый компонент
├── ZorApplication.cs          # Приложение
├── BuildContext.cs            # Контекст
├── RebuildScheduler.cs        # Scheduler
├── Properties/
│   ├── Color.cs              # Цвета
│   ├── Size.cs               # Размеры
│   ├── EdgeInsets.cs         # Отступы
│   └── Alignment.cs          # Выравнивание
└── ZorUI.Core.csproj
```

### ZorUI.Components/
```
ZorUI.Components/
├── Layout/                    # 9 компонентов
│   ├── VStack.cs
│   ├── HStack.cs
│   ├── ZStack.cs
│   ├── Container.cs
│   ├── Grid.cs
│   ├── Wrap.cs
│   ├── ScrollView.cs
│   ├── Spacer.cs
│   └── Divider.cs
│
├── Primitives/                # 5 компонентов
│   ├── Text.cs
│   ├── Button.cs
│   ├── Image.cs
│   ├── Icon.cs
│   └── Label.cs
│
├── Forms/                     # 5 компонентов
│   ├── TextField.cs
│   ├── Checkbox.cs
│   ├── Radio.cs
│   ├── Switch.cs
│   └── Slider.cs
│
├── Navigation/                # 2 компонента
│   ├── Tabs.cs
│   └── Menu.cs
│
├── Overlays/                  # 4 компонента
│   ├── Dialog.cs
│   ├── Popover.cs
│   ├── Tooltip.cs
│   └── Toast.cs
│
├── DataDisplay/               # 7 компонентов
│   ├── Card.cs
│   ├── Accordion.cs
│   ├── Progress.cs
│   ├── Avatar.cs
│   ├── Badge.cs
│   ├── Alert.cs
│   └── Spinner.cs
│
└── ZorUI.Components.csproj
```

### ZorUI.Styling/
```
ZorUI.Styling/
├── Style.cs                   # Система стилей
├── Theme.cs                   # Темы
└── ZorUI.Styling.csproj
```

### ZorUI.Rendering/
```
ZorUI.Rendering/
├── IRenderer.cs              # Интерфейс
├── ConsoleRenderer.cs        # Console
└── ZorUI.Rendering.csproj
```

### ZorUI.Rendering.Skia/ ⭐
```
ZorUI.Rendering.Skia/
├── SkiaRenderer.cs           # GUI рендерер
├── SkiaWindow.cs             # Оконная система
└── ZorUI.Rendering.Skia.csproj
```

### ZorUI.CLI/ ⭐
```
ZorUI.CLI/
├── Program.cs                # Entry point
├── Commands/
│   ├── NewCommand.cs        # zorui new
│   ├── ListCommand.cs       # zorui list
│   └── InfoCommand.cs       # zorui info
├── Templates/
│   ├── DesktopTemplate.cs   # Desktop шаблон
│   ├── ConsoleTemplate.cs   # Console шаблон
│   ├── ComponentTemplate.cs # Component шаблон
│   └── FullTemplate.cs      # Full шаблон
└── ZorUI.CLI.csproj
```

---

## 📂 samples/ (примеры)

### BasicApp/
```
BasicApp/
├── Program.cs                # Простой счетчик
├── BasicApp.csproj
└── README.md
```

### ComponentGallery/
```
ComponentGallery/
├── Program.cs                # Все 32 компонента
├── ComponentGallery.csproj
└── README.md
```

### DesktopApp/ ⭐
```
DesktopApp/
├── Program.cs                # Полноценное GUI!
├── DesktopApp.csproj
└── README.md
```

### MyZorApp/
```
MyZorApp/
├── Program.cs                # Демо приложение
├── MyZorApp.csproj
└── README.md
```

---

## 📂 docs/ (документация)

```
docs/
├── README.md                 # Главная документация
├── getting-started.md        # Начало работы
├── core-concepts.md          # Основные концепции
├── QuickReference.md         # API справка ⭐
├── styling.md                # Стилизация
├── state-management.md       # Управление состоянием
└── best-practices.md         # Лучшие практики
```

---

## 🎯 Рекомендуемая последовательность чтения

### День 1 (30 мин):
1. **[YOUR_ZORUI_FRAMEWORK.md](YOUR_ZORUI_FRAMEWORK.md)** - Этот файл
2. **[START_HERE.md](START_HERE.md)** - С чего начать
3. Установите CLI: `./install-cli.sh`
4. Создайте проект: `zorui new desktop -n Test`
5. Запустите: `cd Test && dotnet run`

### День 2 (1 час):
1. **[QUICK_START_CLI.md](QUICK_START_CLI.md)** - CLI мастер
2. **[docs/QuickReference.md](docs/QuickReference.md)** - API справка
3. Поэкспериментируйте с компонентами

### День 3 (2 часа):
1. **[docs/getting-started.md](docs/getting-started.md)**
2. **[docs/core-concepts.md](docs/core-concepts.md)**
3. Создайте свое приложение

### Продвинутый уровень:
1. **[docs/state-management.md](docs/state-management.md)**
2. **[docs/styling.md](docs/styling.md)**
3. **[docs/best-practices.md](docs/best-practices.md)**

---

## 🎨 Примеры файлов для изучения

### Базовые:
- `samples/BasicApp/Program.cs` - Простейший пример
- `src/ZorUI.Core/ZorComponent.cs` - Как работают компоненты
- `src/ZorUI.Components/Primitives/Button.cs` - Пример компонента

### Продвинутые:
- `samples/DesktopApp/Program.cs` - Полное приложение
- `samples/ComponentGallery/Program.cs` - Все компоненты
- `src/ZorUI.Rendering.Skia/SkiaRenderer.cs` - Рендеринг

### CLI:
- `src/ZorUI.CLI/Program.cs` - CLI entry point
- `src/ZorUI.CLI/Templates/DesktopTemplate.cs` - Генерация кода

---

## 🔥 НАЧНИТЕ ПРЯМО СЕЙЧАС!

```bash
# 1. Установите CLI
./install-cli.sh

# 2. Создайте проект
zorui new desktop --name MyFirstApp

# 3. Запустите
cd MyFirstApp
dotnet run

# 4. Увидите работающее приложение! 🎉
```

---

<div align="center">

**🎊 ВАШ ФРЕЙМВОРК ZORUI ПОЛНОСТЬЮ ГОТОВ! 🎊**

**Всё что нужно - установить CLI и начать создавать!**

**[⬆ Наверх](#-zorui-framework---обзор-файлов)**

</div>
