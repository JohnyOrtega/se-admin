namespace Core.Models.Request;

public class ProprietarioFilterParams : PaginationParams
{
    public string? Name { get; set; }
    public string? Source { get; set; }
    public string? Telephone { get; set; }
    public string? Address { get; set; }
    public string? Neighboor { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Email { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}