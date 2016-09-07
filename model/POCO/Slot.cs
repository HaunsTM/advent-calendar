using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using model.Interfaces;

namespace model.POCO
{
    public class Slot: ISlot
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        public int Number { get; set; }

        public string SlotMessage { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; } /*http://www.entityframeworktutorial.net/code-first/code-first-conventions.aspx*/

        public DateTime EarliestDateOfAllowedOpeningTime { get; set; }
        public Nullable<DateTime> Opened { get; set; }

        #region Navigation properties

        public virtual Calendar Calendar { get; set; }
        
        #endregion

        public Slot()
        {
        }

    }
}