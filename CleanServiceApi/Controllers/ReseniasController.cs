using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleanServiceApi.Data;
using CleanServiceApi.Models;

namespace CleanServiceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReseniasController : ControllerBase
{
    private readonly CleanServiceContext _context;

    public ReseniasController(CleanServiceContext context)
    {
        _context = context;
    }

    // ============================
    // GET: api/Resenias
    // ============================
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Resenia>>> GetResenias()
    {
        return await _context.Resenias
            .Include(r => r.Empleado)
            .Include(r => r.Usuario)
            .ToListAsync();
    }

    // ============================
    // GET: api/Resenias/5
    // ============================
    [HttpGet("{id}")]
    public async Task<ActionResult<Resenia>> GetResenia(int id)
    {
        var resenia = await _context.Resenias
            .Include(r => r.Empleado)
            .Include(r => r.Usuario)
            .FirstOrDefaultAsync(r => r.IdResenia == id);

        if (resenia == null)
            return NotFound();

        return resenia;
    }

    [HttpGet("resenias/emailEmpleado/{email}")]
    public async Task<ActionResult<IEnumerable<Resenia>>> GetReseniasPorEmpleado(string email)
    {
        var resenias = await _context.Resenias
            .Include(r => r.Empleado) // opcional, si quieres traer datos del empleado
            .Include(r => r.Usuario)   // opcional, si quieres traer datos del usuario que hizo la reseña
            .Where(r => r.EmailEmpleado == email)
            .ToListAsync();

        if (!resenias.Any())
            return NotFound();

        return resenias;
    }


    // ============================
    // POST: api/Resenias
    // ============================
    [HttpPost]
    public async Task<ActionResult<Resenia>> PostResenia(Resenia resenia)
    {
        // Validación básica
        var empleadoExiste = await _context.Empleados
            .AnyAsync(e => e.EmailEmpleado == resenia.EmailEmpleado);

        var usuarioExiste = await _context.Usuarios
            .AnyAsync(u => u.EmailUsuario == resenia.EmailUsuario);

        if (!empleadoExiste || !usuarioExiste)
            return BadRequest("Empleado o Usuario no existe");

        _context.Resenias.Add(resenia);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetResenia),
            new { id = resenia.IdResenia },
            resenia
        );
    }

    // ============================
    // PUT: api/Resenias/5
    // ============================
    [HttpPut("{id}")]
    public async Task<IActionResult> PutResenia(int id, Resenia resenia)
    {
        if (id != resenia.IdResenia)
            return BadRequest();

        _context.Entry(resenia).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Resenias.Any(r => r.IdResenia == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // ============================
    // DELETE: api/Resenias/5
    // ============================
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResenia(int id)
    {
        var resenia = await _context.Resenias.FindAsync(id);

        if (resenia == null)
            return NotFound();

        _context.Resenias.Remove(resenia);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
