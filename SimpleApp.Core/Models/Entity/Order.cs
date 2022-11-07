using System;
using System.Collections.Generic;
using SimpleApp.Core.Enums;

namespace SimpleApp.Core.Models.Entity
{
    public class Order : BaseModel
    {
        public Order()
        {
            PlacedAt = DateTime.UtcNow;
            Status = OrderStatus.Placed;
        }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime? FinalizedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public OrderStatus Status { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
