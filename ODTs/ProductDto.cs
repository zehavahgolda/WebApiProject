using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record ProductDto
        (
        int ProductId,
        string? ProductName,
        double? Price,
       // string Catogeryname,
        string Description
        );
   
}
