using Core.Models;

namespace Core.Dtos.AbordagemDto;

public class AbordagemDto
{
    public Guid Id { get; set; }
    public Guid ContatoId { get; set; }
    public Contato Contato { get; set; }
    public string Telephone { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public bool ContactAddressed { get; set; }
    public string ApproachType { get; set; } = string.Empty;
    public DateTime? LastApproachDate { get; set; }
    public DateTime? NextApproachDate { get; set; }
}