using Core.Models;

namespace Core.Dtos.EmpresaDto;

public class EmpresaDto
{
    public Guid Id { get; set; }
    public string FantasyName { get; set; }
    public string SocialReason { get; set; }
    public string Category { get; set; }
    public string Telephone { get; set; }
    public ICollection<Contato> Contatos { get; set; }
}