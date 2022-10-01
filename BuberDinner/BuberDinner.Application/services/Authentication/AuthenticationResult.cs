﻿using BuberDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.services.Authentication
{
    public record AuthenticationResult(
        User User,
        string Token);
}
