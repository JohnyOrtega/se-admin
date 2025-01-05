using Core.Models;

namespace Core.Dtos.ContatoDto;

public class ContatoDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int EmpresaId { get; set; }
    public Empresa Empresa { get; set; }
    public ICollection<Abordagem> Abordagens { get; set; } = [];
}