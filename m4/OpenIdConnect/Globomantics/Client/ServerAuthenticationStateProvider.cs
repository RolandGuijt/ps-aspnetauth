using Globomantics.Models;
using Globomantics.Repositories;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace Globomantics.Client;

public class ServerAuthenticationStateProvider(HttpClient httpClient) : AuthenticationStateProvider {
        private readonly HttpClient _HttpClient = httpClient;

        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return new AuthenticationState(await GetUser());
        }

        private async ValueTask<ClaimsPrincipal> GetUser()
        {
            var response = await _HttpClient.GetAsync("/Account/User?slide=false");
            if (!response.IsSuccessStatusCode)
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

            var jsonClaims = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonClaims);
            var claims = await response.Content.ReadFromJsonAsync<IEnumerable<UserClaim>>();

            if (claims == null)
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

            var identity = new ClaimsIdentity(
                nameof(ServerAuthenticationStateProvider), "name", "role");

            foreach (var claim in claims)
            {
                identity.AddClaim(new Claim(claim.Type, claim.Value.ToString()));
            }

            return new ClaimsPrincipal(identity);
        }
    }
