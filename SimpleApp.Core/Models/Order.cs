using System;
using System.Collections.Generic;

namespace SimpleApp.Core.Models
{
    public class Order : BaseModel
    {
       public Guid UserId { get; set; }
       public User User { get; set; }
       public string Date { get; set; }
       public string Status { get; set; }
       public ICollection<OrderItem> OrderItems { get; set; }
    }
}
