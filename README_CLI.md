# 🎯 ZorUI CLI - Создание проектов за 30 секунд!

## 🚀 Что это?

**ZorUI CLI** - инструмент командной строки для мгновенного создания ZorUI проектов.

Аналог `create-react-app`, `vue create`, `flutter create` для ZorUI! ⚡

---

## ⚡ Quick Demo

```bash
# 1️⃣ Установка (1 команда)
./install-cli.sh

# 2️⃣ Создание проекта (1 команда)  
zorui new desktop --name MyApp

# 3️⃣ Запуск (1 команда)
cd MyApp && dotnet run
```

**Итого: 3 команды → Работающее GUI приложение!** 🎉

---

## 📋 Доступные команды

### `zorui new` - Создать проект

```bash
zorui new desktop --name MyApp        # Desktop GUI (Win/Linux/Mac)
zorui new console --name MyTool       # Console приложение
zorui new component --name MyLib      # Библиотека компонентов
zorui new full --name MyFullApp       # Полноценное приложение
```

### `zorui list` - Список шаблонов

```bash
zorui list
```

Показывает красивую таблицу всех доступных шаблонов.

### `zorui info` - О фреймворке

```bash
zorui info
```

Показывает версию, платформы, возможности.

---

## 🎨 Шаблоны

### 1. Desktop (GUI приложение)

```bash
zorui new desktop --name Calculator
```

**Создает:**
- Окно с SkiaSharp
- Counter пример
- Тема switcher
- Готово к запуску!

**Для:** Windows, Linux, macOS

### 2. Console (CLI приложение)

```bash
zorui new console --name MyCLI
```

**Создает:**
- Console renderer
- Базовый UI
- Простой пример

**Для:** Все платформы

### 3. Component (Библиотека)

```bash
zorui new component --name MyUILib
```

**Создает:**
- Структуру библиотеки
- Примеры компонентов
- NuGet-ready setup

**Для:** Переиспользование

### 4. Full (Полноценное приложение)

```bash
zorui new full --name MyFullApp
```

**Создает:**
- Multi-page структура
- Components папка
- Pages папка
- Styles папка
- Best practices

**Для:** Серьезные проекты

---

## 📖 Установка

### Linux/macOS

```bash
./install-cli.sh
```

### Windows

```cmd
install-cli.cmd
```

### Проверка

```bash
zorui --version
zorui list
```

---

## 💡 Примеры использования

### Создать и запустить

```bash
zorui new desktop -n MyApp && cd MyApp && dotnet run
```

### Создать в конкретной папке

```bash
zorui new desktop -n MyApp -o ./projects
cd projects/MyApp
dotnet run
```

### Создать несколько проектов

```bash
zorui new desktop -n App1
zorui new desktop -n App2
zorui new console -n Tool1
```

### Монорепозиторий

```bash
mkdir MyProject && cd MyProject

zorui new desktop -n Desktop -o ./apps
zorui new console -n API -o ./apps
zorui new component -n Shared -o ./libs

dotnet new sln -n MyProject
dotnet sln add **/*.csproj
```

---

## 🎯 Что дальше?

После создания проекта:

### 1. Запустите

```bash
dotnet run
```

### 2. Изучите код

Откройте `Program.cs` и изучите как устроен UI.

### 3. Экспериментируйте

```csharp
// Добавьте новую кнопку
.AddChild(
    new Button("New Button", () => Console.WriteLine("Clicked!"))
        .Primary()
)

// Добавьте текстовое поле
.AddChild(
    new TextField("Enter text...")
        .WithOnChange(text => Console.WriteLine(text))
)

// Добавьте карточку
.AddChild(
    new Card()
        .WithHeader(new Text("Title"))
        .WithContent(new Text("Content"))
)
```

### 4. Прочитайте документацию

- [Quick Reference](docs/QuickReference.md) - API справка
- [Getting Started](docs/getting-started.md) - Детальное руководство
- [Best Practices](docs/best-practices.md) - Рекомендации

### 5. Соберите приложение

```bash
# Windows .exe
dotnet publish -c Release -r win-x64 --self-contained

# Linux binary
dotnet publish -c Release -r linux-x64 --self-contained

# macOS app
dotnet publish -c Release -r osx-x64 --self-contained
```

---

## 📊 Сравнение способов создания

| Способ | Время | Сложность | Для кого |
|--------|-------|-----------|----------|
| **CLI** | 30 сек | ⭐ Легко | Новички |
| **Готовые примеры** | 10 сек | ⭐ Очень легко | Изучение |
| **Вручную** | 5-10 мин | ⭐⭐⭐ Сложно | Опытные |

**Рекомендация:** Используйте CLI! ⚡

---

## 🛠️ CLI команды - шпаргалка

```bash
# Установка
./install-cli.sh                      # Установить CLI

# Создание
zorui new desktop -n MyApp            # Desktop приложение
zorui new console -n MyTool           # Console приложение
zorui new component -n MyLib          # Библиотека
zorui new full -n MyFullApp           # Полное приложение

# Информация
zorui list                            # Список шаблонов
zorui info                            # О ZorUI
zorui --help                          # Справка

# Обновление
./install-cli.sh                      # Переустановить
dotnet tool update --global ZorUI.CLI # Или так

# Удаление
dotnet tool uninstall --global ZorUI.CLI
```

---

## 🎨 Что создает CLI?

```
zorui new desktop -n MyApp

MyApp/
├── MyApp.csproj           # Готовый проект с зависимостями
├── Program.cs             # Рабочий код с примерами
├── README.md              # Инструкции
└── .gitignore            # Git конфигурация

Содержит:
✅ Counter с кнопками
✅ Dark/Light theme switcher
✅ Готов к запуску dotnet run
✅ Готов к сборке для Win/Linux/Mac
```

---

## 🌟 Особенности CLI

- ✅ **Красивый UI** - С использованием Spectre.Console
- ✅ **Валидация** - Проверка параметров
- ✅ **Успешные сообщения** - Четкие инструкции
- ✅ **Готовый код** - Работает сразу
- ✅ **Best practices** - Правильная структура
- ✅ **Git-ready** - .gitignore включен
- ✅ **Документация** - README в каждом проекте

---

## 🔥 Преимущества

### Без CLI (старый способ):

```bash
mkdir MyApp
cd MyApp
dotnet new console
# ... редактируем .csproj (2 мин)
# ... добавляем ссылки (1 мин)
# ... пишем базовый код (3 мин)
# ... настраиваем структуру (2 мин)
# ... создаем .gitignore (1 мин)
# ИТОГО: ~10 минут
```

### С CLI (новый способ):

```bash
zorui new desktop -n MyApp
# ИТОГО: 5 секунд + готовый рабочий код!
```

**В 120 раз быстрее! ⚡**

---

## 📦 Что включено в шаблоны?

Каждый шаблон включает:

- ✅ Правильный `.csproj` с зависимостями
- ✅ Рабочий код с примерами
- ✅ Комментарии и документация
- ✅ README с инструкциями
- ✅ .gitignore
- ✅ Правильная структура папок
- ✅ Best practices

---

## 🎯 Итог

**ZorUI CLI - это самый быстрый способ начать работу с ZorUI!**

```bash
# 1. Установка
./install-cli.sh

# 2. Создание
zorui new desktop --name MyApp

# 3. Запуск
cd MyApp && dotnet run

# Готово! 🎉
```

---

<div align="center">

**[⬅️ Назад к главной](README.md)** • **[CLI Guide →](CLI_GUIDE.md)** • **[Quick Start →](QUICK_START_CLI.md)**

**Создано с ❤️ ZorUI Team**

</div>
