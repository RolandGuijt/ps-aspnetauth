using Globomantics.Models;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Identity;
using System;

namespace Globomantics.Areas.Identity.Data;

public class ApplicationUser: IdentityUser
    {
        public DateTime CareerStarted { get; set; }
    }
