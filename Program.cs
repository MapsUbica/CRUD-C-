using Microsoft.EntityFrameworkCore; // Importar Entity Framework Core para trabajar con bases de datos
using MiCrud.Models; // Importar los modelos del proyecto

var builder = WebApplication.CreateBuilder(args); // Crear la aplicación web

// Configurar la conexión a MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Obtener la cadena de conexión desde el archivo de configuración
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))); // Configurar Entity Framework Core para usar MySQL

// Agregar servicios para controladores y vistas (MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build(); // Construir la aplicación

app.UseStaticFiles(); // Habilitar archivos estáticos (CSS, JavaScript, imágenes, etc.)
app.UseRouting(); // Habilitar el enrutamiento de las solicitudes HTTP
app.UseAuthorization(); // Habilitar la autorización para la aplicación

// Configurar la ruta predeterminada para los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Define que la página de inicio será HomeController con la acción Index

app.Run(); // Ejecutar la aplicación
