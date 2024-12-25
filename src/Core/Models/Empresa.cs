namespace Core.Models;

public class Empresa : Entity
{
    public string FantasyName { get; set; }
    public string SocialReason { get; set; }
    public string Category { get; set; }
    public string Telephone { get; set; }
    public ICollection<Contato> Contatos { get; set; }
}
