namespace MiCrud.Models
{
    // Define un modelo de vista para manejar errores en la aplicaci�n
    public class ErrorViewModel
    {
        // Identificador de la solicitud, �til para rastrear errores espec�ficos
        public string? RequestId { get; set; }

        // Propiedad calculada que indica si se debe mostrar el RequestId en la vista
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
