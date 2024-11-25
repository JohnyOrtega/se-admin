using AutoMapper;
using Core.Dtos;
using Core.Models;

namespace Api.Configurations.Mappers;

public class ProprietarioProfile : Profile
{
    public ProprietarioProfile()
    {
        CreateMap<Proprietario, ProprietarioDto>().ReverseMap();
    }
}