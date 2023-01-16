using System.ComponentModel.DataAnnotations;

namespace UsingIdentityWithApi.Application.Users.Query.ForgetPassword
{
    public class ForgetPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
