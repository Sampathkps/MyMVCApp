using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessEntities;
using BusinessLayer;
using System.Web.Security;

namespace MyMVCApp.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoLogin(UserDetails u)
        {
            EmployeeBL bal = new EmployeeBL();
            UserStatus userStatus = bal.GetUserValidity(u);
            bool isAdmin = false;

            if (userStatus == UserStatus.AuthenticatedAdmin)
            {
                isAdmin = true;
            }
            else if (userStatus == UserStatus.AuthenticatedUser)
            {
                isAdmin = false;
            }
            else
            {
                ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                return View("Login");
            }

            Session["Admin"] = isAdmin;
            FormsAuthentication.SetAuthCookie(u.UserName, false);
            return RedirectToAction("GetViewModel", "Home");
            //if (bal.IsValidUser(u))
            //{
            //    FormsAuthentication.SetAuthCookie(u.UserName, false);
            //    return RedirectToAction("GetViewModel", "Home");
            //}
            //else
            //{
            //    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
            //    return View("Login");
            //}
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

            
        }
    }
}