using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P2_Ap1_JamesUrena.Models;

public class Componente
{
    [Key]
    public int ComponenteId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Existencia { get; set; }
}
