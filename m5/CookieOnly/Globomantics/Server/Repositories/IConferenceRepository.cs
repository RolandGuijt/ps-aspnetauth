using Globomantics.Client.Models;
using System.Collections.Generic;

namespace Globomantics.Repositories;

public interface IConferenceRepository
{
    int Add(ConferenceModel model);
    IEnumerable<ConferenceModel> GetAll();
}
