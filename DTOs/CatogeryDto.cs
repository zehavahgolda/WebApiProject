using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    namespace DTOs
    {
        public record CatogeryDto
        (
            int CategoryId,  
            string CategoryName
        )
        {
            public CatogeryDto() : this(0, "") { }
        }
    }
}