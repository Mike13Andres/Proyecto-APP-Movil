using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleanServiceApi.Data;
using CleanServiceApi.Models;

namespace CleanServiceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpleadosController : ControllerBase
{
    private readonly CleanServiceContext _context;

    public EmpleadosController(CleanServiceContext context)
    {
        _context = context;
    }

    // 🔹 GET: api/Empleados
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
    {
        return await _context.Empleados.ToListAsync();
    }

    // 🔹 GET: api/Empleados/{email}
    [HttpGet("{email}")]
    public async Task<ActionResult<Empleado>> GetEmpleado(string email)
    {
        var empleado = await _context.Empleados.FindAsync(email);

        if (empleado == null)
            return NotFound();

        return empleado;
    }

     // POST: api/Empleados
    [HttpPost]
    public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
    {
        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmpleado), new { email = empleado.EmailEmpleado }, empleado);
    }

    // 🔹 PUT: api/Empleados/{email}
    [HttpPut("{email}")]
    public async Task<IActionResult> PutEmpleado(string email, Empleado empleado)
    {
        if (email != empleado.EmailEmpleado)
            return BadRequest("El email no coincide.");

        var existe = await _context.Empleados.AnyAsync(e => e.EmailEmpleado == empleado.EmailEmpleado);
        if (!existe)
            return NotFound();

        _context.Entry(empleado).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // 🔹 DELETE: api/Empleados/{email}
    [HttpDelete("{email}")]
    public async Task<IActionResult> DeleteEmpleado(string email)
    {
        var empleado = await _context.Empleados.FindAsync(email);

        if (empleado == null)
            return NotFound();

        _context.Empleados.Remove(empleado);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
