using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.POCO
{
    public class Slot
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        public int Number { get; set; }

        public string SlotMessage { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; } /*http://www.entityframeworktutorial.net/code-first/code-first-conventions.aspx*/
        [NotMapped]
        public String ContentAsBase64
        {
            get
            {
                // Convert the array to a base 64 string.
                var base64String = Convert.ToBase64String(Content);
                return base64String;
            }
        }

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