namespace Core.Dtos.ContatoDto;

public class ContatoCreateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public Guid EmpresaId { get; set; }
}