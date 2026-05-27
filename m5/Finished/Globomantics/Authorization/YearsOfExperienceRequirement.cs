using Globomantics.Models;
using Globomantics.Repositories;
using System;

namespace Globomantics.Authorization;

public class YearsOfExperienceRequirement(int yearsOfExperienceRequired) : IAuthorizationRequirement {
        
        public int YearsOfExperienceRequired { get; set; }
    }
