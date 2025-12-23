using Cliente.Aplicacion.DTO.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Aplicacion.Utilidades
{
    public static class Utils
    {
        public static ClienteDTO ConvertirADto(Dominio.Entidades.Cliente entidad) {
            if (entidad == null)
                return null;
            return new ClienteDTO
            {
                Activo = entidad.Activo,
                Apellido = entidad.Apellido,
                Correo = entidad.Correo,
                Direccion = entidad.Direccion,
                FechaNacimiento = entidad.FechaNacimiento,
                Id = entidad.Id,
                Nombre = entidad.Nombre,
                NumDocumento = entidad.NumDocumento,
                Telefono = entidad.Telefono
            };
        }
    }
}
