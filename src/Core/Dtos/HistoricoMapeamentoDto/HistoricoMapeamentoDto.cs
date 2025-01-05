namespace Core.Dtos.HistoricoMapeamentoDto;

public class HistoricoMapeamentoDto
{
    public Guid Id { get; set; }
    public DateTime MappingDate { get; set; }
    public string CameraType { get; set; } = string.Empty;
    public string RouteLink { get; set; } = string.Empty;
    public decimal Value { get; set; }
}
