﻿namespace DatabaseSecurity.Locks
{
    public interface IProtected
    {
        bool IsMatch(Type t);
    }

    public interface IProtected<T> : IProtected where T : class
    {
        //Used for Writes
        Task<bool> HasAccess(T obj, int identityId, CancellationToken cancellationToken);

        //Used for Reads
        IQueryable<T> Secured(int identityId);
    }
}
