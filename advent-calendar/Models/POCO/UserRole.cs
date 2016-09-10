using System.ComponentModel.DataAnnotations;
using advent_calendar.Models.Interfaces;

namespace advent_calendar.Models.POCO
{
    public class UserRole : IRole
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        public bool IsSuperUser { get; set; }
        public bool CanOpenCalendar { get; set; }
        public bool CanCreateCalendar { get; set; }
        public bool CanCreateUser { get; set; }

        #region Navigation properties

        public virtual ApplicationUser AspNetUser { get; set; }
        public virtual Calendar Calendar { get; set; }

        #endregion

        public UserRole()
        {
        }
    }
}
