using Microsoft.EntityFrameworkCore;
using CleanServiceApi.Models;

namespace CleanServiceApi.Data;

public class CleanServiceContext : DbContext
{
    public CleanServiceContext(DbContextOptions<CleanServiceContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Resenia> Resenias { get; set; }
    public DbSet<Solicitud> Solicitudes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasKey(u => u.EmailUsuario);

        modelBuilder.Entity<Empleado>()
            .HasKey(e => e.EmailEmpleado);

        modelBuilder.Entity<Resenia>()
            .HasOne(r => r.Empleado)
            .WithMany() 
            .HasForeignKey(r => r.EmailEmpleado);

        modelBuilder.Entity<Resenia>()
            .HasOne(r => r.Usuario)
            .WithMany() 
            .HasForeignKey(r => r.EmailUsuario);
        
        modelBuilder.Entity<Solicitud>()
            .HasOne(s => s.Usuario)
            .WithMany() 
            .HasForeignKey(s => s.EmailUsuario)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Solicitud>()
            .HasOne(s => s.Empleado)
            .WithMany() 
            .HasForeignKey(s => s.EmailEmpleado)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
