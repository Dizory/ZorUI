using ZorUI.Core;
using ZorUI.Components.Layout;

namespace ZorUI.Components.Overlays;

/// <summary>
/// Toast notification component for temporary messages.
/// Similar to Radix UI Toast or Material Snackbar.
/// </summary>
public class Toast : Container
{
    /// <summary>
    /// Title of the toast.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Description/message of the toast.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Variant of the toast (visual style).
    /// </summary>
    public ToastVariant Variant { get; set; } = ToastVariant.Default;

    /// <summary>
    /// Duration in milliseconds before auto-closing (null = no auto-close).
    /// </summary>
    public int? Duration { get; set; } = 5000;

    /// <summary>
    /// Whether the toast is currently visible.
    /// </summary>
    public bool IsVisible { get; set; } = false;

    /// <summary>
    /// Whether to show a close button.
    /// </summary>
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// Action element (e.g., a button).
    /// </summary>
    public ZorElement? Action { get; set; }

    /// <summary>
    /// Called when the toast is closed.
    /// </summary>
    public Action? OnClose { get; set; }

    /// <summary>
    /// Creates a new toast.
    /// </summary>
    public Toast(string message, string? title = null)
    {
        Message = message;
        Title = title;
    }

    // Fluent API

    public Toast WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public Toast WithVariant(ToastVariant variant)
    {
        Variant = variant;
        return this;
    }

    public Toast Success()
    {
        Variant = ToastVariant.Success;
        return this;
    }

    public Toast Error()
    {
        Variant = ToastVariant.Error;
        return this;
    }

    public Toast Warning()
    {
        Variant = ToastVariant.Warning;
        return this;
    }

    public Toast Info()
    {
        Variant = ToastVariant.Info;
        return this;
    }

    public Toast WithDuration(int durationMs)
    {
        Duration = durationMs;
        return this;
    }

    public Toast Persistent()
    {
        Duration = null;
        return this;
    }

    public Toast WithAction(ZorElement action)
    {
        Action = action;
        return this;
    }

    public Toast WithOnClose(Action onClose)
    {
        OnClose = onClose;
        return this;
    }

    /// <summary>
    /// Shows the toast.
    /// </summary>
    public void Show()
    {
        IsVisible = true;
        MarkNeedsRebuild();

        if (Duration.HasValue)
        {
            Task.Delay(Duration.Value).ContinueWith(_ => Close());
        }
    }

    /// <summary>
    /// Closes the toast.
    /// </summary>
    public void Close()
    {
        IsVisible = false;
        OnClose?.Invoke();
        MarkNeedsRebuild();
    }
}

/// <summary>
/// Toast notification variants.
/// </summary>
public enum ToastVariant
{
    Default,
    Success,
    Error,
    Warning,
    Info
}

/// <summary>
/// Toast manager for displaying and managing multiple toasts.
/// </summary>
public class ToastManager
{
    private readonly List<Toast> _toasts = new();
    private static ToastManager? _instance;

    /// <summary>
    /// Gets the singleton instance of the toast manager.
    /// </summary>
    public static ToastManager Instance => _instance ??= new ToastManager();

    /// <summary>
    /// Currently active toasts.
    /// </summary>
    public IReadOnlyList<Toast> Toasts => _toasts.AsReadOnly();

    /// <summary>
    /// Maximum number of visible toasts.
    /// </summary>
    public int MaxToasts { get; set; } = 5;

    /// <summary>
    /// Shows a toast notification.
    /// </summary>
    public void Show(Toast toast)
    {
        // Remove oldest toast if at maximum
        if (_toasts.Count >= MaxToasts)
        {
            _toasts[0].Close();
            _toasts.RemoveAt(0);
        }

        toast.OnClose = () => _toasts.Remove(toast);
        _toasts.Add(toast);
        toast.Show();
    }

    /// <summary>
    /// Shows a simple success toast.
    /// </summary>
    public void ShowSuccess(string message, string? title = null)
    {
        Show(new Toast(message, title).Success());
    }

    /// <summary>
    /// Shows a simple error toast.
    /// </summary>
    public void ShowError(string message, string? title = null)
    {
        Show(new Toast(message, title).Error());
    }

    /// <summary>
    /// Shows a simple warning toast.
    /// </summary>
    public void ShowWarning(string message, string? title = null)
    {
        Show(new Toast(message, title).Warning());
    }

    /// <summary>
    /// Shows a simple info toast.
    /// </summary>
    public void ShowInfo(string message, string? title = null)
    {
        Show(new Toast(message, title).Info());
    }

    /// <summary>
    /// Clears all toasts.
    /// </summary>
    public void ClearAll()
    {
        foreach (var toast in _toasts.ToList())
        {
            toast.Close();
        }
        _toasts.Clear();
    }
}
