﻿using DatabaseSecurity.Enums;

namespace DatabaseSecurity.Locks
{
    public abstract class Lock<T> : IProtected<T> where T : class
    {
        public abstract IQueryable<T> Secured(int identityId, DataPermissionEnum requirement);

        public virtual bool IsMatch(Type t)
        {
            return typeof(T).IsAssignableFrom(t);
        }

        public abstract Task<bool> HasAccess(T obj, int identityId, DataPermissionEnum requirement, CancellationToken cancellationToken);
    }
}
