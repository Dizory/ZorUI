# Core Concepts

Понимание основных концепций ZorUI поможет вам эффективно использовать фреймворк.

## Архитектура фреймворка

```
┌─────────────────────────────────────┐
│      ZorUI Application              │
│  ┌───────────────────────────────┐  │
│  │    Component Tree             │  │
│  │  ┌─────────────────────────┐  │  │
│  │  │  ZorComponent (Root)    │  │  │
│  │  │  └─ Build() → Elements  │  │  │
│  │  │     ├─ VStack           │  │  │
│  │  │     │  ├─ Text          │  │  │
│  │  │     │  └─ Button        │  │  │
│  │  │     └─ HStack           │  │  │
│  │  └─────────────────────────┘  │  │
│  └───────────────────────────────┘  │
│                                      │
│  ┌───────────────────────────────┐  │
│  │  State Management             │  │
│  │  - Component State            │  │
│  │  - Rebuild Scheduler          │  │
│  │  - Reactive Updates           │  │
│  └───────────────────────────────┘  │
│                                      │
│  ┌───────────────────────────────┐  │
│  │  Rendering Pipeline           │  │
│  │  - Measure → Layout → Paint   │  │
│  └───────────────────────────────┘  │
└─────────────────────────────────────┘
```

## 1. Elements (Элементы)

### ZorElement

Базовый класс для всех UI элементов. Представляет узел в дереве UI.

```csharp
public abstract class ZorElement
{
    public string Id { get; }
    public ZorElement? Parent { get; }
    public List<ZorElement> Children { get; }
    public BuildContext? Context { get; }
    
    // Lifecycle methods
    public virtual void OnMount();
    public virtual void OnUnmount();
    public virtual void OnUpdate();
    
    // Tree manipulation
    public void AddChild(ZorElement child);
    public void RemoveChild(ZorElement child);
}
```

### Типы элементов

#### Примитивы
```csharp
// Текст
new Text("Hello")

// Изображение
new Image("logo.png")

// Иконка
new Icon("home")
```

#### Контейнеры
```csharp
// Базовый контейнер
new Container()

// Layout контейнеры
new VStack(spacing: 8)
new HStack(spacing: 8)
new ZStack()
```

#### Интерактивные
```csharp
// Кнопка
new Button("Click me")

// Текстовое поле
new TextField("Placeholder")
```

## 2. Components (Компоненты)

### ZorComponent

Компоненты с состоянием, которые могут перестраиваться при изменении данных.

```csharp
public abstract class ZorComponent : ZorElement
{
    // Метод построения UI
    public abstract ZorElement Build();
    
    // Управление состоянием
    protected T? GetState<T>(string key, T? defaultValue = default);
    protected void SetState<T>(string key, T value);
    protected void SetState(Action updateAction);
    
    // Lifecycle
    public override void OnMount();
    public override void OnUnmount();
    protected virtual void BeforeBuild();
    protected virtual void AfterBuild();
}
```

### Жизненный цикл компонента

```
     ┌─────────┐
     │  Create │
     └────┬────┘
          │
          ▼
     ┌─────────┐
     │  Mount  │◄──────────┐
     └────┬────┘           │
          │                │
          ▼                │
    ┌──────────┐           │
    │  Build   │           │
    └────┬─────┘           │
         │                 │
         ▼                 │
    ┌──────────┐           │
    │ Rendered │           │
    └────┬─────┘           │
         │                 │
         ▼                 │
    ┌──────────┐           │
    │  State   │           │
    │  Change  │───────────┘
    └────┬─────┘
         │
         ▼
    ┌──────────┐
    │ Unmount  │
    └──────────┘
```

### Пример компонента

```csharp
public class UserProfile : ZorComponent
{
    private User _user;
    private bool _isEditing = false;

    public UserProfile(User user)
    {
        _user = user;
    }

    protected override void OnMount()
    {
        base.OnMount();
        // Загрузка данных
        LoadUserData();
    }

    protected override void BeforeBuild()
    {
        // Подготовка перед построением
        ValidateData();
    }

    public override ZorElement Build()
    {
        return _isEditing 
            ? BuildEditMode() 
            : BuildViewMode();
    }

    protected override void AfterBuild()
    {
        // Действия после построения
        LogRenderTime();
    }

    protected override void OnUnmount()
    {
        // Очистка ресурсов
        SaveChanges();
        base.OnUnmount();
    }

    private ZorElement BuildViewMode()
    {
        return new VStack(spacing: 16)
            .AddChild(new Text(_user.Name).Bold())
            .AddChild(new Text(_user.Email))
            .AddChild(
                new Button("Edit", () => 
                {
                    SetState(nameof(_isEditing), _isEditing = true);
                })
            );
    }

    private ZorElement BuildEditMode()
    {
        // ... реализация режима редактирования
        return new VStack();
    }
}
```

## 3. State Management

### Локальное состояние компонента

```csharp
public class Counter : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 8)
            .AddChild(new Text($"Count: {_count}"))
            .AddChild(
                new Button("Increment", () => 
                {
                    SetState(nameof(_count), ++_count);
                })
            );
    }
}
```

### Сложное состояние

```csharp
public class Form : ZorComponent
{
    private FormData _data = new FormData();
    private Dictionary<string, string> _errors = new();

    public override ZorElement Build()
    {
        return new VStack(spacing: 12)
            .AddChild(
                new TextField("Email")
                    .WithValue(_data.Email)
                    .WithOnChange(email => 
                    {
                        _data.Email = email;
                        ValidateEmail(email);
                        SetState(() => { }); // Trigger rebuild
                    })
                    .WithError(_errors.GetValueOrDefault("email"))
            )
            .AddChild(
                new Button("Submit", HandleSubmit)
                    .Disabled(!IsFormValid())
            );
    }

    private void ValidateEmail(string email)
    {
        if (!IsValidEmail(email))
        {
            _errors["email"] = "Invalid email format";
        }
        else
        {
            _errors.Remove("email");
        }
    }

    private bool IsFormValid() => 
        !string.IsNullOrEmpty(_data.Email) && 
        _errors.Count == 0;
}
```

### Глобальное состояние (через Services)

```csharp
// Определяем сервис состояния
public class AppState
{
    public User? CurrentUser { get; set; }
    public Theme Theme { get; set; } = Theme.Light();
    public event Action? OnStateChanged;

    public void UpdateUser(User user)
    {
        CurrentUser = user;
        OnStateChanged?.Invoke();
    }
}

// Регистрируем в приложении
var app = new ZorApplication();
var appState = new AppState();
app.RegisterService(appState);

// Используем в компонентах
public class UserMenu : ZorComponent
{
    private AppState? _appState;

    public override void OnMount()
    {
        base.OnMount();
        _appState = Context?.GetService<AppState>();
        
        if (_appState != null)
        {
            _appState.OnStateChanged += HandleStateChange;
        }
    }

    private void HandleStateChange()
    {
        MarkNeedsRebuild();
    }

    public override ZorElement Build()
    {
        var user = _appState?.CurrentUser;
        
        return new HStack(spacing: 8)
            .AddChild(new Text(user?.Name ?? "Guest"))
            .AddChild(new Avatar().WithInitials(user?.Initials ?? "?"));
    }
}
```

## 4. Rebuild Scheduler

Система пакетного обновления для эффективности.

```csharp
// Внутренняя работа
RebuildScheduler:
  ┌─────────────────┐
  │ State Changes   │
  │  - Component A  │
  │  - Component B  │
  │  - Component C  │
  └────────┬────────┘
           │
           ▼
  ┌─────────────────┐
  │  Batch Updates  │ ◄─── Delay (1ms)
  └────────┬────────┘
           │
           ▼
  ┌─────────────────┐
  │  Rebuild Once   │
  │  - Build()      │
  │  - Render()     │
  └─────────────────┘
```

### Ручное управление

```csharp
public class MyComponent : ZorComponent
{
    public override ZorElement Build()
    {
        return new Button("Update", () => 
        {
            // Несколько изменений состояния
            UpdateDataSource();
            ProcessFilters();
            RecalculateResults();
            
            // Триггерим один rebuild
            MarkNeedsRebuild();
            
            // Или принудительно сразу
            Context?.Scheduler.FlushRebuilds();
        });
    }
}
```

## 5. Build Context

Контекст предоставляет доступ к сервисам фреймворка.

```csharp
public class BuildContext
{
    // Scheduler для rebuilds
    public RebuildScheduler Scheduler { get; }
    
    // Текущая тема
    public object? Theme { get; set; }
    
    // Регистрация сервисов
    public void RegisterService<T>(T service) where T : class;
    
    // Получение сервисов
    public T? GetService<T>() where T : class;
}
```

### Использование контекста

```csharp
public class ThemedButton : Button
{
    public override void OnMount()
    {
        base.OnMount();
        
        // Получаем тему из контекста
        if (Context?.Theme is Theme theme)
        {
            ApplyTheme(theme);
        }
        
        // Получаем сервис
        var analytics = Context?.GetService<AnalyticsService>();
        analytics?.TrackEvent("button_mounted");
    }
}
```

## 6. Props Pattern

Передача данных между компонентами.

```csharp
// Компонент с пропсами
public class UserCard : ZorComponent
{
    private readonly User _user;
    private readonly Action<User>? _onEdit;

    public UserCard(User user, Action<User>? onEdit = null)
    {
        _user = user;
        _onEdit = onEdit;
    }

    public override ZorElement Build()
    {
        return new Card()
            .WithHeader(new Text(_user.Name).Bold())
            .WithContent(new Text(_user.Email))
            .WithFooter(
                new Button("Edit", () => _onEdit?.Invoke(_user))
            );
    }
}

// Использование
public class UserList : ZorComponent
{
    private List<User> _users = LoadUsers();

    public override ZorElement Build()
    {
        var list = new VStack(spacing: 12);
        
        foreach (var user in _users)
        {
            list.AddChild(
                new UserCard(user, HandleEditUser)
            );
        }
        
        return list;
    }

    private void HandleEditUser(User user)
    {
        // Обработка редактирования
    }
}
```

## 7. Composition

Композиция компонентов для переиспользования.

```csharp
// Базовый компонент
public class FormField : ZorComponent
{
    protected string Label { get; }
    protected string? Error { get; }
    protected bool Required { get; }

    public FormField(string label, bool required = false, string? error = null)
    {
        Label = label;
        Required = required;
        Error = error;
    }

    public override ZorElement Build()
    {
        return new VStack(spacing: 4)
            .AddChild(BuildLabel())
            .AddChild(BuildInput())
            .AddChild(BuildError());
    }

    protected virtual ZorElement BuildLabel()
    {
        var label = new Text(Label);
        if (Required)
            label = label.WithColor(Color.Red);
        return label;
    }

    protected virtual ZorElement BuildInput()
    {
        return new TextField();
    }

    protected virtual ZorElement BuildError()
    {
        return string.IsNullOrEmpty(Error)
            ? new Text("")
            : new Text(Error).WithColor(Color.Red).WithFontSize(12);
    }
}

// Специализированный компонент
public class EmailField : FormField
{
    private string _value = "";
    private Action<string>? _onChange;

    public EmailField(string label, string value, Action<string>? onChange = null)
        : base(label, required: true)
    {
        _value = value;
        _onChange = onChange;
    }

    protected override ZorElement BuildInput()
    {
        return new TextField("email@example.com")
            .Email()
            .WithValue(_value)
            .WithOnChange(value => _onChange?.Invoke(value));
    }
}
```

## Следующие шаги

- [Components Guide](components/) - Изучите все компоненты
- [Styling](styling.md) - Кастомизация внешнего вида
- [Best Practices](best-practices.md) - Рекомендуемые паттерны
