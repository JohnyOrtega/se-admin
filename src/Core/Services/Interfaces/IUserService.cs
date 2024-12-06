using Core.Dtos.User;
using Core.Models.Request;
using Core.Models.Response;

namespace Core.Services.Interfaces;

public interface IUserService
{
    Task<PagedResponse<UserResponseDto>> GetWithFilters(UserFilterParams filters);
    Task<UserResponseDto> UpdateAsync(UserDto userUpdateDto);
    Task<UserResponseDto> RegisterAsync(UserCreationDto userCreationDto);
    Task<UserResponseDto> GetById(Guid id);
}