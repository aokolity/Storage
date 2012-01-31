using System.Collections.Generic;
using System.Linq;
using Storage.Helpers;
using Storage.Models;
using Storage.ORM;

namespace Storage.DAO
{
    public static class CategoryDAO
    {
        public static CategoryModel GetCategory(int id)
        {
            var storageDbEntities = new StorageDBEntities();

            CategoryModel category = (from c in storageDbEntities.Categories
                                      where c.ID == id && c.UserID == UserHelper.UserID
                                      select new CategoryModel
                                               {
                                                   ID = c.ID,
                                                   Name = c.Name,
                                               }).SingleOrDefault();
            return category;
        }

        public static List<CategoryModel> GetCategoryList()
        {
            var storageDbEntities = new StorageDBEntities();

            List<CategoryModel> list = (from c in storageDbEntities.Categories
                                        where c.UserID == UserHelper.UserID
                                        select new CategoryModel
                                       {
                                           ID = c.ID,
                                           Name = c.Name,
                                       }).ToList();

            return list;
        }

        public static void CreateProduct(CategoryModel categoryModel)
        {
            if (UserHelper.UserID != null)
            {
                var storageDbEntities = new StorageDBEntities();

                Category newCategory = new Category
                                           {
                                               Name = categoryModel.Name,
                                               UserID = UserHelper.UserID.Value
                                           };

                storageDbEntities.Categories.AddObject(newCategory);

                storageDbEntities.SaveChanges();
            }
        }

        public static void UpdateProduct(int id, CategoryModel categoryModel)
        {
            var storageDbEntities = new StorageDBEntities();

            Category category = storageDbEntities.Categories.Where(c => c.ID == id && c.UserID == UserHelper.UserID).FirstOrDefault();

            if (category != null)
            {
                category.Name = categoryModel.Name;
            }

            storageDbEntities.SaveChanges();
        }

        public static void DeleteProduct(int id)
        {
            var storageDbEntities = new StorageDBEntities();

            Category category = storageDbEntities.Categories.Where(p => p.ID == id && p.UserID == UserHelper.UserID).FirstOrDefault();

            storageDbEntities.Categories.DeleteObject(category);

            storageDbEntities.SaveChanges();
        }
    }
}