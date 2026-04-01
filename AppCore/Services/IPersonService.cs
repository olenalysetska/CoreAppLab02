using AppCore.Dto;
using AppCore.Entities;

namespace AppCore.Services;

public interface IPersonService
{
    Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size);
    Task<PersonDto?> FindById(Guid id);
    Task CreatePerson(CreatePersonDto dto);
    Task UpdatePerson(Guid id, UpdatePersonDto dto);
    Task DeletePerson(Guid id);
    Task<Note> AddNoteToPerson(Guid personId, CreateNoteDto noteId);
    Task<PersonDto?> GetPerson(Guid personId);
}