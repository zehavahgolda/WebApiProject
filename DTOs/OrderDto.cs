using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record OrderDto
(
    int OrderId,
    DateOnly OredrDate, 
    double OrderSum,    
    string OrderStatus, 
    string UserFirstName,
    string UserlastName,
    IEnumerable<OrderItemDto> OrderItems 
    )
    {
        public OrderDto() : this(0, DateOnly.FromDateTime(DateTime.Now), 0, "Paid", "", "", new List<OrderItemDto>()) { }
    }
}