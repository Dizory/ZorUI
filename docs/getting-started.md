# Getting Started with ZorUI

Добро пожаловать в ZorUI! Это руководство поможет вам начать работу с фреймворком.

## Установка

### Требования

- .NET 8.0 SDK или выше
- Visual Studio 2022, VS Code, или Rider
- Базовые знания C#

### Установка через NuGet

```bash
# Core библиотека (обязательно)
dotnet add package ZorUI.Core

# Библиотека компонентов (обязательно)
dotnet add package ZorUI.Components

# Система стилей (обязательно)
dotnet add package ZorUI.Styling

# Рендеринг (опционально, зависит от платформы)
dotnet add package ZorUI.Rendering
```

### Создание нового проекта

```bash
# Создаем новый консольный проект
dotnet new console -n MyZorUIApp
cd MyZorUIApp

# Добавляем пакеты ZorUI
dotnet add package ZorUI.Core
dotnet add package ZorUI.Components
dotnet add package ZorUI.Styling
dotnet add package ZorUI.Rendering
```

## Первое приложение

### Hello World

Создайте файл `Program.cs`:

```csharp
using ZorUI.Core;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Rendering;

// Создаем приложение
var app = new ZorApplication();

// Запускаем с простым компонентом
app.Run(new HelloWorld());

// Определяем компонент
public class HelloWorld : ZorComponent
{
    public override ZorElement Build()
    {
        var renderer = new ConsoleRenderer();
        
        var ui = new VStack(spacing: 16)
            .AddChild(
                new Text("Hello, ZorUI!")
                    .WithFontSize(32)
                    .Bold()
            )
            .AddChild(
                new Text("Welcome to the future of .NET UI!")
                    .WithFontSize(16)
            );
        
        renderer.Render(ui);
        return ui;
    }
}
```

Запустите приложение:

```bash
dotnet run
```

## Основные концепции

### 1. Elements (Элементы)

`ZorElement` - базовый класс для всех UI элементов:

```csharp
// Простой элемент
var text = new Text("Hello!");

// Элемент с модификаторами
var styledText = new Text("Styled Text")
    .WithFontSize(20)
    .Bold()
    .WithColor(Color.Blue);
```

### 2. Components (Компоненты)

`ZorComponent` - базовый класс для компонентов с состоянием:

```csharp
public class Counter : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        return new VStack(spacing: 16)
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

### 3. Layouts (Макеты)

Организация элементов в пространстве:

```csharp
// Вертикальный стек
new VStack(spacing: 8,
    new Text("Item 1"),
    new Text("Item 2"),
    new Text("Item 3")
)

// Горизонтальный стек
new HStack(spacing: 16,
    new Button("Cancel"),
    new Button("OK").Primary()
)

// Z-стек (наложение)
new ZStack(
    new Image("background.jpg"),
    new Text("Overlay")
)
```

### 4. State Management (Управление состоянием)

```csharp
public class MyComponent : ZorComponent
{
    // Приватное поле для хранения состояния
    private string _name = "";
    private bool _isEnabled = false;

    public override ZorElement Build()
    {
        return new VStack(spacing: 8)
            .AddChild(
                new TextField("Name")
                    .WithValue(_name)
                    .WithOnChange(value => 
                    {
                        // Обновляем состояние
                        SetState(nameof(_name), _name = value);
                    })
            )
            .AddChild(
                new Switch("Enable", _isEnabled)
                    .WithOnChange(value =>
                    {
                        SetState(nameof(_isEnabled), _isEnabled = value);
                    })
            );
    }
}
```

### 5. Fluent API

ZorUI использует Fluent API для читаемости:

```csharp
new Button("Submit")
    .Primary()                          // Устанавливаем вариант
    .Large()                            // Размер
    .WithFullWidth()                    // На всю ширину
    .WithLeadingIcon(new Icon("check")) // Иконка
    .Disabled(false)                    // Состояние
    .WithOnClick(HandleSubmit)          // Обработчик
```

## Пример: Todo List

Создадим простое приложение для управления задачами:

```csharp
public class TodoApp : ZorComponent
{
    private List<TodoItem> _todos = new();
    private string _newTodoText = "";

    public override ZorElement Build()
    {
        var renderer = new ConsoleRenderer();
        
        var ui = new VStack(spacing: 20)
            .WithPadding(24)
            .AddChild(BuildHeader())
            .AddChild(BuildInput())
            .AddChild(BuildTodoList());
        
        renderer.Render(ui);
        return ui;
    }

    private ZorElement BuildHeader()
    {
        return new VStack(spacing: 8)
            .AddChild(
                new Text("My Todo List")
                    .WithFontSize(32)
                    .Bold()
            )
            .AddChild(
                new Text($"{_todos.Count(t => !t.IsCompleted)} tasks remaining")
                    .WithFontSize(14)
            );
    }

    private ZorElement BuildInput()
    {
        return new HStack(spacing: 8)
            .AddChild(
                new TextField("Enter new task...")
                    .WithValue(_newTodoText)
                    .WithOnChange(text => 
                    {
                        SetState(nameof(_newTodoText), _newTodoText = text);
                    })
            )
            .AddChild(
                new Button("Add", AddTodo)
                    .Primary()
                    .Disabled(string.IsNullOrWhiteSpace(_newTodoText))
            );
    }

    private ZorElement BuildTodoList()
    {
        var list = new VStack(spacing: 8);
        
        foreach (var todo in _todos)
        {
            list.AddChild(BuildTodoItem(todo));
        }
        
        if (_todos.Count == 0)
        {
            list.AddChild(
                new Text("No tasks yet. Add one above!")
                    .WithFontSize(14)
            );
        }
        
        return list;
    }

    private ZorElement BuildTodoItem(TodoItem todo)
    {
        return new HStack(spacing: 12)
            .AddChild(
                new Checkbox("", todo.IsCompleted)
                    .WithOnChange(completed => ToggleTodo(todo))
            )
            .AddChild(
                new Text(todo.Text)
                    .WithFontSize(16)
            )
            .AddChild(new Spacer())
            .AddChild(
                new Button("Delete", () => DeleteTodo(todo))
                    .Destructive()
                    .Small()
            );
    }

    private void AddTodo()
    {
        if (!string.IsNullOrWhiteSpace(_newTodoText))
        {
            _todos.Add(new TodoItem 
            { 
                Text = _newTodoText, 
                IsCompleted = false 
            });
            _newTodoText = "";
            SetState(() => { });
        }
    }

    private void ToggleTodo(TodoItem todo)
    {
        todo.IsCompleted = !todo.IsCompleted;
        SetState(() => { });
    }

    private void DeleteTodo(TodoItem todo)
    {
        _todos.Remove(todo);
        SetState(() => { });
    }
}

public class TodoItem
{
    public string Text { get; set; } = "";
    public bool IsCompleted { get; set; }
}
```

## Следующие шаги

Теперь, когда вы создали свое первое приложение, изучите:

1. **[Core Concepts](core-concepts.md)** - Глубокое понимание архитектуры
2. **[Components Guide](components/)** - Полное руководство по компонентам
3. **[Styling](styling.md)** - Кастомизация внешнего вида
4. **[State Management](state-management.md)** - Продвинутые паттерны состояния
5. **[Best Practices](best-practices.md)** - Рекомендации и паттерны

## Дополнительные ресурсы

- [Примеры кода](../samples/)
- [API Reference](api/)
- [Видео туториалы](https://youtube.com/@zorui)
- [Community Discord](https://discord.gg/zorui)

## Нужна помощь?

- 📖 Читайте [документацию](README.md)
- 💬 Задайте вопрос в [Discord](https://discord.gg/zorui)
- 🐛 Сообщите о проблеме на [GitHub](https://github.com/zorui/zorui/issues)
- 📧 Напишите нам: support@zorui.dev
