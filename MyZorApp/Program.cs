using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Components.Forms;
using ZorUI.Components.DataDisplay;
using ZorUI.Styling;
using ZorUI.Rendering;

namespace MyZorApp;

/// <summary>
/// Ваше первое приложение на ZorUI!
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("🚀 Запуск вашего ZorUI приложения...\n");

        var app = new ZorApplication();
        app.Run(new MyApp());

        Console.WriteLine("\n\n✨ Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}

/// <summary>
/// Главный компонент вашего приложения.
/// Здесь вы можете создавать свой UI!
/// </summary>
public class MyApp : ZorComponent
{
    // Состояние приложения
    private int _counter = 0;
    private string _name = "";
    private bool _darkMode = false;

    public override ZorElement Build()
    {
        // Выбираем тему
        var theme = _darkMode ? Theme.Dark() : Theme.Light();
        
        // Создаем рендерер
        var renderer = new ConsoleRenderer();
        
        // Строим UI
        var ui = new VStack(spacing: theme.Spacing.Space6)
            .WithPadding(EdgeInsets.All(theme.Spacing.Space8))
            .AddChild(BuildHeader(theme))
            .AddChild(new Divider().WithColor(theme.Colors.Border))
            .AddChild(BuildCounterSection(theme))
            .AddChild(new Divider().WithColor(theme.Colors.Border))
            .AddChild(BuildFormSection(theme))
            .AddChild(new Divider().WithColor(theme.Colors.Border))
            .AddChild(BuildFooter(theme));
        
        // Рендерим
        renderer.Render(ui);
        
        return ui;
    }

    /// <summary>
    /// Заголовок приложения
    /// </summary>
    private ZorElement BuildHeader(Theme theme)
    {
        return new VStack(spacing: theme.Spacing.Space2)
            .AddChild(
                new Text("🎨 Мое первое ZorUI приложение!")
                    .WithFontSize(theme.Typography.FontSize3Xl)
                    .Bold()
            )
            .AddChild(
                new Text("Создано с помощью ZorUI Framework")
                    .WithFontSize(theme.Typography.FontSizeSm)
            )
            .AddChild(
                new HStack(spacing: theme.Spacing.Space2)
                    .AddChild(new Text("Тема:"))
                    .AddChild(
                        new Switch(_darkMode ? "Темная 🌙" : "Светлая ☀️", _darkMode)
                            .WithOnChange(dark =>
                            {
                                SetState(nameof(_darkMode), _darkMode = dark);
                                Console.WriteLine($"\n💡 Переключена тема: {(_darkMode ? "Темная" : "Светлая")}");
                            })
                    )
            );
    }

    /// <summary>
    /// Секция со счетчиком
    /// </summary>
    private ZorElement BuildCounterSection(Theme theme)
    {
        return new VStack(spacing: theme.Spacing.Space4)
            .AddChild(
                new Text("📊 Счетчик")
                    .WithFontSize(theme.Typography.FontSizeXl)
                    .SemiBold()
            )
            .AddChild(
                new Card()
                    .WithContent(
                        new VStack(spacing: theme.Spacing.Space4)
                            .AddChild(
                                new Text($"Значение: {_counter}")
                                    .WithFontSize(theme.Typography.FontSize2Xl)
                                    .Bold()
                            )
                            .AddChild(
                                new Progress(_counter % 100, 100)
                                    .Success()
                                    .WithShowValue()
                            )
                            .AddChild(
                                new HStack(spacing: theme.Spacing.Space2)
                                    .AddChild(
                                        new Button("➖ Минус", () =>
                                        {
                                            SetState(nameof(_counter), --_counter);
                                            Console.WriteLine($"📉 Счетчик: {_counter}");
                                        })
                                        .Secondary()
                                    )
                                    .AddChild(
                                        new Button("➕ Плюс", () =>
                                        {
                                            SetState(nameof(_counter), ++_counter);
                                            Console.WriteLine($"📈 Счетчик: {_counter}");
                                        })
                                        .Primary()
                                    )
                                    .AddChild(
                                        new Button("🔄 Сброс", () =>
                                        {
                                            SetState(nameof(_counter), _counter = 0);
                                            Console.WriteLine($"🔄 Счетчик сброшен");
                                        })
                                        .Destructive()
                                    )
                            )
                    )
            );
    }

    /// <summary>
    /// Секция с формой
    /// </summary>
    private ZorElement BuildFormSection(Theme theme)
    {
        return new VStack(spacing: theme.Spacing.Space4)
            .AddChild(
                new Text("📝 Форма ввода")
                    .WithFontSize(theme.Typography.FontSizeXl)
                    .SemiBold()
            )
            .AddChild(
                new Card()
                    .WithContent(
                        new VStack(spacing: theme.Spacing.Space4)
                            .AddChild(
                                new Label("Ваше имя:")
                            )
                            .AddChild(
                                new TextField("Введите имя...")
                                    .WithValue(_name)
                                    .WithOnChange(name =>
                                    {
                                        SetState(nameof(_name), _name = name);
                                        Console.WriteLine($"✏️ Имя изменено: {name}");
                                    })
                            )
                            .AddChild(
                                new Button("👋 Поздороваться", () =>
                                {
                                    if (!string.IsNullOrEmpty(_name))
                                    {
                                        Console.WriteLine($"\n👋 Привет, {_name}!");
                                        Console.WriteLine($"🎉 Счетчик: {_counter}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\n⚠️ Сначала введите имя!");
                                    }
                                })
                                .Primary()
                                .Disabled(string.IsNullOrWhiteSpace(_name))
                            )
                    )
            );
    }

    /// <summary>
    /// Футер приложения
    /// </summary>
    private ZorElement BuildFooter(Theme theme)
    {
        return new VStack(spacing: theme.Spacing.Space2)
            .AddChild(
                new Alert("💡 Совет: Измените код в Program.cs чтобы кастомизировать приложение!")
                    .Info()
            )
            .AddChild(
                new HStack(spacing: theme.Spacing.Space2)
                    .AddChild(new Badge("ZorUI v1.0").Primary())
                    .AddChild(new Badge(".NET 8").Success())
                    .AddChild(new Badge("Ready!").Info())
            );
    }
}
