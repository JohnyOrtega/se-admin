namespace Core.Dtos.User;

public record UserResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public DateTime? UpdatedAt { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? UpdatedBy { get; init; }
}