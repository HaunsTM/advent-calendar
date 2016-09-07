using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public virtual ICollection<Slot> Slots { get; set; }
        
        #endregion

        public Calendar()
        {
            Slots = new Collection<Slot>();
        }
    }
}