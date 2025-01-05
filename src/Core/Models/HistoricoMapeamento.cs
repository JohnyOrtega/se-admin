namespace Core.Models;

public class HistoricoMapeamento : Entity
{
    public Guid MapeadorId { get; set; }
    public virtual Mapeador Mapeador { get; set; }

    public DateTime MappingDate { get; set; }
    public string CameraType { get; set; } = string.Empty;
    public string RouteLink { get; set; } = string.Empty;
    public decimal Value { get; set; }
}
