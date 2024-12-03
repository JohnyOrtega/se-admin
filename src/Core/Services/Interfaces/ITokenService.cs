using System.Security.Claims;
using Core.Models;
using Core.Models.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user);
}