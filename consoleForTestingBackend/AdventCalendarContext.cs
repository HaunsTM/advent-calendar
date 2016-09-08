using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace consoleForTestingBackend
{
    [DbConfigurationType(typeof(MyDbConfiguration))]
    public class AdventCalendarContext : DbContext
    {
        public AdventCalendarContext()
            : base("name=DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
        }
        /*
        public DbSet<model.POCO.Calendar> Calendars { get; set; }
        public DbSet<model.POCO.Slot> Slots { get; set; }
        */
    }

    internal class MyDbConfiguration : DbConfiguration
    {
        public MyDbConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy(10, TimeSpan.FromSeconds(2)));
        }
    }
}
