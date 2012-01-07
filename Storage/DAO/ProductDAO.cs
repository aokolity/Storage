using System.Collections.Generic;
using System.Linq;
using Storage.Models;
using Storage.ORM;

namespace Storage.DAO
{
    public static class ProductDAO
    {
        public static ProductModel GetProduct(int id)
        {
            var storageDbEntities = new StorageDBEntities();

            ProductModel product = (from p in storageDbEntities.Products
                                    where p.ID == id
                                    select new ProductModel
                                               {
                                                   ID = p.ID,
                                                   Code = p.Code,
                                                   Name = p.Name,
                                                   WholesalePrice = p.WholesalePrice,
                                                   ShallowWholesalePrice = p.ShallowWholesalePrice,
                                                   RetailPrice = p.RetailPrice,
                                                   Unit = p.Unit,
                                                   Category = new CategoryModel
                                                   {
                                                       ID = (int?) p.Category.ID, 
                                                       Name = p.Category.Name
                                                   }
                                               }).SingleOrDefault();
            return product;
        }

        public static List<ProductModel> GetProductList()
        {
            var storageDbEntities = new StorageDBEntities();

            List<ProductModel> list = (from p in storageDbEntities.Products
                                       select new ProductModel
                                       {
                                           ID = p.ID,
                                           Code = p.Code,
                                           Name = p.Name,
                                           WholesalePrice = p.WholesalePrice,
                                           ShallowWholesalePrice = p.ShallowWholesalePrice,
                                           RetailPrice = p.RetailPrice,
                                           Unit = p.Unit,
                                           Category = new CategoryModel
                                            {
                                                ID = (int?)p.Category.ID,
                                                Name = p.Category.Name
                                            }
                                       }).ToList();

            return list;
        }

        public static List<ProductModel> GetProductListByCategoryID(int categoryID)
        {
            var storageDbEntities = new StorageDBEntities();

            List<ProductModel> list = (from p in storageDbEntities.Products
                                       where p.CategoryID == categoryID
                                       select new ProductModel
                                       {
                                           ID = p.ID,
                                           Code = p.Code,
                                           Name = p.Name,
                                           WholesalePrice = p.WholesalePrice,
                                           ShallowWholesalePrice = p.ShallowWholesalePrice,
                                           RetailPrice = p.RetailPrice,
                                           Unit = p.Unit,
                                           Category = new CategoryModel
                                           {
                                               ID = (int?)p.Category.ID,
                                               Name = p.Category.Name
                                           }
                                       }).ToList();

            return list;
        }

        public static void CreateProduct(ProductModel productModel)
        {
            var storageDbEntities = new StorageDBEntities();

            Product newProduct = new Product
                                     {
                                         Code = productModel.Code,
                                         Name = productModel.Name,
                                         Unit = productModel.Unit,
                                         WholesalePrice = productModel.WholesalePrice,
                                         ShallowWholesalePrice = productModel.ShallowWholesalePrice,
                                         RetailPrice = productModel.RetailPrice,
                                         CategoryID = productModel.Category.ID
                                     };

            storageDbEntities.Products.AddObject(newProduct);
            storageDbEntities.SaveChanges();
        }

        public static void UpdateProduct(int id, ProductModel productModel)
        {
            var storageDbEntities = new StorageDBEntities();

            Product product = storageDbEntities.Products.Where(p => p.ID == id).FirstOrDefault();

            if (product != null)
            {
                product.Code = productModel.Code;
                product.Name = productModel.Name;
                product.Unit = productModel.Unit;
                product.WholesalePrice = productModel.WholesalePrice;
                product.ShallowWholesalePrice = productModel.ShallowWholesalePrice;
                product.RetailPrice = productModel.RetailPrice;
                product.CategoryID = productModel.Category.ID;
            }

            storageDbEntities.SaveChanges();
        }

        public static void DeleteProduct(int id)
        {
            var storageDbEntities = new StorageDBEntities();

            Product product = storageDbEntities.Products.Where(p => p.ID == id).FirstOrDefault();

            storageDbEntities.Products.DeleteObject(product);

            storageDbEntities.SaveChanges();
        }

        public static ProductModel GetProductByCode(string code)
        {
            var storageDbEntities = new StorageDBEntities();

            ProductModel product = (from p in storageDbEntities.Products
                                    where p.Code == code
                                    select new ProductModel
                                    {
                                        ID = p.ID,
                                        Code = p.Code,
                                        Name = p.Name,
                                        WholesalePrice = p.WholesalePrice,
                                        ShallowWholesalePrice = p.ShallowWholesalePrice,
                                        RetailPrice = p.RetailPrice,
                                        Unit = p.Unit
                                    }).SingleOrDefault();
            return product;
        }
    }
}