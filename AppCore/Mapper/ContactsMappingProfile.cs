using AutoMapper;
using AppCore.Entities;
using AppCore.Dto;

namespace AppCore.Mapper;

public class ContactsMappingProfile : Profile
{
    public ContactsMappingProfile()
    {
        // 1. Из "Человека" в "Карточку" (Person -> PersonDto)
        CreateMap<Person, PersonDto>()
            // Говорим, как собрать FullName из двух кусков
            .ForMember(dest => dest.FullName, 
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            // Говорим, как достать название компании
            .ForMember(dest => dest.EmployerName, 
                opt => opt.MapFrom(src => src.Employer != null ? src.Employer.Name : null));

        // 2. Из "Черновика" в "Человека" (Для создания)
        CreateMap<CreatePersonDto, Person>();

        // 3. Из "Черновика правки" в "Человека" (Для изменения)
        CreateMap<UpdatePersonDto, Person>();

        // 4. Для адреса (в обе стороны сразу благодаря ReverseMap)
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}