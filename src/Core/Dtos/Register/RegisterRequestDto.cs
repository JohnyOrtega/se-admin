namespace Core.Dtos.Register;

public record RegisterRequestDto(string Email, string Password, string Name, string Role);