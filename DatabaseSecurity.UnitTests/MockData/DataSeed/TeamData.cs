using DatabaseSecurity.UnitTests.Context;
using DatabaseSecurity.UnitTests.Models;

namespace DatabaseSecurity.UnitTests.MockData.DataSeed
{
    public class TeamData : IMockData
    {
        public void Seed(TestContext db)
        {
            db.Teams.AddRange(
                new Team
                {
                    Id = 1,
                    Name = "Team 1"
                });
        }
    }
}
