# State Management в ZorUI

Полное руководство по управлению состоянием в ZorUI приложениях.

## Содержание

- [Основы состояния](#основы-состояния)
- [Локальное состояние](#локальное-состояние)
- [Подъем состояния](#подъем-состояния)
- [Глобальное состояние](#глобальное-состояние)
- [Derived State](#derived-state)
- [Async State](#async-state)
- [Паттерны](#паттерны)

## Основы состояния

Состояние в ZorUI - это данные, которые могут изменяться со временем и влиять на отображение компонента.

### Когда использовать состояние

✅ **Используйте состояние для:**
- Пользовательский ввод (текст в поле, выбор checkbox)
- UI состояния (открыт/закрыт, loading/loaded)
- Данные с сервера
- Локальные пользовательские настройки

❌ **НЕ используйте состояние для:**
- Статических данных
- Props, переданных от родителя
- Вычисляемых значений (используйте computed properties)

## Локальное состояние

### Базовое использование

```csharp
public class Counter : ZorComponent
{
    // Приватное поле для хранения состояния
    private int _count = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 16)
            .AddChild(new Text($"Count: {_count}"))
            .AddChild(
                new Button("Increment", () => 
                {
                    // Обновляем состояние
                    SetState(nameof(_count), ++_count);
                })
            );
    }
}
```

### Множественное состояние

```csharp
public class FormComponent : ZorComponent
{
    private string _name = "";
    private string _email = "";
    private bool _agreedToTerms = false;
    private bool _isSubmitting = false;

    public override ZorElement Build()
    {
        return new VStack(spacing: 12)
            .AddChild(
                new TextField("Name")
                    .WithValue(_name)
                    .WithOnChange(value => 
                        SetState(nameof(_name), _name = value))
            )
            .AddChild(
                new TextField("Email")
                    .Email()
                    .WithValue(_email)
                    .WithOnChange(value => 
                        SetState(nameof(_email), _email = value))
            )
            .AddChild(
                new Checkbox("I agree", _agreedToTerms)
                    .WithOnChange(value => 
                        SetState(nameof(_agreedToTerms), _agreedToTerms = value))
            )
            .AddChild(
                new Button("Submit", HandleSubmit)
                    .Primary()
                    .Disabled(!IsValid() || _isSubmitting)
                    .Loading(_isSubmitting)
            );
    }

    private bool IsValid() =>
        !string.IsNullOrEmpty(_name) &&
        !string.IsNullOrEmpty(_email) &&
        _agreedToTerms;

    private async void HandleSubmit()
    {
        SetState(nameof(_isSubmitting), _isSubmitting = true);
        
        try
        {
            await SubmitForm(_name, _email);
            ToastManager.Instance.ShowSuccess("Submitted!");
        }
        catch (Exception ex)
        {
            ToastManager.Instance.ShowError(ex.Message);
        }
        finally
        {
            SetState(nameof(_isSubmitting), _isSubmitting = false);
        }
    }
}
```

### Batch обновления

```csharp
// ✅ Хорошо: Одно обновление для нескольких изменений
private void UpdateAll()
{
    SetState(() =>
    {
        _firstName = "John";
        _lastName = "Doe";
        _age = 30;
    });
    // Только один rebuild
}

// ❌ Плохо: Множественные обновления
private void UpdateAll()
{
    SetState(nameof(_firstName), _firstName = "John");  // Rebuild 1
    SetState(nameof(_lastName), _lastName = "Doe");     // Rebuild 2
    SetState(nameof(_age), _age = 30);                  // Rebuild 3
}
```

## Подъем состояния

Когда несколько компонентов нуждаются в доступе к одному и тому же состоянию, поднимите его в ближайшего общего родителя.

### Проблема: Дублирование состояния

```csharp
// ❌ Плохо: Каждый компонент имеет свое состояние
public class FilteredList : ZorComponent
{
    public override ZorElement Build()
    {
        return new VStack()
            .AddChild(new SearchBar())    // Свой _searchText
            .AddChild(new ItemList());    // Свой _searchText
    }
}
```

### Решение: Подъем состояния

```csharp
// ✅ Хорошо: Состояние в родителе
public class FilteredList : ZorComponent
{
    private string _searchText = "";
    private List<Item> _items = LoadItems();

    public override ZorElement Build()
    {
        var filteredItems = _items
            .Where(i => i.Name.Contains(_searchText, 
                   StringComparison.OrdinalIgnoreCase))
            .ToList();

        return new VStack(spacing: 16)
            .AddChild(
                new SearchBar(
                    value: _searchText,
                    onChange: text => 
                        SetState(nameof(_searchText), _searchText = text)
                )
            )
            .AddChild(new ItemList(filteredItems));
    }
}

// Дочерние компоненты принимают props
public class SearchBar : ZorComponent
{
    private readonly string _value;
    private readonly Action<string> _onChange;

    public SearchBar(string value, Action<string> onChange)
    {
        _value = value;
        _onChange = onChange;
    }

    public override ZorElement Build()
    {
        return new TextField("Search...")
            .WithValue(_value)
            .WithOnChange(_onChange);
    }
}

public class ItemList : ZorComponent
{
    private readonly List<Item> _items;

    public ItemList(List<Item> items) => _items = items;

    public override ZorElement Build()
    {
        var stack = new VStack(spacing: 8);
        foreach (var item in _items)
        {
            stack.AddChild(new ItemCard(item));
        }
        return stack;
    }
}
```

## Глобальное состояние

Для состояния, которое нужно многим компонентам в разных частях дерева.

### Service Pattern

```csharp
// Определяем сервис состояния
public class AppState
{
    private User? _currentUser;
    private Theme _theme = Theme.Light();

    public event Action? OnUserChanged;
    public event Action? OnThemeChanged;

    public User? CurrentUser
    {
        get => _currentUser;
        set
        {
            if (_currentUser != value)
            {
                _currentUser = value;
                OnUserChanged?.Invoke();
            }
        }
    }

    public Theme Theme
    {
        get => _theme;
        set
        {
            if (_theme != value)
            {
                _theme = value;
                OnThemeChanged?.Invoke();
            }
        }
    }

    public bool IsAuthenticated => _currentUser != null;
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
        
        // Получаем сервис
        _appState = Context?.GetService<AppState>();
        
        // Подписываемся на изменения
        if (_appState != null)
        {
            _appState.OnUserChanged += HandleUserChange;
            _appState.OnThemeChanged += HandleThemeChange;
        }
    }

    public override void OnUnmount()
    {
        // Отписываемся
        if (_appState != null)
        {
            _appState.OnUserChanged -= HandleUserChange;
            _appState.OnThemeChanged -= HandleThemeChange;
        }
        
        base.OnUnmount();
    }

    private void HandleUserChange()
    {
        MarkNeedsRebuild();
    }

    private void HandleThemeChange()
    {
        MarkNeedsRebuild();
    }

    public override ZorElement Build()
    {
        var user = _appState?.CurrentUser;

        return new HStack(spacing: 12)
            .AddChild(
                new Avatar()
                    .WithInitials(user?.Initials ?? "?")
            )
            .AddChild(
                new Text(user?.Name ?? "Guest")
            );
    }
}
```

### Store Pattern (Redux-style)

```csharp
// State
public record AppState(
    User? CurrentUser,
    List<Notification> Notifications,
    bool IsLoading
);

// Actions
public abstract record AppAction
{
    public record Login(User User) : AppAction;
    public record Logout : AppAction;
    public record AddNotification(Notification Notification) : AppAction;
    public record SetLoading(bool IsLoading) : AppAction;
}

// Reducer
public class AppStore
{
    private AppState _state = new(null, new(), false);
    public event Action? OnStateChanged;

    public AppState State => _state;

    public void Dispatch(AppAction action)
    {
        _state = action switch
        {
            AppAction.Login login => _state with 
            { 
                CurrentUser = login.User 
            },
            
            AppAction.Logout => _state with 
            { 
                CurrentUser = null 
            },
            
            AppAction.AddNotification add => _state with
            {
                Notifications = _state.Notifications
                    .Append(add.Notification)
                    .ToList()
            },
            
            AppAction.SetLoading loading => _state with 
            { 
                IsLoading = loading.IsLoading 
            },
            
            _ => _state
        };

        OnStateChanged?.Invoke();
    }
}

// Использование
public class LoginButton : ZorComponent
{
    private AppStore? _store;

    public override void OnMount()
    {
        base.OnMount();
        _store = Context?.GetService<AppStore>();
        if (_store != null)
        {
            _store.OnStateChanged += MarkNeedsRebuild;
        }
    }

    public override void OnUnmount()
    {
        if (_store != null)
        {
            _store.OnStateChanged -= MarkNeedsRebuild;
        }
        base.OnUnmount();
    }

    private void HandleLogin()
    {
        var user = new User { Name = "John Doe" };
        _store?.Dispatch(new AppAction.Login(user));
    }

    public override ZorElement Build()
    {
        var isAuthenticated = _store?.State.CurrentUser != null;

        return isAuthenticated
            ? new Button("Logout", () => 
                  _store?.Dispatch(new AppAction.Logout()))
            : new Button("Login", HandleLogin);
    }
}
```

## Derived State

Состояние, вычисляемое из другого состояния.

```csharp
public class TodoList : ZorComponent
{
    private List<Todo> _todos = new();
    private string _filter = "all"; // "all", "active", "completed"

    // Derived state - вычисляется при каждом render
    private List<Todo> FilteredTodos => _filter switch
    {
        "active" => _todos.Where(t => !t.IsCompleted).ToList(),
        "completed" => _todos.Where(t => t.IsCompleted).ToList(),
        _ => _todos
    };

    private int ActiveCount => _todos.Count(t => !t.IsCompleted);
    private int CompletedCount => _todos.Count(t => t.IsCompleted);

    public override ZorElement Build()
    {
        return new VStack(spacing: 16)
            .AddChild(
                new Text($"Active: {ActiveCount}, Completed: {CompletedCount}")
            )
            .AddChild(BuildFilterButtons())
            .AddChild(BuildTodoList());
    }

    private ZorElement BuildFilterButtons()
    {
        return new HStack(spacing: 8)
            .AddChild(BuildFilterButton("all", "All"))
            .AddChild(BuildFilterButton("active", "Active"))
            .AddChild(BuildFilterButton("completed", "Completed"));
    }

    private ZorElement BuildFilterButton(string filter, string label)
    {
        return new Button(label, () => 
        {
            SetState(nameof(_filter), _filter = filter);
        })
        .WithVariant(
            _filter == filter 
                ? ButtonVariant.Primary 
                : ButtonVariant.Secondary
        );
    }

    private ZorElement BuildTodoList()
    {
        var list = new VStack(spacing: 8);
        foreach (var todo in FilteredTodos)
        {
            list.AddChild(BuildTodoItem(todo));
        }
        return list;
    }
}
```

## Async State

Работа с асинхронными операциями.

### Loading States

```csharp
public class DataLoader : ZorComponent
{
    private List<Item>? _data;
    private bool _isLoading = false;
    private string? _error;

    protected override void OnMount()
    {
        base.OnMount();
        LoadData();
    }

    private async void LoadData()
    {
        SetState(nameof(_isLoading), _isLoading = true);
        SetState(nameof(_error), _error = null);

        try
        {
            _data = await ApiService.GetItemsAsync();
            SetState(() => { }); // Trigger rebuild with loaded data
        }
        catch (Exception ex)
        {
            SetState(nameof(_error), _error = ex.Message);
        }
        finally
        {
            SetState(nameof(_isLoading), _isLoading = false);
        }
    }

    public override ZorElement Build()
    {
        if (_isLoading)
        {
            return new VStack(spacing: 16)
                .AddChild(new Spinner().Large())
                .AddChild(new Text("Loading..."));
        }

        if (_error != null)
        {
            return new Alert(_error)
                .Error()
                .Dismissible()
                .WithAction(
                    new Button("Retry", LoadData)
                );
        }

        if (_data == null || _data.Count == 0)
        {
            return new Text("No data available");
        }

        return BuildDataList(_data);
    }
}
```

### Polling

```csharp
public class RealtimeData : ZorComponent
{
    private Data? _data;
    private Timer? _timer;

    protected override void OnMount()
    {
        base.OnMount();
        
        // Загружаем сразу
        LoadData();
        
        // Обновляем каждые 5 секунд
        _timer = new Timer(_ => LoadData(), null, 
                          TimeSpan.FromSeconds(5), 
                          TimeSpan.FromSeconds(5));
    }

    protected override void OnUnmount()
    {
        _timer?.Dispose();
        base.OnUnmount();
    }

    private async void LoadData()
    {
        try
        {
            _data = await ApiService.GetDataAsync();
            SetState(() => { });
        }
        catch (Exception ex)
        {
            ToastManager.Instance.ShowError(ex.Message);
        }
    }

    public override ZorElement Build()
    {
        return _data != null
            ? new Text($"Latest: {_data.Value}")
            : new Spinner();
    }
}
```

## Паттерны

### Form State Pattern

```csharp
public class FormState<T> where T : new()
{
    private T _data = new();
    private Dictionary<string, string> _errors = new();
    private bool _isDirty = false;
    private bool _isSubmitting = false;

    public T Data => _data;
    public IReadOnlyDictionary<string, string> Errors => _errors;
    public bool IsDirty => _isDirty;
    public bool IsSubmitting => _isSubmitting;
    public bool IsValid => _errors.Count == 0;

    public event Action? OnChange;

    public void SetValue<TValue>(string field, TValue value)
    {
        typeof(T).GetProperty(field)?.SetValue(_data, value);
        _isDirty = true;
        OnChange?.Invoke();
    }

    public void SetError(string field, string error)
    {
        _errors[field] = error;
        OnChange?.Invoke();
    }

    public void ClearError(string field)
    {
        _errors.Remove(field);
        OnChange?.Invoke();
    }

    public void SetSubmitting(bool submitting)
    {
        _isSubmitting = submitting;
        OnChange?.Invoke();
    }

    public void Reset()
    {
        _data = new();
        _errors.Clear();
        _isDirty = false;
        _isSubmitting = false;
        OnChange?.Invoke();
    }
}

// Использование
public class RegistrationForm : ZorComponent
{
    private FormState<Registration> _form = new();

    protected override void OnMount()
    {
        base.OnMount();
        _form.OnChange += MarkNeedsRebuild;
    }

    protected override void OnUnmount()
    {
        _form.OnChange -= MarkNeedsRebuild;
        base.OnUnmount();
    }

    public override ZorElement Build()
    {
        return new VStack(spacing: 16)
            .AddChild(
                new TextField("Email")
                    .WithValue(_form.Data.Email)
                    .WithError(_form.Errors.GetValueOrDefault("Email"))
                    .WithOnChange(email => HandleEmailChange(email))
            )
            .AddChild(
                new Button("Submit", HandleSubmit)
                    .Disabled(!_form.IsValid || _form.IsSubmitting)
                    .Loading(_form.IsSubmitting)
            );
    }

    private void HandleEmailChange(string email)
    {
        _form.SetValue(nameof(Registration.Email), email);
        
        if (string.IsNullOrEmpty(email))
            _form.SetError(nameof(Registration.Email), "Required");
        else if (!IsValidEmail(email))
            _form.SetError(nameof(Registration.Email), "Invalid email");
        else
            _form.ClearError(nameof(Registration.Email));
    }
}
```

## Best Practices

1. **Минимизируйте состояние** - Храните только необходимое
2. **Используйте derived state** - Вычисляйте вместо хранения
3. **Поднимайте состояние** - Когда несколько компонентов нуждаются в нем
4. **Batch обновления** - Группируйте изменения состояния
5. **Отписывайтесь от событий** - В `OnUnmount` для избежания утечек памяти
6. **Иммутабельность** - Создавайте новые объекты вместо мутации
7. **Типобезопасность** - Используйте сильные типы для состояния
