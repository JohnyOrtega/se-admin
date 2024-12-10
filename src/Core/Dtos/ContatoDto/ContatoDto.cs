using Core.Models;

namespace Core.Dtos.ContatoDto;

public class ContatoDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public int EmpresaId { get; set; }
    public Empresa Empresa { get; set; }
    public ICollection<Abordagem> Abordagens { get; set; }
}