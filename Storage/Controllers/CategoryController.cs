using System.Collections.Generic;
using System.Web.Mvc;
using Storage.DAO;
using Storage.Models;

namespace Storage.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public ActionResult Index()
        {
            List<CategoryModel> list = CategoryDAO.GetCategoryList();

            return View(list);
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id)
        {
            var category = CategoryDAO.GetCategory(id);

            return View(category);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(CategoryModel categoryModel)
        {
            try
            {
                CategoryDAO.CreateProduct(categoryModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Category/Edit/5
 
        public ActionResult Edit(int id)
        {
            var category = CategoryDAO.GetCategory(id);

            return View(category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, CategoryModel categoryModel)
        {
            try
            {
                CategoryDAO.UpdateProduct(id, categoryModel);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Category/Delete/5
 
        public ActionResult Delete(int id)
        {
            var category = CategoryDAO.GetCategory(id);

            return View(category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, CategoryModel categoryModel)
        {
            try
            {
                CategoryDAO.DeleteProduct(id);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
