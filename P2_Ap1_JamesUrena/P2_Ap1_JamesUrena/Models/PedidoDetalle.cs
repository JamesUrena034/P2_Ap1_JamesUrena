using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2_Ap1_JamesUrena.Models;

public class PedidoDetalle
{
    [Key]
    public int PedidoId { get; set; }

    public int ComponenteId { get; set; }

    public int Cantidad { get; set; }

    public double Precio { get; set; }


}
