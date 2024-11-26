namespace Core.Dtos.Login;

public record RefreshTokenRequestDto(
    string Token, 
    string RefreshToken
);