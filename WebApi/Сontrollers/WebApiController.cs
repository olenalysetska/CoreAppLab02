using AppCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactsController(IPersonService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 1, int size = 10)
    {
        var result = await service.FindAllPeoplePaged(page, size);
        return Ok(result); // Возвращает 200 OK и список людей в JSON
    }

    // Поиск человека по ID: 
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var person = await service.FindById(id);
        if (person == null) return NotFound();
        return Ok(person);
    }
}