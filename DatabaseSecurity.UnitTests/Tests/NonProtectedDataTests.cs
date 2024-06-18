using DatabaseSecurity.UnitOfWork;
using DatabaseSecurity.UnitTests.Models;
using DatabaseSecurity.UnitTests.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DatabaseSecurity.UnitTests.Tests
{
    public class NonProtectedDataTests
    {
        private readonly ITestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public NonProtectedDataTests(ITestRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        [Fact]
        public async Task GetProduct_ShouldGetProduct()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var productId = 1;

            // Act
            var product = await _repository.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

            // Assert
            Assert.NotNull(product);
        }

        [Fact]
        public async Task InsertProduct_ShouldInsertProduct()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var product = new Product
            {
                Name = "new Product",
            };

            // Act
            await _repository.InsertAsync(product, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            // Assert
            Assert.NotEqual(0, product.Id);
        }

        [Fact]
        public async Task UpdateProduct_ShouldUpdateProduct()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var productId = 2;
            var product = await _repository.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
            var updatedName = "updated Product";

            // Act
            product!.Name = updatedName;
            await _repository.UpdateAsync(product, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            // Assert
            Assert.Equal(updatedName, product.Name);
        }

        [Fact]
        public async Task DeleteProduct_ShouldDeleteProduct()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var productId = 3;
            var product = await _repository.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

            // Act
            await _repository.DeleteAsync(product!, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            // Assert
            Assert.Null(await _repository.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken));
        }
    }
}
