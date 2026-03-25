using AppCore.Entities;

namespace AppCore.Dto;

public record PersonDto : ContactDtos
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? MiddleName { get; set; }
    public string? Position { get; init; }
    public DateTime? BirthDate { get; init; }
    public Gender Gender { get; init; }
    public Guid? EmployerId { get; init; }
    public List<NoteDto> Notes { get; init; } = new(); // <-- add this

    public static PersonDto FromEntity(Person person)
    {
        return new PersonDto
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            MiddleName = person.MiddleName,
            Email = person.Email,
            Phone = person.Phone,
            Position = person.Position,
            BirthDate = person.BirthDate,
            Gender = person.Gender,
            Status = person.Status,
            CreatedAt = person.CreatedAt,
            Tags = person.Tags.Select(t => t.Name).ToList(),
            EmployerId = person.Employer?.Id,
            Notes = person.Notes?.Select(n => new NoteDto // <-- add this
            {
                Id = n.Id,
                Content = n.Content
            }).ToList() ?? new List<NoteDto>(),
            Address = new AddressDto(
                person.Address.Street,
                person.Address.City,
                person.Address.PostalCode,
                person.Address.Country,
                person.Address.Type)
        };
    }
};