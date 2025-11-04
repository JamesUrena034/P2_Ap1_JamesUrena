using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using P2_Ap1_JamesUrena.DAL;
using P2_Ap1_JamesUrena.Models;

namespace P2_Ap1_JamesUrena.Services;

public class PedidosServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int pedidoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Registros.AnyAsync(p => p.PedidoId == pedidoId);
    }

    public async Task<bool> Insertar(Pedidos pedido)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        pedido.Total = pedido.PedidoDetalles.Sum(d => d.Precio * d.Cantidad);
        contexto.Registros.Add(pedido);
        await AfectarComponentes(pedido.PedidoDetalles.ToArray(), TipoOperacion.Resta);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task AfectarComponentes(PedidoDetalles[] detalles, TipoOperacion tipoOperacion)
    {
        if (detalles == null || detalles.Length == 0)
            return;

        await using var contexto = await DbFactory.CreateDbContextAsync();

        foreach (var item in detalles)
        {
            var componente = await contexto.Componente.SingleAsync(c => c.ComponenteId == item.ComponenteId);

            if (tipoOperacion == TipoOperacion.Resta)
                componente.Existencia -= item.Cantidad;
            else if (tipoOperacion == TipoOperacion.Suma)
                componente.Existencia += item.Cantidad;
        }

        await contexto.SaveChangesAsync();
    }

    public async Task<bool> Modificar(Pedidos pedido)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var pedidoAnterior = await contexto.Registros
            .Include(p => p.PedidoDetalles)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PedidoId == pedido.PedidoId);

        if (pedidoAnterior == null)
            return false;

        await AfectarComponentes(pedidoAnterior.PedidoDetalles.ToArray(), TipoOperacion.Suma);
        pedido.Total = pedido.PedidoDetalles.Sum(d => d.Precio * d.Cantidad);
        await AfectarComponentes(pedido.PedidoDetalles.ToArray(), TipoOperacion.Resta);

        contexto.Registros.Update(pedido);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Pedidos pedido)
    {
        if (!await Existe(pedido.PedidoId))
            return await Insertar(pedido);
        else
            return await Modificar(pedido);
    }

    public async Task<Pedidos?> Buscar(int pedidoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Registros
            .Include(p => p.PedidoDetalles)
                .ThenInclude(d => d.Componente)
            .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);
    }

    public async Task<bool> Eliminar(int pedidoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var pedido = await contexto.Registros
            .Include(p => p.PedidoDetalles)
            .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);

        if (pedido == null)
            return false;

        await AfectarComponentes(pedido.PedidoDetalles.ToArray(), TipoOperacion.Suma);

        contexto.PedidoDetalles.RemoveRange(pedido.PedidoDetalles);
        contexto.Registros.Remove(pedido);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Pedidos>> GetList(Expression<Func<Pedidos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Registros
            .Include(p => p.PedidoDetalles)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public enum TipoOperacion
    {
        Suma = 1,
        Resta = 2
    }

    public async Task<List<Componente>> ListarComponentes()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Componente.AsNoTracking().ToListAsync();
    }
}
