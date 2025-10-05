using System.Reactive.Subjects;

namespace ZorUI.Core;

/// <summary>
/// Manages scheduling and batching of component rebuilds.
/// Ensures efficient updates by batching multiple state changes
/// into a single rebuild pass.
/// </summary>
public class RebuildScheduler
{
    private readonly HashSet<ZorElement> _dirtyElements = new();
    private readonly Subject<RebuildBatch> _rebuildStream = new();
    private bool _rebuildScheduled = false;
    private readonly object _lock = new();

    /// <summary>
    /// Observable stream of rebuild batches.
    /// Subscribe to this to be notified when rebuilds occur.
    /// </summary>
    public IObservable<RebuildBatch> RebuildStream => _rebuildStream;

    /// <summary>
    /// Schedules an element for rebuild on the next frame.
    /// Multiple calls for the same element are batched.
    /// </summary>
    /// <param name="element">Element to rebuild.</param>
    public void ScheduleRebuild(ZorElement element)
    {
        lock (_lock)
        {
            _dirtyElements.Add(element);

            if (!_rebuildScheduled)
            {
                _rebuildScheduled = true;
                // Schedule rebuild on next frame/tick
                Task.Run(async () =>
                {
                    // Small delay to batch multiple state changes
                    await Task.Delay(1);
                    ProcessRebuilds();
                });
            }
        }
    }

    /// <summary>
    /// Immediately processes all pending rebuilds.
    /// Useful for testing or forcing synchronous updates.
    /// </summary>
    public void FlushRebuilds()
    {
        ProcessRebuilds();
    }

    private void ProcessRebuilds()
    {
        List<ZorElement> elementsToRebuild;
        
        lock (_lock)
        {
            if (_dirtyElements.Count == 0)
            {
                _rebuildScheduled = false;
                return;
            }

            elementsToRebuild = new List<ZorElement>(_dirtyElements);
            _dirtyElements.Clear();
            _rebuildScheduled = false;
        }

        var batch = new RebuildBatch
        {
            Timestamp = DateTime.UtcNow,
            Elements = elementsToRebuild
        };

        // Process rebuilds
        foreach (var element in elementsToRebuild)
        {
            if (element is ZorComponent component)
            {
                RebuildComponent(component);
            }
        }

        // Notify observers
        _rebuildStream.OnNext(batch);
    }

    private void RebuildComponent(ZorComponent component)
    {
        try
        {
            component.BeforeBuild();
            
            // Build new tree
            var newTree = component.Build();
            
            // Update component's children
            component.ClearChildren();
            component.AddChild(newTree);
            
            component.NeedsRebuild = false;
            component.AfterBuild();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error rebuilding component {component.GetType().Name}: {ex.Message}");
        }
    }
}

/// <summary>
/// Represents a batch of elements that were rebuilt together.
/// </summary>
public class RebuildBatch
{
    /// <summary>
    /// When this rebuild batch was processed.
    /// </summary>
    public DateTime Timestamp { get; init; }

    /// <summary>
    /// Elements that were rebuilt in this batch.
    /// </summary>
    public List<ZorElement> Elements { get; init; } = new();
}
