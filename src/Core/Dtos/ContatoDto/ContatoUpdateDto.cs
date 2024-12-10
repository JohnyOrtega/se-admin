namespace Core.Dtos.ContatoDto;

public class ContatoUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
}