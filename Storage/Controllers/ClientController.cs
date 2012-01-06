using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Storage.DAO;
using Storage.Models;

namespace Storage.Controllers
{
    public class ClientController : Controller
    {
        //
        // GET: /Client/

        public ActionResult Index()
        {
            List<ClientModel> list = ClientDAO.GetClientList();

            return View(list);
        }

        //
        // GET: /Client/Details/5

        public ActionResult Details(int id)
        {
            var client = ClientDAO.GetClient(id);

            return View(client);
        }

        //
        // GET: /Client/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Client/Create

        [HttpPost]
        public ActionResult Create(ClientModel clientModel)
        {
            try
            {
                ClientDAO.CreateClient(clientModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Client/Edit/5
 
        public ActionResult Edit(int id)
        {
            var client = ClientDAO.GetClient(id);

            return View(client);
        }

        //
        // POST: /Client/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ClientModel clientModel)
        {
            try
            {
                ClientDAO.UpdateClient(id, clientModel);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Client/Delete/5
 
        public ActionResult Delete(int id)
        {
            var client = ClientDAO.GetClient(id);

            return View(client);
        }

        //
        // POST: /Client/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, ClientModel clientModel)
        {
            try
            {
                ClientDAO.DeleteClient(id);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
