using System.Collections.Generic;
using System.Web.Mvc;
using Storage.DAO;
using Storage.Models;

namespace Storage.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            List<ProductModel> list = ProductDAO.GetProductList();

            return View(list);
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id)
        {
            var product = ProductDAO.GetProduct(id);

            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            try
            {
                ProductDAO.CreateProduct(productModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id)
        {
            var product = ProductDAO.GetProduct(id);

            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ProductModel productModel)
        {
            try
            {
                ProductDAO.UpdateProduct(id, productModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id)
        {
            var product = ProductDAO.GetProduct(id);

            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, ProductModel productModel)
        {
            try
            {
                ProductDAO.DeleteProduct(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult PriceList()
        {
            List<ProductModel> list = ProductDAO.GetProductList();

            return View(list);
        }

        [HttpPost]
        public JsonResult GetProductByCode(string code, string priceType)
        {
            ProductModel productModel = ProductDAO.GetProductByCode(code);

            if (productModel != null)
            {
                decimal price = default(decimal);

                switch (priceType)
                {
                    case "RetailPrice":
                        price = productModel.RetailPrice;
                        break;
                    case "ShallowWholesalePrice":
                        price = productModel.ShallowWholesalePrice;
                        break;
                    case "WholesalePrice":
                        price = productModel.WholesalePrice;
                        break;
                }

                return Json(new
                                {
                                    success = "true",
                                    ID = productModel.ID,
                                    Name = productModel.Name,
                                    Unit = productModel.Unit,
                                    Price = price.ToString(),
                                    Code = code
                                });
            }
            else
            {
                return Json(new { error = "Извините, но по такому коду не найдено ни одного товара." });
            }
        }
    }
}
