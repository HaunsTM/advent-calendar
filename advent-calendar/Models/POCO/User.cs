﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace advent_calendar.Models.POCO
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }

        #region Navigation properties

        public virtual List<UserRole> Roles { get; set; }

        #endregion

        public User()
        {
            this.Roles = new List<UserRole>();
        }

    }
}