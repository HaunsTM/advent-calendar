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


        public DbSet<model.POCO.Calendar> Calendars { get; set; }
        public DbSet<model.POCO.Slot> Slots { get; set; }
        public DbSet<model.POCO.User> Users { get; set; }
        public DbSet<model.POCO.UserRole> UserRoles { get; set; }
    }
}