using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CleanServiceApi.Models;

public class Resenia
{
    [Key]
    public int IdResenia { get; set; }

    public string Comentario { get; set; }

    [Required]
    [ForeignKey("Empleado")]
    public string EmailEmpleado { get; set; }

    [JsonIgnore]
    public Empleado? Empleado { get; set; }

    [Required]
    [ForeignKey("Usuario")]
    public string EmailUsuario { get; set; }

    [JsonIgnore]
    public Usuario? Usuario { get; set; }
}
