namespace Core.Models;

public class Empresa : Entity
{
    public string FantasyName { get; set; } = string.Empty;
    public string SocialReason { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public ICollection<Contato> Contatos { get; set; } = [];
}
