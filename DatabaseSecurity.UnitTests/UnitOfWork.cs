using DatabaseSecurity.UnitOfWork;
using DatabaseSecurity.UnitTests.Context;

namespace DatabaseSecurity.UnitTests
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestContext _ctx;

        public UnitOfWork(TestContext ctx)
        {
            _ctx = ctx;
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _ctx.SaveChangesAsync(cancellationToken);
        }
    }
}
