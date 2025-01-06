using Core.Models;

namespace Core.Dtos.ProprietarioDto;

public class ProprietarioDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
    public string Observations { get; set; } = string.Empty;
    public ICollection<Imovel> Imoveis { get; set; } = [];
    public int ImoveisCount { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}