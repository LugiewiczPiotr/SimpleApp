using System;

namespace SimpleApp.Core.Models
{
    public class Order : BaseModel
    {
       public Guid UserId { get; set; }
       public User User { get; set; }
    }
}
