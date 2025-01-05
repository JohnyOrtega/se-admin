namespace Core.Models.Request;

public class ContatoFilterParams : PaginationParams
{
    public string? Name { get; set; }
    public string? Position { get; set; }
    public string? Telephone { get; set; }
    public string? Email { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string AreaOfActivity { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}