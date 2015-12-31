using System;
using System.Data.Common;
using System.Data.Entity;

namespace advent_calendar.Models
{
    public class AdventCalendarContext : DbContext
    {
        public AdventCalendarContext()
            : base("name=DefaultConnection")
        {
            //Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<model.POCO.Calendar> Calendars { get; set; }
        public DbSet<model.POCO.Slot> Slots { get; set; }
    }
}