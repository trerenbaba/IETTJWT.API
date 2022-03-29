﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IETTJWT.Core.Models
{
    public class AppUser : IdentityUser
    {

        public string City { get; set; }
    }
}
