namespace advent_calendar.Models.Interfaces
{
    public interface ICalendar : IEntity
    {
        int Year { get; set; }
    }
}