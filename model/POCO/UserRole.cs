using System.ComponentModel.DataAnnotations;
using model.Interfaces;

namespace model.POCO
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
        
        public virtual Calendar Calendar { get; set; }

        #endregion

        public UserRole()
        {
        }
    }
}