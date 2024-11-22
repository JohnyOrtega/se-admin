using AutoMapper;
using Core.Dtos;
using Core.Models;

namespace Api.Configurations.Mappers;

public class MotoboyProfile : Profile
{
    public MotoboyProfile()
    {
        CreateMap<Motoboy, MotoboyDto>().ReverseMap();
    }
}