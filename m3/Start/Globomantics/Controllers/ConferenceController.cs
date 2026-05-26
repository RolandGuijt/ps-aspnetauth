using Globomantics.Models;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Globomantics.Controllers;

public class ConferenceController(IConferenceRepository repo) : Controller {
    private readonly IConferenceRepository repo = repo;

    

    public IActionResult Index()
    {
        ViewBag.Title = "Organizer - Conference Overview";
        return View(repo.GetAll());
    }

    public IActionResult Add()
    {
        ViewBag.Title = "Organizer - Add Conference";
        return View(new ConferenceModel());
    }

    [HttpPost]
    public IActionResult Add(ConferenceModel model)
    {
        if (ModelState.IsValid)
            repo.Add(model);

        return RedirectToAction("Index");
    }
}
