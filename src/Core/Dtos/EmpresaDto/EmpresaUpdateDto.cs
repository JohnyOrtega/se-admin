namespace Core.Dtos.EmpresaDto;

public class EmpresaUpdateDto
{
    public Guid Id { get; set; }
    public string FantasyName { get; set; }
    public string SocialReason { get; set; }
    public string Category { get; set; }
    public string Sector { get; set; }
    public string Telephone { get; set; }
}