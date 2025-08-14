using Negocio.DTOs;

namespace Negocio.Interfaces
{
    public interface IVentaService
    {
        Task<VentaDTO> CrearVentaAsync(VentaRequestDTO ventaRequestDTO);

        Task<VentaDTO?> GetVentaByIdAsync(int id);
    }
}