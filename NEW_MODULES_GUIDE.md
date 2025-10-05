# 📚 ZorUI v1.5 - Руководство по новым модулям

**Полное руководство по всем новым возможностям!**

---

## 🎬 1. Animations (ZorUI.Animations)

### Установка

```bash
dotnet add reference ../src/ZorUI.Animations/ZorUI.Animations.csproj
```

### Использование

```csharp
using ZorUI.Animations;

// Простая анимация
new Card("Hello")
    .Animate(Animation.FadeIn());

// С параметрами
new Button("Click")
    .Animate(
        Animation.SlideIn(SlideDirection.Left)
            .WithDuration(300)
            .WithDelay(100)
            .WithEasing(Easing.EaseOutCubic)
    );

// Несколько анимаций
new Container()
    .Animate(
        Animation.FadeIn(),
        Animation.Scale(0.8, 1.0)
    );

// Повторяющаяся
new Spinner()
    .Animate(
        Animation.Rotate(0, 360)
            .WithRepeat(0, autoReverse: false) // 0 = infinite
    );
```

### Доступные анимации:

- `FadeIn()` / `FadeOut()`
- `SlideIn(direction)` / `SlideOut(direction)`
- `Scale(from, to)`
- `Rotate(from, to)`
- `Spring()` - Bounce effect

### Easing функции:

- Linear
- EaseIn, EaseOut, EaseInOut
- EaseInCubic, EaseOutCubic, EaseInOutCubic
- EaseInQuad, EaseOutQuad, EaseInOutQuad
- Spring, Bounce, Elastic
- И еще 10+

---

## 🖱️ 2. Drag & Drop (ZorUI.DragDrop)

### Установка

```bash
dotnet add reference ../src/ZorUI.DragDrop/ZorUI.DragDrop.csproj
```

### Draggable элемент

```csharp
using ZorUI.DragDrop;

new Draggable(
    child: new Card("Перетащи меня"),
    onDragStart: e => Console.WriteLine("Started"),
    onDrag: e => Console.WriteLine($"Position: {e.Position}"),
    onDragEnd: e => Console.WriteLine($"Dropped at {e.Position}")
)
.WithData(myDataObject);
```

### Drop Zone

```csharp
new DropZone(
    child: new Container("Зона для сброса"),
    onDrop: e => HandleDrop(e.Data),
    onDragEnter: e => HighlightZone(),
    onDragLeave: e => UnhighlightZone()
)
.WithAcceptCondition(data => data is Task);
```

### Пример: Сортируемый список

```csharp
var items = new List<string> { "Item 1", "Item 2", "Item 3" };

new VStack(spacing: 8)
    .AddChildren(items.Select((item, index) =>
        new Draggable(
            new Card(item),
            onDragEnd: e => ReorderItem(index, e)
        )
        .WithData(new { item, index })
    ));
```

---

## 📊 3. DataGrid (ZorUI.DataGrid)

### Установка

```bash
dotnet add reference ../src/ZorUI.DataGrid/ZorUI.DataGrid.csproj
```

### Базовое использование

```csharp
using ZorUI.DataGrid;

var users = GetUsers();

new DataGrid<User>()
    .WithItems(users)
    .AddColumn("Name", u => u.Name)
    .AddColumn("Email", u => u.Email)
    .AddColumn("Role", u => u.Role);
```

### С сортировкой и фильтрацией

```csharp
new DataGrid<User>()
    .WithItems(users)
    .AddColumn("Name", u => u.Name)
    .AddColumn("Email", u => u.Email)
    .AddColumn("Status", u => u.Status)
    .WithSorting()          // Включить сортировку
    .WithFiltering()        // Включить фильтрацию
    .WithPagination(20);    // 20 элементов на странице
```

### С кастомными ячейками

```csharp
new DataGrid<User>()
    .WithItems(users)
    .AddColumn("Avatar", u => u.AvatarUrl,
        cellTemplate: u => new Avatar(u.AvatarUrl).Small())
    .AddColumn("Name", u => u.FullName)
    .AddColumn("Status", u => u.Status,
        cellTemplate: u => new Badge(u.Status.ToString())
            .If(u.IsActive, b => b.Success(), b => b.Warning()))
    .WithRowClick(user => ShowUserDetails(user));
```

### С выбором строк

```csharp
var grid = new DataGrid<User>()
    .WithItems(users)
    .AddColumn("Name", u => u.Name)
    .WithSelection()
    .WithSelectionChange(selected => 
        Console.WriteLine($"Selected: {selected.Count}"));

// Получить выбранные
var selected = grid.GetSelectedRows();
```

---

## 🗺️ 4. Routing (ZorUI.Routing)

### Установка

```bash
dotnet add reference ../src/ZorUI.Routing/ZorUI.Routing.csproj
```

### Создание роутера

```csharp
using ZorUI.Routing;

var router = new Router()
    .AddRoute("/", () => new HomePage())
    .AddRoute("/about", () => new AboutPage())
    .AddRoute("/contact", () => new ContactPage());
```

### С параметрами

```csharp
router.AddRoute("/users/:id", parameters => 
{
    var userId = parameters["id"];
    return new UserPage(userId);
});

router.AddRoute("/posts/:category/:id", parameters => 
{
    var category = parameters["category"];
    var postId = parameters["id"];
    return new PostPage(category, postId);
});
```

### Навигация

```csharp
// Navigate to route
router.Navigate("/users/123");

// Back/Forward
router.GoBack();
router.GoForward();

// Check navigation state
if (router.CanGoBack())
    router.GoBack();
```

### RouterOutlet

```csharp
public class App : ZorComponent
{
    private Router _router = CreateRouter();

    public override ZorElement Build()
    {
        return new VStack(spacing: 0)
            .AddChild(BuildNavigation())
            .AddChild(new RouterOutlet(_router));
    }

    ZorElement BuildNavigation()
    {
        return new HStack(spacing: 16)
            .AddChild(NavButton("/", "Home"))
            .AddChild(NavButton("/about", "About"))
            .AddChild(NavButton("/contact", "Contact"));
    }

    ZorElement NavButton(string path, string label)
    {
        return new Button(label, () => _router.Navigate(path))
            .If(_router.CurrentPath == path, 
                b => b.Primary(), 
                b => b.Ghost());
    }
}
```

---

## ⌨️ 5. Keyboard Shortcuts

### Установка

Уже в ZorUI.Core!

### Использование

```csharp
using ZorUI.Core.Input;

var shortcuts = new KeyboardShortcutManager();

// Глобальные shortcuts
shortcuts.Register("Ctrl+S", Save, "Сохранить");
shortcuts.Register("Ctrl+O", Open, "Открыть");
shortcuts.Register("Ctrl+P", Print, "Печать");
shortcuts.Register("Ctrl+W", Close, "Закрыть");

// Комбинации (chord)
shortcuts.Register("Ctrl+K Ctrl+S", ShowKeyboardShortcuts);

// Context-specific
shortcuts.RegisterInContext("editor", "Ctrl+B", Bold);
shortcuts.RegisterInContext("editor", "Ctrl+I", Italic);

// Переключение контекста
shortcuts.SetContext("editor");

// Обработка события
window.OnKeyDown += (key, modifiers) => 
    shortcuts.HandleKeyEvent(key, modifiers);
```

---

## 🛠️ 6. DevTools

### Установка

```bash
dotnet add reference ../src/ZorUI.DevTools/ZorUI.DevTools.csproj
```

### Использование

```csharp
using ZorUI.DevTools;

// В приложении
var shortcuts = new KeyboardShortcutManager();
shortcuts.Register("Ctrl+Shift+D", () => 
    DevTools.Toggle(app.RootElement));

// Или напрямую
DevTools.Open(rootElement);
DevTools.Close();
```

### Панели DevTools:

- **Inspector** - Дерево компонентов с деталями
- **State** - Просмотр состояния
- **Performance** - FPS, render time, memory
- **Console** - Логи приложения

---

## 📅 7. DatePicker & ColorPicker

### DatePicker

```csharp
var birthDate = DateTime.Now;

new DatePicker()
    .WithValue(birthDate)
    .WithOnChange(date => 
        SetState(nameof(birthDate), birthDate = date))
    .WithPlaceholder("Выберите дату")
    .WithFormat("dd.MM.yyyy")
    .WithMinDate(DateTime.Now.AddYears(-100))
    .WithMaxDate(DateTime.Now);
```

### ColorPicker

```csharp
var color = Color.Blue;

new ColorPicker()
    .WithValue(color)
    .WithOnChange(c => 
        SetState(nameof(color), color = c));
```

---

## ⚡ 8. Performance Optimization

### Virtual Scrolling

```csharp
using ZorUI.Core.Performance;

var items = Enumerable.Range(1, 10000)
    .Select(i => new Item { Name = $"Item {i}" })
    .ToList();

new VirtualScrollView<Item>()
    .WithItems(items)
    .WithItemTemplate(item => 
        new Text(item.Name))
    .WithItemHeight(40)
    .WithContainerHeight(600);
```

### Memoization

```csharp
// Вариант 1: MemoizedComponent
public class MyComponent : MemoizedComponent
{
    private string _prop1;
    private int _prop2;

    protected override ZorElement BuildInternal()
    {
        // Вызывается только когда зависимости меняются
        return ComputeExpensiveUI();
    }

    protected override object[] GetDependencies()
    {
        return new object[] { _prop1, _prop2 };
    }
}

// Вариант 2: Memo helper
var memoized = MemoizationHelper.Memoize(
    () => new ExpensiveWidget(),
    dependency1, dependency2
);
```

---

## ✅ 9. Form Validation

### Установка

Уже в ZorUI.Core!

### Использование

```csharp
using ZorUI.Core.Validation;

var validator = new FormValidator()
    .AddRule("name", Rules.Required())
    .AddRule("name", Rules.MinLength(3))
    .AddRule("name", Rules.MaxLength(50))
    .AddRule("email", Rules.Email())
    .AddRule("age", Rules.Range(18, 100))
    .AddRule("phone", Rules.Pattern(@"^\d{10}$"));

// Валидация поля
if (!validator.ValidateField("email", email))
{
    var error = validator.GetError("email");
    ShowError(error);
}

// Валидация всей формы
var formData = new Dictionary<string, object?>
{
    ["name"] = name,
    ["email"] = email,
    ["age"] = age
};

if (validator.ValidateAll(formData))
{
    Submit();
}
else
{
    foreach (var (field, error) in validator.GetAllErrors())
    {
        ShowFieldError(field, error);
    }
}
```

### Кастомные правила

```csharp
public class CustomRule : ValidationRule
{
    public override ValidationResult Validate(object? value)
    {
        // Ваша логика
        return isValid 
            ? ValidationResult.Success()
            : ValidationResult.Error("Custom error");
    }
}

validator.AddRule("field", new CustomRule());
```

---

## 🌐 10. HTTP Client

### Установка

Уже в ZorUI.Core!

### Использование

```csharp
using ZorUI.Core.Http;

var api = new ApiClient("https://api.example.com")
    .WithAuth("your-token")
    .WithTimeout(TimeSpan.FromSeconds(30));

// GET
var users = await api.Get<List<User>>("/users");
var user = await api.Get<User>("/users/123");

// POST
var newUser = new User { Name = "John", Email = "john@example.com" };
await api.Post("/users", newUser);

// PUT
await api.Put($"/users/{user.Id}", updatedUser);

// DELETE
await api.Delete($"/users/{user.Id}");
```

### В компоненте

```csharp
public class UsersPage : ZorComponent
{
    private AsyncState<List<User>> _usersState = new();
    private ApiClient _api = new("https://api.example.com");

    public override void OnMount()
    {
        base.OnMount();
        LoadUsers();
    }

    private async void LoadUsers()
    {
        await AsyncHelper.UseAsync(
            async () => await _api.Get<List<User>>("/users"),
            state => SetState(nameof(_usersState), _usersState = state)
        );
    }

    public override ZorElement Build()
    {
        if (_usersState.IsLoading)
            return new Spinner();

        if (_usersState.HasError)
            return new Text($"Error: {_usersState.Error}");

        return new DataGrid<User>()
            .WithItems(_usersState.Data ?? new());
    }
}
```

---

## 💾 11. Local Storage

### Установка

Уже в ZorUI.Core!

### Использование

```csharp
using ZorUI.Core.Storage;

var storage = new LocalStorage();

// Сохранить
storage.Save("theme", "dark");
storage.Save("settings", new Settings { /* ... */ });
storage.Save("user", currentUser);

// Загрузить
var theme = storage.Load<string>("theme", "light");
var settings = storage.Load<Settings>("settings");

// Проверить
if (storage.Has("user"))
{
    var user = storage.Load<User>("user");
}

// Удалить
storage.Remove("theme");

// Очистить всё
storage.Clear();

// Все ключи
var keys = storage.GetAllKeys();
```

### В компоненте

```csharp
public class App : ZorComponent
{
    private LocalStorage _storage = new();
    private bool _darkMode;

    public override void OnMount()
    {
        base.OnMount();
        // Загрузить сохраненную тему
        _darkMode = _storage.Load<bool>("darkMode", false);
    }

    private void ToggleTheme(bool dark)
    {
        SetState(nameof(_darkMode), _darkMode = dark);
        _storage.Save("darkMode", dark);
    }
}
```

---

## 🧪 12. Testing

### Запуск тестов

```bash
dotnet test
```

### Написание тестов

```csharp
using Xunit;
using FluentAssertions;
using ZorUI.Components.Primitives;

public class MyComponentTests
{
    [Fact]
    public void Component_Build_ReturnsElement()
    {
        // Arrange
        var component = new MyComponent();

        // Act
        var result = component.Build();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<VStack>();
    }

    [Fact]
    public void Component_SetState_TriggersRebuild()
    {
        // Arrange
        var component = new MyComponent();

        // Act
        component.SetState("key", "value");

        // Assert
        component.NeedsRebuild.Should().BeTrue();
    }
}
```

---

## 🎯 КОМПЛЕКСНЫЙ ПРИМЕР

### Полноценное приложение:

```csharp
using ZorUI.Core;
using ZorUI.Core.Input;
using ZorUI.Core.Storage;
using ZorUI.Core.Validation;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Components.Forms;
using ZorUI.Animations;
using ZorUI.DragDrop;
using ZorUI.DataGrid;
using ZorUI.Routing;
using ZorUI.DevTools;

public class AdvancedApp : ZorComponent
{
    private Router _router;
    private LocalStorage _storage = new();
    private KeyboardShortcutManager _shortcuts = new();
    private bool _darkMode;

    public AdvancedApp()
    {
        // Setup routing
        _router = new Router()
            .AddRoute("/", () => new DashboardPage())
            .AddRoute("/users", () => new UsersPage())
            .AddRoute("/tasks", () => new TasksPage());

        // Setup shortcuts
        _shortcuts.Register("Ctrl+D", () => DevTools.Toggle(Build()));
        _shortcuts.Register("Ctrl+1", () => _router.Navigate("/"));
        _shortcuts.Register("Ctrl+2", () => _router.Navigate("/users"));

        // Load settings
        _darkMode = _storage.Load("darkMode", false);
    }

    public override ZorElement Build()
    {
        var theme = _darkMode ? Theme.Dark() : Theme.Light();

        return new VStack(spacing: 0)
            .WithBackground(theme.Colors.Background)
            .AddChild(BuildHeader(theme))
            .AddChild(
                new HStack(spacing: 0)
                    .AddChild(BuildSidebar(theme))
                    .AddChild(new RouterOutlet(_router))
            )
            .Animate(Animation.FadeIn().WithDuration(300));
    }
}

public class DashboardPage : ZorComponent
{
    private List<User> _users = new();
    private ApiClient _api = new("https://api.example.com");

    public override async void OnMount()
    {
        base.OnMount();
        _users = await _api.Get<List<User>>("/users") ?? new();
        MarkNeedsRebuild();
    }

    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .WithPadding(EdgeInsets.All(24))
            .AddChild(
                new Text("Dashboard")
                    .WithFontSize(32)
                    .Bold()
                    .Animate(Animation.SlideIn(SlideDirection.Up))
            )
            .AddChild(
                new DataGrid<User>()
                    .WithItems(_users)
                    .AddColumn("Name", u => u.Name)
                    .AddColumn("Email", u => u.Email)
                    .AddColumn("Status", u => u.Status,
                        cellTemplate: u => new Badge(u.Status))
                    .WithSorting()
                    .WithPagination(20)
                    .Animate(Animation.FadeIn().WithDelay(200))
            );
    }
}
```

---

## 📚 Дополнительная документация

- [NEW_FEATURES_SUMMARY.md](NEW_FEATURES_SUMMARY.md) - Обзор всех новых фич
- [samples/AdvancedDemo](samples/AdvancedDemo/) - Рабочие примеры
- [CHANGELOG.md](CHANGELOG.md) - Детальный список изменений

---

<div align="center">

# 🎉 Готово!

**Все модули задокументированы и готовы к использованию!**

**[⬅ Главная](README.md)** • **[🚀 Демо](samples/AdvancedDemo/)** • **[🧪 Тесты](tests/)**

</div>
