namespace ZorUI.Core;

/// <summary>
/// Build context provides access to framework services and information
/// about the current location in the UI tree.
/// Similar to Flutter's BuildContext.
/// </summary>
public class BuildContext
{
    /// <summary>
    /// The scheduler responsible for managing rebuilds.
    /// </summary>
    public RebuildScheduler Scheduler { get; }

    /// <summary>
    /// The theme currently active in this context.
    /// </summary>
    public object? Theme { get; set; }

    /// <summary>
    /// Services available in this context.
    /// </summary>
    public Dictionary<Type, object> Services { get; } = new();

    /// <summary>
    /// Creates a new build context.
    /// </summary>
    public BuildContext(RebuildScheduler scheduler)
    {
        Scheduler = scheduler;
    }

    /// <summary>
    /// Gets a service from the context.
    /// </summary>
    /// <typeparam name="T">Type of service to retrieve.</typeparam>
    /// <returns>The service instance or null if not found.</returns>
    public T? GetService<T>() where T : class
    {
        if (Services.TryGetValue(typeof(T), out var service))
        {
            return service as T;
        }
        return null;
    }

    /// <summary>
    /// Registers a service in the context.
    /// </summary>
    /// <typeparam name="T">Type of service.</typeparam>
    /// <param name="service">Service instance.</param>
    public void RegisterService<T>(T service) where T : class
    {
        Services[typeof(T)] = service;
    }
}
