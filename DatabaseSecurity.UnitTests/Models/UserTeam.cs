using DatabaseSecurity.Enums;

namespace DatabaseSecurity.UnitTests.Models
{
    public class UserTeam
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TeamId { get; set; }

        public DataPermissionEnum DataRight { get; set; }
    }
}
