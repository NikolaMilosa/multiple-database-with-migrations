using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Shared;

namespace Mssql;

public class MssqlContext : GeneralContext<MssqlContext>
{
    public MssqlContext(DbContextOptions<MssqlContext> options) : base(options)
    {
    }
}

public class MssqlContextCreator : IDesignTimeDbContextFactory<MssqlContext>
{
    public MssqlContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MssqlContext>();
        Apply(optionsBuilder);
        return new MssqlContext(optionsBuilder.Options);
    }

    public static void Apply(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlServer(GeneralContext<MssqlContext>.ConnectionString);
    }
}
