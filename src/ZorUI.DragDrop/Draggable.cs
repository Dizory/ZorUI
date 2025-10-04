using ZorUI.Core;
using ZorUI.Core.Properties;

namespace ZorUI.DragDrop;

/// <summary>
/// Makes an element draggable.
/// </summary>
public class Draggable : ZorComponent
{
    public ZorElement Child { get; set; }
    public object? Data { get; set; }
    public Action<DragEvent>? OnDragStart { get; set; }
    public Action<DragEvent>? OnDrag { get; set; }
    public Action<DragEvent>? OnDragEnd { get; set; }
    public string? DragHandle { get; set; }
    public bool Disabled { get; set; }

    private bool _isDragging = false;
    private Point _dragStartPosition;
    private Point _currentPosition;

    public Draggable(ZorElement child)
    {
        Child = child;
    }

    public override ZorElement Build()
    {
        Child.Metadata["IsDraggable"] = !Disabled;
        Child.Metadata["DragHandle"] = DragHandle;
        Child.Metadata["DragData"] = Data;
        Child.Metadata["OnDragStart"] = new Action<Point>(HandleDragStart);
        Child.Metadata["OnDrag"] = new Action<Point>(HandleDrag);
        Child.Metadata["OnDragEnd"] = new Action<Point>(HandleDragEnd);

        return Child;
    }

    private void HandleDragStart(Point position)
    {
        if (Disabled) return;

        _isDragging = true;
        _dragStartPosition = position;
        _currentPosition = position;

        var dragEvent = new DragEvent
        {
            Position = position,
            Data = Data
        };

        OnDragStart?.Invoke(dragEvent);
    }

    private void HandleDrag(Point position)
    {
        if (!_isDragging) return;

        _currentPosition = position;

        var dragEvent = new DragEvent
        {
            Position = position,
            Delta = new Point(
                position.X - _dragStartPosition.X,
                position.Y - _dragStartPosition.Y
            ),
            Data = Data
        };

        OnDrag?.Invoke(dragEvent);
        MarkNeedsRebuild();
    }

    private void HandleDragEnd(Point position)
    {
        if (!_isDragging) return;

        _isDragging = false;

        var dragEvent = new DragEvent
        {
            Position = position,
            Delta = new Point(
                position.X - _dragStartPosition.X,
                position.Y - _dragStartPosition.Y
            ),
            Data = Data
        };

        OnDragEnd?.Invoke(dragEvent);
    }

    // Fluent API

    public Draggable WithData(object data)
    {
        Data = data;
        return this;
    }

    public Draggable WithDragStart(Action<DragEvent> handler)
    {
        OnDragStart = handler;
        return this;
    }

    public Draggable WithDrag(Action<DragEvent> handler)
    {
        OnDrag = handler;
        return this;
    }

    public Draggable WithDragEnd(Action<DragEvent> handler)
    {
        OnDragEnd = handler;
        return this;
    }

    public Draggable WithDragHandle(string handle)
    {
        DragHandle = handle;
        return this;
    }

    public Draggable IsDisabled(bool disabled)
    {
        Disabled = disabled;
        return this;
    }
}

/// <summary>
/// Drop zone that accepts dragged elements.
/// </summary>
public class DropZone : ZorComponent
{
    public ZorElement Child { get; set; }
    public Action<DragEvent>? OnDrop { get; set; }
    public Action<DragEvent>? OnDragEnter { get; set; }
    public Action<DragEvent>? OnDragLeave { get; set; }
    public Action<DragEvent>? OnDragOver { get; set; }
    public Func<object?, bool>? AcceptCondition { get; set; }
    public bool Disabled { get; set; }

    private bool _isHovering = false;

    public DropZone(ZorElement child)
    {
        Child = child;
    }

    public override ZorElement Build()
    {
        Child.Metadata["IsDropZone"] = !Disabled;
        Child.Metadata["OnDrop"] = new Action<DragEvent>(HandleDrop);
        Child.Metadata["OnDragEnter"] = new Action<DragEvent>(HandleDragEnter);
        Child.Metadata["OnDragLeave"] = new Action<DragEvent>(HandleDragLeave);
        Child.Metadata["OnDragOver"] = new Action<DragEvent>(HandleDragOver);
        Child.Metadata["IsHovering"] = _isHovering;

        return Child;
    }

    private bool CanAccept(object? data)
    {
        if (Disabled) return false;
        if (AcceptCondition == null) return true;
        return AcceptCondition(data);
    }

    private void HandleDrop(DragEvent e)
    {
        if (!CanAccept(e.Data)) return;

        _isHovering = false;
        OnDrop?.Invoke(e);
        MarkNeedsRebuild();
    }

    private void HandleDragEnter(DragEvent e)
    {
        if (!CanAccept(e.Data)) return;

        _isHovering = true;
        OnDragEnter?.Invoke(e);
        MarkNeedsRebuild();
    }

    private void HandleDragLeave(DragEvent e)
    {
        _isHovering = false;
        OnDragLeave?.Invoke(e);
        MarkNeedsRebuild();
    }

    private void HandleDragOver(DragEvent e)
    {
        if (!CanAccept(e.Data)) return;
        OnDragOver?.Invoke(e);
    }

    // Fluent API

    public DropZone WithDrop(Action<DragEvent> handler)
    {
        OnDrop = handler;
        return this;
    }

    public DropZone WithDragEnter(Action<DragEvent> handler)
    {
        OnDragEnter = handler;
        return this;
    }

    public DropZone WithDragLeave(Action<DragEvent> handler)
    {
        OnDragLeave = handler;
        return this;
    }

    public DropZone WithAcceptCondition(Func<object?, bool> condition)
    {
        AcceptCondition = condition;
        return this;
    }

    public DropZone IsDisabled(bool disabled)
    {
        Disabled = disabled;
        return this;
    }
}

/// <summary>
/// Drag event data.
/// </summary>
public class DragEvent
{
    public Point Position { get; set; }
    public Point Delta { get; set; }
    public object? Data { get; set; }
}

/// <summary>
/// 2D Point structure.
/// </summary>
public record struct Point(double X, double Y);
