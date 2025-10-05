using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;

namespace ZorUI.Components.Forms;

/// <summary>
/// Date picker component for selecting dates.
/// </summary>
public class DatePicker : ZorComponent
{
    private DateTime? _value;
    private DateTime _displayMonth;
    private bool _isOpen = false;

    public DateTime? Value
    {
        get => _value;
        set
        {
            _value = value;
            _displayMonth = value ?? DateTime.Now;
            MarkNeedsRebuild();
        }
    }

    public Action<DateTime?>? OnChange { get; set; }
    public string Placeholder { get; set; } = "Select date...";
    public string Format { get; set; } = "MM/dd/yyyy";
    public DateTime? MinDate { get; set; }
    public DateTime? MaxDate { get; set; }

    public DatePicker()
    {
        _displayMonth = DateTime.Now;
    }

    public override ZorElement Build()
    {
        var formattedDate = _value?.ToString(Format) ?? Placeholder;

        var trigger = new TextField(Placeholder)
            .WithValue(formattedDate)
            .Metadata["OnClick"] = new Action(() =>
            {
                _isOpen = !_isOpen;
                MarkNeedsRebuild();
            });

        if (!_isOpen)
        {
            return new Container().AddChild(trigger);
        }

        return new VStack(spacing: 0)
            .AddChild(trigger)
            .AddChild(BuildCalendar());
    }

    private ZorElement BuildCalendar()
    {
        return new VStack(spacing: 8)
            .WithPadding(EdgeInsets.All(16))
            .AddChild(BuildMonthNav())
            .AddChild(BuildWeekDays())
            .AddChild(BuildDays());
    }

    private ZorElement BuildMonthNav()
    {
        return new HStack(spacing: 8)
            .AddChild(
                new Button("<", PreviousMonth)
                    .Secondary()
                    .Small()
            )
            .AddChild(
                new Text(_displayMonth.ToString("MMMM yyyy"))
                    .Bold()
            )
            .AddChild(new Spacer())
            .AddChild(
                new Button(">", NextMonth)
                    .Secondary()
                    .Small()
            );
    }

    private ZorElement BuildWeekDays()
    {
        var weekDays = new HStack(spacing: 4);
        var dayNames = new[] { "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa" };

        foreach (var day in dayNames)
        {
            weekDays.AddChild(
                new Container()
                    .WithPadding(EdgeInsets.All(8))
                    .AddChild(new Text(day).Bold())
            );
        }

        return weekDays;
    }

    private ZorElement BuildDays()
    {
        var grid = new Grid(columns: 7, spacing: 4);
        var firstDay = new DateTime(_displayMonth.Year, _displayMonth.Month, 1);
        var daysInMonth = DateTime.DaysInMonth(_displayMonth.Year, _displayMonth.Month);
        var startDayOfWeek = (int)firstDay.DayOfWeek;

        // Empty cells for days before month starts
        for (int i = 0; i < startDayOfWeek; i++)
        {
            grid.AddChild(new Container());
        }

        // Days of month
        for (int day = 1; day <= daysInMonth; day++)
        {
            var date = new DateTime(_displayMonth.Year, _displayMonth.Month, day);
            var isSelected = _value?.Date == date.Date;
            var isDisabled = (MinDate.HasValue && date < MinDate.Value) ||
                            (MaxDate.HasValue && date > MaxDate.Value);

            var dayButton = new Button(day.ToString(), () => SelectDate(date))
                .Small();

            if (isSelected) dayButton.Primary();
            else dayButton.Ghost();

            if (isDisabled) dayButton.Disabled(true);

            grid.AddChild(dayButton);
        }

        return grid;
    }

    private void SelectDate(DateTime date)
    {
        _value = date;
        _isOpen = false;
        OnChange?.Invoke(_value);
        MarkNeedsRebuild();
    }

    private void PreviousMonth()
    {
        _displayMonth = _displayMonth.AddMonths(-1);
        MarkNeedsRebuild();
    }

    private void NextMonth()
    {
        _displayMonth = _displayMonth.AddMonths(1);
        MarkNeedsRebuild();
    }

    // Fluent API

    public DatePicker WithValue(DateTime? value)
    {
        Value = value;
        return this;
    }

    public DatePicker WithOnChange(Action<DateTime?> handler)
    {
        OnChange = handler;
        return this;
    }

    public DatePicker WithPlaceholder(string placeholder)
    {
        Placeholder = placeholder;
        return this;
    }

    public DatePicker WithFormat(string format)
    {
        Format = format;
        return this;
    }

    public DatePicker WithMinDate(DateTime minDate)
    {
        MinDate = minDate;
        return this;
    }

    public DatePicker WithMaxDate(DateTime maxDate)
    {
        MaxDate = maxDate;
        return this;
    }
}
