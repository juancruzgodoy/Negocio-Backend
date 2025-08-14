using Microsoft.EntityFrameworkCore;
using Negocio.Interfaces;

namespace Negocio.API.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly NegocioContext _context;

        public ProductoRepository(NegocioContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task AddAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
        }

        public void Update(Producto producto)
        {
            _context.Entry(producto).State = EntityState.Modified;
        }

        public void Delete(Producto producto)
        {
            _context.Productos.Remove(producto);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}