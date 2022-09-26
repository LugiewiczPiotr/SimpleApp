using System.Collections.Generic;

namespace SimpleApp.WebApi.DTO
{
    public class ManageOrderDto
    {
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
