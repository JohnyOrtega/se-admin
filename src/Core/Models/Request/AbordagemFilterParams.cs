namespace Core.Models.Request;

public class AbordagemFilterParams : PaginationParams
{
    public string? Telephone { get; set; }
    public string? Status { get; set; }
    public string? Comment { get; set; }
    public bool? ContactAddressed { get; set; }
    public string? ApproachType { get; set; }
    public DateTime? LastApproachDate { get; set; }
    public DateTime? NextApproachDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

}