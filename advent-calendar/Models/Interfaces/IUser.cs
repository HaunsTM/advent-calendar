namespace advent_calendar.Models.Interfaces
{
    public interface IUser : IEntity
    {
        string Name { get; set; }
        string Email { get; set; }
    }
}
