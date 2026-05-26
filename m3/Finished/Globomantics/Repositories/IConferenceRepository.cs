using Globomantics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Globomantics.Repositories;

public interface IConferenceRepository
{
    Task<int> Add(ConferenceModel model);
    Task<IEnumerable<ConferenceModel>> GetAll();
    Task<ConferenceModel> GetById(int id);
}
