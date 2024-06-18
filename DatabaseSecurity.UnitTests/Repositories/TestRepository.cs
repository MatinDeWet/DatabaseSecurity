using DatabaseSecurity.Repositories;
using DatabaseSecurity.UnitTests.Context;
using DatabaseSecurity.UnitTests.Models;

namespace DatabaseSecurity.UnitTests.Repositories
{
    public class TestRepository : Repository<TestContext>, ITestRepository
    {
        public TestRepository(TestContext context) : base(context)
        {
        }

        public IQueryable<Product> Products => Set<Product>();
    }
}
