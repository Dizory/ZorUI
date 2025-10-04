using ZorUI.Core;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;

namespace ZorUI.Components.DataDisplay;

/// <summary>
/// Alert component for displaying important messages.
/// Similar to Material Alert or Bootstrap Alert.
/// </summary>
public class Alert : Container
{
    /// <summary>
    /// Title of the alert.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Message/description of the alert.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Variant of the alert.
    /// </summary>
    public AlertVariant Variant { get; set; } = AlertVariant.Info;

    /// <summary>
    /// Whether the alert can be dismissed.
    /// </summary>
    public bool IsDismissible { get; set; } = false;

    /// <summary>
    /// Whether the alert is currently visible.
    /// </summary>
    public bool IsVisible { get; set; } = true;

    /// <summary>
    /// Icon to display (optional).
    /// </summary>
    public ZorElement? Icon { get; set; }

    /// <summary>
    /// Action element (e.g., a button).
    /// </summary>
    public ZorElement? Action { get; set; }

    /// <summary>
    /// Called when the alert is dismissed.
    /// </summary>
    public Action? OnDismiss { get; set; }

    /// <summary>
    /// Creates a new alert.
    /// </summary>
    public Alert(string message, string? title = null)
    {
        Message = message;
        Title = title;
    }

    // Fluent API

    public Alert WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public Alert WithVariant(AlertVariant variant)
    {
        Variant = variant;
        return this;
    }

    public Alert Info()
    {
        Variant = AlertVariant.Info;
        return this;
    }

    public Alert Success()
    {
        Variant = AlertVariant.Success;
        return this;
    }

    public Alert Warning()
    {
        Variant = AlertVariant.Warning;
        return this;
    }

    public Alert Error()
    {
        Variant = AlertVariant.Error;
        return this;
    }

    public Alert Dismissible(bool dismissible = true)
    {
        IsDismissible = dismissible;
        return this;
    }

    public Alert WithIcon(ZorElement icon)
    {
        Icon = icon;
        return this;
    }

    public Alert WithAction(ZorElement action)
    {
        Action = action;
        return this;
    }

    public Alert WithOnDismiss(Action onDismiss)
    {
        OnDismiss = onDismiss;
        return this;
    }

    /// <summary>
    /// Dismisses the alert.
    /// </summary>
    public void Dismiss()
    {
        IsVisible = false;
        OnDismiss?.Invoke();
        MarkNeedsRebuild();
    }
}
