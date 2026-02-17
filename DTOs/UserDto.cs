using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record UserDto
    (
        int Id,
        string FirstName,
        string LastName,
        string Email,
        string? Password, 
        string? Phone,    
        string? Address  
    )
    {
        public UserDto() : this(0, "", "", "", null, null, null) { }
    }
}