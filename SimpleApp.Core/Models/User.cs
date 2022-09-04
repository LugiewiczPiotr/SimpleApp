﻿using System.Collections.Generic;

namespace SimpleApp.Core.Models
{
    public class User : BaseModel
    {
        public string Email { get; set; }   
        public string Password { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
