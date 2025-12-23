using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Aplicacion.DTO.RequestResponse
{
    public class RequestDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public object? Body { get; set; }
    }
}
