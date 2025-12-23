using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Aplicacion.DTO.Cliente
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        public string NumDocumento { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public DateOnly FechaNacimiento { get; set; }

        public string Direccion { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public bool Activo { get; set; }
    }
}
