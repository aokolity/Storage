using System.Web.Mvc;
using Storage.DAO;

namespace Storage.Controllers
{
    public class ValidationController : Controller
    {
        public JsonResult IsProductCodeAvailable(string code, string codeInitialValue)
        {
            // check Edit case
            if(code == codeInitialValue)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            if(!ProductDAO.IsProductCodeAvailable(code))
            {
                return Json("Товар с таким Кодом уже существует.", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}