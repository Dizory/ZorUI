# 🎨 ZorUI VS Code Extension - Руководство

## 🎯 Что это?

**VS Code расширение для ZorUI** - инструмент повышения продуктивности для разработчиков!

### ✨ Возможности:

- ⚡ **30+ Code Snippets** - Быстрое создание компонентов
- 🎨 **120+ иконок** - Radix UI стиль
- 🚀 **Команды** - Генерация файлов
- 🔧 **Настройки** - Кастомизация

---

## 📦 Установка

### Из marketplace (скоро):

1. Откройте VS Code
2. Extensions (`Ctrl+Shift+X`)
3. Найдите "ZorUI"
4. Нажмите Install

### Вручную:

```bash
cd vscode-extension
npm install
npm run package
code --install-extension zorui-1.0.0.vsix
```

---

## 🚀 Использование

### Code Snippets

Набирайте префикс и нажимайте `Tab`:

#### Создать компонент:

```csharp
// Напишите: zuicomp + Tab

public class MyComponent : ZorComponent
{
    private int _state = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .AddChild(
                new Text("Hello").WithFontSize(24)
            );
    }
}
```

#### Создать кнопку:

```csharp
// Напишите: zuibtn + Tab

new Button("Click me", HandleClick)
    .Primary()
    .Medium()
```

#### Создать VStack:

```csharp
// Напишите: zuivstack + Tab

new VStack(spacing: 20)
    .WithPadding(EdgeInsets.All(16))
    .AddChild(child)
```

#### Создать иконку:

```csharp
// Напишите: zuiicon + Tab

new Icon(ZorIcon.Home)
    .WithSize(24)
    .WithColor(theme.Colors.Primary)
```

### Команды

#### Создать компонент:

1. `Ctrl+Shift+P` (или `Cmd+Shift+P` на Mac)
2. Наберите `ZorUI: Create Component`
3. Введите имя компонента
4. Файл создан!

#### Вставить иконку:

1. `Ctrl+Shift+P`
2. `ZorUI: Insert Icon`
3. Выберите иконку из списка
4. Код вставлен!

---

## 📋 Все Snippets

### Компоненты

| Префикс | Что создает |
|---------|-------------|
| `zuicomp` | ZorComponent |
| `zuibtn` | Button |
| `zuitext` | Text |
| `zuicard` | Card |
| `zuitf` | TextField |
| `zuidialog` | Dialog |
| `zuiicon` | Icon |
| `zuitas` | Tabs |

### Layout

| Префикс | Что создает |
|---------|-------------|
| `zuivstack` | VStack |
| `zuihstack` | HStack |
| `zuizstack` | ZStack |
| `zuicont` | Container |
| `zuigrid` | Grid |
| `zuiscroll` | ScrollView |
| `zuipage` | Полный layout страницы |

### Формы

| Префикс | Что создает |
|---------|-------------|
| `zuichk` | Checkbox |
| `zuiswitch` | Switch |
| `zuiradio` | RadioGroup |
| `zuislider` | Slider |
| `zuiform` | Полная форма |

---

## 🎨 Система иконок

### 120+ иконок в стиле Radix UI!

```csharp
using ZorUI.Icons;

// Создать иконку
var icon = new ZorIconComponent(ZorIcon.Home)
    .WithSize(24)
    .WithColor(Color.Blue);

// Использовать хелперы
var icon2 = ZIcon.Home().Medium();
var icon3 = ZIcon.Settings().Large();
```

### Категории иконок:

- 🧭 **Navigation** (18) - Home, Menu, Chevrons, Arrows
- ⚡ **Actions** (16) - Plus, Edit, Delete, Search
- 👤 **User** (9) - User, Settings, Lock
- 💬 **Communication** (7) - Mail, Phone, Chat, Bell
- 🎵 **Media** (11) - Play, Pause, Volume
- 📁 **Files** (6) - File, Folder
- ⚠ **Status** (10) - Info, Warning, Error, Success
- 💙 **Social** (8) - Heart, Star, Bookmark
- 📅 **Time** (4) - Calendar, Clock
- 📐 **Layout** (6) - Grid, List, Columns
- 👁 **Visibility** (3) - Eye, EyeOff
- 📝 **Text** (8) - Bold, Italic, Align
- 🔗 **Misc** (30+) - Link, Globe, Moon, Sun
- 💻 **Dev** (7) - Code, Terminal, Git

**[Полный список иконок](docs/ICONS.md)**

---

## 💡 Примеры

### Счетчик

```csharp
// zuicomp + Tab, затем модифицируйте:

public class Counter : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .AddChild(
                new Text($"Count: {_count}")
                    .WithFontSize(32)
                    .Bold()
            )
            .AddChild(
                new Button("Increment", () => 
                    SetState(nameof(_count), ++_count))
                    .Primary()
            );
    }
}
```

### Форма с иконками

```csharp
new VStack(spacing: 16)
    .AddChild(
        new HStack(spacing: 8)
            .AddChild(ZIcon.User().Medium())
            .AddChild(
                new TextField("Name")
                    .WithValue(_name)
                    .WithOnChange(v => SetState(nameof(_name), _name = v))
            )
    )
    .AddChild(
        new HStack(spacing: 8)
            .AddChild(ZIcon.Mail().Medium())
            .AddChild(
                new TextField("Email")
                    .WithValue(_email)
                    .WithOnChange(v => SetState(nameof(_email), _email = v))
            )
    )
    .AddChild(
        new Button("Submit", HandleSubmit)
            .Primary()
            .WithLeadingIcon(ZIcon.Check())
    );
```

### Навигация с иконками

```csharp
new HStack(spacing: 16)
    .AddChild(
        new Button("", () => GoHome())
            .Ghost()
            .WithLeadingIcon(ZIcon.Home())
    )
    .AddChild(
        new Button("", () => OpenSearch())
            .Ghost()
            .WithLeadingIcon(ZIcon.Search())
    )
    .AddChild(
        new Button("", () => OpenSettings())
            .Ghost()
            .WithLeadingIcon(ZIcon.Settings())
    );
```

---

## ⚙️ Настройки

Откройте VS Code Settings и найдите "ZorUI":

```json
{
  "zorui.useFluentApi": true,
  "zorui.defaultTheme": "light"
}
```

---

## 🔧 Разработка

### Установка зависимостей:

```bash
cd vscode-extension
npm install
```

### Компиляция:

```bash
npm run compile
```

### Watch режим:

```bash
npm run watch
```

### Создание .vsix:

```bash
npm run package
```

### Тестирование:

Нажмите `F5` в VS Code для запуска Extension Development Host.

---

## 📚 Ресурсы

- [README расширения](vscode-extension/README.md) - Полная документация
- [Список иконок](docs/ICONS.md) - Все иконки
- [ZorUI Docs](docs/) - Документация фреймворка
- [GitHub](https://github.com/zorui/zorui) - Репозиторий

---

## 🎯 Следующие шаги

1. **Установите расширение** - Следуйте инструкциям выше
2. **Попробуйте snippets** - `zuicomp`, `zuibtn`, etc.
3. **Используйте команды** - `ZorUI: Create Component`
4. **Исследуйте иконки** - 120+ иконок доступны!

---

## 🤝 Помощь

Нашли баг или есть идея? 

- 🐛 [GitHub Issues](https://github.com/zorui/zorui/issues)
- 💬 [Discord](https://discord.gg/zorui)

---

## 📝 Лицензия

MIT License

---

<div align="center">

# 🎉 Приятной работы с ZorUI!

**Расширение делает разработку в 10 раз быстрее! ⚡**

</div>
