namespace Core.Models.Request;

public class EmpresaFilterParams : PaginationParams
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}