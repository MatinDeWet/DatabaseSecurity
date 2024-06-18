using DatabaseSecurity.UnitTests.Context;
using DatabaseSecurity.UnitTests.Models;

namespace DatabaseSecurity.UnitTests.MockData.DataSeed
{
    public class ClientData : IMockData
    {
        public void Seed(TestContext db)
        {
            db.Clients.AddRange(
                new Client
                {
                    Id = 1,
                    Name = "Client 1",
                    TeamId = 1
                },
                new Client
                {
                    Id = 2,
                    Name = "Client 2",
                    TeamId = 1
                });
        }
    }
}
