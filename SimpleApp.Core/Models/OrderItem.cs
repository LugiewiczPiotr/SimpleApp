using System;

namespace SimpleApp.Core.Models
{
    public class OrderItem 
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
