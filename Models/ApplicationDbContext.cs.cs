using Microsoft.EntityFrameworkCore; // Importa Entity Framework Core para manejar la base de datos

namespace MiCrud.Models
{
    // Define el contexto de la base de datos, que actúa como puente entre la aplicación y la base de datos
    public class ApplicationDbContext : DbContext
    {
        // Constructor que recibe opciones de configuración para el contexto
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet representa las tablas en la base de datos
        public DbSet<Categoria> Categorias { get; set; } // Tabla de categorías
        public DbSet<Producto> Productos { get; set; }   // Tabla de productos
    }
}
