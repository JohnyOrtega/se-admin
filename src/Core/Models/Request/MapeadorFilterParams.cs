namespace Core.Models.Request;

public class MapeadorFilterParams : PaginationParams
{
    public string? Name { get; set; }
    public string? City { get; set; }
    public string? Zone { get; set; }
    public string? Vehicle { get; set; }
    public string? CameraType { get; set; }
    public string? CelphoneModel { get; set; }
    public DateTime? LastMapping { get; set; }
}