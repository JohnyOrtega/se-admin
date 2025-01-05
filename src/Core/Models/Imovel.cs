namespace Core.Models;

public class Imovel : Entity
{
    public string Address { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
    public string Zone { get; set; } = string.Empty;
    public string PropertyProfile { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string Availability { get; set; } = string.Empty;
    public decimal RentValue { get; set; }
    public decimal SaleValue { get; set; }
    public decimal IptuAnnual { get; set; }
    public decimal IptuMonthly { get; set; }
    public decimal SearchMeterage { get; set; }
    public decimal TotalArea { get; set; }
    public string RealEstate { get; set; } = string.Empty;
    public Guid ProprietarioId { get; set; }
    public Proprietario Proprietario { get; set; }
}