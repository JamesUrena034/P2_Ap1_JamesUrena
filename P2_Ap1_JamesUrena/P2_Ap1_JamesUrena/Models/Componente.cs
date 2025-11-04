using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2_Ap1_JamesUrena.Models;

public class Componente
{
    [Key]
    public int ComponenteId { get; set; }

    [Required(ErrorMessage = "Debe tener una descripcion")]
    public string Descripcion { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public double Precio { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "La existencia no puede ser negativa")]
    public int Existencia { get; set; }

    [InverseProperty("Componente")]
    public virtual ICollection<PedidoDetalles> PedidoDetalles { get; set; } = new List<PedidoDetalles>();
}
