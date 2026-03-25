using AppCore.Dto;
using AppCore.Services;
using AppCore.Entities;
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
        return Ok(result); // Возвращает 200 OK и список людей в J
    }

    // Поиск человека по ID: 
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var person = await service.FindById(id);
        if (person == null) return NotFound();
        return Ok(person);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonDto dto)
    {
        await service.CreatePerson(dto);
        return Created("api/contacts", dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePersonDto dto)
    {
        var person = await service.FindById(id);
        if (person == null) return NotFound();
        
        await service.UpdatePerson(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await service.DeletePerson(id);
        return NoContent();
    }
    
    [HttpPost("{contactId:guid}/notes")]
    [ProducesResponseType(typeof(NoteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddNote(
        [FromRoute] Guid contactId,
        [FromBody] CreateNoteDto dto)
    {
        var note = await service.AddNoteToPerson(contactId, Dto);
        return CreatedAtAction(nameof(GetNotes), new { contactId }, note);
    }

    [HttpGet("{contactId:guid}/notes")]
    [ProducesResponseType(typeof(IEnumerable<NoteDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNotes([FromRoute] Guid contactId)
    {
        var person = await service.GetPerson(contactId);
        return Ok(person.Notes);
    }
}