namespace DatabaseSecurity.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
