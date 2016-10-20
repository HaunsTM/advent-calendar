using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace advent_calendar.Models.POCO
{
    public class ApplicationUserRole
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        public bool IsSuperUser { get; set; }
        public bool CanOpenCalendar { get; set; }
        public bool CanCreateCalendar { get; set; }
        public bool CanCreateUser { get; set; }
        public string Name { get; set; }

        #region Navigation properties

        public virtual List<ApplicationUser> ApplicationUsers { get; set; }

        #endregion

        public ApplicationUserRole()
        {
            this.ApplicationUsers = new List<ApplicationUser>();
        }
    }
}