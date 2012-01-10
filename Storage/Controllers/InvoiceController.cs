using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Storage.DAO;
using Storage.Models;
using Storage.ViewModels;

namespace Storage.Controllers
{
    public class InvoiceController : Controller
    {
        //
        // GET: /Invoice/

        public ActionResult Index(string type)
        {
            List<InvoiceModel> list = InvoiceDAO.GetInvoiceList(type);

            ViewBag.Type = type;

            return View(list);
        }

        //
        // GET: /Invoice/Create

        public ActionResult Create(string type)
        {
            ViewBag.Type = type;

            var invoiceModel = new InvoiceModel
                                   {
                                       Number = InvoiceDAO.GetNextInvoiceNumber(type),
                                       Date = DateTime.Now,
                                       Type = type
                                   };

            var clientList = ClientDAO.GetClientList().Select(client => new SelectListItem
                                                                    {
                                                                        Text = client.Name, Value = client.ID.ToString()
                                                                    }).ToList();

            
            return View(new InvoiceViewModel
                            {
                                InvoiceModel = invoiceModel,
                                Clients = clientList
                            });
        } 

        //
        // POST: /Invoice/Create

        [HttpPost]
        public ActionResult Create(InvoiceViewModel invoiceViewModel)
        {
            try
            {
                InvoiceDAO.SaveInvoice(invoiceViewModel.InvoiceModel);

                return RedirectToAction("Index", new { type = invoiceViewModel.InvoiceModel.Type });
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Print(int id)
        {
            InvoiceModel invoiceModel = InvoiceDAO.GetInvoice(id);

            ViewBag.Type = invoiceModel.Type;

            return View(invoiceModel);
        }

        public ActionResult Delete(int id, string type)
        {
            InvoiceDAO.DeleteInvoice(id);

            return RedirectToAction("Index", new {type = type});
        }
    }
}
