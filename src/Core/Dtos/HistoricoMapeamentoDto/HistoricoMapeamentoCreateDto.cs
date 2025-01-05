namespace Core.Dtos.HistoricoMapeamentoDto;

public class HistoricoMapeamentoCreateDto
{
    public Guid MapeadorId { get; set; }
    public DateTime MappingDate { get; set; }
    public string CameraType { get; set; }
    public string RouteLink { get; set; }
    public decimal Value { get; set; }
}
