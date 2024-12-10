namespace Core.Models.Request;

public class ContatoFilterParams : PaginationParams
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}