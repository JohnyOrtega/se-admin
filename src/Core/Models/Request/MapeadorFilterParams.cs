namespace Core.Models.Request;

public class MapeadorFilterParams : PaginationParams
{
    public string? Name { get; set; }
    public string? City { get; set; }
    public string? Vehicle { get; set; }
    public DateTime? LastMapping { get; set; }
}