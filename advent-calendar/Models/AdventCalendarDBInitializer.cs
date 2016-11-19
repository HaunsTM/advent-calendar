using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using advent_calendar.Models.POCO;

namespace advent_calendar.Models
{
    public class AdventCalendarDBInitializer<T> : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        private List<ApplicationUserRole> ApplicationUserRoles()
        {
            var applicationUserRoles = new List<ApplicationUserRole>
            {
                new ApplicationUserRole { Active = true, Name = ConfigurationManager.AppSettings["SUPER_ADMINISTRATOR"], IsSuperUser = true, CanCreateCalendar = true, CanCreateUser = true, CanOpenCalendar = true},
                new ApplicationUserRole { Active = true, Name = ConfigurationManager.AppSettings["USER_ADMINISTRATOR"], IsSuperUser = false, CanCreateCalendar = true, CanCreateUser = true, CanOpenCalendar = true},
                new ApplicationUserRole { Active = true, Name = ConfigurationManager.AppSettings["STANDARD_USER"], IsSuperUser = false, CanCreateCalendar = false, CanCreateUser = false, CanOpenCalendar = true},
            };
            return applicationUserRoles;
        }
        

        protected override void Seed(ApplicationDbContext context)
        {
            context.ApplicationUserRoles.AddRange(this.ApplicationUserRoles());

            context.SaveChanges();
        }

    }
}