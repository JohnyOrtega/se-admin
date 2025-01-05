namespace Core.Dtos.Pedido;

public class PedidoCreateDto
{
    public Guid Id { get; set; }
    public string Client { get; set; } = string.Empty;
    public DateTime EntryDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string Performer { get; set; } = string.Empty;
    public string Coordinator { get; set; } = string.Empty;
    public string Expander { get; set; } = string.Empty;
    public string Order { get; set; } = string.Empty;
    public string PropertyValue { get; set; } = string.Empty;
    public string ZeroPoint { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Observations { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PropertyType { get; set; } = string.Empty;
    public double TotalArea { get; set; }
    public double BuiltArea { get; set; }
    public int ParkingSpaces { get; set; }
    public double MinimumMeterage { get; set; }
    public double MaximumMeterage { get; set; }
    public string StreetView { get; set; } = string.Empty;
    public DateTime StreetViewDate { get; set; }
    public bool OnlineCreated { get; set; }
    public DateTime OnlineDate { get; set; }
    public bool MappingCompleted { get; set; }
}