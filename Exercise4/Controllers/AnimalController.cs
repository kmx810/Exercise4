namespace Exercise4.Controllers;

using Microsoft.AspNetCore.Mvc;
using Exercise4.Models;


[ApiController]
[Route("api/animals")]
public class AnimalController : ControllerBase
{
    private static readonly List<Animal> _animals = new()
    {
        new Animal { Id = 1, Name = "Burek", Category = "Dog", Weight = 12.5, FurColor = "Brown" },
        new Animal { Id = 2, Name = "Mruczek", Category = "Cat", Weight = 4.3, FurColor = "Black" },
        new Animal { Id = 3, Name = "Kiki", Category = "Parrot", Weight = 0.5, FurColor = "Green" }
    };

    [HttpGet]
    public IActionResult GetAnimals()
    {
        return Ok(_animals);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimal(int id)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
        {
            return NotFound($"Animal with id {id} was not found.");
        }

        return Ok(animal);
    }

    [HttpPost]
    public IActionResult CreateAnimal(Animal animal)
    {
        animal.Id = _animals.Count > 0 ? _animals.Max(a => a.Id) + 1 : 1;
        _animals.Add(animal);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, Animal updatedAnimal)
    {
        var existingAnimal = _animals.FirstOrDefault(a => a.Id == id);

        if (existingAnimal == null)
        {
            return NotFound($"Animal with id {id} was not found.");
        }

        existingAnimal.Name = updatedAnimal.Name;
        existingAnimal.Category = updatedAnimal.Category;
        existingAnimal.Weight = updatedAnimal.Weight;
        existingAnimal.FurColor = updatedAnimal.FurColor;

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animalToDelete = _animals.FirstOrDefault(a => a.Id == id);
        if (animalToDelete == null)
        {
            return NoContent();
        }

        _animals.Remove(animalToDelete);
        return NoContent();
    }
}
