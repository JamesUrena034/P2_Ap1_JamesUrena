using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2_Ap1_JamesUrena.Models;

public class PedidoDetalles
{
    [Key]
    public int DetalleId { get; set; }

    public int PedidoId { get; set; }

    public int ComponenteId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public int Cantidad { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public double Precio { get; set; }

    [ForeignKey("PedidoId")]
    [InverseProperty("PedidoDetalles")]
    public virtual Pedidos Pedido { get; set; } = null!;

    [ForeignKey("ComponenteId")]
    [InverseProperty("PedidoDetalles")]
    public virtual Componente Componente { get; set; } = null!;
}
