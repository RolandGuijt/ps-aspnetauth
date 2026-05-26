using Microsoft.AspNetCore.Identity;
using System;

// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.
namespace IdentityProvider.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public DateTime CareerStarted { get; set; }
}
