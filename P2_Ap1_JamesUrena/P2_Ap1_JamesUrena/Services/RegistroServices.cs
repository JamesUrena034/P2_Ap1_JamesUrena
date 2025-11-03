using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using P2_Ap1_JamesUrena.DAL;
using P2_Ap1_JamesUrena.Models;

namespace P2_Ap1_JamesUrena.Services;

public class RegistroServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<List<Pedidos>> GetList(Expression<Func<Pedidos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Registros
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();

    }
}
