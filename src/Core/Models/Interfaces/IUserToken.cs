namespace Core.Models.Interfaces;

public interface IUserToken
{
    Guid Id { get; }
    string Email { get; }
    string Name { get; }
    string Role { get; }
}