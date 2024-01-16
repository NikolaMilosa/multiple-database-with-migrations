using Domain;
using Microsoft.EntityFrameworkCore;

namespace Shared;

public abstract class GeneralContext<T> : DbContext, IDataProvider where T: DbContext
{
    public static string ConnectionString => Environment.GetEnvironmentVariable("CONNECTION_STRING")!;

    public DbSet<Member> Members {get; set;}

    public DbSet<Project> Projects {get; set;}

    public DbSet<Assignation> Assignation {get; set;}

    DbSet<Member> IDataProvider.Members() => Members;

    DbSet<Project> IDataProvider.Projects() => Projects;

    DbSet<Assignation> IDataProvider.Assignation() => Assignation;

    public GeneralContext(DbContextOptions<T> options) : base(options)
    {
    }

    // Save changes is already implemented

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var members = new Member[] {
            new Member { Id = 1, FullName = "John Doe", Email = "john@doe.com" },
            new Member { Id = 2, FullName = "Simon Piece", Email = "simon@piece.com" },
        };

        var projects = new Project[] {
            new Project { Id = 1, Name = "R&D" },
            new Project { Id = 2, Name = "SDK" }
        };

        var assignations = new Assignation[] {
            new Assignation { Id = 1, Role = "Team Lead", MemberId = members[0].Id, ProjectId = projects[0].Id },
            new Assignation { Id = 2, Role = "Advisor", MemberId = members[0].Id, ProjectId = projects[1].Id },
            new Assignation { Id = 3, Role = "Developer", MemberId = members[1].Id, ProjectId = projects[1].Id },
        };

        modelBuilder.Entity<Member>().HasData(members);
        modelBuilder.Entity<Project>().HasData(projects);
        modelBuilder.Entity<Assignation>().HasData(assignations);

        base.OnModelCreating(modelBuilder);
    }
}