using DatabaseSecurity.UnitTests.Context;
using DatabaseSecurity.UnitTests.Models;

namespace DatabaseSecurity.UnitTests.MockData.DataSeed
{
    public class UserData : IMockData
    {
        public void Seed(TestContext db)
        {
            db.Users.AddRange(
                new User
                {
                    Id = 1,
                    Name = "User 1"
                });
        }
    }
}
