using System;
using System.Collections.Generic;

namespace SimpleApp.Core.Models
{
    public class Order : BaseModel
    {
        public Order()
        {
            PlacedOn = DateTime.Now;
            Status = OrderStatus.Placed;
        }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime PlacedOn { get; set; }
        public DateTime? FinalizedOn { get; set; }
        public DateTime? CancelledOn { get; set; }
        public OrderStatus Status { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
