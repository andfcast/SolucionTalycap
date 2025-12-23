using Cliente.Dominio.Entidades;
using Cliente.Dominio.Repositories;
using Cliente.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
namespace Cliente.Infraestructura.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteDbContext _context;

        public ClienteRepository(ClienteDbContext context)
        {
            _context = context;
        }
        public async Task<Dominio.Entidades.Cliente> ObtenerInfo(string identificacion) {
            try
            {
                var result = _context.Clientes.FromSqlInterpolated($"EXEC dbo.sp_InfoCliente {identificacion}");
                Dominio.Entidades.Cliente objRes = (result.AsEnumerable<Dominio.Entidades.Cliente>().ToList()).FirstOrDefault();
                return objRes;
            }
            catch (Exception)
            {
                return null;
            }

            
        }
    }
}
