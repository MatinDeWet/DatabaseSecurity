using DatabaseSecurity.Repositories;
using DatabaseSecurity.UnitTests.Models;

namespace DatabaseSecurity.UnitTests.Repositories
{
    public interface IProtectedTestRepository : IRepository
    {
        IQueryable<Client> Clients { get; }

        IQueryable<UserTeam> UserTeams { get; }

        IQueryable<User> Users { get; }

        IQueryable<Team> Teams { get; }
    }
}
