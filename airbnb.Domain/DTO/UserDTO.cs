using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace airbnb.Domain.DTO
{
    public record struct UserDTO(
            string Username,
            string Email,
            string Password
        );
}
