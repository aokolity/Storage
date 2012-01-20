using System;
using System.Collections.Generic;
using System.Linq;
using Storage.Models;
using Storage.ORM;

namespace Storage.DAO
{
    public class InvoiceDAO
    {
        public static List<InvoiceModel> GetInvoiceList(string type)
        {
            var storageDbEntities = new StorageDBEntities();

            List<InvoiceModel> list = (from im in storageDbEntities.Invoices
                                       where im.Type == type
                                       select new InvoiceModel
                                                  {
                                                      ID = im.ID,
                                                      Number = im.Number,
                                                      Date = im.Date
                                                  }
                                      ).ToList();

            return list;
        }

        public static int GetNextInvoiceNumber(string type)
        {
            var storageDbEntities = new StorageDBEntities();

            var currentNumber = (from i in storageDbEntities.Invoices
                                 where i.Type == type
                                 select i.Number).DefaultIfEmpty().Max();

            return currentNumber + 1;
        }

        public static InvoiceModel GetInvoice(int id)
        {
            var storageDbEntities = new StorageDBEntities();

            Invoice invoice = storageDbEntities.Invoices.Where(inv => inv.ID == id).FirstOrDefault();

            if (invoice != null)
            {
                InvoiceModel invoiceModel = new InvoiceModel
                                                {
                                                    Supplier = new ClientModel
                                                                   {
                                                                       ID = invoice.Supplier.ID,
                                                                       Name = invoice.Supplier.Name,
                                                                       Address = invoice.Supplier.Address,
                                                                       Telephone = invoice.Supplier.Telephone
                                                                   },
                                                    Recipient = new ClientModel
                                                                    {
                                                                        ID = invoice.Recipient.ID,
                                                                        Name = invoice.Recipient.Name,
                                                                        Address = invoice.Recipient.Address,
                                                                        Telephone = invoice.Recipient.Telephone
                                                                    },
                                                    Date = invoice.Date,
                                                    Type = invoice.Type,
                                                    Number = invoice.Number,
                                                    PriceType = invoice.PriceType,
                                                    ID = invoice.ID
                                                };

                foreach (ProductsInInvoice productsInInvoice in invoice.ProductsInInvoices.ToList())
                {
                    ProductsInInvoiceModel productsInInvoiceModel = new ProductsInInvoiceModel
                                                                        {
                                                                            ID = productsInInvoice.ID,
                                                                            Price = productsInInvoice.Price,
                                                                            Quantity = productsInInvoice.Quantity,
                                                                            Total = productsInInvoice.Price * Convert.ToDecimal(productsInInvoice.Quantity),
                                                                            Product = new ProductModel
                                                                                          {
                                                                                              ID = productsInInvoice.Product.ID,
                                                                                              Name = productsInInvoice.Product.Name,
                                                                                              Code = productsInInvoice.Product.Code,
                                                                                              Unit = productsInInvoice.Product.Unit,
                                                                                          }
                                                                        };

                    invoiceModel.Products.Add(productsInInvoiceModel);

                    invoiceModel.MasterTotal += productsInInvoiceModel.Total;
                }

                invoiceModel.Products = invoiceModel.Products.OrderBy(p => Convert.ToInt32(p.Product.Code)).ToList();

                for (int position = 0; position < invoiceModel.Products.Count; position++)
                {
                    invoiceModel.Products[position].Position = position + 1;
                }

                return invoiceModel;
            }

            return null;
        }

        public static void SaveInvoice(InvoiceModel invoiceModel)
        {
            var storageDbEntities = new StorageDBEntities();

            Invoice invoice = new Invoice
                                  {
                                      SupplierID = invoiceModel.Supplier.ID,
                                      RecipientID = invoiceModel.Recipient.ID,
                                      Date = invoiceModel.Date,
                                      Type = invoiceModel.Type,
                                      Number = invoiceModel.Number,
                                      PriceType = invoiceModel.PriceType
                                  };

            foreach (ProductsInInvoiceModel productsInInvoiceModel in invoiceModel.Products.Where(p => p.ProductID > 0).ToList())
            {
                ProductsInInvoice productsInInvoice = new ProductsInInvoice
                                                          {
                                                              Price = productsInInvoiceModel.Price,
                                                              Quantity = productsInInvoiceModel.Quantity,
                                                              ProductID = productsInInvoiceModel.ProductID
                                                          };

                invoice.ProductsInInvoices.Add(productsInInvoice);
            }

            storageDbEntities.Invoices.AddObject(invoice);

            storageDbEntities.SaveChanges();
        }

        public static void DeleteInvoice(int id)
        {
            var storageDbEntities = new StorageDBEntities();

            Invoice invoice = storageDbEntities.Invoices.Where(p => p.ID == id).FirstOrDefault();

            if (invoice != null)
            {
                foreach (ProductsInInvoice productsInInvoice in invoice.ProductsInInvoices.ToList())
                {
                    storageDbEntities.ProductsInInvoices.DeleteObject(productsInInvoice);
                    storageDbEntities.SaveChanges();
                }

                storageDbEntities.Invoices.DeleteObject(invoice);
                storageDbEntities.SaveChanges();
            }
        }

        public static void UpdateInvoice(InvoiceModel invoiceModel)
        {
            
        }
    }
}