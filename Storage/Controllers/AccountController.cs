﻿using System;
using System.Web.Mvc;
using System.Web.Security;
using Storage.DAO;
using Storage.Models;

namespace Storage.Controllers
{
	public class AccountController: Controller
	{
        //
        // GET: /Account/LogOn

		public ActionResult LogOn()
		{
			return View();
		}

        //
        // POST: /Account/LogOn

		[HttpPost]
		public ActionResult LogOn(LogOnModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
                if (UserDAO.ValidateUser(model.UserName, model.Password))
				{
                    if (UserDAO.IsActive(model.UserName))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                        if (!String.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("loginFailed", "Продлите свою учетную запись.");
                    }
				}

                ModelState.AddModelError("loginFailed", "Неправильный логин или пароль.");
			}
			
			return View(model);
		}

        //
        // GET: /Account/LogOff

		public ActionResult LogOff()
		{
            FormsAuthentication.SignOut();

			return RedirectToAction("Logon", "Account");
		}
	}
}
