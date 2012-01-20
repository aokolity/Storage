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
                                Clients = clientList,
                                PriceTypes = GetPriceTypes()
                            });
        } 

        //
        // POST: /Invoice/Create

        [HttpPost]
        public ActionResult Create(InvoiceModel invoiceModel)
        {
            try
            {
                InvoiceDAO.SaveInvoice(invoiceModel);

                return RedirectToAction("Index", new { type = invoiceModel.Type });
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

        public ActionResult Edit(int id, string type)
        {
            ViewBag.Type = type;

            var invoiceModel = InvoiceDAO.GetInvoice(id);

            var clientList = ClientDAO.GetClientList().Select(client => new SelectListItem
                                                                    {
                                                                        Text = client.Name, Value = client.ID.ToString()
                                                                    }).ToList();

            return View(new InvoiceViewModel
                            {
                                InvoiceModel = invoiceModel,
                                Clients = clientList,
                                PriceTypes = GetPriceTypes()
                            });
        }

        [HttpPost]
        public ActionResult Edit(InvoiceModel invoiceModel)
        {
            try
            {
                InvoiceDAO.UpdateInvoice(invoiceModel);

                return RedirectToAction("Index", new { type = invoiceModel.Type });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id, string type)
        {
            InvoiceDAO.DeleteInvoice(id);

            return RedirectToAction("Index", new {type});
        }

        private List<SelectListItem> GetPriceTypes()
        {
            return new List<SelectListItem>
                                        {
                                            new SelectListItem {Value = "RetailPrice", Text = "Цена (розница)"},
                                            new SelectListItem {Value = "ShallowWholesalePrice", Text = "Цена (мелкий опт)"},
                                            new SelectListItem {Value = "WholesalePrice", Text = "Цена (опт)"}
                                        };
        }
    }
}
