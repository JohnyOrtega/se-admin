using AutoMapper;
using Core.Dtos.EmpresaDto;
using Core.Dtos.Pedido;
using Core.Models;

namespace Api.Configurations.Mappers;

public class PedidoProfile : Profile
{
    public PedidoProfile()
    {
        CreateMap<Pedido, EmpresaDto>().ReverseMap();
        CreateMap<PedidoCreateDto, Pedido>();
        CreateMap<PedidoUpdateDto, Pedido>();
    }
}