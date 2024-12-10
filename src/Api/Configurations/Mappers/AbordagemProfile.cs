using AutoMapper;
using Core.Dtos.AbordagemDto;
using Core.Models;

namespace Api.Configurations.Mappers;

public class AbordagemProfile : Profile
{
    public AbordagemProfile()
    {
        CreateMap<Abordagem, AbordagemDto>().ReverseMap();
        CreateMap<AbordagemCreateDto, Abordagem>();
        CreateMap<AbordagemUpdateDto, Abordagem>();
    }
}