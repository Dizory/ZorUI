using ZorUI.Core;

namespace ZorUI.Components.Layout;

/// <summary>
/// Grid layout component for arranging children in rows and columns.
/// Similar to CSS Grid or SwiftUI Grid.
/// </summary>
public class Grid : Container
{
    /// <summary>
    /// Number of columns in the grid.
    /// </summary>
    public int Columns { get; set; } = 1;

    /// <summary>
    /// Number of rows in the grid (auto-calculated if not set).
    /// </summary>
    public int? Rows { get; set; }

    /// <summary>
    /// Spacing between columns.
    /// </summary>
    public double ColumnSpacing { get; set; } = 0;

    /// <summary>
    /// Spacing between rows.
    /// </summary>
    public double RowSpacing { get; set; } = 0;

    /// <summary>
    /// Creates a new grid layout.
    /// </summary>
    public Grid(int columns = 1, params ZorElement[] children) : base(children)
    {
        Columns = columns;
    }

    // Fluent API

    public Grid WithColumns(int columns)
    {
        Columns = columns;
        return this;
    }

    public Grid WithRows(int rows)
    {
        Rows = rows;
        return this;
    }

    public Grid WithColumnSpacing(double spacing)
    {
        ColumnSpacing = spacing;
        return this;
    }

    public Grid WithRowSpacing(double spacing)
    {
        RowSpacing = spacing;
        return this;
    }

    public Grid WithSpacing(double spacing)
    {
        ColumnSpacing = spacing;
        RowSpacing = spacing;
        return this;
    }

    /// <summary>
    /// Gets the calculated number of rows based on children count.
    /// </summary>
    public int CalculatedRows => 
        Rows ?? (int)Math.Ceiling((double)Children.Count / Columns);
}

/// <summary>
/// Grid item with specific positioning.
/// </summary>
public class GridItem : Container
{
    /// <summary>
    /// Column index (0-based).
    /// </summary>
    public int Column { get; set; } = 0;

    /// <summary>
    /// Row index (0-based).
    /// </summary>
    public int Row { get; set; } = 0;

    /// <summary>
    /// Number of columns to span.
    /// </summary>
    public int ColumnSpan { get; set; } = 1;

    /// <summary>
    /// Number of rows to span.
    /// </summary>
    public int RowSpan { get; set; } = 1;

    /// <summary>
    /// Creates a new grid item.
    /// </summary>
    public GridItem(ZorElement child)
    {
        AddChild(child);
    }

    // Fluent API

    public GridItem AtPosition(int column, int row)
    {
        Column = column;
        Row = row;
        return this;
    }

    public GridItem WithColumnSpan(int span)
    {
        ColumnSpan = span;
        return this;
    }

    public GridItem WithRowSpan(int span)
    {
        RowSpan = span;
        return this;
    }

    public GridItem Spanning(int columnSpan, int rowSpan)
    {
        ColumnSpan = columnSpan;
        RowSpan = rowSpan;
        return this;
    }
}
