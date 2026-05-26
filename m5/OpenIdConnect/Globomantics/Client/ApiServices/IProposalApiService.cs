using Globomantics.Models;
using Globomantics.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Globomantics.Client.ApiServices;

public interface IProposalApiService
    {
        Task Add(ProposalModel model);
        Task Approve(int proposalId);
        Task<IEnumerable<ProposalModel>> GetAll(int conferenceId);
    }
