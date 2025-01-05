namespace Core.Dtos.EmpresaDto;

public class EmpresaCreateDto
{
    public Guid Id { get; set; }
    public string FantasyName { get; set; } = string.Empty;
    public string SocialReason { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
}