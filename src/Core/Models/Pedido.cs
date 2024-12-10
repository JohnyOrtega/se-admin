namespace Core.Models;

public class Pedido : Entity
{
    public string Client { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string Performer { get; set; }
    public string Coordinator { get; set; }
    public string Expander { get; set; }
    public string Order { get; set; }
    public string PropertyValue { get; set; }
    public string ZeroPoint { get; set; }
    public string Status { get; set; }
    public string Observations { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string PropertyType { get; set; }
    public double TotalArea { get; set; }
    public double BuiltArea { get; set; }
    public int ParkingSpaces { get; set; }
    public double MinimumMeterage { get; set; }
    public double MaximumMeterage { get; set; }
    public string StreetView { get; set; }
    public DateTime StreetViewDate { get; set; }
    public bool OnlineCreated { get; set; }
    public DateTime OnlineDate { get; set; }
    public bool MappingCompleted { get; set; }
    public ICollection<PedidoImovel> PedidoImoveis { get; set; }
}