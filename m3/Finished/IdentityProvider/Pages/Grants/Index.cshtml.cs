using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IdentityProvider.Pages.Grants;

[SecurityHeaders]
[Authorize]
public class Index(IIdentityServerInteractionService interaction,
        IClientStore clients,
        IResourceStore resources,
        IEventService events) : PageModel
{
    private readonly IIdentityServerInteractionService _interaction = interaction;
    private readonly IClientStore _clients = clients;
    private readonly IResourceStore _resources = resources;
    private readonly IEventService _events = events;

    

    public ViewModel View { get; set; }
        
    public async Task OnGet(CancellationToken ct)
    {
        var grants = await _interaction.GetAllUserGrantsAsync(ct);

        List<GrantViewModel> list = [];
        foreach (var grant in grants)
        {
            var client = await _clients.FindClientByIdAsync(grant.ClientId, ct);
            if (client != null)
            {
                var resources = await _resources.FindResourcesByScopeAsync(grant.Scopes, ct);

                var item = new GrantViewModel()
                {
                    ClientId = client.ClientId,
                    ClientName = client.ClientName ?? client.ClientId,
                    ClientLogoUrl = client.LogoUri,
                    ClientUrl = client.ClientUri,
                    Description = grant.Description,
                    Created = grant.CreationTime,
                    Expires = grant.Expiration,
                    IdentityGrantNames = resources.IdentityResources.Select(x => x.DisplayName ?? x.Name).ToArray(),
                    ApiGrantNames = resources.ApiScopes.Select(x => x.DisplayName ?? x.Name).ToArray()
                };

                list.Add(item);
            }
        }

        View = new ViewModel
        {
            Grants = list
        };
    }

    [BindProperty]
    [Required]
    public string ClientId { get; set; }

    public async Task<IActionResult> OnPost(CancellationToken ct)
    {
        await _interaction.RevokeUserConsentAsync(ClientId, ct);

        return RedirectToPage("/Grants/Index");
    }
}

