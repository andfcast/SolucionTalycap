using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Aplicacion.DTO.RequestResponse
{
    public class ResponseDTO
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = String.Empty;
        public object? ResultData { get; set; }
    }
}
