namespace ZorUI.Core.Performance;

/// <summary>
/// Component that caches its build result and only rebuilds when dependencies change.
/// </summary>
public abstract class MemoizedComponent : ZorComponent
{
    private ZorElement? _cachedBuild;
    private int _lastDependencyHash;

    /// <summary>
    /// Override to provide dependencies that trigger rebuild when changed.
    /// </summary>
    protected virtual object[] GetDependencies() => Array.Empty<object>();

    public sealed override ZorElement Build()
    {
        var currentHash = ComputeDependencyHash();

        if (_cachedBuild != null && _lastDependencyHash == currentHash)
        {
            return _cachedBuild;
        }

        _cachedBuild = BuildInternal();
        _lastDependencyHash = currentHash;
        return _cachedBuild;
    }

    /// <summary>
    /// Implement actual build logic here.
    /// </summary>
    protected abstract ZorElement BuildInternal();

    private int ComputeDependencyHash()
    {
        var deps = GetDependencies();
        var hash = 17;

        foreach (var dep in deps)
        {
            hash = hash * 31 + (dep?.GetHashCode() ?? 0);
        }

        return hash;
    }

    /// <summary>
    /// Force rebuild regardless of dependencies.
    /// </summary>
    protected void InvalidateCache()
    {
        _cachedBuild = null;
    }
}

/// <summary>
/// Memoized wrapper for any component.
/// </summary>
public class Memo : MemoizedComponent
{
    private readonly Func<ZorElement> _builder;
    private readonly object[] _dependencies;

    public Memo(Func<ZorElement> builder, params object[] dependencies)
    {
        _builder = builder;
        _dependencies = dependencies;
    }

    protected override ZorElement BuildInternal() => _builder();

    protected override object[] GetDependencies() => _dependencies;
}

/// <summary>
/// Helper methods for memoization.
/// </summary>
public static class MemoizationHelper
{
    /// <summary>
    /// Memoize a build function with dependencies.
    /// </summary>
    public static Memo Memoize(Func<ZorElement> builder, params object[] dependencies)
    {
        return new Memo(builder, dependencies);
    }
}
