using ZorUI.Core;
using ZorUI.Core.Properties;

namespace ZorUI.Rendering;

/// <summary>
/// Interface for rendering ZorUI elements to various platforms.
/// Implementations can target different backends (SkiaSharp, Console, HTML, etc.).
/// </summary>
public interface IRenderer
{
    /// <summary>
    /// Renders a ZorUI element tree.
    /// </summary>
    /// <param name="root">Root element to render.</param>
    void Render(ZorElement root);

    /// <summary>
    /// Measures the size of an element.
    /// </summary>
    /// <param name="element">Element to measure.</param>
    /// <param name="constraints">Size constraints.</param>
    /// <returns>Measured size.</returns>
    Size Measure(ZorElement element, SizeConstraints constraints);

    /// <summary>
    /// Lays out an element at a specific position and size.
    /// </summary>
    /// <param name="element">Element to layout.</param>
    /// <param name="x">X position.</param>
    /// <param name="y">Y position.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    void Layout(ZorElement element, double x, double y, double width, double height);

    /// <summary>
    /// Begins a new frame.
    /// </summary>
    void BeginFrame();

    /// <summary>
    /// Ends the current frame and presents it.
    /// </summary>
    void EndFrame();

    /// <summary>
    /// Clears the rendering surface.
    /// </summary>
    void Clear();
}
