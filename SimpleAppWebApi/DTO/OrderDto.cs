using System;
using System.Collections.Generic;

namespace SimpleApp.WebApi.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }   
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
