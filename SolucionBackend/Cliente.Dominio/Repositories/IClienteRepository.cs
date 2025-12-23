using Cliente.Dominio.Entidades;

namespace Cliente.Dominio.Repositories
{
    public interface IClienteRepository
    {
        Task<Entidades.Cliente> ObtenerInfo(string identificacion);
    }
}
