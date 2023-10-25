using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace airbnb.Contracts.Authentication
{
    public record LoginResponse(
            string Email,
            string Username
        );
}
