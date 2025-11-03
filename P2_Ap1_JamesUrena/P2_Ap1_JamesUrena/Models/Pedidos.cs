using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2_Ap1_JamesUrena.Models;

public class Pedidos
{
    [Key]
    public int PedidoId { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria")]
    public DateTime Fecha { get; set; } = DateTime.Now;
    public string NombreCliente { get; set; } = string.Empty;

    public double Total { get; set; }
}
