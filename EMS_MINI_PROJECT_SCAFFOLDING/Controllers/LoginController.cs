using EMS_MINI_PROJECT_SCAFFOLDING.Models;
using EMS_MINI_PROJECT_SCAFFOLDING.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EMS_MINI_PROJECT_SCAFFOLDING.Controllers
{
    public class LoginController : Controller
    {
        private EMSContext loginContext = new EMSContext();

        public User_Master_174778 User_Master_174778
        {
            get => default(User_Master_174778);
            set
            {
            }
        }

        // GET: Login

        public ActionResult Login()
        {
            //This is for example , we need to remove this code later  
            Log.Info("Login-page started...");

            // Boolean msg = CheckSetupType();  
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.User_Master_174778 objUser)
        {

            //if (ModelState.IsValid)
            //{
            using (loginContext)
                {
                    var obj = loginContext.User_Master_174778.Where(a => a.UserName.Contains(objUser.UserName) && a.UserPassword.Contains(objUser.UserPassword) && a.UserType.Contains(objUser.UserType)).FirstOrDefault();
                    if (obj != null)
                    {
                    //if (ModelState.IsValid)
                   //var validdata= ModelState.IsValid.(obj.UserName, obj.UserPassword, obj.UserType)
                        if (IsValid(obj.UserName, obj.UserPassword, obj.UserType))
                        {
                            Session["UserID"] = obj.UserID.ToString();
                            Session["UserName"] = obj.UserName.ToString();
                            Session["UserType"] = obj.UserType.ToString();
                            FormsAuthentication.SetAuthCookie(obj.UserID.ToString(), false);
                            FormsAuthentication.SetAuthCookie(obj.UserName, false);
                            FormsAuthentication.SetAuthCookie(obj.UserType, false);

                            return RedirectToAction("UserDashBoard");
                        }
                    else
                    {
                        ModelState.AddModelError("", "Please try with correct credentials!!");
                    }
                    }
                 return View(obj);

                }
        }

        private bool IsValid(string userName, string userPassword, string userType)
        {
            bool IsValid = false;
            using (loginContext)
            {
                var user=loginContext.User_Master_174778.FirstOrDefault(u => u.UserName == userName);
                if(user!=null)
                {
                    if(user.UserPassword == userPassword)
                    {
                        IsValid = true;
                    }
                }
            }
            return IsValid;
        }



        //  }
        //return View(objUser);

        [OutputCache(NoStore = true , Duration =0, VaryByParam = "None")]
        public ActionResult UserDashBoard()
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login");
                }
                
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
           
        }

        public ActionResult AccessDenied()
        {
            
            return View();

        }

        public ActionResult Logout()
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    Session.Abandon();
                }

                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                return View();
                throw ex;
            }
            
        }

    }
}