using Core.Models;

namespace Core.Dtos.ProprietarioDto;

public class ProprietarioDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Source { get; set; }
    public string Telephone { get; set; }
    public string Address { get; set; }
    public string Neighboor { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Email { get; set; }
    public string Observations { get; set; }
    public ICollection<Imovel> Imoveis { get; set; }
    public int ImoveisCount { get; set; }
}