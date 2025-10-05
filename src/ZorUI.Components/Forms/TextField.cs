using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;

namespace ZorUI.Components.Forms;

/// <summary>
/// Text input field component.
/// Similar to Radix UI TextField or HTML input[type="text"].
/// </summary>
public class TextField : Container
{
    /// <summary>
    /// Current value of the text field.
    /// </summary>
    public string Value { get; set; } = "";

    /// <summary>
    /// Placeholder text shown when empty.
    /// </summary>
    public string? Placeholder { get; set; }

    /// <summary>
    /// Input type (text, password, email, etc.).
    /// </summary>
    public TextFieldType Type { get; set; } = TextFieldType.Text;

    /// <summary>
    /// Whether the field is disabled.
    /// </summary>
    public bool IsDisabled { get; set; } = false;

    /// <summary>
    /// Whether the field is read-only.
    /// </summary>
    public bool IsReadOnly { get; set; } = false;

    /// <summary>
    /// Whether the field is required.
    /// </summary>
    public bool IsRequired { get; set; } = false;

    /// <summary>
    /// Whether the field has an error.
    /// </summary>
    public bool HasError { get; set; } = false;

    /// <summary>
    /// Error message to display.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Maximum length of input.
    /// </summary>
    public int? MaxLength { get; set; }

    /// <summary>
    /// Called when the value changes.
    /// </summary>
    public Action<string>? OnChange { get; set; }

    /// <summary>
    /// Called when the field receives focus.
    /// </summary>
    public Action? OnFocus { get; set; }

    /// <summary>
    /// Called when the field loses focus.
    /// </summary>
    public Action? OnBlur { get; set; }

    /// <summary>
    /// Icon to display at the start of the field.
    /// </summary>
    public ZorElement? LeadingIcon { get; set; }

    /// <summary>
    /// Icon to display at the end of the field.
    /// </summary>
    public ZorElement? TrailingIcon { get; set; }

    /// <summary>
    /// Creates a new text field.
    /// </summary>
    public TextField(string? placeholder = null)
    {
        Placeholder = placeholder;
    }

    // Fluent API

    public TextField WithValue(string value)
    {
        Value = value;
        return this;
    }

    public TextField WithPlaceholder(string placeholder)
    {
        Placeholder = placeholder;
        return this;
    }

    public TextField WithType(TextFieldType type)
    {
        Type = type;
        return this;
    }

    public TextField Password()
    {
        Type = TextFieldType.Password;
        return this;
    }

    public TextField Email()
    {
        Type = TextFieldType.Email;
        return this;
    }

    public TextField Number()
    {
        Type = TextFieldType.Number;
        return this;
    }

    public TextField Disabled(bool disabled = true)
    {
        IsDisabled = disabled;
        return this;
    }

    public TextField ReadOnly(bool readOnly = true)
    {
        IsReadOnly = readOnly;
        return this;
    }

    public TextField Required(bool required = true)
    {
        IsRequired = required;
        return this;
    }

    public TextField WithError(string? errorMessage = null)
    {
        HasError = errorMessage != null;
        ErrorMessage = errorMessage;
        return this;
    }

    public TextField WithMaxLength(int maxLength)
    {
        MaxLength = maxLength;
        return this;
    }

    public TextField WithOnChange(Action<string> onChange)
    {
        OnChange = onChange;
        return this;
    }

    public TextField WithLeadingIcon(ZorElement icon)
    {
        LeadingIcon = icon;
        return this;
    }

    public TextField WithTrailingIcon(ZorElement icon)
    {
        TrailingIcon = icon;
        return this;
    }
}

/// <summary>
/// Text field input types.
/// </summary>
public enum TextFieldType
{
    Text,
    Password,
    Email,
    Number,
    Tel,
    Url,
    Search,
    Date,
    Time,
    DateTime
}
