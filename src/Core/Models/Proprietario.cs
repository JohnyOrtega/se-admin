namespace Core.Models;

public class Proprietario : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<Imovel> Imoveis { get; set; } = [];
    public string Observations { get; set; } = string.Empty;
}