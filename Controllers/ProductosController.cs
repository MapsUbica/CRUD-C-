using Microsoft.AspNetCore.Mvc; // Espacio de nombres necesario para los controladores en ASP.NET Core MVC
using MiCrud.Models; // Importa los modelos de la aplicación
using System.Linq; // Permite el uso de LINQ para consultas a la base de datos
using Microsoft.EntityFrameworkCore; // Necesario para incluir relaciones en consultas a la base de datos

namespace MiCrud.Controllers
{
    // Controlador para la gestión de productos
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context; // Contexto de la base de datos

        // Constructor que recibe el contexto de la base de datos mediante inyección de dependencias
        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción que devuelve la lista de productos
        public IActionResult Index()
        {
            // Obtiene todos los productos e incluye la información de la categoría
            var productos = _context.Productos.Include(p => p.Categoria).ToList();
            return View(productos);
        }

        // Acción que muestra el formulario para crear un nuevo producto
        public IActionResult Create()
        {
            // Carga la lista de categorías para el formulario
            ViewBag.Categorias = _context.Categorias.ToList();
            return View();
        }

        // Acción que maneja el envío del formulario para crear un producto
        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            // Si el modelo no es válido, recargar la lista de categorías y volver a la vista
            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = _context.Categorias.ToList();
                return View(producto);
            }

            try
            {
                // Agrega el producto a la base de datos y guarda los cambios
                _context.Productos.Add(producto);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Producto creado exitosamente.";
                return RedirectToAction(nameof(Index)); // Redirige a la lista de productos
            }
            catch (Exception ex)
            {
                // En caso de error, muestra un mensaje de error y recarga las categorías
                ModelState.AddModelError(string.Empty, "Error al guardar el producto: " + ex.Message);
                ViewBag.Categorias = _context.Categorias.ToList();
                return View(producto);
            }
        }

        // Acción que muestra el formulario para editar un producto
        public IActionResult Edit(int id)
        {
            var producto = _context.Productos.Find(id); // Busca el producto por su ID
            if (producto == null) return NotFound(); // Si no existe, devuelve un error 404
            ViewBag.Categorias = _context.Categorias.ToList(); // Carga la lista de categorías
            return View(producto);
        }

        // Acción que maneja la actualización de un producto
        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = _context.Categorias.ToList();
                return View(producto);
            }

            try
            {
                _context.Update(producto);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Producto actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar el producto: " + ex.Message);
                ViewBag.Categorias = _context.Categorias.ToList();
                return View(producto);
            }
        }

        // Acción que muestra la vista de confirmación para eliminar un producto
        public IActionResult Delete(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null) return NotFound();
            return View(producto);
        }

        // Acción que maneja la eliminación del producto
        [HttpPost, ActionName("Delete")] // Indica que esta acción corresponde a la eliminación confirmada
        public IActionResult DeleteConfirmed(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null) return NotFound();

            try
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Producto eliminado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al eliminar el producto: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
