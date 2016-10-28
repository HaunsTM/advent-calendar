using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using API.Models.POCO;

namespace API.Models
{
    public class AdventCalendarDBInitializer<T> : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        private List<ApplicationUserRole> ApplicationUserRoles()
        {
            var applicationUserRoles = new List<ApplicationUserRole>
            {
                new ApplicationUserRole { Active = true, Name = ConfigurationManager.AppSettings["STRING_SUPER_ADMINISTRATOR"], IsSuperUser = true, CanCreateCalendar = true, CanCreateUser = true, CanOpenCalendar = true},
                new ApplicationUserRole { Active = true, Name = ConfigurationManager.AppSettings["STRING_USER_ADMINISTRATOR"], IsSuperUser = false, CanCreateCalendar = true, CanCreateUser = true, CanOpenCalendar = true},
                new ApplicationUserRole { Active = true, Name = ConfigurationManager.AppSettings["STRING_STANDARD_USER"], IsSuperUser = false, CanCreateCalendar = false, CanCreateUser = false, CanOpenCalendar = true},
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