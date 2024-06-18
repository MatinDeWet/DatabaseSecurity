using Microsoft.EntityFrameworkCore;

namespace DatabaseSecurity.Repositories
{
    public abstract class Repository<TCtx> : IRepository where TCtx : DbContext
    {
        protected readonly TCtx _context;

        protected Repository(TCtx context)
        {
            _context = context;
        }

        public virtual IQueryable<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }

        public virtual Task InsertAsync<T>(T obj, CancellationToken cancellationToken) where T : class
        {
            _context.Add(obj);
            return Task.CompletedTask;
        }

        public virtual Task InsertAsync<T>(List<T> obj, CancellationToken cancellationToken) where T : class
        {
            _context.AddRange(obj);
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync<T>(T obj, CancellationToken cancellationToken) where T : class
        {
            _context.Update(obj);
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync<T>(List<T> obj, CancellationToken cancellationToken) where T : class
        {
            _context.UpdateRange(obj);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync<T>(T obj, CancellationToken cancellationToken) where T : class
        {
            _context.Remove(obj);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync<T>(List<T> obj, CancellationToken cancellationToken) where T : class
        {
            _context.RemoveRange(obj);
            return Task.CompletedTask;
        }
    }
}
