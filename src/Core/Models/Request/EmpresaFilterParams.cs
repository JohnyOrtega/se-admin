namespace Core.Models.Request;

public class EmpresaFilterParams : PaginationParams
{
    public string? FantasyName { get; set; }
    public string? SocialReason { get; set; }
    public string? Category { get; set; }
    public string? Telephone { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

}