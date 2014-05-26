using GridUI.Persistance;

namespace GridUI.Models
{
    public class UserVm
    {
        #region Constructors

        public UserVm(User user)
        {
            Id = user.Id.ToString();
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        #endregion

        #region Properties


        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        #endregion
    }
}