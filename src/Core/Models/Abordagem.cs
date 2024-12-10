namespace Core.Models;

public class Abordagem : Entity
{
    public Guid ContatoId { get; set; }
    public Contato Contato { get; set; }
    public string Telephone { get; set; }
    public string Status { get; set; }
    public string Comment { get; set; }
    public bool ContactAddressed { get; set; }
    public string ApproachType { get; set; }
    public DateTime? LastApproachDate { get; set; }
    public DateTime? NextApproachDate { get; set; }
}
