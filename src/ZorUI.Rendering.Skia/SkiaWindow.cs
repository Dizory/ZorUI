using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Windows.Forms;
using ZorUI.Core;
using ZorUI.Core.Properties;

namespace ZorUI.Rendering.Skia;

/// <summary>
/// Cross-platform window for displaying ZorUI applications.
/// Uses SkiaSharp for rendering on Windows, Linux, and macOS.
/// </summary>
public class SkiaWindow : Form
{
    private readonly SKGLControl _skControl;
    private readonly SkiaRenderer _renderer;
    private ZorComponent? _rootComponent;
    private DateTime _lastFrameTime = DateTime.Now;

    /// <summary>
    /// Gets or sets the root component of the application.
    /// </summary>
    public ZorComponent? RootComponent
    {
        get => _rootComponent;
        set
        {
            _rootComponent = value;
            if (_rootComponent != null)
            {
                _rootComponent.Context = new BuildContext(new RebuildScheduler());
                _rootComponent.OnMount();
            }
            Invalidate();
        }
    }

    /// <summary>
    /// Gets the renderer used by this window.
    /// </summary>
    public SkiaRenderer Renderer => _renderer;

    /// <summary>
    /// Creates a new SkiaWindow with specified dimensions and title.
    /// </summary>
    public SkiaWindow(int width, int height, string title)
    {
        Text = title;
        ClientSize = new System.Drawing.Size(width, height);
        StartPosition = FormStartPosition.CenterScreen;

        _renderer = new SkiaRenderer
        {
            SurfaceSize = new Size(width, height)
        };

        _skControl = new SKGLControl
        {
            Dock = DockStyle.Fill
        };

        _skControl.PaintSurface += OnPaintSurface;
        _skControl.Resize += (s, e) => OnResize();
        
        Controls.Add(_skControl);

        // Setup animation loop
        var timer = new System.Windows.Forms.Timer { Interval = 16 }; // ~60 FPS
        timer.Tick += (s, e) => _skControl.Invalidate();
        timer.Start();
    }

    private void OnResize()
    {
        _renderer.SurfaceSize = new Size(ClientSize.Width, ClientSize.Height);
        _skControl.Invalidate();
    }

    private void OnPaintSurface(object? sender, SKPaintGLSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.White);

        if (_rootComponent != null)
        {
            try
            {
                // Build UI
                var ui = _rootComponent.Build();
                
                // Render
                _renderer.RenderToCanvas(canvas, ui);
            }
            catch (Exception ex)
            {
                // Draw error message
                using var paint = new SKPaint
                {
                    Color = SKColors.Red,
                    TextSize = 16,
                    IsAntialias = true
                };

                canvas.DrawText(
                    $"Error: {ex.Message}",
                    10, 30,
                    paint
                );
            }
        }

        // Draw FPS counter
        DrawFpsCounter(canvas);
    }

    private void DrawFpsCounter(SKCanvas canvas)
    {
        var now = DateTime.Now;
        var fps = 1000.0 / (now - _lastFrameTime).TotalMilliseconds;
        _lastFrameTime = now;

        using var paint = new SKPaint
        {
            Color = SKColors.Gray,
            TextSize = 12,
            IsAntialias = true
        };

        canvas.DrawText(
            $"FPS: {fps:F1}",
            10,
            ClientSize.Height - 10,
            paint
        );
    }

    /// <summary>
    /// Shows the window and starts the application loop.
    /// </summary>
    public new void Show()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(this);
    }
}
