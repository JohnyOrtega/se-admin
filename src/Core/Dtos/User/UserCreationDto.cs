namespace Core.Dtos.User;

public record UserCreationDto
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public List<string>? Roles { get; init; }
}