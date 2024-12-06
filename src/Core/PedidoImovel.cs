using Core.Models;

namespace Core;

public class PedidoImovel : Entity
{
    public Guid PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;

    public Guid ImovelId { get; set; }
    public Imovel Imovel { get; set; } = null!;

    public Guid ProprietarioId { get; set; }
    public Proprietario Proprietario { get; set; } = null!;
}