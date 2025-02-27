namespace MiCrud.Models
{
    // Define un modelo de vista para manejar errores en la aplicación
    public class ErrorViewModel
    {
        // Identificador de la solicitud, útil para rastrear errores específicos
        public string? RequestId { get; set; }

        // Propiedad calculada que indica si se debe mostrar el RequestId en la vista
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
