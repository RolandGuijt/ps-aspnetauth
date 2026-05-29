using Globomantics.Client.Models;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Globomantics.Server.Controllers;

[ApiController]
    [Route("/api/conference")]
    [Authorize]
    public class ConferenceController(IConferenceRepository repo) : Controller {
        private readonly IConferenceRepository _Repo = repo;
        
        public IEnumerable<ConferenceModel> GetAll()
        {
            return _Repo.GetAll();
        }

        [HttpPost]
        public IActionResult Add(ConferenceModel model) 
        { 
            var id = _Repo.Add(model);
            return Ok(id);
        }
    }
