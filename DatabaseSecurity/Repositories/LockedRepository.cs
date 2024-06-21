using DatabaseSecurity.Identity;
using DatabaseSecurity.Locks;
using Microsoft.EntityFrameworkCore;

namespace DatabaseSecurity.Repositories
{
    public class LockedRepository<TCtx> : Repository<TCtx>, IRepository where TCtx : DbContext
    {
        private readonly IEnumerable<IProtected> _protection;
        private readonly IIdentityInfo _info;

        public LockedRepository(TCtx context, IIdentityInfo info, IEnumerable<IProtected> protection) : base(context)
        {
            _info = info;
            _protection = protection;
        }

        public override IQueryable<T> Set<T>() where T : class
        {
            if (_protection.FirstOrDefault(x => x.IsMatch(typeof(T))) is IProtected<T> entityLock)
                return entityLock.Secured(_info.GetIdentityId());

            return _context.Set<T>();
        }

        public override async Task InsertAsync<T>(T obj, CancellationToken cancellationToken) where T : class
        {
            var hasAccess = await HasAccess(obj, cancellationToken);

            if (!hasAccess)
                throw new UnauthorizedAccessException();

            _context.Add(obj);
        }

        public override async Task InsertAsync<T>(List<T> obj, CancellationToken cancellationToken) where T : class
        {
            foreach (var item in obj)
                await InsertAsync(item, cancellationToken);
        }

        public override async Task UpdateAsync<T>(T obj, CancellationToken cancellationToken) where T : class
        {
            var hasAccess = await HasAccess(obj, cancellationToken);

            if (!hasAccess)
                throw new UnauthorizedAccessException();

            _context.Update(obj);
        }

        public override async Task UpdateAsync<T>(List<T> obj, CancellationToken cancellationToken) where T : class
        {
            foreach (var item in obj)
                await UpdateAsync(item, cancellationToken);
        }

        public override async Task DeleteAsync<T>(T obj, CancellationToken cancellationToken) where T : class
        {
            var hasAccess = await HasAccess(obj, cancellationToken);

            if (!hasAccess)
                throw new UnauthorizedAccessException();

            _context.Remove(obj);
        }

        public override async Task DeleteAsync<T>(List<T> obj, CancellationToken cancellationToken) where T : class
        {
            foreach (var item in obj)
                await DeleteAsync(item, cancellationToken);
        }

        private async Task<bool> HasAccess<T>(T obj, CancellationToken cancellationToken) where T : class
        {
            var result = true;

            if (_protection.FirstOrDefault(x => x.IsMatch(typeof(T))) is IProtected<T> entityLock)
            {
                result = await entityLock.HasAccess(obj, _info.GetIdentityId(), cancellationToken);
            }

            return result;
        }
    }
}
