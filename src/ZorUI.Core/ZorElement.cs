namespace ZorUI.Core;

/// <summary>
/// Base class for all UI elements in ZorUI framework.
/// This is the fundamental building block of the UI tree.
/// Inspired by React's Element and SwiftUI's View protocol.
/// </summary>
public abstract class ZorElement
{
    /// <summary>
    /// Unique identifier for this element instance.
    /// Used for reconciliation and efficient updates.
    /// </summary>
    public string Id { get; internal set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Type identifier for this element.
    /// Used to determine if elements can be reused during updates.
    /// </summary>
    public virtual string ElementType => GetType().Name;

    /// <summary>
    /// Parent element in the UI tree.
    /// Null for root elements.
    /// </summary>
    public ZorElement? Parent { get; internal set; }

    /// <summary>
    /// Children elements in the UI tree.
    /// </summary>
    public List<ZorElement> Children { get; } = new();

    /// <summary>
    /// Metadata and properties attached to this element.
    /// </summary>
    public Dictionary<string, object?> Metadata { get; } = new();

    /// <summary>
    /// Build context for accessing framework services.
    /// </summary>
    public BuildContext? Context { get; internal set; }

    /// <summary>
    /// Indicates whether this element needs to be redrawn.
    /// </summary>
    public bool NeedsRebuild { get; set; } = true;

    /// <summary>
    /// Called when the element is mounted to the UI tree.
    /// Override to perform initialization.
    /// </summary>
    public virtual void OnMount()
    {
    }

    /// <summary>
    /// Called when the element is unmounted from the UI tree.
    /// Override to perform cleanup.
    /// </summary>
    public virtual void OnUnmount()
    {
    }

    /// <summary>
    /// Called when the element's properties have changed.
    /// Override to respond to property updates.
    /// </summary>
    public virtual void OnUpdate()
    {
    }

    /// <summary>
    /// Marks this element as needing rebuild.
    /// This will trigger a re-render on the next frame.
    /// </summary>
    public void MarkNeedsRebuild()
    {
        NeedsRebuild = true;
        Context?.Scheduler.ScheduleRebuild(this);
    }

    /// <summary>
    /// Adds a child element to this element.
    /// </summary>
    public void AddChild(ZorElement child)
    {
        child.Parent = this;
        child.Context = Context;
        Children.Add(child);
    }

    /// <summary>
    /// Removes a child element from this element.
    /// </summary>
    public void RemoveChild(ZorElement child)
    {
        child.Parent = null;
        Children.Remove(child);
    }

    /// <summary>
    /// Clears all children from this element.
    /// </summary>
    public void ClearChildren()
    {
        foreach (var child in Children)
        {
            child.Parent = null;
        }
        Children.Clear();
    }

    /// <summary>
    /// Visits this element and all its descendants.
    /// Useful for traversing the UI tree.
    /// </summary>
    public void Visit(Action<ZorElement> visitor)
    {
        visitor(this);
        foreach (var child in Children)
        {
            child.Visit(visitor);
        }
    }
}
