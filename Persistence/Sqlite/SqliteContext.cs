using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Shared;

namespace Sqlite;

public class SqliteContext : GeneralContext<SqliteContext>
{
    public SqliteContext(DbContextOptions<SqliteContext> options) : base(options)
    {
    }
}

public class SqliteContextCreator : IDesignTimeDbContextFactory<SqliteContext>
{
    public SqliteContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SqliteContext>();
        Apply(optionsBuilder);
        return new SqliteContext(optionsBuilder.Options);
    }

    public static void Apply(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite(GeneralContext<SqliteContext>.ConnectionString);
    }
}
