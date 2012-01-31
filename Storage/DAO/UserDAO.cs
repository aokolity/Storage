using System;
using Storage.Models;
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

        public static int? GetUserID(string userName)
        {
            var storageDbEntities = new StorageDBEntities();

            User user = storageDbEntities.Users.Where(u => u.Username == userName).FirstOrDefault();

            return user != null ? user.ID : (int?) null;
        }

        public static UserModel GetUser(string userName)
        {
            var storageDbEntities = new StorageDBEntities();

            UserModel userModel = (from u in storageDbEntities.Users
                                   where u.Username == userName
                                   select new UserModel
                                              {
                                                  ID = u.ID,
                                                  Name = u.Name,
                                                  Surname = u.Surname
                                              }).FirstOrDefault();

            return userModel;
        }

        public static void UpdateUser(UserModel userModel)
        {
            var storageDbEntities = new StorageDBEntities();

            User user = storageDbEntities.Users.Where(u => u.ID == userModel.ID).FirstOrDefault();

            if (user != null)
            {
                user.Name = userModel.Name;
                user.Surname = userModel.Surname;
            }

            storageDbEntities.SaveChanges();
        }

        public static bool IsActive(string userName)
        {
            var storageDbEntities = new StorageDBEntities();

            User user = storageDbEntities.Users.Where(u => u.Username == userName).FirstOrDefault();

            if(user != null)
            {
                return user.ExpirationDate >= DateTime.Now;
            }

            return false;
        }
    }
}
