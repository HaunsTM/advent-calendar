namespace advent_calendar.Models.Interfaces
{
    public interface IUserRole : IEntity
    {
        bool IsSuperUser { get; set; }
        bool CanOpenCalendar { get; set; }
        bool CanCreateCalendar { get; set; }
        bool CanCreateUser { get; set; }
    }
}