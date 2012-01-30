using System.Web.Mvc;
using Storage.DAO;
using Storage.Helpers;
using Storage.Models;

namespace Storage.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/Edit

        public ActionResult Edit()
        {
            UserModel userModel = UserDAO.GetUser(UserHelper.UserName);

            return View(userModel);
        }

        //
        // POST: /User/Edit

        [HttpPost]
        public ActionResult Edit(UserModel userModel)
        {
            try
            {
                UserDAO.UpdateUser(userModel);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
