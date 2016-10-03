namespace model.Interfaces
{
    public interface IUser : IEntity
    {
        string Name { get; set; }
        string Email { get; set; }
    }
}
