using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleanServiceApi.Data;

public class CleanServiceContextFactory
    : IDesignTimeDbContextFactory<CleanServiceContext>
{
    public CleanServiceContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CleanServiceContext>();

        // NO depende de appsettings.json
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CleanService;Username=postgres;Password=12345678");

        return new CleanServiceContext(optionsBuilder.Options);
    }
}
