using Cliente.Aplicacion.DTO.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Aplicacion.Servicios.Interfaces
{
    public interface IClienteService
    {
        Task<ResponseDTO> ObtenerInfoCliente(string identificacion);
    }
}
