using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MiCrud.Models;

// Contexto de base de datos para interactuar con MySQL a través de Entity Framework Core
public partial class MiCrudDbContext : DbContext
{
    // Constructor sin parámetros requerido por Entity Framework
    public MiCrudDbContext()
    {
    }

    // Constructor que recibe opciones de configuración (por ejemplo, cadena de conexión)
    public MiCrudDbContext(DbContextOptions<MiCrudDbContext> options)
        : base(options)
    {
    }

    // Definición de las tablas en la base de datos como DbSet (representan colecciones de entidades)
    public virtual DbSet<Categoria> Categorias { get; set; }
    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }

    // Método para configurar la conexión a la base de datos (solo se usa si no se pasa configuración desde fuera)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning Para proteger información sensible en la cadena de conexión, muévela fuera del código fuente.
        => optionsBuilder.UseMySql(
            "server=localhost;database=mi_crud_db;uid=root",
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb")
        );

    // Método para configurar el modelo de datos, definir claves primarias, relaciones y restricciones
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configura la codificación de caracteres y collation de la base de datos
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        // Configuración de la tabla 'Categorias'
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY"); // Define la clave primaria
            entity.ToTable("categorias"); // Asigna el nombre de la tabla
            entity.Property(e => e.Id).HasColumnType("int(11)"); // Define el tipo de dato en la BD
            entity.Property(e => e.Nombre).HasMaxLength(255); // Define la longitud máxima del campo
        });

        // Configuración de la tabla '__efmigrationshistory' (historial de migraciones de EF Core)
        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY"); // Clave primaria
            entity.ToTable("__efmigrationshistory"); // Nombre de la tabla en la BD
            entity.Property(e => e.MigrationId).HasMaxLength(150); // Máx. 150 caracteres
            entity.Property(e => e.ProductVersion).HasMaxLength(32); // Máx. 32 caracteres
        });

        // Configuración de la tabla 'Productos'
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY"); // Define la clave primaria
            entity.ToTable("productos"); // Nombre de la tabla en la BD
            entity.HasIndex(e => e.CategoriaId, "CategoriaId"); // Índice en la clave foránea
            entity.Property(e => e.Id).HasColumnType("int(11)"); // Tipo de dato
            entity.Property(e => e.CategoriaId).HasColumnType("int(11)"); // Clave foránea
            entity.Property(e => e.Descripcion).HasColumnType("text"); // Tipo de dato
            entity.Property(e => e.Nombre).HasMaxLength(255); // Máx. 255 caracteres
            entity.Property(e => e.StockDisponible).HasColumnType("int(11)"); // Tipo de dato
            entity.Property(e => e.Tipo).HasMaxLength(100); // Máx. 100 caracteres

            // Configuración de la relación entre 'Productos' y 'Categorias'
            entity.HasOne(d => d.Categoria) // Un producto pertenece a una categoría
                .WithMany(p => p.Productos) // Una categoría puede tener muchos productos
                .HasForeignKey(d => d.CategoriaId) // Clave foránea en 'Productos'
                .HasConstraintName("productos_ibfk_1"); // Nombre de la restricción en la BD
        });

        // Método parcial para permitir configuraciones adicionales en una clase parcial
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
