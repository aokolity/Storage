using System.Collections.Generic;
using System.Web.Mvc;
using Storage.Models;

namespace Storage.ViewModels
{
    public class InvoiceViewModel
    {
        public InvoiceViewModel()
        {
            InvoiceModel = new InvoiceModel();
            Clients = new List<SelectListItem>();
        }

        public InvoiceModel InvoiceModel { get; set; }
        public List<SelectListItem> Clients { get; set; }
    }
}