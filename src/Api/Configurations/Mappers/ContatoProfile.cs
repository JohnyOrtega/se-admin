using AutoMapper;
using Core.Dtos.ContatoDto;
using Core.Models;

namespace Api.Configurations.Mappers;

public class ContatoProfile : Profile
{
    public ContatoProfile()
    {
        CreateMap<Contato, ContatoDto>().ReverseMap();
        CreateMap<ContatoCreateDto, Contato>();
        CreateMap<ContatoUpdateDto, Contato>();
    }
}