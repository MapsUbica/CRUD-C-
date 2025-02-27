using System;  // Espacio de nombres base para funcionalidades principales de .NET
using System.Collections.Generic;  // Permite el uso de colecciones como List<T> e ICollection<T>
using System.ComponentModel.DataAnnotations;  // Necesario para las validaciones en los modelos

namespace MiCrud.Models
{
    // Define la entidad "Categoria" que representa la tabla "Categorias" en la base de datos
    public partial class Categoria
    {
        // Propiedad que representa el ID de la categoría (Clave primaria)
        public int Id { get; set; }

        // Validación: El nombre es obligatorio
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        // Validación: El nombre no puede exceder los 100 caracteres
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; } = null!; // Campo obligatorio, no puede ser null

        // Relación uno a muchos: Una categoría puede tener muchos productos
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
