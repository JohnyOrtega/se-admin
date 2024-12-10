using AutoMapper;
using Core.Dtos.EmpresaDto;
using Core.Models;

namespace Api.Configurations.Mappers;

public class PedidoProfile : Profile
{
    public PedidoProfile()
    {
        CreateMap<Pedido, EmpresaDto>().ReverseMap();
        CreateMap<EmpresaCreateDto, Pedido>();
        CreateMap<EmpresaUpdateDto, Pedido>();
    }
}