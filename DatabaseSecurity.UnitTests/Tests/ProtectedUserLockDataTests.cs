using DatabaseSecurity.Info;
using DatabaseSecurity.UnitOfWork;
using DatabaseSecurity.UnitTests.Models;
using DatabaseSecurity.UnitTests.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DatabaseSecurity.UnitTests.Tests
{
    public class ProtectedUserLockDataTests
    {
        private readonly IProtectedTestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInfoSetter _infoSetter;

        public ProtectedUserLockDataTests(IProtectedTestRepository repository, IUnitOfWork unitOfWork, IInfoSetter infoSetter)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _infoSetter = infoSetter;
        }

        #region GetClient
        [Fact]
        public async Task GetClient_ShouldGetClient()
        {
            // Arrange
            var userClaims = GetUser();
            _infoSetter.SetUser(userClaims.ToDictionary(c => c.Type, c => (object)c.Value));

            var cancellationToken = new CancellationToken();
            var clientId = 1;

            // Act
            var client = await _repository.Clients.FirstOrDefaultAsync(p => p.Id == clientId, cancellationToken);

            // Assert
            Assert.NotNull(client);
        }
        #endregion

        #region InsertClient
        [Fact]
        public async Task InsertClient_ShouldInsertClient()
        {
            // Arrange
            var userClaims = GetUser();
            _infoSetter.SetUser(userClaims.ToDictionary(c => c.Type, c => (object)c.Value));

            var cancellationToken = new CancellationToken();
            var client = new Client
            {
                Name = "New Client",
                TeamId = 1
            };

            // Act

            await _repository.InsertAsync(client, cancellationToken);

            // Assert
            Assert.NotEqual(0, client.Id);
        }
        #endregion

        #region UpdateClient
        [Fact]
        public async Task UpdateClient_ShouldUpdateClient()
        {
            // Arrange
            var userClaims = GetUser();
            _infoSetter.SetUser(userClaims.ToDictionary(c => c.Type, c => (object)c.Value));

            var cancellationToken = new CancellationToken();
            var clientId = 1;
            var client = await _repository.Clients.FirstOrDefaultAsync(p => p.Id == clientId, cancellationToken);
            var updatedName = "Updated Client";

            // Act
            client!.Name = updatedName;
            await _repository.UpdateAsync(client, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            // Assert
            Assert.Equal(updatedName, client.Name);
        }
        #endregion

        #region DeleteClient
        [Fact]
        public async Task DeleteClient_ShouldDeleteClient()
        {
            // Arrange
            var userClaims = GetUser();
            _infoSetter.SetUser(userClaims.ToDictionary(c => c.Type, c => (object)c.Value));

            var cancellationToken = new CancellationToken();
            var clientId = 2;
            var client = await _repository.Clients.FirstOrDefaultAsync(p => p.Id == clientId, cancellationToken);

            // Act
            await _repository.DeleteAsync(client!, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            // Assert
            var deletedClient = await _repository.Clients.FirstOrDefaultAsync(p => p.Id == clientId, cancellationToken);
            Assert.Null(deletedClient);
        }
        #endregion

        private ICollection<Claim> GetUser()
        {
            return new List<Claim>
            {
                new Claim("sub", "1"),
            };
        }
    }
}
