using DatabaseSecurity.Enums;

namespace DatabaseSecurity
{
    public class DataAccessRequirment
    {
        private DataPermissionEnum _requirment;

        public DataAccessRequirment()
        {
            SetAccessRequirement(DataPermissionEnum.Read);
        }

        public void SetAccessRequirement(DataPermissionEnum requirement)
        {
            if (requirement == DataPermissionEnum.None)
                throw new AggregateException("Access Requirement 'None' is invalid");

            _requirment = requirement;
        }

        public DataPermissionEnum GetAccessRequirment()
        {
            return _requirment;
        }
    }
}
