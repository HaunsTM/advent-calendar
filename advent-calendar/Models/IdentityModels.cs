﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace advent_calendar.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole,
    CustomUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        #region Custom

        public virtual ApplicationUser IsAdministratedBy { get; set; }

        #region Navigation properties

        public virtual advent_calendar.Models.POCO.ApplicationUserRole ApplicationUserRole { get; set; }
        public virtual System.Collections.Generic.List<advent_calendar.Models.POCO.Calendar> Calendars { get; set; }

        #endregion

        public ApplicationUser()
        {
            this.Calendars = new System.Collections.Generic.List<advent_calendar.Models.POCO.Calendar>();
        }
        
        #endregion

    }
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole,
    int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
#if DEBUG
            Database.SetInitializer<ApplicationDbContext>(new AdventCalendarDBInitializer<ApplicationDbContext>());
#endif
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<advent_calendar.Models.POCO.ApplicationUserRole> ApplicationUserRoles { get; set; }
        public System.Data.Entity.DbSet<advent_calendar.Models.POCO.Calendar> Calendars { get; set; }
        public System.Data.Entity.DbSet<advent_calendar.Models.POCO.Slot> Slots { get; set; }
    }
}