namespace Core.Dtos.User;

public record UserResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public List<string>? Roles { get; init; }
}