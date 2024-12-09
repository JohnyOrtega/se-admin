using AutoMapper;
using Core.Dtos.Pedido;
using Core.Models;

namespace Api.Configurations.Mappers;

public class PedidoProfile : Profile
{
    public PedidoProfile()
    {
        CreateMap<Pedido, PedidoDto>().ReverseMap();
        CreateMap<PedidoCreateDto, Pedido>();
        CreateMap<PedidoUpdateDto, Pedido>();
    }
}