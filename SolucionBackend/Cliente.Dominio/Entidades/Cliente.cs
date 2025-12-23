using System;
using System.Collections.Generic;

namespace Cliente.Dominio.Entidades;

public partial class Cliente
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
