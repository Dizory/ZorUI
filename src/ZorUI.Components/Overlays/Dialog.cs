using ZorUI.Core;
using ZorUI.Components.Layout;

namespace ZorUI.Components.Overlays;

/// <summary>
/// Modal dialog component for displaying content over the main UI.
/// Similar to Radix UI Dialog or HTML dialog element.
/// </summary>
public class Dialog : Container
{
    /// <summary>
    /// Whether the dialog is currently open.
    /// </summary>
    public bool IsOpen { get; set; } = false;

    /// <summary>
    /// Title of the dialog.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Description text for the dialog (for accessibility).
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Whether clicking outside the dialog should close it.
    /// </summary>
    public bool CloseOnOutsideClick { get; set; } = true;

    /// <summary>
    /// Whether pressing Escape key should close the dialog.
    /// </summary>
    public bool CloseOnEscape { get; set; } = true;

    /// <summary>
    /// Whether to show a close button.
    /// </summary>
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// Called when the dialog is opened.
    /// </summary>
    public Action? OnOpen { get; set; }

    /// <summary>
    /// Called when the dialog is closed.
    /// </summary>
    public Action? OnClose { get; set; }

    /// <summary>
    /// Content to display in the dialog.
    /// </summary>
    public ZorElement? Content { get; set; }

    /// <summary>
    /// Footer content (typically buttons).
    /// </summary>
    public ZorElement? Footer { get; set; }

    /// <summary>
    /// Creates a new dialog.
    /// </summary>
    public Dialog(string? title = null)
    {
        Title = title;
    }

    // Fluent API

    public Dialog WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public Dialog WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public Dialog WithContent(ZorElement content)
    {
        Content = content;
        return this;
    }

    public Dialog WithFooter(ZorElement footer)
    {
        Footer = footer;
        return this;
    }

    public Dialog WithCloseOnOutsideClick(bool close = true)
    {
        CloseOnOutsideClick = close;
        return this;
    }

    public Dialog WithCloseOnEscape(bool close = true)
    {
        CloseOnEscape = close;
        return this;
    }

    public Dialog WithShowCloseButton(bool show = true)
    {
        ShowCloseButton = show;
        return this;
    }

    public Dialog WithOnOpen(Action onOpen)
    {
        OnOpen = onOpen;
        return this;
    }

    public Dialog WithOnClose(Action onClose)
    {
        OnClose = onClose;
        return this;
    }

    /// <summary>
    /// Opens the dialog.
    /// </summary>
    public void Open()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            OnOpen?.Invoke();
            MarkNeedsRebuild();
        }
    }

    /// <summary>
    /// Closes the dialog.
    /// </summary>
    public void Close()
    {
        if (IsOpen)
        {
            IsOpen = false;
            OnClose?.Invoke();
            MarkNeedsRebuild();
        }
    }

    /// <summary>
    /// Toggles the dialog open/closed state.
    /// </summary>
    public void Toggle()
    {
        if (IsOpen)
            Close();
        else
            Open();
    }
}

/// <summary>
/// Alert dialog for confirmations and important messages.
/// Similar to Radix UI AlertDialog.
/// </summary>
public class AlertDialog : Dialog
{
    /// <summary>
    /// Variant of the alert (info, warning, error, success).
    /// </summary>
    public AlertVariant Variant { get; set; } = AlertVariant.Info;

    /// <summary>
    /// Creates a new alert dialog.
    /// </summary>
    public AlertDialog(string? title = null) : base(title)
    {
        CloseOnOutsideClick = false; // Alert dialogs require explicit action
    }

    // Fluent API

    public AlertDialog WithVariant(AlertVariant variant)
    {
        Variant = variant;
        return this;
    }

    public AlertDialog Info()
    {
        Variant = AlertVariant.Info;
        return this;
    }

    public AlertDialog Warning()
    {
        Variant = AlertVariant.Warning;
        return this;
    }

    public AlertDialog Error()
    {
        Variant = AlertVariant.Error;
        return this;
    }

    public AlertDialog Success()
    {
        Variant = AlertVariant.Success;
        return this;
    }
}

/// <summary>
/// Alert dialog variants.
/// </summary>
public enum AlertVariant
{
    Info,
    Warning,
    Error,
    Success
}
