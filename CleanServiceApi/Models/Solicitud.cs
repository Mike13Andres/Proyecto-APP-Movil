using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleanServiceApi.Enums;
using System.Text.Json.Serialization;

namespace CleanServiceApi.Models;

public class Solicitud
{
    [Key]
    public int IdSolicitud { get; set; }

    public double PrecioHora { get; set; }
    public int Disponibilidad { get; set; }
    public int TipoServicio { get; set; }
    public int Estado { get; set; }
    public string fecha { get; set; }
    public string hora { get; set; }

    // Imagen (ruta o URL)
    public string? ImagenUrl { get; set; }

    [Required]
    [ForeignKey("Usuario")]
    public string EmailUsuario { get; set; }

    [JsonIgnore]
    public Usuario? Usuario { get; set; }

    [Required]
    [ForeignKey("Empleado")]
    public string EmailEmpleado { get; set; }

    [JsonIgnore]
    public Empleado? Empleado { get; set; }

}
