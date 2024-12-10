namespace Core.Models.Request;

public class AbordagemFilterParams : PaginationParams
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}