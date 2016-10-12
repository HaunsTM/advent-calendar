namespace advent_calendar.Models.Interfaces
{
    public interface IApplicationUserRole : IEntity
    {
        bool IsSuperUser { get; set; }
        bool CanOpenCalendar { get; set; }
        bool CanCreateCalendar { get; set; }
        bool CanCreateUser { get; set; }
        string Name { get; set; }
    }
}