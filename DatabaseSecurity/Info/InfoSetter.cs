namespace DatabaseSecurity.Info
{
    public class InfoSetter : IInfoSetter
    {
        private Dictionary<string, object> _claims;

        public InfoSetter()
        {
            _claims = new Dictionary<string, object>();
        }

        public object GetValue(string name)
        {
            if (!_claims.TryGetValue(name, out var value))
            {
                return default!;
            }

            return value;
        }

        public bool HasValue(string name)
        {
            return _claims.ContainsKey(name);
        }

        public void SetUser(Dictionary<string, object> claims)
        {
            _claims = claims;
        }
    }
}
