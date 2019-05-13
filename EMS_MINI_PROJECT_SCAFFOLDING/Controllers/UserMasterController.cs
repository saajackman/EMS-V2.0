using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using EMS_MINI_PROJECT_SCAFFOLDING.Models;
using Rotativa;

namespace EMS_MINI_PROJECT_SCAFFOLDING.Controllers
{
    public class UserMasterController : Controller
    {
        private EMSContext db = new EMSContext();

        public User_Master_174778 User_Master_174778
        {
            get => default(User_Master_174778);
            set
            {
            }
        }

        // GET: UserMaster
        public ActionResult Index(string pSortOn, string sortOn, string keyword, string orderBy, int? page)
        {
            try
            { 
            if (Session["UserID"] != null)
            {
                   
                //usertype
                ViewBag.UserType = Session["UserType"];

                //sorting
                ViewBag.OrderBy = orderBy;
                ViewBag.Keyword = keyword;
                ViewBag.SortOn = sortOn;

                var x = db.User_Master_174778.AsQueryable();


                //checking usertype and showing data
                 string usertype = Session["UserType"].ToString();
                 
                 if (usertype != "S")
                 {
                    var userID = Convert.ToInt32(Session["UserID"]); 
                    x = db.User_Master_174778.Where(t => t.UserID == userID).AsQueryable();
                 }
                  
                      

                int recordsPerPage = 10;
                if (!page.HasValue)
                {
                    page = 1; // set initial page value
                    if (string.IsNullOrWhiteSpace(orderBy) || orderBy.Equals("asc"))
                    {
                        orderBy = "desc";
                    }
                    else
                    {
                        orderBy = "asc";
                    }
                }

                // override the sort order if the previous sort order and current request sort order is different
                if (!string.IsNullOrWhiteSpace(sortOn) && !sortOn.Equals(pSortOn, StringComparison.CurrentCultureIgnoreCase))
                {
                    orderBy = "asc";
                }


                switch (sortOn)
                {
                    case "UserID":
                        if (orderBy.Equals("desc"))
                        {
                            x = x.OrderByDescending(u => u.UserID);
                        }
                        else
                        {
                            x = x.OrderBy(u => u.UserID);
                        }
                        break;


                    case "UserName":

                        if (orderBy.Equals("desc"))
                        {
                            x = x.OrderByDescending(u => u.UserName);
                        }
                        else
                        {
                            x = x.OrderBy(u => u.UserName);
                        }
                        break;

                    case "UserType":

                        if (orderBy.Equals("desc"))
                        {
                            x = x.OrderByDescending(u => u.UserType);
                        }
                        else
                        {
                            x = x.OrderBy(u => u.UserType);
                        }
                        break;
                    default:
                        x = x.OrderBy(u => u.UserID);
                        break;
                }


                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    x = x.Where(f => f.UserName.StartsWith(keyword) || f.UserID.ToString().StartsWith(keyword) || f.UserType.StartsWith(keyword));
                }

                    var finalList = x.ToPagedList(page.Value, recordsPerPage);
                         
                    ViewBag.Added = false;
                    ViewBag.Updated = false;
                    ViewBag.Deleted = false;
                    if (TempData.ContainsKey("Added"))
                    {
                        ViewBag.Added = true;
                    }
                    if (TempData.ContainsKey("Updated"))
                    {
                        ViewBag.Updated = true;
                    }
                    if (TempData.ContainsKey("Deleted"))
                    {
                        ViewBag.Deleted = true;
                    }
                    TempData.Clear();


                    return View(finalList);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
         }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        // GET: UserMaster/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    User_Master_174778 user_Master_174778 = db.User_Master_174778.Find(id);
                    if (user_Master_174778 == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user_Master_174778);
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


        }

        // GET: UserMaster/Create
        public ActionResult Create()
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    string usertype = Session["UserType"].ToString();
                    if (usertype != "R")
                    {
                        ViewBag.Status = false;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("AccessDenied", "Login");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }      

        }
    

        // POST: UserMaster/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,UserPassword,UserType")] User_Master_174778 user_Master_174778)
        {
            ViewBag.Status = false;
            try
            { 

                if (ModelState.IsValid)
                {
                    db.User_Master_174778.Add(user_Master_174778);
                    db.SaveChanges();
                    TempData["Added"] = true;
                   
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return View(user_Master_174778);
        }

        // GET: UserMaster/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["UserID"] != null)
            {
                try
                {
                    string usertype = Session["UserType"].ToString();
                    if (usertype != "R")
                    {

                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        User_Master_174778 user_Master_174778 = db.User_Master_174778.Find(id);
                        if (user_Master_174778 == null)
                        {
                            return HttpNotFound();
                        }
                        return View(user_Master_174778);
                    }
                    else
                    {
                        return RedirectToAction("AccessDenied", "Login");
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

        // POST: UserMaster/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,UserPassword,UserType")] User_Master_174778 user_Master_174778)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_Master_174778).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Updated"] = true;
                return RedirectToAction("Index");
            }
            return View(user_Master_174778);
        }

        // GET: UserMaster/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {

            
            if (Session["UserID"] != null)
            {

                string usertype = Session["UserType"].ToString();
                if (usertype != "R")
                {  if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User_Master_174778 user_Master_174778 = db.User_Master_174778.Find(id);
                if (user_Master_174778 == null)
                {
                    return HttpNotFound();
                }
                return View(user_Master_174778);
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Login");
                }
              
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // POST: UserMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User_Master_174778 user_Master_174778 = db.User_Master_174778.Find(id);
            db.User_Master_174778.Remove(user_Master_174778);
            db.SaveChanges();
            TempData["Deleted"] = true;
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Logout()
        {
            try
            {

            
            if (Session["UserID"] != null)
            {
                Session.Abandon();
            }

            return RedirectToAction("Login", "Login" );
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public ActionResult ExportData()
        { 
            var exportData = db.User_Master_174778.ToList();
            return View(exportData);
        }
        public ActionResult PrintExportData()
        {
            var print = new ActionAsPdf("ExportData");
            return print;
        }

    }
}
