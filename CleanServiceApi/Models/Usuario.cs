using System.ComponentModel.DataAnnotations;

namespace CleanServiceApi.Models;

public class Usuario
{
    [Key]
    public string EmailUsuario { get; set; }

    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }
    public string Contrasena { get; set; }
}
