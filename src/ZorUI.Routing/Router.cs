using ZorUI.Core;

namespace ZorUI.Routing;

/// <summary>
/// Router for handling navigation in ZorUI applications.
/// </summary>
public class Router
{
    private readonly Dictionary<string, RouteDefinition> _routes = new();
    private string _currentPath = "/";
    private readonly List<string> _history = new();
    private int _historyIndex = -1;
    public event Action<string>? OnNavigate;

    /// <summary>
    /// Register a route.
    /// </summary>
    public Router AddRoute(string path, Func<ZorComponent> factory)
    {
        _routes[path] = new RouteDefinition
        {
            Path = path,
            Factory = factory,
            IsParameterized = false
        };
        return this;
    }

    /// <summary>
    /// Register a route with parameters (e.g., "/users/:id").
    /// </summary>
    public Router AddRoute(string path, Func<Dictionary<string, string>, ZorComponent> factoryWithParams)
    {
        _routes[path] = new RouteDefinition
        {
            Path = path,
            FactoryWithParams = factoryWithParams,
            IsParameterized = true
        };
        return this;
    }

    /// <summary>
    /// Navigate to a path.
    /// </summary>
    public void Navigate(string path)
    {
        if (path == _currentPath) return;

        // Add to history
        _historyIndex++;
        if (_historyIndex < _history.Count)
        {
            _history.RemoveRange(_historyIndex, _history.Count - _historyIndex);
        }
        _history.Add(path);

        _currentPath = path;
        OnNavigate?.Invoke(path);
    }

    /// <summary>
    /// Go back in history.
    /// </summary>
    public void GoBack()
    {
        if (CanGoBack())
        {
            _historyIndex--;
            _currentPath = _history[_historyIndex];
            OnNavigate?.Invoke(_currentPath);
        }
    }

    /// <summary>
    /// Go forward in history.
    /// </summary>
    public void GoForward()
    {
        if (CanGoForward())
        {
            _historyIndex++;
            _currentPath = _history[_historyIndex];
            OnNavigate?.Invoke(_currentPath);
        }
    }

    /// <summary>
    /// Check if can go back.
    /// </summary>
    public bool CanGoBack() => _historyIndex > 0;

    /// <summary>
    /// Check if can go forward.
    /// </summary>
    public bool CanGoForward() => _historyIndex < _history.Count - 1;

    /// <summary>
    /// Get component for current path.
    /// </summary>
    public ZorComponent GetCurrentComponent()
    {
        var (route, parameters) = MatchRoute(_currentPath);

        if (route == null)
        {
            return CreateNotFoundComponent();
        }

        if (route.IsParameterized && route.FactoryWithParams != null)
        {
            return route.FactoryWithParams(parameters);
        }

        return route.Factory?.Invoke() ?? CreateNotFoundComponent();
    }

    private (RouteDefinition? route, Dictionary<string, string> parameters) MatchRoute(string path)
    {
        // Exact match first
        if (_routes.TryGetValue(path, out var exactRoute))
        {
            return (exactRoute, new Dictionary<string, string>());
        }

        // Try parameterized routes
        foreach (var route in _routes.Values.Where(r => r.IsParameterized))
        {
            var parameters = MatchParameterizedRoute(route.Path, path);
            if (parameters != null)
            {
                return (route, parameters);
            }
        }

        return (null, new Dictionary<string, string>());
    }

    private Dictionary<string, string>? MatchParameterizedRoute(string routePath, string actualPath)
    {
        var routeParts = routePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var actualParts = actualPath.Split('/', StringSplitOptions.RemoveEmptyEntries);

        if (routeParts.Length != actualParts.Length)
            return null;

        var parameters = new Dictionary<string, string>();

        for (int i = 0; i < routeParts.Length; i++)
        {
            if (routeParts[i].StartsWith(':'))
            {
                var paramName = routeParts[i][1..];
                parameters[paramName] = actualParts[i];
            }
            else if (routeParts[i] != actualParts[i])
            {
                return null;
            }
        }

        return parameters;
    }

    private ZorComponent CreateNotFoundComponent()
    {
        return new NotFoundPage();
    }

    public string CurrentPath => _currentPath;
}

/// <summary>
/// Route definition.
/// </summary>
internal class RouteDefinition
{
    public string Path { get; set; } = "";
    public Func<ZorComponent>? Factory { get; set; }
    public Func<Dictionary<string, string>, ZorComponent>? FactoryWithParams { get; set; }
    public bool IsParameterized { get; set; }
}

/// <summary>
/// Default 404 page.
/// </summary>
internal class NotFoundPage : ZorComponent
{
    public override ZorElement Build()
    {
        return new ZorElement { ElementType = "NotFound" };
    }
}

/// <summary>
/// Router outlet component that displays the current route.
/// </summary>
public class RouterOutlet : ZorComponent
{
    private readonly Router _router;

    public RouterOutlet(Router router)
    {
        _router = router;
        _router.OnNavigate += _ => MarkNeedsRebuild();
    }

    public override ZorElement Build()
    {
        return _router.GetCurrentComponent().Build();
    }
}
