﻿using System;
using System.Collections.Generic;
using System.Linq;
using Storage.Helpers;
using Storage.Models;
using Storage.ORM;

namespace Storage.DAO
{
    public static class ProductDAO
    {
        public static bool IsProductCodeAvailable(string code)
        {
            var storageDbEntities = new StorageDBEntities();

            return storageDbEntities.Products.Where(p => p.Code == code && p.UserID == UserHelper.UserID).FirstOrDefault() == null;
        }

        public static ProductModel GetProduct(int id)
        {
            var storageDbEntities = new StorageDBEntities();

            ProductModel product = (from p in storageDbEntities.Products
                                    where p.ID == id && p.UserID == UserHelper.UserID
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
                                       where p.UserID == UserHelper.UserID
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

            list = list.OrderBy(l => Convert.ToInt32(l.Code)).ToList();

            return list;
        }

        public static List<ProductModel> GetProductListByCategory(int categoryID)
        {
            return GetProductListByFilter(categoryID, String.Empty, String.Empty);
        }

        public static List<ProductModel> GetProductListByFilter(int categoryID, string code, string name)
        {
            var storageDbEntities = new StorageDBEntities();

            IQueryable<Product> products = storageDbEntities.Products;

            if(categoryID != -1)
            {
                products = products.Where(p => p.CategoryID == categoryID);
            }

            if(!String.IsNullOrEmpty(code))
            {
                products = products.Where(p => p.Code == code);
            }

            if(!String.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }

            List<ProductModel> list = products.Where(p => p.UserID == UserHelper.UserID).Select(p => new ProductModel
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
                                                                                               }).ToList();
                
            list = list.OrderBy(l => Convert.ToInt32(l.Code)).ToList();

            return list;
        }

        public static void CreateProduct(ProductModel productModel)
        {
            if (UserHelper.UserID != null)
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
                                             CategoryID = productModel.Category.ID,
                                             UserID = UserHelper.UserID.Value
                                         };

                storageDbEntities.Products.AddObject(newProduct);

                storageDbEntities.SaveChanges();
            }
        }

        public static void UpdateProduct(int id, ProductModel productModel)
        {
            var storageDbEntities = new StorageDBEntities();

            Product product = storageDbEntities.Products.Where(p => p.ID == id && p.UserID == UserHelper.UserID).FirstOrDefault();

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

            Product product = storageDbEntities.Products.Where(p => p.ID == id && p.UserID == UserHelper.UserID).FirstOrDefault();

            storageDbEntities.Products.DeleteObject(product);

            storageDbEntities.SaveChanges();
        }

        public static ProductModel GetProductByCode(string code)
        {
            var storageDbEntities = new StorageDBEntities();

            ProductModel product = (from p in storageDbEntities.Products
                                    where p.Code == code && p.UserID == UserHelper.UserID
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