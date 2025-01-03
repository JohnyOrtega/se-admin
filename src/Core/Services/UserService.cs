using AutoMapper;
using Core.Dtos.User;
using Core.Exceptions;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    public async Task<PagedResponse<UserResponseDto>> GetWithFilters(UserFilterParams filters)
    {
        var pageNumber = filters.PageNumber;
        var pageSize = filters.PageSize;
        
        var query = _userRepository.GetWithFilters(filters);
        
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        
        var users = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var items = _mapper.Map<List<UserResponseDto>>(users);
        
        return new PagedResponse<UserResponseDto>()
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalItems = totalItems
        };
    }
    
    
    public async Task<UserResponseDto> UpdateAsync(UserDto userDto)
    {
        var user = await _userRepository.GetByIdAsync(userDto.Id);
        
        if (!string.IsNullOrEmpty(userDto.Password))
        {
            var newPasswordHash = _passwordHasher.HashPassword(user, userDto.Password);
            user.PasswordHash = newPasswordHash;
        }
        
        _mapper.Map(userDto, user);
        
        var userUpdated = await _userRepository.UpdateAsync(user);
        return _mapper.Map<UserResponseDto>(userUpdated);
    }
    
    public async Task<UserResponseDto> RegisterAsync(UserCreationDto userCreationDto)
    {
        if (string.IsNullOrWhiteSpace(userCreationDto.Email))
            throw new ArgumentException("Email cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(userCreationDto.Password))
            throw new ArgumentException("Password cannot be null or empty.");
        
        if (string.IsNullOrWhiteSpace(userCreationDto.Name))
            throw new ArgumentException("Name cannot be null or empty.");
        
        if (string.IsNullOrWhiteSpace(userCreationDto.Role))
            throw new ArgumentException("Role cannot be null or empty.");
        
        var existsUser = await _userRepository.ExistsByEmailAsync(userCreationDto.Email);
        if (existsUser)
        {
            throw AlreadyExistsException.For("Usu�rio", userCreationDto.Email);
        }
        
        var user = new User
        {
            Role = userCreationDto.Role,
            Email = userCreationDto.Email,
            Name = userCreationDto.Name,
        };
        
        var passwordHashed = _passwordHasher.HashPassword(user, userCreationDto.Password);
        user.PasswordHash = passwordHashed;
        
        await _userRepository.AddAsync(user);

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto> GetById(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserResponseDto>(user);
    }
}