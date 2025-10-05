# Component Gallery - ZorUI

Полная галерея всех компонентов ZorUI с интерактивными примерами.

## Что демонстрирует

Этот пример показывает **все компоненты** ZorUI в действии:

### Layout Components
- ✅ VStack - Вертикальный стек
- ✅ HStack - Горизонтальный стек
- ✅ Container - Контейнер
- ✅ Spacer - Гибкое пространство
- ✅ Divider - Разделитель

### UI Components
- ✅ Text - Текст со стилями
- ✅ Button - Кнопки всех вариантов
- ✅ Image - Изображения
- ✅ Icon - Иконки
- ✅ Label - Метки

### Form Components
- ✅ TextField - Текстовые поля
- ✅ Checkbox - Чекбоксы
- ✅ Radio & RadioGroup - Радио-кнопки
- ✅ Switch - Переключатели
- ✅ Slider - Слайдеры

### Navigation
- ✅ Tabs - Вкладки
- ✅ Menu - Меню

### Overlays
- ✅ Dialog - Диалоги
- ✅ AlertDialog - Алерты
- ✅ Popover - Поповеры
- ✅ Tooltip - Подсказки
- ✅ Toast - Уведомления

### Data Display
- ✅ Card - Карточки
- ✅ Accordion - Аккордеон
- ✅ Progress - Прогресс-бар
- ✅ Avatar - Аватары
- ✅ Badge - Бейджи
- ✅ Alert - Алерты
- ✅ Spinner - Спиннеры

## Запуск

```bash
cd samples/ComponentGallery
dotnet run
```

## Использование как справочника

Этот пример служит живой документацией. Вы можете:

1. **Изучить код** - Посмотрите, как используется каждый компонент
2. **Скопировать примеры** - Используйте код в своих проектах
3. **Экспериментировать** - Измените параметры и посмотрите результат

## Структура кода

```csharp
public class GalleryApp : ZorComponent
{
    public override ZorElement Build()
    {
        var ui = new VStack(spacing: 16)
            .WithPadding(20);

        // Добавляем секции для каждого типа компонентов
        ui.AddChild(BuildLayoutSection());
        ui.AddChild(BuildButtonSection());
        ui.AddChild(BuildFormSection());
        ui.AddChild(BuildNavigationSection());
        // ... и т.д.
        
        renderer.Render(ui);
        return ui;
    }
}
```

## Расширение галереи

Чтобы добавить свои примеры:

1. Создайте новый метод `BuildYourSection()`
2. Добавьте компоненты с примерами
3. Добавьте секцию в `Build()` метод

```csharp
private ZorElement BuildYourSection()
{
    return new VStack(spacing: 8)
        .AddChild(
            new Text("Your Section")
                .WithFontSize(20)
                .SemiBold()
        )
        .AddChild(
            // Ваши примеры здесь
        );
}
```

## Следующие шаги

После изучения галереи:

1. Создайте свое приложение на базе понравившихся компонентов
2. Изучите [документацию](../../docs/) для детального понимания
3. Прочитайте [Best Practices](../../docs/best-practices.md)
4. Посмотрите другие примеры

## Обратная связь

Нашли баг или хотите предложить улучшение?
- Откройте [Issue](https://github.com/zorui/zorui/issues)
- Напишите в [Discord](https://discord.gg/zorui)
