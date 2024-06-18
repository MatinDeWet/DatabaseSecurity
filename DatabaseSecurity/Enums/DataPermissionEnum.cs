namespace DatabaseSecurity.Enums
{
    [Flags]
    public enum DataPermissionEnum
    {
        None = 0,
        Read = 1,
        Write = Read | 2,
        Delete = Write | 4
    }
}
