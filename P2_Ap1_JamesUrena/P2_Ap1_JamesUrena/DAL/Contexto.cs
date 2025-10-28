using System.Collections.Generic;
using P2_Ap1_JamesUrena.Models;
using Microsoft.EntityFrameworkCore;

namespace P2_Ap1_JamesUrena.DAL;

public class Contexto : DbContext
{
    public DbSet<Registros> Registros { get; set; }
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

}
