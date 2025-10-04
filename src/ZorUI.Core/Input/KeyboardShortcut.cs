namespace ZorUI.Core.Input;

/// <summary>
/// Represents a keyboard shortcut.
/// </summary>
public class KeyboardShortcut
{
    public Keys Key { get; set; }
    public ModifierKeys Modifiers { get; set; }
    public Action? Action { get; set; }
    public string? Description { get; set; }

    /// <summary>
    /// Parse shortcut string like "Ctrl+S" or "Ctrl+Shift+P".
    /// </summary>
    public static KeyboardShortcut Parse(string shortcut, Action action, string? description = null)
    {
        var parts = shortcut.Split('+');
        var modifiers = ModifierKeys.None;
        var key = Keys.None;

        foreach (var part in parts)
        {
            var normalized = part.Trim().ToLowerInvariant();
            
            if (normalized == "ctrl" || normalized == "control")
                modifiers |= ModifierKeys.Control;
            else if (normalized == "shift")
                modifiers |= ModifierKeys.Shift;
            else if (normalized == "alt")
                modifiers |= ModifierKeys.Alt;
            else if (normalized == "cmd" || normalized == "command" || normalized == "meta")
                modifiers |= ModifierKeys.Command;
            else if (Enum.TryParse<Keys>(part, true, out var parsedKey))
                key = parsedKey;
        }

        return new KeyboardShortcut
        {
            Key = key,
            Modifiers = modifiers,
            Action = action,
            Description = description
        };
    }

    /// <summary>
    /// Check if this shortcut matches the given key event.
    /// </summary>
    public bool Matches(Keys key, ModifierKeys modifiers)
    {
        return Key == key && Modifiers == modifiers;
    }

    /// <summary>
    /// Get display string for this shortcut.
    /// </summary>
    public string ToDisplayString()
    {
        var parts = new List<string>();

        if (Modifiers.HasFlag(ModifierKeys.Control))
            parts.Add("Ctrl");
        if (Modifiers.HasFlag(ModifierKeys.Shift))
            parts.Add("Shift");
        if (Modifiers.HasFlag(ModifierKeys.Alt))
            parts.Add("Alt");
        if (Modifiers.HasFlag(ModifierKeys.Command))
            parts.Add("Cmd");

        parts.Add(Key.ToString());

        return string.Join("+", parts);
    }
}

/// <summary>
/// Manager for keyboard shortcuts.
/// </summary>
public class KeyboardShortcutManager
{
    private readonly List<KeyboardShortcut> _shortcuts = new();
    private readonly Dictionary<string, List<KeyboardShortcut>> _contextShortcuts = new();
    private string? _currentContext;

    /// <summary>
    /// Register a global shortcut.
    /// </summary>
    public void Register(string shortcut, Action action, string? description = null)
    {
        _shortcuts.Add(KeyboardShortcut.Parse(shortcut, action, description));
    }

    /// <summary>
    /// Register a context-specific shortcut.
    /// </summary>
    public void RegisterInContext(string context, string shortcut, Action action, string? description = null)
    {
        if (!_contextShortcuts.ContainsKey(context))
        {
            _contextShortcuts[context] = new();
        }

        _contextShortcuts[context].Add(KeyboardShortcut.Parse(shortcut, action, description));
    }

    /// <summary>
    /// Set current context.
    /// </summary>
    public void SetContext(string? context)
    {
        _currentContext = context;
    }

    /// <summary>
    /// Handle key event.
    /// </summary>
    public bool HandleKeyEvent(Keys key, ModifierKeys modifiers)
    {
        // Try context shortcuts first
        if (_currentContext != null && _contextShortcuts.TryGetValue(_currentContext, out var contextShortcuts))
        {
            foreach (var shortcut in contextShortcuts)
            {
                if (shortcut.Matches(key, modifiers))
                {
                    shortcut.Action?.Invoke();
                    return true;
                }
            }
        }

        // Try global shortcuts
        foreach (var shortcut in _shortcuts)
        {
            if (shortcut.Matches(key, modifiers))
            {
                shortcut.Action?.Invoke();
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Get all registered shortcuts.
    /// </summary>
    public List<KeyboardShortcut> GetAllShortcuts()
    {
        var all = new List<KeyboardShortcut>(_shortcuts);

        foreach (var contextShortcuts in _contextShortcuts.Values)
        {
            all.AddRange(contextShortcuts);
        }

        return all;
    }

    /// <summary>
    /// Unregister a shortcut by its display string.
    /// </summary>
    public void Unregister(string shortcut)
    {
        _shortcuts.RemoveAll(s => s.ToDisplayString() == shortcut);

        foreach (var context in _contextShortcuts.Values)
        {
            context.RemoveAll(s => s.ToDisplayString() == shortcut);
        }
    }
}

/// <summary>
/// Keyboard keys enumeration.
/// </summary>
public enum Keys
{
    None = 0,
    
    // Letters
    A, B, C, D, E, F, G, H, I, J, K, L, M,
    N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
    
    // Numbers
    D0, D1, D2, D3, D4, D5, D6, D7, D8, D9,
    
    // Function keys
    F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,
    
    // Special keys
    Space, Enter, Escape, Tab, Backspace, Delete,
    Home, End, PageUp, PageDown,
    Left, Right, Up, Down,
    Insert,
    
    // Punctuation
    Comma, Period, Slash, Backslash,
    Semicolon, Quote, Backtick,
    LeftBracket, RightBracket,
    Minus, Equal
}

/// <summary>
/// Modifier keys.
/// </summary>
[Flags]
public enum ModifierKeys
{
    None = 0,
    Control = 1,
    Shift = 2,
    Alt = 4,
    Command = 8  // For macOS
}
