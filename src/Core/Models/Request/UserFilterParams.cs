namespace Core.Models.Request;

public class UserFilterParams : PaginationParams
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }    
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}