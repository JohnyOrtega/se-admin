using AutoMapper;
using Core.Dtos;
using Core.Models;

namespace Api.Configurations.Mappers;

public class MapeadorProfile : Profile
{
    public MapeadorProfile()
    {
        CreateMap<Mapeador, MapeadorDto>().ReverseMap();
    }
}