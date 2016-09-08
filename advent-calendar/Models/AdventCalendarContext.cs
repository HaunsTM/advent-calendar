using System.Data.Entity;
using advent_calendar.Models.POCO;

namespace advent_calendar.Models
{
    public class AdventCalendarContext : DbContext
    {
        public AdventCalendarContext()
            : base("name=DefaultConnection")
        {
            //Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Slot> Slots { get; set; }
    }
}