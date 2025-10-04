# Best Practices

Рекомендации и паттерны для эффективной разработки с ZorUI.

## Содержание

- [Структура проекта](#структура-проекта)
- [Компоненты](#компоненты)
- [State Management](#state-management)
- [Performance](#performance)
- [Стилизация](#стилизация)
- [Accessibility](#accessibility)
- [Тестирование](#тестирование)

## Структура проекта

### Рекомендуемая структура

```
MyApp/
├── src/
│   ├── Components/          # Переиспользуемые компоненты
│   │   ├── Common/         # Базовые компоненты
│   │   ├── Forms/          # Формы и поля
│   │   └── Layout/         # Layout компоненты
│   │
│   ├── Pages/              # Страницы/экраны
│   │   ├── HomePage.cs
│   │   ├── ProfilePage.cs
│   │   └── SettingsPage.cs
│   │
│   ├── Services/           # Бизнес-логика
│   │   ├── ApiService.cs
│   │   └── AuthService.cs
│   │
│   ├── Models/             # Модели данных
│   │   └── User.cs
│   │
│   ├── Styles/             # Стили и темы
│   │   ├── AppTheme.cs
│   │   └── AppStyles.cs
│   │
│   └── Program.cs          # Entry point
│
└── tests/                  # Тесты
```

### Организация компонентов

```csharp
// ✅ Хорошо: Один компонент - один файл
// UserCard.cs
public class UserCard : ZorComponent { ... }

// ❌ Плохо: Множество компонентов в одном файле
public class UserCard : ZorComponent { ... }
public class UserAvatar : ZorComponent { ... }
public class UserInfo : ZorComponent { ... }
```

## Компоненты

### Принципы проектирования

#### 1. Single Responsibility

```csharp
// ✅ Хорошо: Компонент делает одну вещь
public class UserAvatar : ZorComponent
{
    private readonly User _user;
    
    public UserAvatar(User user) => _user = user;
    
    public override ZorElement Build()
    {
        return new Avatar()
            .WithImage(_user.AvatarUrl)
            .WithInitials(_user.Initials)
            .WithStatus(_user.Status);
    }
}

// ❌ Плохо: Компонент делает слишком много
public class UserProfile : ZorComponent
{
    // Отображает аватар, информацию, посты, друзей, и т.д.
}
```

#### 2. Композиция над наследованием

```csharp
// ✅ Хорошо: Композиция
public class ProfileCard : ZorComponent
{
    private readonly User _user;
    
    public override ZorElement Build()
    {
        return new Card()
            .WithHeader(new UserHeader(_user))
            .WithContent(new UserInfo(_user))
            .WithFooter(new UserActions(_user));
    }
}

// ❌ Плохо: Глубокая иерархия наследования
public class BaseCard : ZorComponent { }
public class UserCard : BaseCard { }
public class ProfileCard : UserCard { }
```

#### 3. Props для конфигурации

```csharp
// ✅ Хорошо: Явные props
public class Button : ZorComponent
{
    private readonly string _text;
    private readonly Action? _onClick;
    private readonly ButtonVariant _variant;
    
    public Button(string text, Action? onClick = null, 
                  ButtonVariant variant = ButtonVariant.Default)
    {
        _text = text;
        _onClick = onClick;
        _variant = variant;
    }
}

// ❌ Плохо: Неявная конфигурация
public class Button : ZorComponent
{
    public string Text { get; set; }  // Может измениться после создания
    public Action? OnClick { get; set; }
}
```

### Размер компонентов

```csharp
// ✅ Хорошо: Маленький, фокусированный компонент
public class SearchBar : ZorComponent
{
    private string _query = "";
    
    public override ZorElement Build()
    {
        return new TextField("Search...")
            .WithValue(_query)
            .WithOnChange(q => SetState(nameof(_query), _query = q))
            .WithLeadingIcon(new Icon("search"));
    }
}

// ❌ Плохо: Огромный компонент (> 300 строк)
public class Dashboard : ZorComponent
{
    // Слишком много логики и UI в одном компоненте
    // Разбейте на более мелкие компоненты
}
```

### Переиспользование

```csharp
// ✅ Хорошо: Переиспользуемый компонент
public class IconButton : ZorComponent
{
    private readonly string _icon;
    private readonly Action? _onClick;
    private readonly string? _tooltip;
    
    public IconButton(string icon, Action? onClick = null, string? tooltip = null)
    {
        _icon = icon;
        _onClick = onClick;
        _tooltip = tooltip;
    }
    
    public override ZorElement Build()
    {
        var button = new Button("")
            .Ghost()
            .WithLeadingIcon(new Icon(_icon))
            .WithOnClick(_onClick);
        
        return _tooltip != null
            ? new Tooltip(_tooltip).WithTrigger(button)
            : button;
    }
}

// Использование
new IconButton("edit", HandleEdit, "Edit item")
new IconButton("delete", HandleDelete, "Delete item")
new IconButton("share", HandleShare, "Share")
```

## State Management

### Локальное состояние

```csharp
// ✅ Хорошо: Состояние инкапсулировано
public class ToggleSwitch : ZorComponent
{
    private bool _isOn = false;
    private readonly Action<bool>? _onToggle;
    
    public ToggleSwitch(bool initialState = false, Action<bool>? onToggle = null)
    {
        _isOn = initialState;
        _onToggle = onToggle;
    }
    
    private void Toggle()
    {
        SetState(nameof(_isOn), _isOn = !_isOn);
        _onToggle?.Invoke(_isOn);
    }
}

// ❌ Плохо: Состояние в публичных свойствах
public class ToggleSwitch : ZorComponent
{
    public bool IsOn { get; set; }  // Может быть изменено извне
}
```

### Подъем состояния

```csharp
// ✅ Хорошо: Состояние в родительском компоненте
public class FilteredList : ZorComponent
{
    private string _filter = "";
    private List<Item> _items = LoadItems();
    
    public override ZorElement Build()
    {
        var filtered = _items
            .Where(i => i.Name.Contains(_filter))
            .ToList();
        
        return new VStack(spacing: 16)
            .AddChild(new SearchBar(_filter, f => 
            {
                SetState(nameof(_filter), _filter = f);
            }))
            .AddChild(new ItemList(filtered));
    }
}
```

### Избегайте излишнего состояния

```csharp
// ✅ Хорошо: Вычисляемое значение
public class UserProfile : ZorComponent
{
    private string _firstName = "John";
    private string _lastName = "Doe";
    
    private string FullName => $"{_firstName} {_lastName}";
    
    public override ZorElement Build()
    {
        return new Text(FullName);  // Вычисляется при рендере
    }
}

// ❌ Плохо: Дублирование состояния
public class UserProfile : ZorComponent
{
    private string _firstName = "John";
    private string _lastName = "Doe";
    private string _fullName = "John Doe";  // Избыточно!
}
```

## Performance

### Избегайте ненужных rebuild'ов

```csharp
// ✅ Хорошо: Обновляем только при изменении
public class OptimizedComponent : ZorComponent
{
    private int _count = 0;
    
    public override ZorElement Build()
    {
        return new Button("Increment", () =>
        {
            var newCount = _count + 1;
            if (newCount != _count)  // Проверка изменения
            {
                SetState(nameof(_count), _count = newCount);
            }
        });
    }
}

// ❌ Плохо: Всегда вызываем SetState
public class UnoptimizedComponent : ZorComponent
{
    private int _count = 0;
    
    public override ZorElement Build()
    {
        return new Button("Increment", () =>
        {
            SetState(nameof(_count), _count = _count);  // Ненужный rebuild
        });
    }
}
```

### Ленивая загрузка

```csharp
// ✅ Хорошо: Загружаем данные при необходимости
public class LazyLoadedList : ZorComponent
{
    private List<Item>? _items;
    private bool _isLoading = false;
    
    protected override void OnMount()
    {
        base.OnMount();
        LoadItems();
    }
    
    private async void LoadItems()
    {
        SetState(nameof(_isLoading), _isLoading = true);
        _items = await ApiService.GetItemsAsync();
        SetState(nameof(_isLoading), _isLoading = false);
    }
    
    public override ZorElement Build()
    {
        if (_isLoading)
            return new Spinner();
        
        if (_items == null)
            return new Text("No data");
        
        return BuildList(_items);
    }
}
```

### Batch updates

```csharp
// ✅ Хорошо: Batch обновления
public class BatchedComponent : ZorComponent
{
    private int _count = 0;
    private string _status = "";
    
    private void UpdateAll()
    {
        SetState(() =>
        {
            _count++;
            _status = "Updated";
            // Только один rebuild для обоих изменений
        });
    }
}

// ❌ Плохо: Множественные SetState
public class UnbatchedComponent : ZorComponent
{
    private int _count = 0;
    private string _status = "";
    
    private void UpdateAll()
    {
        SetState(nameof(_count), ++_count);     // Rebuild 1
        SetState(nameof(_status), _status = "Updated");  // Rebuild 2
    }
}
```

## Стилизация

### Используйте систему дизайна

```csharp
// ✅ Хорошо: Использует Theme
public class ThemedButton : ZorComponent
{
    public override ZorElement Build()
    {
        var theme = Context?.Theme as Theme ?? Theme.Light();
        
        return new Button("Click")
            .WithStyle(new Style
            {
                BackgroundColor = theme.Colors.Primary,
                ForegroundColor = theme.Colors.PrimaryForeground,
                Padding = EdgeInsets.All(theme.Spacing.Space4),
                BorderRadius = theme.Radius.Md
            });
    }
}

// ❌ Плохо: Жестко заданные значения
public class HardcodedButton : ZorComponent
{
    public override ZorElement Build()
    {
        return new Button("Click")
            .WithStyle(new Style
            {
                BackgroundColor = Color.FromHex("#3B82F6"),
                Padding = EdgeInsets.All(16),
                BorderRadius = 8
            });
    }
}
```

### Создавайте переиспользуемые стили

```csharp
// ✅ Хорошо: Централизованные стили
public static class AppStyles
{
    public static Style Card(Theme theme) => new Style
    {
        BackgroundColor = theme.Colors.Card,
        BorderRadius = theme.Radius.Lg,
        Padding = EdgeInsets.All(theme.Spacing.Space6),
        BorderWidth = 1,
        BorderColor = theme.Colors.Border
    };
    
    public static Style Heading(Theme theme) => new Style
    {
        FontSize = theme.Typography.FontSize2Xl,
        FontWeight = FontWeight.Bold,
        ForegroundColor = theme.Colors.Foreground
    };
}

// Использование
new Container()
    .WithStyle(AppStyles.Card(theme));
```

## Accessibility

### Добавляйте описательный текст

```csharp
// ✅ Хорошо: Добавлены alt-тексты и labels
new Image("user.jpg")
    .WithAltText("User profile photo");

new TextField("Email")
    .Required()
    .WithValue(_email);

new Button("")
    .WithLeadingIcon(new Icon("close"))
    .WithAltText("Close dialog");  // Для screen readers
```

### Логическая структура

```csharp
// ✅ Хорошо: Логическая структура
new VStack(spacing: 20)
    .AddChild(
        new Text("Settings")
            .WithFontSize(24)
            .Bold()  // Heading
    )
    .AddChild(
        new Text("Configure your preferences")
            .WithFontSize(14)  // Subheading
    )
    .AddChild(BuildSettings());
```

## Тестирование

### Testable components

```csharp
// ✅ Хорошо: Легко тестируемый компонент
public class Counter : ZorComponent
{
    private int _count;
    
    public Counter(int initialCount = 0)
    {
        _count = initialCount;
    }
    
    public void Increment() => SetState(nameof(_count), ++_count);
    public void Decrement() => SetState(nameof(_count), --_count);
    public int GetCount() => _count;
    
    public override ZorElement Build()
    {
        return new VStack(spacing: 8)
            .AddChild(new Text($"Count: {_count}"))
            .AddChild(new Button("+", Increment))
            .AddChild(new Button("-", Decrement));
    }
}

// Тест
[TestMethod]
public void Counter_Increment_IncreasesCount()
{
    var counter = new Counter(0);
    counter.Increment();
    Assert.AreEqual(1, counter.GetCount());
}
```

### Разделение логики и UI

```csharp
// ✅ Хорошо: Бизнес-логика отделена
public class ValidationService
{
    public static bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Length > 3;
    }
}

public class LoginForm : ZorComponent
{
    private string _email = "";
    
    private bool IsValid() => ValidationService.IsValidEmail(_email);
    
    // ... UI код
}

// Легко тестировать
[TestMethod]
public void ValidationService_ValidEmail_ReturnsTrue()
{
    Assert.IsTrue(ValidationService.IsValidEmail("test@test.com"));
}
```

## Общие рекомендации

### 1. Явность лучше неявности

```csharp
// ✅ Хорошо: Явно
new Button("Submit", HandleSubmit)
    .Primary()
    .Disabled(_isSubmitting);

// ❌ Плохо: Неявно
new Button("Submit");  // Что произойдет при клике?
```

### 2. Fail Fast

```csharp
// ✅ Хорошо: Проверка на раннем этапе
public MyComponent(User user)
{
    _user = user ?? throw new ArgumentNullException(nameof(user));
}

// ❌ Плохо: Ошибка позже
public MyComponent(User user)
{
    _user = user;  // Может быть null
}
```

### 3. Документация

```csharp
/// <summary>
/// User profile card displaying avatar, name, and status.
/// </summary>
/// <param name="user">User to display.</param>
/// <param name="onEdit">Called when edit button is clicked.</param>
public class UserCard : ZorComponent
{
    private readonly User _user;
    private readonly Action? _onEdit;
    
    public UserCard(User user, Action? onEdit = null)
    {
        _user = user;
        _onEdit = onEdit;
    }
    
    // ...
}
```

### 4. Константы вместо магических чисел

```csharp
// ✅ Хорошо: Именованные константы
public static class Constants
{
    public const int MaxUsernameLength = 50;
    public const int MinPasswordLength = 8;
    public const double DefaultAnimationDuration = 300;
}

// ❌ Плохо: Магические числа
if (username.Length > 50) { }
if (password.Length < 8) { }
Task.Delay(300);
```

## Антипаттерны

### Избегайте

❌ **God Components** - Компоненты, делающие слишком много
❌ **Prop Drilling** - Передача props через множество уровней
❌ **State в неправильном месте** - Состояние не там, где оно используется
❌ **Жестко заданные значения** - Магические числа и строки
❌ **Игнорирование lifecycle** - Утечки памяти и незакрытые ресурсы
❌ **Мутация props** - Изменение переданных параметров
❌ **Глубокая вложенность** - Более 3-4 уровней вложенности

## Заключение

Следование этим практикам поможет вам создавать:
- ✅ Понятный и поддерживаемый код
- ✅ Производительные приложения
- ✅ Переиспользуемые компоненты
- ✅ Легко тестируемый код
- ✅ Доступные интерфейсы

Помните: правила созданы, чтобы их нарушать, но только с веской причиной!
