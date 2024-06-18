namespace DatabaseSecurity.UnitTests.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int TeamId { get; set; }
    }
}
