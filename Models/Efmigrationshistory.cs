using System;
using System.Collections.Generic;

namespace MiCrud.Models;

// Define una clase parcial para representar el historial de migraciones en Entity Framework
public partial class Efmigrationshistory
{
    // Identificador único de la migración aplicada en la base de datos
    public string MigrationId { get; set; } = null!;

    // Versión de Entity Framework utilizada para la migración
    public string ProductVersion { get; set; } = null!;
}
