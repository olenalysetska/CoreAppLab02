using AppCore.Dto;
using AppCore.Entities;
using AppCore.Repositories;
using AppCore.Services;
using AutoMapper;

namespace AppCore.Services;

public class PersonService(IContactUnitOfWork unitOfWork, IMapper mapper) : IPersonService
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

    public Task<Note> AddNoteToPerson(Guid personId, CreateNoteDto noteId)
    {
        throw new NotImplementedException();
    }

    public Task<PersonDto?> GetPerson(Guid personId)
    {
        throw new NotImplementedException();
    }

    public async Task CreatePerson(CreatePersonDto dto)
    {
        var person = mapper.Map<CreatePersonDto, Person>(dto);
        await unitOfWork.Persons.AddAsync(person);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdatePerson(Guid id, UpdatePersonDto dto)
    {
        var existing = await unitOfWork.Persons.FindByIdAsync(id);
        if (existing is null) return;

        mapper.Map(dto, existing);
        await unitOfWork.Persons.UpdateAsync(existing);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeletePerson(Guid id)
    {
        await unitOfWork.Persons.RemoveByIdAsync(id);
        await unitOfWork.SaveChangesAsync();
    }
}