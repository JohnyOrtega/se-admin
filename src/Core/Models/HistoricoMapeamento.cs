namespace Core.Models;

public class HistoricoMapeamento : Entity
{
    public Guid MapeadorId { get; set; }
    public virtual Mapeador Mapeador { get; set; }

    public DateTime MappingDate { get; set; }
    public string CameraType { get; set; }
    public string RouteLink { get; set; }
    public decimal Value { get; set; }
}
