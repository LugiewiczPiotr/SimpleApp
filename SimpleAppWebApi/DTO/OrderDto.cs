using System;
using System.Collections.Generic;

namespace SimpleApp.WebApi.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime PlacedOn { get; set; }
        public DateTime? FinalizedOn { get; set; }
        public DateTime? CancelledOn { get; set; }
        public string Status { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
