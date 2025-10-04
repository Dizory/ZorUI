using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;

namespace ZorUI.DataGrid;

/// <summary>
/// Data grid component for displaying tabular data with sorting, filtering, and pagination.
/// </summary>
/// <typeparam name="T">Type of items in the grid</typeparam>
public class DataGrid<T> : ZorComponent
{
    private List<T> _items = new();
    private List<DataGridColumn<T>> _columns = new();
    private string? _sortColumn;
    private bool _sortDescending = false;
    private Dictionary<string, string> _filters = new();
    private int _currentPage = 1;
    private int _pageSize = 10;
    private bool _enableSorting = false;
    private bool _enableFiltering = false;
    private bool _enablePagination = false;
    private HashSet<T> _selectedRows = new();
    private bool _enableSelection = false;

    public List<T> Items
    {
        get => _items;
        set
        {
            _items = value;
            MarkNeedsRebuild();
        }
    }

    public Action<T>? OnRowClick { get; set; }
    public Action<List<T>>? OnSelectionChange { get; set; }

    public override ZorElement Build()
    {
        var filteredItems = ApplyFilters(_items);
        var sortedItems = ApplySort(filteredItems);
        var pagedItems = ApplyPagination(sortedItems);

        return new VStack(spacing: 0)
            .AddChild(BuildTable(pagedItems))
            .AddChild(BuildPagination(sortedItems.Count));
    }

    private ZorElement BuildTable(List<T> items)
    {
        var table = new VStack(spacing: 0);

        // Header
        table.AddChild(BuildHeader());

        // Rows
        foreach (var item in items)
        {
            table.AddChild(BuildRow(item));
        }

        return table;
    }

    private ZorElement BuildHeader()
    {
        var header = new HStack(spacing: 0);

        if (_enableSelection)
        {
            header.AddChild(BuildSelectAllCell());
        }

        foreach (var column in _columns)
        {
            header.AddChild(BuildHeaderCell(column));
        }

        return header;
    }

    private ZorElement BuildHeaderCell(DataGridColumn<T> column)
    {
        var cell = new HStack(spacing: 8)
            .WithPadding(EdgeInsets.All(12));

        cell.AddChild(new Text(column.Header).Bold());

        if (_enableSorting && column.Sortable)
        {
            var sortIcon = _sortColumn == column.Key
                ? (_sortDescending ? "▼" : "▲")
                : "⇅";

            cell.AddChild(new Text(sortIcon));
            cell.Metadata["OnClick"] = new Action(() => ToggleSort(column.Key));
        }

        return cell;
    }

    private ZorElement BuildRow(T item)
    {
        var row = new HStack(spacing: 0);
        var isSelected = _selectedRows.Contains(item);

        if (_enableSelection)
        {
            row.AddChild(BuildSelectCell(item, isSelected));
        }

        foreach (var column in _columns)
        {
            row.AddChild(BuildCell(item, column));
        }

        if (OnRowClick != null)
        {
            row.Metadata["OnClick"] = new Action(() => OnRowClick(item));
        }

        return row;
    }

    private ZorElement BuildCell(T item, DataGridColumn<T> column)
    {
        var value = column.ValueGetter(item);
        var content = column.CellTemplate != null
            ? column.CellTemplate(item)
            : new Text(value?.ToString() ?? "");

        return new Container()
            .WithPadding(EdgeInsets.All(12))
            .AddChild(content);
    }

    private ZorElement BuildSelectAllCell()
    {
        var allSelected = _items.All(i => _selectedRows.Contains(i));
        return new Container()
            .WithPadding(EdgeInsets.All(12))
            .AddChild(new Checkbox("", allSelected)
                .WithOnChange(checked =>
                {
                    if (checked)
                        _selectedRows = new HashSet<T>(_items);
                    else
                        _selectedRows.Clear();
                    
                    OnSelectionChange?.Invoke(_selectedRows.ToList());
                    MarkNeedsRebuild();
                }));
    }

    private ZorElement BuildSelectCell(T item, bool isSelected)
    {
        return new Container()
            .WithPadding(EdgeInsets.All(12))
            .AddChild(new Checkbox("", isSelected)
                .WithOnChange(checked =>
                {
                    if (checked)
                        _selectedRows.Add(item);
                    else
                        _selectedRows.Remove(item);
                    
                    OnSelectionChange?.Invoke(_selectedRows.ToList());
                    MarkNeedsRebuild();
                }));
    }

    private ZorElement BuildPagination(int totalItems)
    {
        if (!_enablePagination) return new Container();

        var totalPages = (int)Math.Ceiling((double)totalItems / _pageSize);

        return new HStack(spacing: 8)
            .WithPadding(EdgeInsets.All(16))
            .AddChild(new Button("Previous", () => PreviousPage())
                .Secondary()
                .Disabled(_currentPage <= 1))
            .AddChild(new Text($"Page {_currentPage} of {totalPages}"))
            .AddChild(new Button("Next", () => NextPage())
                .Secondary()
                .Disabled(_currentPage >= totalPages));
    }

    private List<T> ApplyFilters(List<T> items)
    {
        if (!_enableFiltering || _filters.Count == 0)
            return items;

        return items.Where(item =>
        {
            foreach (var (key, filterValue) in _filters)
            {
                var column = _columns.FirstOrDefault(c => c.Key == key);
                if (column == null) continue;

                var value = column.ValueGetter(item)?.ToString() ?? "";
                if (!value.Contains(filterValue, StringComparison.OrdinalIgnoreCase))
                    return false;
            }
            return true;
        }).ToList();
    }

    private List<T> ApplySort(List<T> items)
    {
        if (!_enableSorting || _sortColumn == null)
            return items;

        var column = _columns.FirstOrDefault(c => c.Key == _sortColumn);
        if (column == null) return items;

        var sorted = _sortDescending
            ? items.OrderByDescending(i => column.ValueGetter(i))
            : items.OrderBy(i => column.ValueGetter(i));

        return sorted.ToList();
    }

    private List<T> ApplyPagination(List<T> items)
    {
        if (!_enablePagination)
            return items;

        var skip = (_currentPage - 1) * _pageSize;
        return items.Skip(skip).Take(_pageSize).ToList();
    }

    private void ToggleSort(string columnKey)
    {
        if (_sortColumn == columnKey)
        {
            _sortDescending = !_sortDescending;
        }
        else
        {
            _sortColumn = columnKey;
            _sortDescending = false;
        }
        MarkNeedsRebuild();
    }

    private void NextPage()
    {
        _currentPage++;
        MarkNeedsRebuild();
    }

    private void PreviousPage()
    {
        _currentPage--;
        MarkNeedsRebuild();
    }

    // Fluent API

    public DataGrid<T> WithItems(List<T> items)
    {
        Items = items;
        return this;
    }

    public DataGrid<T> AddColumn(DataGridColumn<T> column)
    {
        _columns.Add(column);
        return this;
    }

    public DataGrid<T> AddColumn(string header, Func<T, object?> valueGetter, string? key = null)
    {
        _columns.Add(new DataGridColumn<T>
        {
            Key = key ?? header,
            Header = header,
            ValueGetter = valueGetter
        });
        return this;
    }

    public DataGrid<T> WithSorting(bool enabled = true)
    {
        _enableSorting = enabled;
        return this;
    }

    public DataGrid<T> WithFiltering(bool enabled = true)
    {
        _enableFiltering = enabled;
        return this;
    }

    public DataGrid<T> WithPagination(int pageSize = 10)
    {
        _enablePagination = true;
        _pageSize = pageSize;
        return this;
    }

    public DataGrid<T> WithSelection(bool enabled = true)
    {
        _enableSelection = enabled;
        return this;
    }

    public DataGrid<T> WithRowClick(Action<T> handler)
    {
        OnRowClick = handler;
        return this;
    }

    public DataGrid<T> WithSelectionChange(Action<List<T>> handler)
    {
        OnSelectionChange = handler;
        return this;
    }

    public List<T> GetSelectedRows() => _selectedRows.ToList();
}

/// <summary>
/// Column definition for DataGrid.
/// </summary>
public class DataGridColumn<T>
{
    public string Key { get; set; } = "";
    public string Header { get; set; } = "";
    public Func<T, object?> ValueGetter { get; set; } = _ => null;
    public Func<T, ZorElement>? CellTemplate { get; set; }
    public bool Sortable { get; set; } = true;
    public bool Filterable { get; set; } = true;
    public double? Width { get; set; }
}
