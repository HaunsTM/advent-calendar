using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.POCO
{
    public class Calendar
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        public int Year { get; set; }

        public string ContentType { get; set; }
        public byte[] Content { get; set; } /*http://www.entityframeworktutorial.net/code-first/code-first-conventions.aspx*/
        [NotMapped]
        public String ContentAsBase64 {
            get
            {
                // Convert the array to a base 64 string.
                var base64String = Convert.ToBase64String(Content);
                return base64String;
            }
        }
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