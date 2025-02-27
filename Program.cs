using Microsoft.EntityFrameworkCore; // Importar Entity Framework Core para trabajar con bases de datos
using MiCrud.Models; // Importar los modelos del proyecto

var builder = WebApplication.CreateBuilder(args); // Crear la aplicaci�n web

// Configurar la conexi�n a MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Obtener la cadena de conexi�n desde el archivo de configuraci�n
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))); // Configurar Entity Framework Core para usar MySQL

// Agregar servicios para controladores y vistas (MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build(); // Construir la aplicaci�n

app.UseStaticFiles(); // Habilitar archivos est�ticos (CSS, JavaScript, im�genes, etc.)
app.UseRouting(); // Habilitar el enrutamiento de las solicitudes HTTP
app.UseAuthorization(); // Habilitar la autorizaci�n para la aplicaci�n

// Configurar la ruta predeterminada para los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Define que la p�gina de inicio ser� HomeController con la acci�n Index

app.Run(); // Ejecutar la aplicaci�n
