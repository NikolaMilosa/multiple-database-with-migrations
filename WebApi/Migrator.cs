

using Microsoft.EntityFrameworkCore;

public class Migrator<T> : IHostedService where T: DbContext
{
    private readonly ILogger<Migrator<T>> _logger;
    private readonly IServiceScope _scope;
    private readonly DbContext _db;

    public Migrator(ILogger<Migrator<T>> logger, IServiceScopeFactory factory)
    {
        _logger = logger;
        _scope = factory.CreateScope();
        _db = _scope.ServiceProvider.GetRequiredService<T>();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting migration of database...");

        while (true)
        {
            try
            {
                await _db.Database.MigrateAsync(cancellationToken);
                break;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Got exception: {e}");
                _logger.LogInformation("Waiting for connection to the database...");
                await Task.Delay(500, cancellationToken);
            }
        }
        await _db.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}