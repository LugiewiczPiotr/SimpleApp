using System.Collections.Generic;
using SimpleApp.Core.Models;

namespace SimpleApp.WebApi.DTO
{
    public class ManageOrderDto
    {
        public ICollection<OrderItemDto> OrderItems { get; set; }
        public StatusOrder Status { get; set; }
    }
}
