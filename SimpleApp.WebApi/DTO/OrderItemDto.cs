using System;

namespace SimpleApp.WebApi.DTO
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
