using AutoMapper;
using Core.Dtos.MapeadorDto;
using Core.Models;

namespace Api.Configurations.Mappers;

public class MapeadorProfile : Profile
{
    public MapeadorProfile()
    {
        CreateMap<Mapeador, MapeadorDto>().ReverseMap();
        CreateMap<MapeadorCreateDto, Mapeador>();
        CreateMap<MapeadorUpdateDto, Mapeador>();
    }
}