using System.ComponentModel.DataAnnotations;

namespace UsingIdentityWithApi.Application.Users.Query.Login
{
    public class LoginUserDto
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
