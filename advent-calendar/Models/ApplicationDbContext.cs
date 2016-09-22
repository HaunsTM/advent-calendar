using System.Configuration;
using System.Data.Entity;

namespace advent_calendar.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base(DefaultConnection)
        {
        }

        public static string DefaultConnection
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();
            }
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<model.Interfaces.ICalendar> Calendars { get; set; }
        public DbSet<model.Interfaces.ISlot> Slots { get; set; }
        public DbSet<model.Interfaces.IUser> Users { get; set; }
        public DbSet<model.Interfaces.IUserRole> UserRoles { get; set; }
    }
}
