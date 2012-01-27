using Storage.ORM;
using System.Linq;

namespace Storage.DAO
{
    public static class UserDAO
    {
        public static bool ValidateUser(string userName, string password)
        {
            var storageDbEntities = new StorageDBEntities();

            User user = storageDbEntities.Users.Where(u => u.Username == userName && u.Password == password).FirstOrDefault();

            return user != null;
        }

        public static string GetUserFullName(string userName)
        {
            var storageDbEntities = new StorageDBEntities();

            User user = storageDbEntities.Users.Where(u => u.Username == userName).FirstOrDefault();

            return user != null ? string.Format("{0} {1}", user.Name, user.Surname) : string.Empty;
        }

        public static User GetUser(string userName)
        {
            var storageDbEntities = new StorageDBEntities();

            return storageDbEntities.Users.Where(u => u.Username == userName).FirstOrDefault();
        }
    }
}
