using System.Net.Http.Json;
using Globomantics.Client.Models;

namespace Globomantics.Client.ApiServices;

public class ConferenceApiService(HttpClient client) : IConferenceApiService {
        private readonly HttpClient _Client = client;
        
        public async Task<IEnumerable<ConferenceModel>> GetAll()
        {
            return await _Client
                .GetFromJsonAsync<IEnumerable<ConferenceModel>>("/api/conference");
        }

        public async Task Add(ConferenceModel model)
        {
            await _Client.PostAsJsonAsync("/api/conference", model);
        }
    }
