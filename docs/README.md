# ZorUI Documentation

Добро пожаловать в документацию ZorUI!

## 📚 Содержание

### Начало работы
- **[Getting Started](getting-started.md)** - Установка и первое приложение
- **[Quick Reference](QuickReference.md)** - Краткая справка по API
- **[Core Concepts](core-concepts.md)** - Основные концепции фреймворка

### Руководства
- **[Styling](styling.md)** - Система стилей и тем
- **[State Management](state-management.md)** - Управление состоянием
- **[Best Practices](best-practices.md)** - Рекомендации и паттерны

### Компоненты
- **[Layout Components](components/layout.md)** - VStack, HStack, Grid, etc.
- **[Form Components](components/forms.md)** - TextField, Checkbox, Switch, etc.
- **[Navigation](components/navigation.md)** - Tabs, Menu, etc.
- **[Overlays](components/overlays.md)** - Dialog, Popover, Toast, etc.
- **[Data Display](components/data-display.md)** - Card, Progress, Avatar, etc.

### API Reference
- **[Core API](api/core.md)** - ZorElement, ZorComponent, ZorApplication
- **[Styling API](api/styling.md)** - Style, Theme, ColorPalette
- **[Components API](api/components.md)** - Полная документация компонентов

## 🚀 Быстрые ссылки

### Новичкам
1. [Установка](getting-started.md#установка)
2. [Первое приложение](getting-started.md#первое-приложение)
3. [Основы компонентов](core-concepts.md#components)
4. [Quick Reference](QuickReference.md)

### Опытным разработчикам
1. [Core Concepts](core-concepts.md)
2. [Best Practices](best-practices.md)
3. [State Management](state-management.md)
4. [API Reference](api/)

## 💡 Примеры

### Простой Counter
```csharp
public class Counter : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 16)
            .AddChild(new Text($"Count: {_count}").WithFontSize(24))
            .AddChild(new Button("Increment", () => 
            {
                SetState(nameof(_count), ++_count);
            }).Primary());
    }
}
```

### Форма с валидацией
```csharp
public class LoginForm : ZorComponent
{
    private string _email = "";
    private string _password = "";
    private string? _error;

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .WithPadding(24)
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
                    .WithFullWidth()
                    .Disabled(!IsValid())
            );
    }

    private bool IsValid() => 
        !string.IsNullOrEmpty(_email) && _password.Length >= 8;

    private void HandleLogin()
    {
        ToastManager.Instance.ShowSuccess("Logged in!");
    }
}
```

## 🎨 Галерея компонентов

Запустите Component Gallery для интерактивного просмотра:

```bash
cd samples/ComponentGallery
dotnet run
```

## 📖 Дополнительные ресурсы

### Туториалы
- [Building a Todo App](tutorials/todo-app.md)
- [Creating a Dashboard](tutorials/dashboard.md)
- [Form Validation](tutorials/form-validation.md)

### Рецепты
- [Custom Components](recipes/custom-components.md)
- [Theming](recipes/theming.md)
- [Animations](recipes/animations.md)

### Видео
- [Introduction to ZorUI](https://youtube.com/@zorui)
- [Building Your First App](https://youtube.com/@zorui)

## 🤝 Сообщество

- **Discord**: [discord.gg/zorui](https://discord.gg/zorui)
- **GitHub**: [github.com/zorui/zorui](https://github.com/zorui/zorui)
- **Twitter**: [@ZorUIFramework](https://twitter.com/ZorUIFramework)

## 🐛 Нашли ошибку?

Если вы нашли ошибку в документации:
1. Откройте issue на [GitHub](https://github.com/zorui/zorui/issues)
2. Или создайте Pull Request с исправлением
3. Или сообщите в [Discord](https://discord.gg/zorui)

## 📝 Contributing

Хотите помочь с документацией? См. [CONTRIBUTING.md](../CONTRIBUTING.md)
