using System;
using System.Collections.Generic;
using System.Linq;
//using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
//using DotNetOpenAuth.AspNet;
//using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using DemoCCM.Models;
using DemoCCM.Controllers;


namespace WebsiteBanHang.Controllers
{
    [Authorize]
    //[InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        ConceptMapDBContext db = new ConceptMapDBContext();
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(DemoCCM.Models.User model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (IsValid(model.UserName, model.Pass))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "TrangChu");
                }
            }

            // If we got this far, something failed, redisplay form
            //ModelState.AddModelError("", "Tên đăng nhập hoặc Mật khẩu sai!");
            return View();
        }


        // POST: /Account/LogOff
     
        private bool IsValid(string UserName, string Pass)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool isvalid = false;

            var c = db.Users.FirstOrDefault(m=>m.UserName==UserName);
            if (c != null)
            {
                if(c.Pass==(crypto.Compute(Pass, c.PassSalt)))
                {
                    isvalid = true;
                }
            }

            return isvalid;
        }

        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "TrangChu");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
            if (!ModelState.IsValid) //false
            {
                

            }
            if (ModelState.IsValid)
            {
          
                // Attempt to register the user
                try
                {
                    using (var db1 = new ConceptMapDBContext())
                    {
                        var crypto = new SimpleCrypto.PBKDF2();
                       var encrPass = crypto.Compute(model.Pass);

                        var systemUser = db.Users.Create();

                 
                        systemUser.UserName = model.UserName;
                        systemUser.Pass =encrPass;
                        systemUser.PassSalt = crypto.Salt;


                        db1.Users.Add(systemUser);
                        db1.SaveChanges();
                    }
                    return RedirectToAction("Index", "TrangChu");

                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", "Tên đăng nhập này đã được sử dụng !");

                    //ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

    }
}
