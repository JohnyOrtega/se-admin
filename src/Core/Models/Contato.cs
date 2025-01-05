namespace Core.Models;

public class Contato : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string AreaOfActivity { get; set; } = string.Empty;
    public string Observations { get; set; } = string.Empty;
    public Guid EmpresaId { get; set; }
    public Empresa Empresa { get; set; }
    public ICollection<Abordagem> Abordagens { get; set; } = [];
}
