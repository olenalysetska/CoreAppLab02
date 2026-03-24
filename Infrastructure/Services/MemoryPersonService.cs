using AppCore.Dto;
using AppCore.Repositories;
using AppCore.Services;

namespace Infrastructure.Services;

public class MemoryPersonService(IContactUnitOfWork unitOfWork) : IPersonService
{
    public async Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size)
    {
        var result = await unitOfWork.Persons.FindPagedAsync(page, size);
        var dtos = result.Items.Select(PersonDto.FromEntity).ToList();
        return new PagedResult<PersonDto>(dtos, result.TotalCount, result.Page, result.PageSize);
    }

    // Остальные методы  просто добавь пустые с throw new NotImplementedException()
    public async Task<PersonDto?> FindById(Guid id)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(id);
        return person == null ? null : PersonDto.FromEntity(person);
    }

    public Task<PersonDto> CreatePerson(CreatePersonDto dto)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePerson(Guid id, UpdatePersonDto dto)
    {
        throw new NotImplementedException();
    }

    public Task DeletePerson(Guid id)
    {
        throw new NotImplementedException();
    }
}