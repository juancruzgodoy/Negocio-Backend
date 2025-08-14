namespace Negocio.Interfaces
{
    public interface IVentaRepository
    {
        Task<IEnumerable<Venta>> GetAllAsync();
        Task<Venta?> GetByIdAsync(int id);
        Task AddAsync(Venta venta);
        void Update(Venta venta);
        void Delete(Venta venta);
        Task SaveChangesAsync();        
    }
}
