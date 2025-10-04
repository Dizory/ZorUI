namespace ZorUI.Core.Validation;

/// <summary>
/// Form validation helper.
/// </summary>
public class FormValidator
{
    private readonly Dictionary<string, List<ValidationRule>> _rules = new();
    private readonly Dictionary<string, string?> _errors = new();

    /// <summary>
    /// Add validation rule for a field.
    /// </summary>
    public FormValidator AddRule(string field, ValidationRule rule)
    {
        if (!_rules.ContainsKey(field))
        {
            _rules[field] = new();
        }
        _rules[field].Add(rule);
        return this;
    }

    /// <summary>
    /// Validate a field.
    /// </summary>
    public bool ValidateField(string field, object? value)
    {
        if (!_rules.ContainsKey(field))
            return true;

        _errors.Remove(field);

        foreach (var rule in _rules[field])
        {
            var result = rule.Validate(value);
            if (!result.IsValid)
            {
                _errors[field] = result.ErrorMessage;
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Validate all fields.
    /// </summary>
    public bool ValidateAll(Dictionary<string, object?> values)
    {
        _errors.Clear();
        var isValid = true;

        foreach (var (field, value) in values)
        {
            if (!ValidateField(field, value))
            {
                isValid = false;
            }
        }

        return isValid;
    }

    /// <summary>
    /// Get error for a field.
    /// </summary>
    public string? GetError(string field)
    {
        return _errors.TryGetValue(field, out var error) ? error : null;
    }

    /// <summary>
    /// Check if field has error.
    /// </summary>
    public bool HasError(string field) => _errors.ContainsKey(field);

    /// <summary>
    /// Get all errors.
    /// </summary>
    public Dictionary<string, string?> GetAllErrors() => new(_errors);

    /// <summary>
    /// Check if form is valid.
    /// </summary>
    public bool IsValid => _errors.Count == 0;
}

/// <summary>
/// Base validation rule.
/// </summary>
public abstract class ValidationRule
{
    public string? ErrorMessage { get; set; }

    public abstract ValidationResult Validate(object? value);
}

/// <summary>
/// Validation result.
/// </summary>
public class ValidationResult
{
    public bool IsValid { get; set; }
    public string? ErrorMessage { get; set; }

    public static ValidationResult Success() => new() { IsValid = true };
    public static ValidationResult Error(string message) => new() { IsValid = false, ErrorMessage = message };
}

/// <summary>
/// Common validation rules.
/// </summary>
public static class Rules
{
    public static ValidationRule Required(string? message = null) => new RequiredRule
    {
        ErrorMessage = message ?? "This field is required"
    };

    public static ValidationRule MinLength(int length, string? message = null) => new MinLengthRule(length)
    {
        ErrorMessage = message ?? $"Minimum length is {length}"
    };

    public static ValidationRule MaxLength(int length, string? message = null) => new MaxLengthRule(length)
    {
        ErrorMessage = message ?? $"Maximum length is {length}"
    };

    public static ValidationRule Email(string? message = null) => new EmailRule
    {
        ErrorMessage = message ?? "Invalid email address"
    };

    public static ValidationRule Pattern(string regex, string? message = null) => new PatternRule(regex)
    {
        ErrorMessage = message ?? "Invalid format"
    };

    public static ValidationRule Range(double min, double max, string? message = null) => new RangeRule(min, max)
    {
        ErrorMessage = message ?? $"Value must be between {min} and {max}"
    };
}

// Concrete validation rules

internal class RequiredRule : ValidationRule
{
    public override ValidationResult Validate(object? value)
    {
        var isValid = value switch
        {
            null => false,
            string s => !string.IsNullOrWhiteSpace(s),
            _ => true
        };

        return isValid ? ValidationResult.Success() : ValidationResult.Error(ErrorMessage ?? "Required");
    }
}

internal class MinLengthRule : ValidationRule
{
    private readonly int _minLength;
    public MinLengthRule(int minLength) => _minLength = minLength;

    public override ValidationResult Validate(object? value)
    {
        var str = value?.ToString() ?? "";
        return str.Length >= _minLength 
            ? ValidationResult.Success() 
            : ValidationResult.Error(ErrorMessage ?? $"Min length: {_minLength}");
    }
}

internal class MaxLengthRule : ValidationRule
{
    private readonly int _maxLength;
    public MaxLengthRule(int maxLength) => _maxLength = maxLength;

    public override ValidationResult Validate(object? value)
    {
        var str = value?.ToString() ?? "";
        return str.Length <= _maxLength 
            ? ValidationResult.Success() 
            : ValidationResult.Error(ErrorMessage ?? $"Max length: {_maxLength}");
    }
}

internal class EmailRule : ValidationRule
{
    public override ValidationResult Validate(object? value)
    {
        var str = value?.ToString() ?? "";
        var isValid = System.Text.RegularExpressions.Regex.IsMatch(
            str, 
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
        );
        return isValid 
            ? ValidationResult.Success() 
            : ValidationResult.Error(ErrorMessage ?? "Invalid email");
    }
}

internal class PatternRule : ValidationRule
{
    private readonly string _pattern;
    public PatternRule(string pattern) => _pattern = pattern;

    public override ValidationResult Validate(object? value)
    {
        var str = value?.ToString() ?? "";
        var isValid = System.Text.RegularExpressions.Regex.IsMatch(str, _pattern);
        return isValid 
            ? ValidationResult.Success() 
            : ValidationResult.Error(ErrorMessage ?? "Invalid format");
    }
}

internal class RangeRule : ValidationRule
{
    private readonly double _min;
    private readonly double _max;
    
    public RangeRule(double min, double max)
    {
        _min = min;
        _max = max;
    }

    public override ValidationResult Validate(object? value)
    {
        if (value == null) return ValidationResult.Error("Value is required");
        
        if (double.TryParse(value.ToString(), out var num))
        {
            return num >= _min && num <= _max
                ? ValidationResult.Success()
                : ValidationResult.Error(ErrorMessage ?? $"Range: {_min}-{_max}");
        }

        return ValidationResult.Error("Not a number");
    }
}
