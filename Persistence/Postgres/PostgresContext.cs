using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Shared;

namespace Postgres;

public class PostgresContext : GeneralContext<PostgresContext>
{
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }
}

public class PostgresContextCreator : IDesignTimeDbContextFactory<PostgresContext>
{
    public PostgresContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostgresContext>();
        Apply(optionsBuilder);
        return new PostgresContext(optionsBuilder.Options);
    }
    
    public static void Apply(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql(GeneralContext<PostgresContext>.ConnectionString);
    }
}