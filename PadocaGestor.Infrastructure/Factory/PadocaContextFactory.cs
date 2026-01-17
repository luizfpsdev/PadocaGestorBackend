using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PadocaGestor.Infrastructure.Database;

namespace PadocaGestor.Infrastructure.Factory
{
    public class PadocaContextFactory
        : IDesignTimeDbContextFactory<PadocaContext>
    {
        public PadocaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PadocaContext>();

            optionsBuilder.UseNpgsql(
                "Host = localhost; Port = 5432; Database = padoca; Username = postgres; Password = postgres"
            );

            return new PadocaContext(optionsBuilder.Options);
        }
    }
}
