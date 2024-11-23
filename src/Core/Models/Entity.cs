namespace Core.Models;

public class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
}