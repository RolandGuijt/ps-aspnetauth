using Globomantics.Models;
using Globomantics.Repositories;
using System.Threading.Tasks;
using System;

namespace Globomantics.Authorization;

public class ProposalAuthorizationHandler :
        AuthorizationHandler<ProposalRequirement, ProposalModel>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ProposalRequirement requirement, 
            ProposalModel resource)
        {
            if (context.User.Identity.Name.ToLower() == 
                resource.Speaker.ToLower() && !resource.Approved)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
