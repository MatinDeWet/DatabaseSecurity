namespace DatabaseSecurity.Info
{
    public interface IInfoSetter
    {
        void SetUser(Dictionary<string, object> claims);

        bool HasValue(string name);

        object GetValue(string name);
    }
}
