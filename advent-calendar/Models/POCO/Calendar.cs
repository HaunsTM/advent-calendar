using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using advent_calendar.Models.Interfaces;

namespace advent_calendar.Models.POCO
{
    public class Calendar: ICalendar
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        public int Year { get; set; }

        public string ContentType { get; set; }
        public byte[] Content { get; set; } /*http://www.entityframeworktutorial.net/code-first/code-first-conventions.aspx*/
        public string Name { get; set; }

        #region Navigation properties

        public virtual List<Slot> Slots { get; set; }
        public virtual List<ApplicationUser> ApplicationUsers { get; set; }

        #endregion

        public Calendar()
        {
            this.Slots = new List<Slot>();
            this.ApplicationUsers = new List<ApplicationUser>();
        }
    }
}