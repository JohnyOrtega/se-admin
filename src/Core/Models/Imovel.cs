namespace Core.Models;

public class Imovel : Entity
{
    public string Address { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zone { get; set; }
    public string PropertyProfile { get; set; }
    public string Link { get; set; }
    public string Availability { get; set; }
    public decimal RentValue { get; set; }
    public decimal SaleValue { get; set; }
    public decimal IptuAnnual { get; set; }
    public decimal IptuMonthly { get; set; }
    public decimal SearchMeterage { get; set; }
    public decimal TotalArea { get; set; }
    public string RealEstate { get; set; }
    public Guid ProprietarioId { get; set; }
    public Proprietario Proprietario { get; set; }
}