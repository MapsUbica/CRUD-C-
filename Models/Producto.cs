using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Importar para validaciones

namespace MiCrud.Models;

// Representa la entidad "Producto" que se almacena en la base de datos
public partial class Producto
{
    // Clave primaria del producto
    public int Id { get; set; }

    // Nombre del producto (obligatorio, con máximo 100 caracteres)
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
    public string Nombre { get; set; } = null!; // No puede ser nulo

    // Descripción del producto (opcional, con máximo 500 caracteres)
    [Required(ErrorMessage = "La descripción es obligatoria")]
    [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres")]
    public string? Descripcion { get; set; }

    // Tipo de producto (obligatorio, con máximo 50 caracteres)
    [Required(ErrorMessage = "El tipo es obligatorio")]
    [StringLength(50, ErrorMessage = "El tipo no puede superar los 50 caracteres")]
    public string? Tipo { get; set; }

    // Stock disponible (obligatorio, debe ser un número positivo o cero)
    [Required(ErrorMessage = "El stock disponible es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser un número positivo")]
    public int? StockDisponible { get; set; }

    // Identificador de la categoría a la que pertenece el producto (clave foránea)
    [Required(ErrorMessage = "Debe seleccionar una categoría")]
    public int? CategoriaId { get; set; }

    // Relación con la entidad "Categoria" (un producto pertenece a una categoría)
    public virtual Categoria? Categoria { get; set; }
}
