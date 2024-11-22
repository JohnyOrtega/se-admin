using AutoMapper;
using Core.Dtos;
using Core.Models.Response;

namespace Api.Configurations.Mappers;

public class PagedResponseProfile : Profile
{
    public PagedResponseProfile()
    {
        CreateMap(typeof(PagedResponse<>), typeof(PagedResponseDto<>));
    }
}