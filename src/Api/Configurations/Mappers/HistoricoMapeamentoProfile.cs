using AutoMapper;
using Core.Dtos.HistoricoMapeamentoDto;
using Core.Models;

namespace Api.Configurations.Mappers;

public class HistoricoMapeamentoProfile : Profile
{
    public HistoricoMapeamentoProfile()
    {
        CreateMap<HistoricoMapeamento, HistoricoMapeamentoDto>().ReverseMap();
        CreateMap<HistoricoMapeamentoCreateDto, HistoricoMapeamento>();
        CreateMap<HistoricoMapeamentoUpdateDto, HistoricoMapeamento>();
    }
}