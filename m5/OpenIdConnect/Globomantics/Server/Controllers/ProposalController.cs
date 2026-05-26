using Globomantics.Models;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace Globomantics.Server.Controllers;

[ApiController]
    [Route("/api/proposal")]
    [Authorize]
    public class ProposalController(IProposalRepository repo) : Controller {
        private readonly IProposalRepository _Repo = repo;

        

        [HttpGet("{conferenceId}")]
        public IEnumerable<ProposalModel> GetAll(int conferenceId)
        {
            return _Repo.GetAllForConference(conferenceId);
        }

        [HttpPost]
        public IActionResult Add(ProposalModel model) 
        { 
            var id = _Repo.Add(model);
            return Ok(id);
        }

        [HttpGet("approve/{proposalId}")]
        public IActionResult Approve(int proposalId) 
        {
            var prop = _Repo.Approve(proposalId);
            return Ok(prop);
        }
    }
