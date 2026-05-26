using Globomantics.Models;
using Globomantics.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Globomantics.Client.ApiServices;

public interface IConferenceApiService
    {
        Task Add(ConferenceModel model);
        Task<IEnumerable<ConferenceModel>> GetAll();
    }
