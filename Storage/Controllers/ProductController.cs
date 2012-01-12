using System.Collections.Generic;
using System.Web.Mvc;
using Storage.DAO;
using Storage.Helpers;
using Storage.Models;
using System.Linq;

namespace Storage.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            var categories = GetCategories();

            categories.Insert(0, new SelectListItem { Value = "-1", Text = "Все" });

            ViewBag.Categories = categories;

            List<ProductModel> list = ProductDAO.GetProductList();

            return View(list);
        }

        //
        // POST: /Product/

        [HttpPost]
        public ActionResult Index(int categoryID, string code)
        {
            List<SelectListItem> categories = GetCategories();

            categories.Insert(0, new SelectListItem { Value = "-1", Text = "Все" });

            foreach (SelectListItem selectListItem in categories)
            {
                if(selectListItem.Value == categoryID.ToString())
                {
                    selectListItem.Selected = true;
                }
            }

            ViewBag.Categories = categories;
            ViewBag.Code = code;

            List<ProductModel> list = ProductDAO.GetProductListByCategoryIDAndCode(categoryID, code);

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
            ViewBag.Categories = GetCategories();

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

            List<SelectListItem> categories = GetCategories();

            foreach (SelectListItem selectListItem in categories)
            {
                if (product.Category.ID.HasValue && selectListItem.Value == product.Category.ID.ToString())
                {
                    selectListItem.Selected = true;
                }
            }

            ViewBag.Categories = categories;

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
            var categories = GetCategories();

            categories.Insert(0, new SelectListItem { Value = "-1", Text = "Все" });

            ViewBag.Categories = categories;

            List<ProductModel> list = ProductDAO.GetProductList();

            return View(list);
        }

        [HttpPost]
        public ActionResult PriceList(int categoryID)
        {
            List<SelectListItem> categories = GetCategories();

            categories.Insert(0, new SelectListItem { Value = "-1", Text = "Все" });

            foreach (SelectListItem selectListItem in categories)
            {
                if (selectListItem.Value == categoryID.ToString())
                {
                    selectListItem.Selected = true;
                }
            }

            ViewBag.Categories = categories;

            List<ProductModel> list = ProductDAO.GetProductListByCategoryIDAndCode(categoryID, string.Empty);

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
                                    Price = CurrencyHelper.FormatCurrency(price),
                                    Code = code
                                });
            }

            return Json(new { error = "Извините, но по такому коду не найдено ни одного товара." });
        }

        private static List<SelectListItem> GetCategories()
        {
            List<SelectListItem> categories = CategoryDAO.GetCategoryList().Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();
            return categories;
        }
    }
}
