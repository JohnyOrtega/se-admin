namespace Core.Models.Request;

public class ImovelFilterParams : PaginationParams
{
    public string? Address { get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Zone { get; set; }
    public string? PropertyProfile { get; set; }
    public string? Availability { get; set; }
    public decimal? MinRentValue { get; set; }
    public decimal? MaxRentValue { get; set; }
    public decimal? MinSaleValue { get; set; }
    public decimal? MaxSaleValue { get; set; }
    public decimal? MinIptuValue { get; set; }
    public decimal? MaxIptuValue { get; set; }
    public decimal? MinSearchMeterage { get; set; }
    public decimal? MaxSearchMeterage { get; set; }
    public decimal? MinTotalArea { get; set; }
    public decimal? MaxTotalArea { get; set; }
    public string? RealEstate { get; set; }
    public Guid? ProprietarioId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}