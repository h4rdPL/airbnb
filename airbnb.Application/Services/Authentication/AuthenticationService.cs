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
    public class AuthenticationService : IAuthenticationService
    {
        List<User> _users = new List<User>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public AuthenticationResult Login(UserLoginDTO login)
        {
            var user = _users.FirstOrDefault(x => x.Email == login.Email);

            if (user == null)
            {
                throw new Exception("Podany użytkownik nie istnieje w bazie danych");
            } 
            else if (user.Password != login.Password)
            {
                throw new Exception($"Hasło dla użytkownika {login.Email} nie jest poprawne");
            }
            return new AuthenticationResult(user);  

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public AuthenticationResult Register(UserDTO userDTO)
        {
            if(userDTO.Password is null)
            {
                throw new Exception("Password can not be empty");
            } else if (userDTO.Email is null) 
            {
                throw new Exception("Email can not be empty");
            } else if (userDTO.Username is null)
            {
                throw new Exception("Username can not be empty");
            }

            var user = new User
            {
                Email = userDTO.Email,
                Username = userDTO.Username,
                Password = userDTO.Password,
            };

            _users.Add(user);

            return new AuthenticationResult(user);
        }


    }
}
