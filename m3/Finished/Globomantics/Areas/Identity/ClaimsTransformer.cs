using Globomantics.Models;
using Globomantics.Repositories;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Globomantics.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Globomantics.Areas.Identity;

public class ClaimsTransformer(IUserStore<ApplicationUser> userStore) : IClaimsTransformation {
        private readonly IUserStore<ApplicationUser> _UserStore = userStore;

        
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        { 
            var clonedPrincipal = principal.Clone();
            if (clonedPrincipal.Identity == null)
                return clonedPrincipal;
            var identity = (ClaimsIdentity)clonedPrincipal.Identity;

            var existingClaim = identity.Claims.FirstOrDefault(c => c.Type == "careerstarted");
            if (existingClaim != null)
                return clonedPrincipal;

            var nameIdClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (nameIdClaim == null)
                return clonedPrincipal;

            var user = await _UserStore.FindByIdAsync(nameIdClaim.Value, CancellationToken.None);
            if (user != null)
                identity.AddClaim(new Claim("careerstarted", user.CareerStarted.ToString()));

            return clonedPrincipal;    
        }
    }
