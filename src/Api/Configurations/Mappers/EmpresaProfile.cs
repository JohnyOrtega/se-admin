using AutoMapper;
using Core.Dtos.EmpresaDto;
using Core.Models;

namespace Api.Configurations.Mappers;

public class EmpresaProfile : Profile
{
    public EmpresaProfile()
    {
        CreateMap<Empresa, EmpresaDto>().ReverseMap();
        CreateMap<EmpresaCreateDto, Empresa>();
        CreateMap<EmpresaUpdateDto, Empresa>();
    }
}