using AutoMapper;
using AppCore.Entities;
using AppCore.Dto;

namespace AppCore.Mapper;

public class ContactsMappingProfile : Profile
{
    public ContactsMappingProfile()
    {
       
        CreateMap<Person, PersonDto>();
        // Говорим, как собрать FullName из двух кусков

        //  Из "Черновика" в "Человека" (Для создания)
        CreateMap<CreatePersonDto, Person>();

        //  Из "Черновика правки" в "Человека" (Для изменения)
        CreateMap<UpdatePersonDto, Person>();

        //  Для адреса (в обе стороны сразу благодаря ReverseMap)
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}