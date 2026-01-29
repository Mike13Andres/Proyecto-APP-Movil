using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanServiceApi.Models;

public class Empleado
{
    [Key]
    public string EmailEmpleado { get; set; }

    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }
    public string Contrasena { get; set; }
}
