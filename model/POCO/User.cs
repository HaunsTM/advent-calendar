using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.POCO
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        
    }
}
