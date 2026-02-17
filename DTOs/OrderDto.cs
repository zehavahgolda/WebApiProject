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
        int OrderSum,
        string UserFirstName,
        string UserlastName
    )
    {

        public OrderDto() : this(0, DateOnly.FromDateTime(DateTime.Now), 0, "", "") { }
    }
}