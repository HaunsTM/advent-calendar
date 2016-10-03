using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace advent_calendar.Models.POCO
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        #region Navigation properties

        public virtual UserRole UserRole { get; set; }
        public virtual List<Calendar> Calendars { get; set; }

        #endregion

        public User()
        {
            this.Calendars = new List<Calendar>();
        }
    }
}