# Styling in ZorUI

Полное руководство по стилизации компонентов в ZorUI.

## Содержание

- [Основы стилизации](#основы-стилизации)
- [Цвета](#цвета)
- [Типография](#типография)
- [Spacing и Layout](#spacing-и-layout)
- [Темы](#темы)
- [Кастомные стили](#кастомные-стили)
- [Responsive дизайн](#responsive-дизайн)

## Основы стилизации

ZorUI использует объектно-ориентированный подход к стилизации без CSS.

### Fluent API стилизация

```csharp
new Text("Styled Text")
    .WithFontSize(20)
    .Bold()
    .WithColor(Color.Blue)
    .Underline();
```

### Style объекты

```csharp
var buttonStyle = new Style
{
    BackgroundColor = Color.FromHex("#3B82F6"),
    ForegroundColor = Color.White,
    BorderRadius = 8,
    Padding = EdgeInsets.Symmetric(horizontal: 16, vertical: 8)
};

new Button("Styled")
    .WithStyle(buttonStyle);
```

## Цвета

### Предопределенные цвета

```csharp
Color.Black
Color.White
Color.Red
Color.Green
Color.Blue
Color.Yellow
Color.Cyan
Color.Magenta
Color.Gray
Color.Transparent
```

### Создание цветов

#### Из HEX

```csharp
var blue = Color.FromHex("#3B82F6");
var blueWithAlpha = Color.FromHex("#3B82F680");  // 50% opacity
```

#### Из RGB

```csharp
var red = Color.FromRgb(255, 0, 0);
var purple = new Color(128, 0, 128);
```

#### Из HSL

```csharp
// Hue: 0-360, Saturation: 0-1, Lightness: 0-1
var color = Color.FromHsl(215, 0.92, 0.6);
```

### Модификаторы цвета

```csharp
var baseColor = Color.Blue;

// Прозрачность
var transparent = baseColor.WithAlpha(0.5);

// Светлее/темнее
var lighter = baseColor.Lighter(0.2);  // +20% lightness
var darker = baseColor.Darker(0.2);    // -20% lightness

// Конвертация
var hex = baseColor.ToHex();  // "#0000FF"
```

### Цветовые палитры

```csharp
// Используем палитру темы
var theme = Theme.Light();
var primary = theme.Colors.Primary;
var background = theme.Colors.Background;
var error = theme.Colors.Error;

// Семантические цвета
theme.Colors.Success   // Зеленый
theme.Colors.Warning   // Желтый/оранжевый
theme.Colors.Error     // Красный
theme.Colors.Info      // Синий
```

## Типография

### Размеры шрифта

```csharp
var theme = Theme.Light();

// Предопределенные размеры
theme.Typography.FontSizeXs    // 12px
theme.Typography.FontSizeSm    // 14px
theme.Typography.FontSizeBase  // 16px
theme.Typography.FontSizeLg    // 18px
theme.Typography.FontSizeXl    // 20px
theme.Typography.FontSize2Xl   // 24px
theme.Typography.FontSize3Xl   // 30px
theme.Typography.FontSize4Xl   // 36px

// Использование
new Text("Large Title")
    .WithFontSize(theme.Typography.FontSize3Xl);
```

### Веса шрифта

```csharp
new Text("Text")
    .WithFontWeight(FontWeight.Thin)        // 100
    .WithFontWeight(FontWeight.ExtraLight)  // 200
    .WithFontWeight(FontWeight.Light)       // 300
    .WithFontWeight(FontWeight.Normal)      // 400
    .WithFontWeight(FontWeight.Medium)      // 500
    .WithFontWeight(FontWeight.SemiBold)    // 600
    .WithFontWeight(FontWeight.Bold)        // 700
    .WithFontWeight(FontWeight.ExtraBold)   // 800
    .WithFontWeight(FontWeight.Black);      // 900

// Или короткие методы
new Text("Text")
    .Light()
    .SemiBold()
    .Bold();
```

### Высота строки

```csharp
var theme = Theme.Light();

new Text("Multi-line text")
    .WithLineHeight(theme.Typography.LineHeightTight)    // 1.25
    .WithLineHeight(theme.Typography.LineHeightNormal)   // 1.5
    .WithLineHeight(theme.Typography.LineHeightRelaxed); // 1.75
```

### Семейства шрифтов

```csharp
new Text("Custom Font")
    .WithFontFamily("Inter")
    .WithFontFamily("Roboto, sans-serif")
    .WithFontFamily(theme.Typography.FontFamily);
```

### Типографические стили

```csharp
// Заголовки
new Text("Heading 1")
    .WithFontSize(36)
    .Bold();

new Text("Heading 2")
    .WithFontSize(30)
    .SemiBold();

// Параграфы
new Text("Body text")
    .WithFontSize(16)
    .WithLineHeight(1.5);

// Подписи
new Text("Caption")
    .WithFontSize(12)
    .WithColor(Color.Gray);

// Моноширинный
new Text("Code: console.log()")
    .WithFontFamily("Courier New, monospace");
```

## Spacing и Layout

### EdgeInsets (Padding/Margin)

```csharp
// Одинаковый отступ со всех сторон
EdgeInsets.All(16)

// Симметричные отступы
EdgeInsets.Symmetric(
    horizontal: 16,  // left + right
    vertical: 8      // top + bottom
)

// Только горизонтальные/вертикальные
EdgeInsets.Horizontal(16)  // left + right
EdgeInsets.Vertical(8)     // top + bottom

// Индивидуальные значения
new EdgeInsets(
    top: 10,
    right: 15,
    bottom: 10,
    left: 15
)

// Нулевой отступ
EdgeInsets.Zero
```

### Spacing Scale

```csharp
var theme = Theme.Light();
var spacing = theme.Spacing;

// Базовая единица (обычно 4px)
spacing.BaseUnit    // 4px

// Кратные значения
spacing.Space1      // 4px   (1x)
spacing.Space2      // 8px   (2x)
spacing.Space3      // 12px  (3x)
spacing.Space4      // 16px  (4x)
spacing.Space5      // 20px  (5x)
spacing.Space6      // 24px  (6x)
spacing.Space8      // 32px  (8x)
spacing.Space10     // 40px  (10x)
spacing.Space12     // 48px  (12x)
spacing.Space16     // 64px  (16x)
spacing.Space20     // 80px  (20x)

// Использование
new VStack(spacing: theme.Spacing.Space4)
    .WithPadding(EdgeInsets.All(theme.Spacing.Space6));
```

### Размеры

```csharp
// Фиксированные размеры
new Container()
    .WithWidth(300)
    .WithHeight(200);

// Min/Max размеры
new Container()
    .WithMinWidth(100)
    .WithMaxWidth(500)
    .WithMinHeight(50)
    .WithMaxHeight(300);

// Size объект
var size = new Size(200, 100);
var square = Size.Square(100);

// Constraints
var constraints = new SizeConstraints(
    minWidth: 100,
    maxWidth: 500,
    minHeight: 50,
    maxHeight: 300
);
```

### Border Radius

```csharp
var theme = Theme.Light();
var radius = theme.Radius;

radius.None    // 0
radius.Sm      // 4px
radius.Base    // 6px
radius.Md      // 8px
radius.Lg      // 12px
radius.Xl      // 16px
radius.Full    // 9999px (circle)

// Использование
new Container()
    .WithStyle(new Style 
    { 
        BorderRadius = theme.Radius.Md 
    });
```

## Темы

### Предопределенные темы

```csharp
// Светлая тема
var light = Theme.Light();

// Темная тема
var dark = Theme.Dark();

// Применение
var app = new ZorApplication();
app.Context.Theme = dark;
```

### Создание кастомной темы

```csharp
var customTheme = new Theme
{
    IsDark = false,
    
    // Цветовая палитра
    Colors = new ColorPalette
    {
        Primary = Color.FromHex("#FF6B6B"),
        PrimaryForeground = Color.White,
        
        Secondary = Color.FromHex("#4ECDC4"),
        SecondaryForeground = Color.White,
        
        Background = Color.FromHex("#F7F7F7"),
        Foreground = Color.FromHex("#2C3E50"),
        
        Success = Color.FromHex("#2ECC71"),
        Warning = Color.FromHex("#F39C12"),
        Error = Color.FromHex("#E74C3C"),
        Info = Color.FromHex("#3498DB"),
        
        Border = Color.FromHex("#E0E0E0"),
        Card = Color.White,
        Muted = Color.FromHex("#95A5A6")
    },
    
    // Типография
    Typography = new Typography
    {
        FontFamily = "Inter, system-ui, sans-serif",
        FontSizeXs = 12,
        FontSizeSm = 14,
        FontSizeBase = 16,
        FontSizeLg = 18,
        FontSizeXl = 20,
        FontSize2Xl = 24,
        FontSize3Xl = 30,
        FontSize4Xl = 36,
        LineHeightTight = 1.25,
        LineHeightNormal = 1.5,
        LineHeightRelaxed = 1.75
    },
    
    // Spacing
    Spacing = new SpacingScale
    {
        BaseUnit = 4
    },
    
    // Border Radius
    Radius = new RadiusScale
    {
        None = 0,
        Sm = 4,
        Base = 6,
        Md = 8,
        Lg = 12,
        Xl = 16,
        Full = 9999
    },
    
    // Shadows
    Shadows = new ShadowScale
    {
        Sm = "0 1px 2px 0 rgba(0, 0, 0, 0.05)",
        Base = "0 1px 3px 0 rgba(0, 0, 0, 0.1)",
        Md = "0 4px 6px -1px rgba(0, 0, 0, 0.1)",
        Lg = "0 10px 15px -3px rgba(0, 0, 0, 0.1)",
        Xl = "0 20px 25px -5px rgba(0, 0, 0, 0.1)"
    }
};

// Применение темы
app.Context.Theme = customTheme;
```

### Использование темы в компонентах

```csharp
public class ThemedComponent : ZorComponent
{
    public override ZorElement Build()
    {
        var theme = Context?.Theme as Theme ?? Theme.Light();
        
        return new Container()
            .WithBackground(theme.Colors.Background)
            .WithPadding(EdgeInsets.All(theme.Spacing.Space4))
            .AddChild(
                new Text("Themed Text")
                    .WithColor(theme.Colors.Foreground)
                    .WithFontSize(theme.Typography.FontSizeBase)
            );
    }
}
```

### Переключение тем

```csharp
public class ThemeSwitcher : ZorComponent
{
    private bool _isDark = false;

    public override ZorElement Build()
    {
        return new Switch("Dark Mode", _isDark)
            .WithOnChange(isDark =>
            {
                SetState(nameof(_isDark), _isDark = isDark);
                
                // Обновляем тему приложения
                Context.Theme = isDark ? Theme.Dark() : Theme.Light();
            });
    }
}
```

## Кастомные стили

### Создание переиспользуемых стилей

```csharp
public static class AppStyles
{
    public static Style PrimaryButton => new Style
    {
        BackgroundColor = Color.FromHex("#3B82F6"),
        ForegroundColor = Color.White,
        BorderRadius = 8,
        Padding = EdgeInsets.Symmetric(horizontal: 16, vertical: 12),
        FontWeight = FontWeight.SemiBold
    };
    
    public static Style Card => new Style
    {
        BackgroundColor = Color.White,
        BorderRadius = 12,
        Padding = EdgeInsets.All(16),
        BorderWidth = 1,
        BorderColor = Color.FromHex("#E5E7EB")
    };
    
    public static Style Heading => new Style
    {
        FontSize = 24,
        FontWeight = FontWeight.Bold,
        ForegroundColor = Color.FromHex("#1F2937")
    };
}

// Использование
new Button("Click me")
    .WithStyle(AppStyles.PrimaryButton);

new Container()
    .WithStyle(AppStyles.Card)
    .AddChild(
        new Text("Title")
            .WithStyle(AppStyles.Heading)
    );
```

### Композиция стилей

```csharp
// Базовый стиль
var baseStyle = new Style
{
    Padding = EdgeInsets.All(16),
    BorderRadius = 8
};

// Расширение стиля
var primaryStyle = baseStyle.Clone();
primaryStyle.BackgroundColor = Color.Blue;
primaryStyle.ForegroundColor = Color.White;

var secondaryStyle = baseStyle.Clone();
secondaryStyle.BackgroundColor = Color.Gray;
secondaryStyle.ForegroundColor = Color.Black;
```

### Условные стили

```csharp
public class ConditionalButton : ZorComponent
{
    private bool _isHovered = false;

    public override ZorElement Build()
    {
        var style = _isHovered
            ? new Style 
              { 
                  BackgroundColor = Color.Blue.Darker(0.1) 
              }
            : new Style 
              { 
                  BackgroundColor = Color.Blue 
              };
        
        return new Button("Hover me")
            .WithStyle(style);
    }
}
```

## Responsive дизайн

### Адаптивные размеры

```csharp
public class ResponsiveCard : ZorComponent
{
    public override ZorElement Build()
    {
        // Получаем размер экрана (псевдокод, зависит от платформы)
        var screenWidth = GetScreenWidth();
        
        var cardWidth = screenWidth switch
        {
            < 600 => screenWidth - 32,    // Mobile: full width with margin
            < 1024 => 600,                // Tablet: fixed 600px
            _ => 800                       // Desktop: fixed 800px
        };
        
        return new Card()
            .WithWidth(cardWidth)
            .WithContent(new Text("Responsive!"));
    }
}
```

### Адаптивный Layout

```csharp
public class ResponsiveLayout : ZorComponent
{
    public override ZorElement Build()
    {
        var isMobile = GetScreenWidth() < 768;
        
        return isMobile
            ? BuildMobileLayout()
            : BuildDesktopLayout();
    }
    
    private ZorElement BuildMobileLayout()
    {
        return new VStack(spacing: 16,
            new Text("Mobile Layout"),
            new Button("Full Width").WithFullWidth()
        );
    }
    
    private ZorElement BuildDesktopLayout()
    {
        return new HStack(spacing: 24,
            new Text("Desktop Layout"),
            new Button("Fixed Width")
        );
    }
}
```

## Best Practices

### 1. Используйте систему spacing

```csharp
// ✅ Хорошо: Использует систему
var theme = Theme.Light();
new VStack(spacing: theme.Spacing.Space4);

// ❌ Плохо: Магические числа
new VStack(spacing: 17);
```

### 2. Переиспользуйте стили

```csharp
// ✅ Хорошо: Переиспользуемые стили
public static class Styles
{
    public static Style Button => new Style { ... };
}

// ❌ Плохо: Дублирование
new Button("One").WithStyle(new Style { ... });
new Button("Two").WithStyle(new Style { ... });
```

### 3. Используйте семантические цвета

```csharp
// ✅ Хорошо: Семантические цвета
new Button("Delete")
    .WithBackground(theme.Colors.Destructive);

// ❌ Плохо: Жестко заданные цвета
new Button("Delete")
    .WithBackground(Color.FromHex("#FF0000"));
```

### 4. Придерживайтесь темы

```csharp
// ✅ Хорошо: Использует тему
var theme = Context.Theme as Theme;
new Text("Text")
    .WithFontSize(theme.Typography.FontSizeBase);

// ❌ Плохо: Игнорирует тему
new Text("Text")
    .WithFontSize(16);
```

## Примеры

### Карточка с полной стилизацией

```csharp
public class StyledCard : ZorComponent
{
    public override ZorElement Build()
    {
        var theme = Theme.Light();
        
        return new Card()
            .WithStyle(new Style
            {
                BackgroundColor = theme.Colors.Card,
                BorderRadius = theme.Radius.Lg,
                Padding = EdgeInsets.All(theme.Spacing.Space6),
                BorderWidth = 1,
                BorderColor = theme.Colors.Border
            })
            .WithHeader(
                new Text("Card Title")
                    .WithFontSize(theme.Typography.FontSizeLg)
                    .Bold()
                    .WithColor(theme.Colors.Foreground)
            )
            .WithContent(
                new Text("Card content goes here")
                    .WithFontSize(theme.Typography.FontSizeBase)
                    .WithColor(theme.Colors.MutedForeground)
                    .WithLineHeight(theme.Typography.LineHeightNormal)
            )
            .WithFooter(
                new HStack(spacing: theme.Spacing.Space2)
                    .AddChild(
                        new Button("Cancel")
                            .Secondary()
                    )
                    .AddChild(
                        new Button("Save")
                            .Primary()
                    )
            );
    }
}
```
