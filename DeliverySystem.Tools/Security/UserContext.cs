namespace DeliverySystem.Tools.Security
{
    #region Interface

    public interface IUserContext
    {
        void SetDetails(UserDetails userIds);
        UserDetails UserDetails { get; }
    }

    #endregion

    public class UserContext : IUserContext
    {
        public UserDetails UserDetails { get; private set; }

        public void SetDetails(UserDetails userDetails)
        {
            if (UserDetails != null) return;
            UserDetails = userDetails;
        }
    }
}
