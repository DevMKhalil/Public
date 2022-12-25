using System.ComponentModel.DataAnnotations;

namespace UsingIdentityWithApi.Application.Users.Query.Register
{
    public class RegisterUserDto
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
