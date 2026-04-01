using AppCore.Dto;
using AppCore.Entities;
using AppCore.Repositories;
using AppCore.Services;
using AutoMapper;
using AppCore.Exceptions;

namespace Infrastructure.Services;

public class MemoryPersonService(IContactUnitOfWork unitOfWork, IMapper mapper) : IPersonService
{
    public async Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size)
    {
        var result = await unitOfWork.Persons.FindPagedAsync(page, size);
        var dtos = result.Items.Select(PersonDto.FromEntity).ToList();
        return new PagedResult<PersonDto>(dtos, result.TotalCount, result.Page, result.PageSize);
    }

    public async Task<PersonDto?> FindById(Guid id)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(id);
        return person == null ? null : PersonDto.FromEntity(person);
    }

    public async Task CreatePerson(CreatePersonDto dto)
    {
        var person = mapper.Map<CreatePersonDto, Person>(dto);
        await unitOfWork.Persons.AddAsync(person);
    }

    public async Task UpdatePerson(Guid id, UpdatePersonDto dto)
    {
        var person = mapper.Map<UpdatePersonDto, Person>(dto);
        person.Id = id;
        await unitOfWork.Persons.AddAsync(person);
    }

    public async Task DeletePerson(Guid id)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(id);
        if (person != null)
            await unitOfWork.Persons.RemoveByIdAsync(person.Id);
    }

    public Task<Note> AddNoteToPerson(Guid personId, CreatePersonDto noteId)
    {
        throw new NotImplementedException();
    }

    public async Task<Note> AddNoteToPerson(Guid personId, CreateNoteDto noteDto)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(personId);

        if (person == null)
            throw new ContactNotFoundException($"Person with id={personId} not found!");

        person.Notes ??= new List<Note>();

        var note = new Note { Content = noteDto.Content };
        person.Notes.Add(note);

        await unitOfWork.Persons.UpdateAsync(person);
        await unitOfWork.SaveChangesAsync();

        return note;
    }

    public async Task<PersonDto> GetPerson(Guid personId)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(personId);

        if (person == null)
            throw new ContactNotFoundException($"Person with id={personId} not found!");

        return PersonDto.FromEntity(person);
    }
}