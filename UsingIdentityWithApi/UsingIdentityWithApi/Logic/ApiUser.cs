﻿using Microsoft.AspNetCore.Identity;

namespace UsingIdentityWithApi.Logic
{
    public class ApiUser : IdentityUser<string>
    {
        public string Locale { get; set; } = "ar-EG";
    }
}
