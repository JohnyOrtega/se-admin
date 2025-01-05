namespace Core.Dtos.MapeadorDto;

public class MapeadorUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Zone { get; set; } = string.Empty;
    public DateTime LastMapping { get; set; }
    public string Pix { get; set; } = string.Empty;
    public string Vehicle { get; set; } = string.Empty;
    public string CameraType { get; set; } = string.Empty;
    public string CelphoneModel { get; set; } = string.Empty;
    public string Observations { get; set; } = string.Empty;
}
