using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public AuthenticationResult Login(string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is not User user)
                throw new Exception("User With Given Email Does Not Exist");

            if (user.Password != password)
                throw new Exception("Invalid Password");

            var token = _jwtTokenGenerator.GenerateToken(
                user);

            return new AuthenticationResult(
                user,
                token);
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is not null)
                throw new DublicateEmailException();
                //throw new Exception("User With Given Email Already Exist");

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                EMail = email,
                Password = password
            };

            _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(
                user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
