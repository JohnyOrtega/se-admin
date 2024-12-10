using Core.Models;

namespace Core.Dtos.AbordagemDto;

public class AbordagemDto
{
    public Guid Id { get; set; }
    public Guid ContatoId { get; set; }
    public Contato Contato { get; set; }
    public string Telephone { get; set; }
    public string NegotiationStatus { get; set; }
    public string Comment { get; set; }
    public bool ContactAddressed { get; set; }
    public string ApproachType { get; set; }
    public DateTime? LastApproachDate { get; set; }
    public DateTime? NextApproachDate { get; set; }
}