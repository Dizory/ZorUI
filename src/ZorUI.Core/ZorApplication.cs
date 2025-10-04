namespace ZorUI.Core;

/// <summary>
/// The main application class for ZorUI applications.
/// This is the entry point and manages the application lifecycle.
/// </summary>
public class ZorApplication
{
    private ZorComponent? _rootComponent;
    private BuildContext? _buildContext;
    private RebuildScheduler? _scheduler;

    /// <summary>
    /// Gets the current application instance (singleton).
    /// </summary>
    public static ZorApplication? Current { get; private set; }

    /// <summary>
    /// Gets the root component of the application.
    /// </summary>
    public ZorComponent? RootComponent => _rootComponent;

    /// <summary>
    /// Gets the build context for the application.
    /// </summary>
    public BuildContext? BuildContext => _buildContext;

    /// <summary>
    /// Initializes a new ZorUI application.
    /// </summary>
    public ZorApplication()
    {
        Current = this;
        _scheduler = new RebuildScheduler();
        _buildContext = new BuildContext(_scheduler);
    }

    /// <summary>
    /// Runs the application with the specified root component.
    /// </summary>
    /// <param name="rootComponent">The root component of the application.</param>
    public void Run(ZorComponent rootComponent)
    {
        _rootComponent = rootComponent;
        _rootComponent.Context = _buildContext;
        
        // Mount the root component
        _rootComponent.OnMount();
        
        // Initial build
        _scheduler?.ScheduleRebuild(_rootComponent);
    }

    /// <summary>
    /// Shuts down the application gracefully.
    /// </summary>
    public void Shutdown()
    {
        if (_rootComponent != null)
        {
            _rootComponent.Visit(element => element.OnUnmount());
        }
    }

    /// <summary>
    /// Registers a global service that can be accessed throughout the application.
    /// </summary>
    /// <typeparam name="T">Type of service.</typeparam>
    /// <param name="service">Service instance.</param>
    public void RegisterService<T>(T service) where T : class
    {
        _buildContext?.RegisterService(service);
    }

    /// <summary>
    /// Gets a global service.
    /// </summary>
    /// <typeparam name="T">Type of service.</typeparam>
    /// <returns>Service instance or null.</returns>
    public T? GetService<T>() where T : class
    {
        return _buildContext?.GetService<T>();
    }
}
