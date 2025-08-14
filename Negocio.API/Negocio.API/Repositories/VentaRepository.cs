using Microsoft.EntityFrameworkCore;
using Negocio.Interfaces;
using Negocio;

namespace Negocio.API.Repositories
{
    public class VentaRepository: IVentaRepository
    {
       private readonly NegocioContext _context;

        public VentaRepository(NegocioContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venta>> GetAllAsync()
        {
            return await _context.Venta.Include(v => v.DetalleVenta).ToListAsync();
        }

        public async Task<Venta?> GetByIdAsync(int id)
        {
            return await _context.Venta.Include(v => v.DetalleVenta).FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task AddAsync(Venta venta)
        {
            await _context.Venta.AddAsync(venta);
        }
        public void Update(Venta venta)
        {
            _context.Entry(venta).State = EntityState.Modified;
        }
        public void Delete(Venta venta)
        {
            _context.Venta.Remove(venta);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
