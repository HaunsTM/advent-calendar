namespace advent_calendar.Models.Interfaces
{
    public interface ICalendar : IEntity
    {

        string ContentType { get; set; }
        byte[] Content { get; set; }
        string Name { get; set; }
        int Year { get; set; }
    }
}