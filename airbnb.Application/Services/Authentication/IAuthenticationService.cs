using airbnb.Contracts.Authentication;
using airbnb.Domain;
using airbnb.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace airbnb.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Register(UserDTO userDTO);
        AuthenticationResult Login(UserLoginDTO login);
    }


}
