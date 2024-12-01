namespace Exercise4.Controllers;

using Microsoft.AspNetCore.Mvc;
using Exercise4.Models;


[ApiController]
[Route("api/animals/{animalId:int}/visits")]
public class VisitController : ControllerBase
{
    private static readonly List<Visit> _visits = new()
    {
        new Visit { Id = 1, AnimalId = 1, VisitDate = DateTime.Now.AddDays(-10), Description = "Vaccination", Price = 50.0m },
        new Visit { Id = 2, AnimalId = 2, VisitDate = DateTime.Now.AddDays(-5), Description = "General check-up", Price = 30.0m }
    };

    [HttpGet]
    public IActionResult GetVisitsForAnimal(int animalId)
    {
        var visits = _visits.Where(v => v.AnimalId == animalId).ToList();
        if (!visits.Any())
        {
            return NotFound($"No visits found for animal with id {animalId}.");
        }

        return Ok(visits);
    }

    [HttpPost]
    public IActionResult AddVisit(int animalId, Visit visit)
    {
        visit.Id = _visits.Count > 0 ? _visits.Max(v => v.Id) + 1 : 1;
        visit.AnimalId = animalId;
        _visits.Add(visit);

        return StatusCode(StatusCodes.Status201Created);
    }
}
