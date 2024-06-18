using DatabaseSecurity.Enums;
using DatabaseSecurity.Locks;
using DatabaseSecurity.UnitTests.Context;
using DatabaseSecurity.UnitTests.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseSecurity.UnitTests.Locks
{
    public class ClientLock : Lock<Client>
    {
        private readonly TestContext _context;

        public ClientLock(TestContext context)
        {
            _context = context;
        }

        public override IQueryable<Client> Secured(int identityId, DataPermissionEnum requirement)
        {
            var qry = from c in _context.Clients
                      join ut in _context.UserTeams on c.TeamId equals ut.TeamId
                      where ut.UserId == identityId
                      && ut.DataRight.HasFlag(requirement)
                      select c;

            return qry;
        }

        public override async Task<bool> HasAccess(Client obj, int identityId, DataPermissionEnum requirement, CancellationToken cancellationToken)
        {
            var teamId = obj.TeamId;

            if (teamId == 0)
            {
                return false;
            }

            var query = from ut in _context.UserTeams
                        where ut.UserId == identityId
                        && ut.DataRight.HasFlag(requirement)
                        && ut.TeamId == teamId
                        select ut.TeamId;

            return await query.AnyAsync(cancellationToken);
        }
    }
}
