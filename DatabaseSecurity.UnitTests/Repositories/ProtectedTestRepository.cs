using DatabaseSecurity.Identity;
using DatabaseSecurity.Locks;
using DatabaseSecurity.Repositories;
using DatabaseSecurity.UnitTests.Context;
using DatabaseSecurity.UnitTests.Models;

namespace DatabaseSecurity.UnitTests.Repositories
{
    public class ProtectedTestRepository : LockedRepository<TestContext>, IProtectedTestRepository
    {
        public ProtectedTestRepository(TestContext context, IIdentityInfo info, IEnumerable<IProtected> protection) : base(context, info, protection)
        {
        }

        public IQueryable<Client> Clients => Set<Client>();

        public IQueryable<UserTeam> UserTeams => Set<UserTeam>();

        public IQueryable<User> Users => Set<User>();

        public IQueryable<Team> Teams => Set<Team>();
    }
}
