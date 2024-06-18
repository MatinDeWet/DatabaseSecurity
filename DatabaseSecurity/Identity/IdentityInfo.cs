using DatabaseSecurity.Info;

namespace DatabaseSecurity.Identity
{
    public class IdentityInfo : IIdentityInfo
    {
        private readonly IInfoSetter _infoSetter;

        public IdentityInfo(IInfoSetter infoSetter)
        {
            _infoSetter = infoSetter;
        }

        public int GetIdentityId()
        {
            var identityId = (string)_infoSetter.GetValue("sub");

            if (!int.TryParse(identityId, out int result))
                return 0;

            return result;
        }
    }
}
