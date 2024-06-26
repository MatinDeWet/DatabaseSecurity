﻿using DatabaseSecurity.UnitTests.Context;
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
                    TeamId = 1
                });
        }
    }
}
