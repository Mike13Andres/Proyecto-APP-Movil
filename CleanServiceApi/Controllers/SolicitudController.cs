using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleanServiceApi.Data;
using CleanServiceApi.Models;

namespace CleanServiceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolicitudesController : ControllerBase
{
    private readonly CleanServiceContext _context;

    public SolicitudesController(CleanServiceContext context)
    {
        _context = context;
    }

    // GET: api/Solicitudes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Solicitud>>> GetSolicitudes()
    {
        return await _context.Solicitudes
            .Include(s => s.Usuario)
            .Include(s => s.Empleado)
            .ToListAsync();
    }

    // GET: api/Solicitudes/emailUsuario/{email}
    [HttpGet("emailUsuario/{email}")]
    public async Task<ActionResult<IEnumerable<Solicitud>>> GetSolicitudesPorUsuario(string email)
    {
        var solicitudes = await _context.Solicitudes
            .Include(s => s.Usuario)
            .Include(s => s.Empleado)
            .Where(s => s.EmailUsuario == email)
            .ToListAsync();

        if (!solicitudes.Any())
            return NotFound();

        return solicitudes;
    }

    // GET: api/Solicitudes/emailEmpleado/{email}
    [HttpGet("emailEmpleado/{email}")]
    public async Task<ActionResult<IEnumerable<Solicitud>>> GetSolicitudesPorEmpleado(string email)
    {
        var solicitudes = await _context.Solicitudes
            .Include(s => s.Usuario)
            .Include(s => s.Empleado)
            .Where(s => s.EmailEmpleado == email)
            .ToListAsync();

        if (!solicitudes.Any())
            return NotFound();

        return solicitudes;
    }


    // GET: api/Solicitudes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Solicitud>> GetSolicitud(int id)
    {
        var solicitud = await _context.Solicitudes
            .Include(s => s.Usuario)
            .Include(s => s.Empleado)
            .FirstOrDefaultAsync(s => s.IdSolicitud == id);

        if (solicitud == null)
            return NotFound();

        return solicitud;
    }

    // POST: api/Solicitudes
    [HttpPost]
    public async Task<ActionResult<Solicitud>> PostSolicitud(Solicitud solicitud)
    {
        _context.Solicitudes.Add(solicitud);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSolicitud), new { id = solicitud.IdSolicitud }, solicitud);
    }

    // PUT: api/Solicitudes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSolicitud(int id, Solicitud solicitud)
    {
        if (id != solicitud.IdSolicitud)
            return BadRequest();

        _context.Entry(solicitud).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Solicitudes.Any(s => s.IdSolicitud == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/Solicitudes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSolicitud(int id)
    {
        var solicitud = await _context.Solicitudes.FindAsync(id);
        if (solicitud == null)
            return NotFound();

        _context.Solicitudes.Remove(solicitud);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
