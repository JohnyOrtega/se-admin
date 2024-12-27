namespace Core.Models;

public class Contato : Entity
{
    public string Name { get; set; }
    public string Position { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string AreaOfActivity { get; set; }
    public string Observations { get; set; }
    public Guid EmpresaId { get; set; }
    public Empresa Empresa { get; set; }
    public ICollection<Abordagem> Abordagens { get; set; }
}
