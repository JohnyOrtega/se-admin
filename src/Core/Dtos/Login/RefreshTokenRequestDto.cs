namespace Core.Dtos.Login;

public record RefreshTokenRequestDto(
    string AccessToken, 
    string RefreshToken
);