using Core.Models;

namespace Core.Dtos.MapeadorDto;

public class MapeadorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Telephone { get; set; }
    public string City { get; set; }
    public string Zone { get; set; }
    public DateTime LastMapping { get; set; }
    public string Pix { get; set; }
    public string Vehicle { get; set; }
    public string CameraType { get; set; }
    public string CelphoneModel { get; set; }
    public string Observations { get; set; }
    public ICollection<HistoricoMapeamento> HistoricoMapeamentos { get; set; }

}