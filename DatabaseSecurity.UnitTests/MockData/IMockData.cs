using DatabaseSecurity.UnitTests.Context;

namespace DatabaseSecurity.UnitTests.MockData
{
    public interface IMockData
    {
        public void Seed(TestContext db);
    }
}
