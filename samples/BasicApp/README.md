# BasicApp - ZorUI Example

Простое приложение-счетчик, демонстрирующее основы ZorUI.

## Что демонстрирует

- ✅ Создание компонента (`ZorComponent`)
- ✅ Управление состоянием (`SetState`)
- ✅ Обработка событий (клики кнопок)
- ✅ Layout с `VStack` и `HStack`
- ✅ Использование базовых компонентов (Text, Button)
- ✅ Fluent API

## Запуск

```bash
cd samples/BasicApp
dotnet run
```

## Код

```csharp
public class CounterApp : ZorComponent
{
    private int _count = 0;

    public override ZorElement Build()
    {
        var renderer = new ConsoleRenderer();
        
        var ui = new VStack(spacing: 20)
            .WithPadding(20)
            .AddChild(
                new Text("Counter Application")
                    .WithFontSize(32)
                    .Bold()
            )
            .AddChild(
                new Text($"Count: {_count}")
                    .WithFontSize(24)
            )
            .AddChild(
                new HStack(spacing: 10)
                    .AddChild(
                        new Button("Decrement", () => Decrement())
                            .Secondary()
                    )
                    .AddChild(
                        new Button("Increment", () => Increment())
                            .Primary()
                    )
                    .AddChild(
                        new Button("Reset", () => Reset())
                            .Destructive()
                    )
            );

        renderer.Render(ui);
        return ui;
    }

    private void Increment()
    {
        SetState(nameof(_count), ++_count);
    }

    private void Decrement()
    {
        SetState(nameof(_count), --_count);
    }

    private void Reset()
    {
        SetState(nameof(_count), _count = 0);
    }
}
```

## Следующие шаги

После изучения этого примера, попробуйте:

1. **Изменить стили** - Измените цвета, размеры, отступы
2. **Добавить валидацию** - Не позволяйте счетчику быть отрицательным
3. **Добавить анимацию** - Анимируйте изменение числа
4. **Добавить историю** - Сохраняйте историю изменений
5. **Добавить персистентность** - Сохраняйте значение при перезапуске

## Похожие примеры

- [ComponentGallery](../ComponentGallery/) - Все компоненты в одном месте
- [TodoApp](../TodoApp/) - Приложение для задач (скоро)
- [FormExample](../FormExample/) - Работа с формами (скоро)
