using Domain;
using Microsoft.EntityFrameworkCore;

namespace Shared;

public interface IDataProvider
{
    DbSet<Member> Members();

    DbSet<Project> Projects();

    DbSet<Assignation> Assignation();

    int SaveChanges();
}
