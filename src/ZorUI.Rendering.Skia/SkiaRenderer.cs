using SkiaSharp;
using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Components.Forms;
using ZorUI.Styling;

namespace ZorUI.Rendering.Skia;

/// <summary>
/// SkiaSharp-based renderer for cross-platform GUI.
/// Supports Windows, Linux, and macOS.
/// </summary>
public class SkiaRenderer : IRenderer
{
    private SKCanvas? _canvas;
    private readonly Dictionary<ZorElement, LayoutInfo> _layoutCache = new();
    
    /// <summary>
    /// Current rendering surface size.
    /// </summary>
    public Size SurfaceSize { get; set; } = new(800, 600);

    /// <summary>
    /// Background color.
    /// </summary>
    public Color BackgroundColor { get; set; } = Color.White;

    /// <summary>
    /// Renders the element tree to the given canvas.
    /// </summary>
    public void RenderToCanvas(SKCanvas canvas, ZorElement root)
    {
        _canvas = canvas;
        BeginFrame();
        Render(root);
        EndFrame();
    }

    public void Render(ZorElement root)
    {
        if (_canvas == null) return;

        // Measure and layout
        var constraints = new SizeConstraints(
            0, SurfaceSize.Width,
            0, SurfaceSize.Height
        );
        
        var size = Measure(root, constraints);
        Layout(root, 0, 0, size.Width, size.Height);

        // Paint
        PaintElement(root);
    }

    public Size Measure(ZorElement element, SizeConstraints constraints)
    {
        return element switch
        {
            Text text => MeasureText(text, constraints),
            Button button => MeasureButton(button, constraints),
            TextField textField => MeasureTextField(textField, constraints),
            VStack vstack => MeasureVStack(vstack, constraints),
            HStack hstack => MeasureHStack(hstack, constraints),
            Container container => MeasureContainer(container, constraints),
            _ => new Size(100, 40)
        };
    }

    public void Layout(ZorElement element, double x, double y, double width, double height)
    {
        _layoutCache[element] = new LayoutInfo
        {
            X = x,
            Y = y,
            Width = width,
            Height = height
        };

        // Layout children based on element type
        switch (element)
        {
            case VStack vstack:
                LayoutVStack(vstack, x, y, width, height);
                break;
            case HStack hstack:
                LayoutHStack(hstack, x, y, width, height);
                break;
            case Container container:
                LayoutContainer(container, x, y, width, height);
                break;
        }
    }

    public void BeginFrame()
    {
        if (_canvas == null) return;

        _canvas.Clear(ToSkColor(BackgroundColor));
    }

    public void EndFrame()
    {
        // Nothing to do - drawing is immediate with SkiaSharp
    }

    public void Clear()
    {
        _layoutCache.Clear();
    }

    private void PaintElement(ZorElement element)
    {
        if (_canvas == null || !_layoutCache.TryGetValue(element, out var layout))
            return;

        switch (element)
        {
            case Text text:
                PaintText(text, layout);
                break;
            case Button button:
                PaintButton(button, layout);
                break;
            case TextField textField:
                PaintTextField(textField, layout);
                break;
            case Container container:
                PaintContainer(container, layout);
                break;
        }

        // Paint children
        foreach (var child in element.Children)
        {
            PaintElement(child);
        }
    }

    #region Measure Methods

    private Size MeasureText(Text text, SizeConstraints constraints)
    {
        using var paint = new SKPaint
        {
            TextSize = (float)(text.FontSize ?? 16),
            IsAntialias = true
        };

        var bounds = new SKRect();
        paint.MeasureText(text.Content, ref bounds);

        return new Size(bounds.Width + 4, bounds.Height + 8);
    }

    private Size MeasureButton(Button button, SizeConstraints constraints)
    {
        var textElement = button.Children.OfType<Text>().FirstOrDefault();
        var textSize = textElement != null 
            ? MeasureText(textElement, constraints)
            : new Size(80, 32);

        var padding = 32; // 16px on each side
        return new Size(
            textSize.Width + padding,
            Math.Max(textSize.Height + 16, 40)
        );
    }

    private Size MeasureTextField(TextField textField, SizeConstraints constraints)
    {
        return new Size(
            Math.Min(constraints.MaxWidth, 200),
            40
        );
    }

    private Size MeasureVStack(VStack stack, SizeConstraints constraints)
    {
        double totalHeight = 0;
        double maxWidth = 0;
        var spacing = stack.Spacing;

        foreach (var child in stack.Children)
        {
            var childSize = Measure(child, constraints);
            maxWidth = Math.Max(maxWidth, childSize.Width);
            totalHeight += childSize.Height;
        }

        if (stack.Children.Count > 1)
        {
            totalHeight += spacing * (stack.Children.Count - 1);
        }

        return new Size(maxWidth, totalHeight);
    }

    private Size MeasureHStack(HStack stack, SizeConstraints constraints)
    {
        double totalWidth = 0;
        double maxHeight = 0;
        var spacing = stack.Spacing;

        foreach (var child in stack.Children)
        {
            var childSize = Measure(child, constraints);
            totalWidth += childSize.Width;
            maxHeight = Math.Max(maxHeight, childSize.Height);
        }

        if (stack.Children.Count > 1)
        {
            totalWidth += spacing * (stack.Children.Count - 1);
        }

        return new Size(totalWidth, maxHeight);
    }

    private Size MeasureContainer(Container container, SizeConstraints constraints)
    {
        if (container.Children.Count == 0)
        {
            return new Size(
                container.Width ?? 0,
                container.Height ?? 0
            );
        }

        var child = container.Children[0];
        return Measure(child, constraints);
    }

    #endregion

    #region Layout Methods

    private void LayoutVStack(VStack stack, double x, double y, double width, double height)
    {
        var padding = stack.Padding ?? EdgeInsets.Zero;
        var currentY = y + padding.Top;
        var contentX = x + padding.Left;
        var contentWidth = width - padding.Horizontal;

        foreach (var child in stack.Children)
        {
            if (!_layoutCache.TryGetValue(child, out var childLayout))
            {
                var constraints = new SizeConstraints(0, contentWidth, 0, height);
                var childSize = Measure(child, constraints);
                Layout(child, contentX, currentY, childSize.Width, childSize.Height);
                currentY += childSize.Height + stack.Spacing;
            }
            else
            {
                currentY += childLayout.Height + stack.Spacing;
            }
        }
    }

    private void LayoutHStack(HStack stack, double x, double y, double width, double height)
    {
        var padding = stack.Padding ?? EdgeInsets.Zero;
        var currentX = x + padding.Left;
        var contentY = y + padding.Top;
        var contentHeight = height - padding.Vertical;

        foreach (var child in stack.Children)
        {
            if (!_layoutCache.TryGetValue(child, out var childLayout))
            {
                var constraints = new SizeConstraints(0, width, 0, contentHeight);
                var childSize = Measure(child, constraints);
                Layout(child, currentX, contentY, childSize.Width, childSize.Height);
                currentX += childSize.Width + stack.Spacing;
            }
            else
            {
                currentX += childLayout.Width + stack.Spacing;
            }
        }
    }

    private void LayoutContainer(Container container, double x, double y, double width, double height)
    {
        var padding = container.Padding ?? EdgeInsets.Zero;
        
        foreach (var child in container.Children)
        {
            var childX = x + padding.Left;
            var childY = y + padding.Top;
            var childWidth = width - padding.Horizontal;
            var childHeight = height - padding.Vertical;
            
            Layout(child, childX, childY, childWidth, childHeight);
        }
    }

    #endregion

    #region Paint Methods

    private void PaintText(Text text, LayoutInfo layout)
    {
        if (_canvas == null) return;

        using var paint = new SKPaint
        {
            Color = ToSkColor(text.Color ?? Color.Black),
            TextSize = (float)(text.FontSize ?? 16),
            IsAntialias = true,
            FakeBoldText = text.FontWeight == FontWeight.Bold
        };

        _canvas.DrawText(
            text.Content,
            (float)layout.X,
            (float)(layout.Y + layout.Height * 0.75), // Baseline adjustment
            paint
        );
    }

    private void PaintButton(Button button, LayoutInfo layout)
    {
        if (_canvas == null) return;

        // Determine colors based on variant
        var bgColor = button.Variant switch
        {
            ButtonVariant.Primary => Color.FromHex("#3B82F6"),
            ButtonVariant.Secondary => Color.FromHex("#64748B"),
            ButtonVariant.Destructive => Color.FromHex("#EF4444"),
            ButtonVariant.Ghost => Color.Transparent,
            _ => Color.FromHex("#E5E7EB")
        };

        var textColor = button.Variant switch
        {
            ButtonVariant.Primary => Color.White,
            ButtonVariant.Secondary => Color.White,
            ButtonVariant.Destructive => Color.White,
            ButtonVariant.Ghost => Color.FromHex("#374151"),
            _ => Color.FromHex("#374151")
        };

        // Draw background
        if (bgColor != Color.Transparent)
        {
            using var bgPaint = new SKPaint
            {
                Color = ToSkColor(bgColor),
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            var rect = new SKRect(
                (float)layout.X,
                (float)layout.Y,
                (float)(layout.X + layout.Width),
                (float)(layout.Y + layout.Height)
            );

            _canvas.DrawRoundRect(rect, 6, 6, bgPaint);
        }

        // Draw border for ghost buttons
        if (button.Variant == ButtonVariant.Ghost)
        {
            using var borderPaint = new SKPaint
            {
                Color = ToSkColor(Color.FromHex("#D1D5DB")),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                IsAntialias = true
            };

            var rect = new SKRect(
                (float)layout.X,
                (float)layout.Y,
                (float)(layout.X + layout.Width),
                (float)(layout.Y + layout.Height)
            );

            _canvas.DrawRoundRect(rect, 6, 6, borderPaint);
        }

        // Draw text
        var textElement = button.Children.OfType<Text>().FirstOrDefault();
        if (textElement != null)
        {
            using var textPaint = new SKPaint
            {
                Color = ToSkColor(textColor),
                TextSize = 16,
                IsAntialias = true,
                TextAlign = SKTextAlign.Center
            };

            _canvas.DrawText(
                textElement.Content,
                (float)(layout.X + layout.Width / 2),
                (float)(layout.Y + layout.Height / 2 + 5),
                textPaint
            );
        }
    }

    private void PaintTextField(TextField textField, LayoutInfo layout)
    {
        if (_canvas == null) return;

        // Draw background
        using var bgPaint = new SKPaint
        {
            Color = SKColors.White,
            Style = SKPaintStyle.Fill,
            IsAntialias = true
        };

        var rect = new SKRect(
            (float)layout.X,
            (float)layout.Y,
            (float)(layout.X + layout.Width),
            (float)(layout.Y + layout.Height)
        );

        _canvas.DrawRoundRect(rect, 4, 4, bgPaint);

        // Draw border
        using var borderPaint = new SKPaint
        {
            Color = ToSkColor(textField.HasError 
                ? Color.FromHex("#EF4444") 
                : Color.FromHex("#D1D5DB")),
            Style = SKPaintStyle.Stroke,
            StrokeWidth = textField.HasError ? 2 : 1,
            IsAntialias = true
        };

        _canvas.DrawRoundRect(rect, 4, 4, borderPaint);

        // Draw text or placeholder
        using var textPaint = new SKPaint
        {
            Color = string.IsNullOrEmpty(textField.Value)
                ? SKColors.Gray
                : SKColors.Black,
            TextSize = 14,
            IsAntialias = true
        };

        var displayText = string.IsNullOrEmpty(textField.Value)
            ? (textField.Placeholder ?? "")
            : (textField.Type == TextFieldType.Password 
                ? new string('•', textField.Value.Length)
                : textField.Value);

        _canvas.DrawText(
            displayText,
            (float)(layout.X + 8),
            (float)(layout.Y + layout.Height / 2 + 5),
            textPaint
        );
    }

    private void PaintContainer(Container container, LayoutInfo layout)
    {
        if (_canvas == null) return;

        // Draw background if specified
        if (container.Background != null)
        {
            using var bgPaint = new SKPaint
            {
                Color = ToSkColor(container.Background.Value),
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            var rect = new SKRect(
                (float)layout.X,
                (float)layout.Y,
                (float)(layout.X + layout.Width),
                (float)(layout.Y + layout.Height)
            );

            _canvas.DrawRect(rect, bgPaint);
        }
    }

    #endregion

    #region Helper Methods

    private SKColor ToSkColor(Color color)
    {
        return new SKColor(
            color.R,
            color.G,
            color.B,
            (byte)(color.A * 255)
        );
    }

    #endregion

    private class LayoutInfo
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
