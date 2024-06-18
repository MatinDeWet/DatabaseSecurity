using DatabaseSecurity.Enums;
using DatabaseSecurity.UnitTests.Context;
using DatabaseSecurity.UnitTests.Models;

namespace DatabaseSecurity.UnitTests.MockData.DataSeed
{
    public class UserTeamData : IMockData
    {
        public void Seed(TestContext db)
        {
            db.UserTeams.AddRange(
                new UserTeam
                {
                    Id = 1,
                    UserId = 1,
                    TeamId = 1,
                    DataRight = DataPermissionEnum.Read
                },
                new UserTeam
                {
                    Id = 2,
                    UserId = 2,
                    TeamId = 1,
                    DataRight = DataPermissionEnum.Write
                },
                new UserTeam
                {
                    Id = 3,
                    UserId = 3,
                    TeamId = 1,
                    DataRight = DataPermissionEnum.Delete
                });
        }
    }
}
