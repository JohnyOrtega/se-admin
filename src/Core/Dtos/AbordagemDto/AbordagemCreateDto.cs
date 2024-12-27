namespace Core.Dtos.AbordagemDto;

public class AbordagemCreateDto
{
    public Guid Id { get; set; }
    public Guid ContatoId { get; set; }
    public string Telephone { get; set; }
    public string Status { get; set; }
    public string Comment { get; set; }
    public bool ContactAddressed { get; set; }
    public string ApproachType { get; set; }
    public string UserEmail { get; set; }
    public DateTime? LastApproachDate { get; set; }
    public DateTime? NextApproachDate { get; set; }
}