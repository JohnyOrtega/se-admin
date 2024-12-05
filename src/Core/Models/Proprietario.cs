namespace Core.Models;

public class Proprietario : Entity
{
    public string Name { get; set; }
    public string Source { get; set; }
    public string Telephone { get; set; }
    public string Address { get; set; }
    public string Neighboor { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Email { get; set; }
    public ICollection<Imovel> Imoveis { get; set; }
    public string Observations { get; set; }
}