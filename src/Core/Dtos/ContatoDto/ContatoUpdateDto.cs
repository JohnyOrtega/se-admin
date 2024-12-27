namespace Core.Dtos.ContatoDto;

public class ContatoUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string AreaOfActivity { get; set; }
    public string Observations { get; set; }
}