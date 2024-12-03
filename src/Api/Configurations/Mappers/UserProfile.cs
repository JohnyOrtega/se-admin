using AutoMapper;
using Core.Dtos.User;
using Core.Models;

namespace Api.Configurations.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDto>();
        CreateMap<UserDto, User>();
    }
}