using DatabaseSecurity.Repositories;
using DatabaseSecurity.UnitTests.Models;

namespace DatabaseSecurity.UnitTests.Repositories
{
    public interface ITestRepository : IRepository
    {
        IQueryable<Product> Products { get; }
    }
}
