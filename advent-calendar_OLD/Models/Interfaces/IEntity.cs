namespace advent_calendar.Models.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        bool Active { get; set; }
    }
}