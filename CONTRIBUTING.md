# Contributing to ZorUI

Спасибо за ваш интерес к ZorUI! Мы рады любому вкладу в развитие проекта.

## 📋 Содержание

- [Code of Conduct](#code-of-conduct)
- [Как помочь](#как-помочь)
- [Процесс разработки](#процесс-разработки)
- [Структура проекта](#структура-проекта)
- [Стиль кода](#стиль-кода)
- [Тестирование](#тестирование)
- [Отправка Pull Request](#отправка-pull-request)

## Code of Conduct

Участвуя в проекте, вы соглашаетесь соблюдать наш [Code of Conduct](CODE_OF_CONDUCT.md).

## Как помочь

Существует множество способов внести вклад в ZorUI:

### 🐛 Сообщить о баге

1. Проверьте, не был ли баг уже зарегистрирован в [Issues](https://github.com/zorui/zorui/issues)
2. Создайте новый issue с подробным описанием:
   - Шаги для воспроизведения
   - Ожидаемое поведение
   - Фактическое поведение
   - Версия ZorUI и .NET
   - Операционная система

### ✨ Предложить новую функцию

1. Откройте issue с тегом `feature request`
2. Опишите:
   - Проблему, которую решает функция
   - Предлагаемое решение
   - Альтернативные варианты
   - Примеры использования

### 📝 Улучшить документацию

- Исправить опечатки
- Добавить примеры кода
- Улучшить объяснения
- Перевести документацию

### 🔧 Исправить баг

1. Найдите issue с тегом `bug` и `good first issue`
2. Прокомментируйте, что берете его в работу
3. Создайте Pull Request с исправлением

### 🎨 Добавить новый компонент

1. Обсудите компонент в issue перед началом работы
2. Следуйте существующим паттернам API
3. Добавьте документацию и примеры
4. Покройте тестами

## Процесс разработки

### 1. Fork и Clone

```bash
# Fork репозиторий на GitHub

# Clone вашего fork
git clone https://github.com/YOUR_USERNAME/zorui.git
cd zorui

# Добавьте upstream remote
git remote add upstream https://github.com/zorui/zorui.git
```

### 2. Создайте ветку

```bash
# Обновите main
git checkout main
git pull upstream main

# Создайте feature ветку
git checkout -b feature/my-awesome-feature

# Или bugfix ветку
git checkout -b fix/issue-123
```

### 3. Разработка

```bash
# Установите зависимости
dotnet restore

# Соберите проект
dotnet build

# Запустите тесты
dotnet test

# Запустите примеры
dotnet run --project samples/ComponentGallery
```

### 4. Commit изменений

Следуйте [Conventional Commits](https://www.conventionalcommits.org/):

```bash
# Примеры хороших commit сообщений:
git commit -m "feat: add Carousel component"
git commit -m "fix: resolve button click issue on mobile"
git commit -m "docs: improve TextField documentation"
git commit -m "refactor: simplify theme system"
git commit -m "test: add tests for Dialog component"
```

Типы commit'ов:
- `feat`: новая функция
- `fix`: исправление бага
- `docs`: изменения в документации
- `style`: форматирование кода
- `refactor`: рефакторинг без изменения функциональности
- `test`: добавление или изменение тестов
- `chore`: обновление зависимостей, конфигурации

### 5. Push и Pull Request

```bash
# Push в ваш fork
git push origin feature/my-awesome-feature

# Создайте Pull Request на GitHub
```

## Структура проекта

```
ZorUI/
├── src/
│   ├── ZorUI.Core/           # Ядро фреймворка
│   ├── ZorUI.Components/     # Библиотека компонентов
│   ├── ZorUI.Styling/        # Система стилей
│   └── ZorUI.Rendering/      # Рендеринг
│
├── samples/                  # Примеры приложений
│   ├── BasicApp/
│   └── ComponentGallery/
│
├── tests/                    # Тесты
│   ├── ZorUI.Core.Tests/
│   └── ZorUI.Components.Tests/
│
├── docs/                     # Документация
└── templates/                # Шаблоны проектов
```

## Стиль кода

### C# Conventions

```csharp
// Классы - PascalCase
public class MyComponent : ZorComponent

// Методы - PascalCase
public void HandleClick()

// Приватные поля - _camelCase
private int _count = 0;

// Публичные свойства - PascalCase
public string Name { get; set; }

// Параметры - camelCase
public void DoSomething(string userName)

// Локальные переменные - camelCase
var myVariable = 10;

// Константы - PascalCase
public const int MaxItems = 100;
```

### Fluent API

```csharp
// ✅ Хорошо: Читаемый fluent API
new Button("Submit")
    .Primary()
    .Large()
    .WithFullWidth()
    .Disabled(false);

// ❌ Плохо: Всё в одну строку
new Button("Submit").Primary().Large().WithFullWidth().Disabled(false);
```

### Документация

```csharp
/// <summary>
/// Краткое описание компонента.
/// Дополнительные детали и примеры использования.
/// </summary>
/// <param name="value">Описание параметра.</param>
/// <returns>Описание возвращаемого значения.</returns>
public class MyComponent : ZorComponent
{
    /// <summary>
    /// Описание свойства.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Описание метода.
    /// </summary>
    public void DoSomething()
    {
        // Реализация
    }
}
```

### Комментарии

```csharp
// ✅ Хорошо: Объясняет "почему"
// We use a delay here to batch multiple state updates
// and avoid unnecessary re-renders
await Task.Delay(1);

// ❌ Плохо: Объясняет "что" (код уже это показывает)
// Set count to zero
count = 0;
```

## Тестирование

### Структура тестов

```csharp
[TestClass]
public class ButtonTests
{
    [TestMethod]
    public void Button_Click_RaisesEvent()
    {
        // Arrange
        bool clicked = false;
        var button = new Button("Test", () => clicked = true);

        // Act
        button.OnClick?.Invoke();

        // Assert
        Assert.IsTrue(clicked);
    }

    [TestMethod]
    public void Button_Disabled_DoesNotRaiseEvent()
    {
        // Arrange
        bool clicked = false;
        var button = new Button("Test", () => clicked = true)
            .Disabled();

        // Act
        // (Disabled button shouldn't respond to clicks)

        // Assert
        Assert.IsFalse(clicked);
    }
}
```

### Запуск тестов

```bash
# Все тесты
dotnet test

# Конкретный проект
dotnet test tests/ZorUI.Core.Tests

# С coverage
dotnet test /p:CollectCoverage=true
```

## Отправка Pull Request

### Checklist

Перед отправкой PR убедитесь, что:

- [ ] Код соответствует стилю проекта
- [ ] Все тесты проходят успешно
- [ ] Добавлены новые тесты (если применимо)
- [ ] Обновлена документация
- [ ] Добавлены XML комментарии к публичным API
- [ ] Commit сообщения следуют Conventional Commits
- [ ] Нет конфликтов с main веткой

### Шаблон PR

```markdown
## Описание

Краткое описание изменений.

## Тип изменения

- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Связанные Issues

Closes #123

## Как протестировано

Опишите, как вы тестировали изменения.

## Screenshots (если применимо)

Добавьте скриншоты для UI изменений.

## Checklist

- [ ] Код следует стилю проекта
- [ ] Тесты добавлены/обновлены
- [ ] Документация обновлена
- [ ] Все тесты проходят
```

### Review процесс

1. Автоматические проверки (CI/CD) должны пройти успешно
2. Минимум один approve от мейнтейнера
3. Все комментарии должны быть разрешены
4. Обсуждение и итерации при необходимости

## Дополнительные ресурсы

- [Architecture Decision Records](docs/adr/)
- [API Design Guidelines](docs/api-design.md)
- [Performance Guidelines](docs/performance.md)
- [Community Discord](https://discord.gg/zorui)

## Вопросы?

Если у вас есть вопросы:

- Спросите в [Discussions](https://github.com/zorui/zorui/discussions)
- Присоединитесь к [Discord](https://discord.gg/zorui)
- Напишите на email: contributors@zorui.dev

Спасибо за ваш вклад! 🎉
