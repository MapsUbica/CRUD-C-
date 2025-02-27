using Microsoft.AspNetCore.Mvc;  // Importa las herramientas necesarias para los controladores MVC
using MiCrud.Models;  // Importa el espacio de nombres donde está el modelo Categoria
using System.Linq;  // Permite el uso de consultas LINQ
using Microsoft.EntityFrameworkCore;  // Necesario para usar Entity Framework Core y sus funciones avanzadas

namespace MiCrud.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context; // Contexto de base de datos para acceder a Categorias

        // Constructor que inyecta el contexto de base de datos
        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para listar todas las categorías y mostrarlas en la vista Index
        public IActionResult Index()
        {
            var categorias = _context.Categorias.ToList(); // Obtiene todas las categorías de la base de datos
            return View(categorias); // Retorna la vista con la lista de categorías
        }

        // Método para mostrar el formulario de creación de categorías
        public IActionResult Create()
        {
            return View();
        }

        // Método que recibe el formulario de creación y guarda la categoría en la base de datos
        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            // Limpiar espacios en el nombre antes de validar
            categoria.Nombre = categoria.Nombre?.Trim();

            // Verifica si ya existe una categoría con el mismo nombre (ignorando mayúsculas y espacios)
            if (_context.Categorias.Any(c => EF.Functions.Like(c.Nombre, categoria.Nombre)))
            {
                ModelState.AddModelError("Nombre", "Ya existe una categoría con este nombre.");
            }

            // Si la validación es correcta, se guarda la categoría
            if (ModelState.IsValid)
            {
                _context.Categorias.Add(categoria);
                _context.SaveChanges(); // Guarda los cambios en la base de datos
                return RedirectToAction(nameof(Index)); // Redirige al listado de categorías
            }
            return View(categoria); // Si hay errores, se regresa al formulario con los datos ingresados
        }

        // Método para mostrar el formulario de edición de una categoría existente
        public IActionResult Edit(int id)
        {
            var categoria = _context.Categorias.Find(id); // Busca la categoría por ID
            if (categoria == null) return NotFound(); // Si no existe, retorna un error 404
            return View(categoria); // Si existe, carga la vista con los datos de la categoría
        }

        // Método que procesa la edición de la categoría
        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
            // Limpiar espacios en el nombre antes de validar
            categoria.Nombre = categoria.Nombre?.Trim();

            // Validar que no haya otra categoría con el mismo nombre
            if (_context.Categorias.Any(c => EF.Functions.Like(c.Nombre, categoria.Nombre) && c.Id != categoria.Id))
            {
                ModelState.AddModelError("Nombre", "Otra categoría ya tiene este nombre.");
            }

            // Si la validación es correcta, se actualiza la categoría
            if (ModelState.IsValid)
            {
                _context.Update(categoria);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Redirige a la lista de categorías
            }
            return View(categoria); // Si hay errores, regresa al formulario con los datos ingresados
        }

        // Método para mostrar la vista de confirmación de eliminación
        public IActionResult Delete(int id)
        {
            var categoria = _context.Categorias.Find(id); // Busca la categoría por ID
            if (categoria == null) return NotFound(); // Si no existe, retorna un error 404
            return View(categoria); // Si existe, carga la vista de confirmación
        }

        // Método que confirma la eliminación de la categoría
        [HttpPost, ActionName("Delete")] // Se usa un alias para evitar colisión de nombres con el método anterior
        public IActionResult DeleteConfirmed(int id)
        {
            var categoria = _context.Categorias.Find(id); // Busca la categoría
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria); // Elimina la categoría
                _context.SaveChanges(); // Guarda los cambios en la base de datos
            }
            return RedirectToAction(nameof(Index)); // Redirige a la lista de categorías
        }
    }
}


