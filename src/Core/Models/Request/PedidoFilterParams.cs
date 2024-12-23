namespace Core.Models.Request;

public class PedidoFilterParams : PaginationParams
{
    public string? Performer { get; set; }
    public string? Expander { get; set; }
    public string? Coordinator { get; set; }
    public DateTime? EntryDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public string? Client { get; set; }
    public string? Order { get; set; }
    public string? PropertyType { get; set; }
    public int? ParkingSpaces { get; set; }
    public string? Status { get; set; }
    public string? ZeroPoint { get; set; }
    public string? PropertyValue { get; set; }
    public bool? OnlineCreated { get; set; }
    public DateTime? OnlineDate { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public bool? MappingCompleted { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}