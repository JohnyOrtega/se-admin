namespace Core.Dtos;

public class MotoboyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Telephone { get; set; }
    public string City { get; set; }
    public DateTime LastMapping { get; set; }
    public string Pix { get; set; }
    public string Vehicle { get; set; }
    public string Observations { get; set; }
}