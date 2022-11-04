using System;
using System.Collections.Generic;

namespace SimpleApp.WebApi.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime? FinalizedAt { get; set; }
        public DateTime? CancelledAT { get; set; }
        public string Status { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
