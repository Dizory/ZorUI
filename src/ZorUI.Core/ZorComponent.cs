namespace ZorUI.Core;

/// <summary>
/// Base class for stateful components in ZorUI.
/// Components manage their own state and can rebuild when state changes.
/// Inspired by React's Component and Flutter's StatefulWidget.
/// </summary>
public abstract class ZorComponent : ZorElement
{
    private readonly Dictionary<string, object?> _state = new();
    private bool _isMounted = false;

    /// <summary>
    /// Indicates whether this component is currently mounted.
    /// </summary>
    public bool IsMounted => _isMounted;

    /// <summary>
    /// Build method that returns the UI tree for this component.
    /// This is where you define your component's structure using the fluent API.
    /// </summary>
    /// <returns>The root element of this component's UI tree.</returns>
    public abstract ZorElement Build();

    /// <summary>
    /// Gets the current value of a state property.
    /// </summary>
    /// <typeparam name="T">Type of the state value.</typeparam>
    /// <param name="key">State key.</param>
    /// <param name="defaultValue">Default value if key doesn't exist.</param>
    /// <returns>The state value or default.</returns>
    protected T? GetState<T>(string key, T? defaultValue = default)
    {
        if (_state.TryGetValue(key, out var value))
        {
            return value is T typedValue ? typedValue : defaultValue;
        }
        return defaultValue;
    }

    /// <summary>
    /// Sets a state property and triggers a rebuild.
    /// This is the primary way to update component state.
    /// </summary>
    /// <typeparam name="T">Type of the state value.</typeparam>
    /// <param name="key">State key.</param>
    /// <param name="value">New state value.</param>
    protected void SetState<T>(string key, T value)
    {
        var oldValue = _state.TryGetValue(key, out var old) ? old : default;
        
        if (!EqualityComparer<object?>.Default.Equals(oldValue, value))
        {
            _state[key] = value;
            if (_isMounted)
            {
                MarkNeedsRebuild();
            }
        }
    }

    /// <summary>
    /// Updates state using an action and triggers a rebuild.
    /// Convenient for inline state updates.
    /// </summary>
    /// <param name="updateAction">Action that updates the state.</param>
    protected void SetState(Action updateAction)
    {
        updateAction();
        if (_isMounted)
        {
            MarkNeedsRebuild();
        }
    }

    /// <summary>
    /// Called when the component is first mounted.
    /// Override to perform initialization.
    /// </summary>
    public override void OnMount()
    {
        _isMounted = true;
        base.OnMount();
    }

    /// <summary>
    /// Called when the component is unmounted.
    /// Override to perform cleanup.
    /// </summary>
    public override void OnUnmount()
    {
        _isMounted = false;
        base.OnUnmount();
    }

    /// <summary>
    /// Lifecycle method called before the component builds.
    /// Override to prepare state or data before building.
    /// </summary>
    protected virtual void BeforeBuild()
    {
    }

    /// <summary>
    /// Lifecycle method called after the component builds.
    /// Override to perform actions after the UI tree is created.
    /// </summary>
    protected virtual void AfterBuild()
    {
    }
}
