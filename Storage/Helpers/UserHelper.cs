using System.Web;
using Storage.DAO;

namespace Storage.Helpers
{
    public static class UserHelper
    {
        public static string UserName
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }

        public static int UserID
        {
            get
            {
                return UserDAO.GetUser(UserName).ID;
            }
        }
    }
}