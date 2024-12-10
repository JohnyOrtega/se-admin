namespace Core.Models.Request;

public class AbordagemFilterParams : PaginationParams
{
    public DateTime? NextApproachDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}