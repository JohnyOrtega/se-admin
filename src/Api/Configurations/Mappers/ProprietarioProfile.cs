using AutoMapper;
using Core.Dtos.ProprietarioDto;
using Core.Models;

namespace Api.Configurations.Mappers;

public class ProprietarioProfile : Profile
{
    public ProprietarioProfile()
    {
        CreateMap<Proprietario, ProprietarioDto>().ForMember(dest => dest.ImoveisCount, opt => opt.MapFrom(src => src.Imoveis.Count));
        CreateMap<Proprietario, ProprietarioCreateDto>().ReverseMap();
        CreateMap<Proprietario, ProprietarioUpdateDto>().ReverseMap();
    }
}