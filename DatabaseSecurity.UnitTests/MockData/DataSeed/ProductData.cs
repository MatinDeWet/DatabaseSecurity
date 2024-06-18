using DatabaseSecurity.UnitTests.Context;
using DatabaseSecurity.UnitTests.Models;

namespace DatabaseSecurity.UnitTests.MockData.DataSeed
{
    public class ProductData : IMockData
    {
        public void Seed(TestContext db)
        {
            db.Products.AddRange(
                new Product
                {
                    Id = 1,
                    Name = "Product 1"
                },
                new Product
                {
                    Id = 2,
                    Name = "Product 2"
                },
                new Product
                {
                    Id = 3,
                    Name = "Product 3"
                });
        }
    }
}
