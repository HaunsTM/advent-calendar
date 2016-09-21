using System.Configuration;
using System.Data.Entity;
using advent_calendar.Models.POCO;
using Microsoft.AspNet.Identity.EntityFramework;

namespace advent_calendar.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
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


        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<UserRole>  UserRoles { get; set; }
        public DbSet<Slot> Slots { get; set; }
    }
}
