using System.Text;
using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Components.Forms;

namespace ZorUI.Rendering;

/// <summary>
/// Simple console-based renderer for debugging and testing.
/// Renders ZorUI elements as ASCII art in the console.
/// </summary>
public class ConsoleRenderer : IRenderer
{
    private readonly StringBuilder _buffer = new();
    private int _cursorX = 0;
    private int _cursorY = 0;

    public void Render(ZorElement root)
    {
        BeginFrame();
        RenderElement(root, 0, 0);
        EndFrame();
    }

    private void RenderElement(ZorElement element, int indent, int depth)
    {
        var indentStr = new string(' ', indent * 2);

        // Render based on element type
        switch (element)
        {
            case Text text:
                WriteLine($"{indentStr}[Text] {text.Content}", ConsoleColor.White);
                break;

            case Button button:
                var buttonText = button.Children.OfType<Text>().FirstOrDefault()?.Content ?? "Button";
                WriteLine($"{indentStr}[Button:{button.Variant}] {buttonText}", ConsoleColor.Cyan);
                break;

            case VStack vstack:
                WriteLine($"{indentStr}[VStack] spacing: {vstack.Spacing}", ConsoleColor.Yellow);
                break;

            case HStack hstack:
                WriteLine($"{indentStr}[HStack] spacing: {hstack.Spacing}", ConsoleColor.Yellow);
                break;

            case TextField textField:
                WriteLine($"{indentStr}[TextField] {textField.Placeholder ?? textField.Value}", ConsoleColor.Green);
                break;

            case Checkbox checkbox:
                var check = checkbox.IsChecked ? "☑" : "☐";
                WriteLine($"{indentStr}[Checkbox] {check} {checkbox.Label}", ConsoleColor.Green);
                break;

            case Switch switchEl:
                var sw = switchEl.IsOn ? "ON" : "OFF";
                WriteLine($"{indentStr}[Switch] [{sw}] {switchEl.Label}", ConsoleColor.Green);
                break;

            default:
                WriteLine($"{indentStr}[{element.GetType().Name}]", ConsoleColor.Gray);
                break;
        }

        // Render children
        foreach (var child in element.Children)
        {
            RenderElement(child, indent + 1, depth + 1);
        }
    }

    private void WriteLine(string text, ConsoleColor color = ConsoleColor.White)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        _buffer.AppendLine(text);
        Console.WriteLine(text);
        Console.ForegroundColor = prevColor;
    }

    public Size Measure(ZorElement element, SizeConstraints constraints)
    {
        // Simple measurement - return fixed size for now
        return new Size(100, 20);
    }

    public void Layout(ZorElement element, double x, double y, double width, double height)
    {
        // Store layout information in metadata
        element.Metadata["LayoutX"] = x;
        element.Metadata["LayoutY"] = y;
        element.Metadata["LayoutWidth"] = width;
        element.Metadata["LayoutHeight"] = height;
    }

    public void BeginFrame()
    {
        _buffer.Clear();
        Console.Clear();
        WriteLine("=== ZorUI Console Renderer ===", ConsoleColor.Magenta);
        WriteLine("");
    }

    public void EndFrame()
    {
        WriteLine("");
        WriteLine("==============================", ConsoleColor.Magenta);
    }

    public void Clear()
    {
        Console.Clear();
        _buffer.Clear();
    }
}
