using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using model.Interfaces;

namespace model.POCO
{
    public class Calendar: ICalendar
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        public int Year { get; set; }

        #region Navigation properties
        
        public virtual List<Slot> Slots { get; set; }
        public virtual List<User> Users { get; set; }

        #endregion

        public Calendar()
        {
            this.Slots = new List<Slot>();
            this.Users = new List<User>();
        }
    }
}