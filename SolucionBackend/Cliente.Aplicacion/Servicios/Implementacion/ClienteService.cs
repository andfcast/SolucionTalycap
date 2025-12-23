using Cliente.Aplicacion.DTO.RequestResponse;
using Cliente.Aplicacion.Servicios.Interfaces;
using Cliente.Aplicacion.Utilidades;
using Cliente.Dominio.Repositories;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Reflection.Metadata.Ecma335;

namespace Cliente.Aplicacion.Servicios.Implementacion
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository) { 
            _repository = repository;
        }
        public async Task<ResponseDTO> ObtenerInfoCliente(string identificacion) { 
            ResponseDTO response = new ResponseDTO();
            response.ResultData = Utils.ConvertirADto(await _repository.ObtenerInfo(identificacion));
            response.IsValid = response.ResultData != null;
            response.Message = response.IsValid ? "" : "No hay registros";
            return response;
        }

    }
}
