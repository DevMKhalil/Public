﻿using System.ComponentModel.DataAnnotations;

namespace UsingIdentityWithApi.Application.Users.Commands.ResetPassword
{
    public class ResetPasswordDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
