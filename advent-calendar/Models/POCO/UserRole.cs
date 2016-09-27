using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using advent_calendar.Models.Interfaces;

namespace advent_calendar.Models.POCO
{
    public class UserRole : IUserRole
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        public bool IsSuperUser { get; set; }
        public bool CanOpenCalendar { get; set; }
        public bool CanCreateCalendar { get; set; }
        public bool CanCreateUser { get; set; }

        #region Navigation properties

        public virtual List<ApplicationUser> ApplicationUsers { get; set; }

        #endregion

        public UserRole()
        {
            this.ApplicationUsers = new List<ApplicationUser>();
        }
    }
}