using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Storage.Models
{
    public class InvoiceModel
    {
        public InvoiceModel()
        {
            Supplier = new ClientModel();
            Recipient = new ClientModel();

            Products = new List<ProductsInInvoiceModel>();
        }

        public int ID { get; set; }
        public ClientModel Supplier { get; set; }
        public ClientModel Recipient { get; set; }
        public List<ProductsInInvoiceModel> Products { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        public string Type { get; set; }
        [DisplayName("Номер")]
        public int Number { get; set; }
        public decimal MasterTotal { get; set; }
    }

    public class ProductsInInvoiceModel
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductModel Product { get; set; }
        public int Position { get; set; }
        public decimal Total { get; set; }
    }
}