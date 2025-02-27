using System.Diagnostics; // Permite el uso de la clase Activity para rastrear solicitudes y depuraci�n
using Microsoft.AspNetCore.Mvc; // Espacio de nombres necesario para trabajar con controladores y acciones en ASP.NET Core MVC
using MiCrud.Models; // Importa el espacio de nombres donde est�n los modelos de la aplicaci�n

namespace MiCrud.Controllers; // Define el espacio de nombres del controlador

// Controlador principal de la aplicaci�n
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger; // Inyecci�n de dependencia para el servicio de registro (logging)

    // Constructor del controlador que recibe un servicio de logging
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // Acci�n que devuelve la vista principal (p�gina de inicio)
    public IActionResult Index()
    {
        return View(); // Retorna la vista "Index.cshtml"
    }

    // Acci�n que devuelve la vista de privacidad
    public IActionResult Privacy()
    {
        return View(); // Retorna la vista "Privacy.cshtml"
    }

    // Acci�n que maneja errores y muestra una vista de error personalizada
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        // Crea un modelo de error con el identificador de la solicitud actual
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
