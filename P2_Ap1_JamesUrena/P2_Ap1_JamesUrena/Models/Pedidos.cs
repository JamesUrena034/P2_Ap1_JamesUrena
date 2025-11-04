using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2_Ap1_JamesUrena.Models;

public class Pedidos
{
    [Key]
    public int PedidoId { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
    public string NombreCliente { get; set; } = string.Empty;

    [Range(0, double.MaxValue, ErrorMessage = "El total no puede ser negativo")]
    public double Total { get; set; }

    [InverseProperty("Pedido")]
    public virtual ICollection<PedidoDetalles> PedidoDetalles { get; set; } = new List<PedidoDetalles>();
}
