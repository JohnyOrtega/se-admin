using AutoMapper;
using Core.Dtos;
using Core.Models;

namespace Api.Configurations.Mappers;

public class CemiterioProfile : Profile
{
    public CemiterioProfile()
    {
        CreateMap<Cemiterio, CemiterioDto>().ReverseMap();
    }
}