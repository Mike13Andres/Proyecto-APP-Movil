using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleanServiceApi.Data;
using CleanServiceApi.Models;

namespace CleanServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly CleanServiceContext _context;

        public UsuariosController(CleanServiceContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string email)
        {
            var usuario = await _context.Usuarios.FindAsync(email);
            if (usuario == null)
                return NotFound();

            return usuario;
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.EmailUsuario))
                return BadRequest("El email es obligatorio");

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { email = usuario.EmailUsuario }, usuario);
        }

        // PUT: api/Usuarios/{email}
        [HttpPut("{email}")]
        public async Task<IActionResult> PutUsuario(string email, Usuario usuario)
        {
            if (email != usuario.EmailUsuario)
                return BadRequest();

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(u => u.EmailUsuario == email))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Usuarios/{email}
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUsuario(string email)
        {
            var usuario = await _context.Usuarios.FindAsync(email);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
