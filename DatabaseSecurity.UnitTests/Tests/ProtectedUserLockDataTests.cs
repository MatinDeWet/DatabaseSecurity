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
        public async Task GetClient_WithReadPermission_ShouldGetClient()
        {
            // Arrange
            var userClaims = GetUserWithReadPermission();
            _infoSetter.SetUser(userClaims.ToDictionary(c => c.Type, c => (object)c.Value));

            var cancellationToken = new CancellationToken();
            var clientId = 1;

            // Act
            var client = await _repository.Clients.FirstOrDefaultAsync(p => p.Id == clientId, cancellationToken);

            // Assert
            Assert.NotNull(client);
        }

        [Fact]
        public async Task GetClient_WithWritePermission_ShouldGetClient()
        {
            // Arrange
            var userClaims = GetUserWithWritePermission();
            _infoSetter.SetUser(userClaims.ToDictionary(c => c.Type, c => (object)c.Value));

            var cancellationToken = new CancellationToken();
            var clientId = 1;

            // Act
            var client = await _repository.Clients.FirstOrDefaultAsync(p => p.Id == clientId, cancellationToken);

            // Assert
            Assert.NotNull(client);
        }

        [Fact]
        public async Task GetClient_WithDeletePermission_ShouldGetClient()
        {
            // Arrange
            var userClaims = GetUserWithDeletePermission();
            _infoSetter.SetUser(userClaims.ToDictionary(c => c.Type, c => (object)c.Value));

            var cancellationToken = new CancellationToken();
            var clientId = 1;

            // Act
            var product = await _repository.Clients.FirstOrDefaultAsync(p => p.Id == clientId, cancellationToken);

            // Assert
            Assert.NotNull(product);
        }
        #endregion

        #region InsertClient
        [Fact]
        public async Task InsertClient_WithReadPermission_ShouldInsertClient()
        {
            // Arrange
            var userClaims = GetUserWithReadPermission();
            _infoSetter.SetUser(userClaims.ToDictionary(c => c.Type, c => (object)c.Value));

            var cancellationToken = new CancellationToken();
            var client = new Client
            {
                Name = "New Client",
                TeamId = 1
            };

            // Act

            Func<Task> act = () => _repository.InsertAsync(client, cancellationToken);

            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(act);
        }

        [Fact]
        public async Task InsertClient_WithWritePermission_ShouldInsertClient()
        {
            // Arrange
            var userClaims = GetUserWithWritePermission();
            _infoSetter.SetUser(userClaims.ToDictionary(c => c.Type, c => (object)c.Value));

            var cancellationToken = new CancellationToken();
            var client = new Client
            {
                Name = "New Client",
                TeamId = 1
            };

            // Act
            await _repository.InsertAsync(client, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            // Assert
            Assert.NotEqual(0, client.Id);
        }

        [Fact]
        public async Task InsertClient_WithDeletePermission_ShouldInsertClient()
        {
            // Arrange
            var userClaims = GetUserWithDeletePermission();
            _infoSetter.SetUser(userClaims.ToDictionary(c => c.Type, c => (object)c.Value));

            var cancellationToken = new CancellationToken();
            var client = new Client
            {
                Name = "New Client",
                TeamId = 1
            };

            // Act
            await _repository.InsertAsync(client, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            // Assert
            Assert.NotEqual(0, client.Id);
        }
        #endregion

        #region UpdateClient
        [Fact]
        public async Task UpdateClient_WithReadPermission_ShouldUpdateClient()
        {
            // Arrange
            var userClaims = GetUserWithReadPermission();
            _infoSetter.SetUser(userClaims.ToDictionary(c => c.Type, c => (object)c.Value));

            var cancellationToken = new CancellationToken();
            var clientId = 1;
            var client = await _repository.Clients.FirstOrDefaultAsync(p => p.Id == clientId, cancellationToken);
            var updatedName = "Updated Client";

            // Act
            client!.Name = updatedName;

            Func<Task> act = () => _repository.UpdateAsync(client, cancellationToken);

            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(act);
        }

        [Fact]
        public async Task UpdateClient_WithWritePermission_ShouldUpdateClient()
        {
            // Arrange
            var userClaims = GetUserWithWritePermission();
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

        [Fact]
        public async Task UpdateClient_WithDeletePermission_ShouldUpdateClient()
        {
            // Arrange
            var userClaims = GetUserWithDeletePermission();
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
        public async Task DeleteClient_WithReadPermission_ShouldDeleteClient()
        {
            // Arrange
            var userClaims = GetUserWithReadPermission();
            _infoSetter.SetUser(userClaims.ToDictionary(c => c.Type, c => (object)c.Value));

            var cancellationToken = new CancellationToken();
            var clientId = 2;
            var client = await _repository.Clients.FirstOrDefaultAsync(p => p.Id == clientId, cancellationToken);

            // Act
            Func<Task> act = () => _repository.DeleteAsync(client!, cancellationToken);

            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(act);
        }

        [Fact]
        public async Task DeleteClient_WithWritePermission_ShouldDeleteClient()
        {
            // Arrange
            var userClaims = GetUserWithWritePermission();
            _infoSetter.SetUser(userClaims.ToDictionary(c => c.Type, c => (object)c.Value));

            var cancellationToken = new CancellationToken();
            var clientId = 2;
            var client = await _repository.Clients.FirstOrDefaultAsync(p => p.Id == clientId, cancellationToken);

            // Act
            Func<Task> act = () => _repository.DeleteAsync(client!, cancellationToken);

            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(act);
        }

        [Fact]
        public async Task DeleteClient_WithDeletePermission_ShouldDeleteClient()
        {
            // Arrange
            var userClaims = GetUserWithDeletePermission();
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

        private ICollection<Claim> GetUserWithReadPermission()
        {
            return new List<Claim>
            {
                new Claim("sub", "1"),
            };
        }

        private ICollection<Claim> GetUserWithWritePermission()
        {
            return new List<Claim>
            {
                new Claim("sub", "2"),
            };
        }

        private ICollection<Claim> GetUserWithDeletePermission()
        {
            return new List<Claim>
            {
                new Claim("sub", "3"),
            };
        }
    }
}
