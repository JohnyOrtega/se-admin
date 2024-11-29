using AutoMapper;
using Core.Dtos;
using Core.Models;

namespace Api.Configurations.Mappers;

public class ImovelProfile : Profile
{
    public ImovelProfile()
    {
        CreateMap<Imovel, ImovelDto>().ReverseMap();
    }
}