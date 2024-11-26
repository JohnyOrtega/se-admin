namespace Core.Dtos.Login;

public record LoginResponseDto(
    string Token, 
    string RefreshToken, 
    DateTime TokenExpiration
);