using System;

namespace SimpleApp.Core.Models.Entities
{
    public class OrderItem
    {
        public decimal Quantity { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
