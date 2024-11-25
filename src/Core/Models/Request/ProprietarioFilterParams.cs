namespace Core.Models.Request;

public class ProprietarioFilterParams : PaginationParams
{
    public string? Name { get; set; }
    public string? City { get; set; }
}