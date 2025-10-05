# Мое ZorUI приложение

Это ваше первое приложение на ZorUI Framework!

## 🚀 Запуск

```bash
cd MyZorApp
dotnet run
```

## 📝 Что внутри

- ✅ Счетчик с кнопками
- ✅ Форма для ввода имени
- ✅ Переключение темной/светлой темы
- ✅ Progress bar
- ✅ Карточки (Cards)
- ✅ Badges и Alerts

## 🎨 Кастомизация

### Изменить цвета

```csharp
var theme = new Theme
{
    Colors = new ColorPalette
    {
        Primary = Color.FromHex("#FF6B6B"),
        // ... другие цвета
    }
};
```

### Добавить новые компоненты

```csharp
// В методе Build()
ui.AddChild(
    new Button("Новая кнопка", () => Console.WriteLine("Клик!"))
        .Primary()
);
```

### Добавить состояние

```csharp
// В классе MyApp
private bool _myState = false;

// В Build()
new Checkbox("Опция", _myState)
    .WithOnChange(value => SetState(nameof(_myState), _myState = value))
```

## 📚 Документация

- [ZorUI Documentation](../docs/)
- [Quick Reference](../docs/QuickReference.md)
- [Examples](../samples/)

## 💡 Примеры кода

### Создание списка

```csharp
var items = new List<string> { "Один", "Два", "Три" };
var list = new VStack(spacing: 8);

foreach (var item in items)
{
    list.AddChild(new Text($"• {item}"));
}
```

### Условный рендеринг

```csharp
if (_counter > 10)
{
    ui.AddChild(new Alert("Счетчик больше 10!").Warning());
}
```

### Tabs

```csharp
new Tabs("home")
    .AddTab(new Tab("home", "Главная")
        .WithContent(new Text("Контент главной")))
    .AddTab(new Tab("settings", "Настройки")
        .WithContent(new Text("Контент настроек")))
```

## 🎯 Следующие шаги

1. Измените стили и цвета
2. Добавьте новые компоненты
3. Создайте свою форму
4. Добавьте валидацию
5. Попробуйте Tabs и Accordion
6. Создайте dashboard

Удачи! 🚀
