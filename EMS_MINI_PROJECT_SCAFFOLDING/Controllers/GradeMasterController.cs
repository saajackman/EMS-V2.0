using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EMS_MINI_PROJECT_SCAFFOLDING.Models;
using PagedList;
using Rotativa;

namespace EMS_MINI_PROJECT_SCAFFOLDING.Controllers
{
    public class GradeMasterController : Controller
    {
        private EMSContext db = new EMSContext();

        public Grade_Master_174778 Grade_Master_174778
        {
            get => default(Grade_Master_174778);
            set
            {
            }
        }

        // GET: GradeMaster
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

                var x = db.Grade_Master_174778.AsQueryable();

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
                    case "Grade_Code":
                        if (orderBy.Equals("desc"))
                        {
                            x = x.OrderByDescending(u => u.Grade_Code);
                        }
                        else
                        {
                            x = x.OrderBy(u => u.Grade_Code);
                        }
                        break;


                    case "Description":

                        if (orderBy.Equals("desc"))
                        {
                            x = x.OrderByDescending(u => u.Description);
                        }
                        else
                        {
                            x = x.OrderBy(u => u.Description);
                        }
                        break;

                    case "Max_Salary":

                        if (orderBy.Equals("desc"))
                        {
                            x = x.OrderByDescending(u => u.Max_Salary);
                        }
                        else
                        {
                            x = x.OrderBy(u => u.Max_Salary);
                        }
                        break;

                    case "Min_Salary":

                        if (orderBy.Equals("desc"))
                        {
                            x = x.OrderByDescending(u => u.Min_Salary);
                        }
                        else
                        {
                            x = x.OrderBy(u => u.Min_Salary);
                        }
                        break;
                    default:
                        x = x.OrderBy(u => u.Grade_Code);
                        break;
                }



                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    x = x.Where(f => f.Grade_Code.ToString().StartsWith(keyword) || f.Description.StartsWith(keyword)
                    || f.Min_Salary.ToString().StartsWith(keyword) || f.Max_Salary.ToString().StartsWith(keyword));
                }
                    ViewBag.Added = false;
                    ViewBag.Updated = false;
                    ViewBag.Deleted = false;
                    if (TempData.ContainsKey("Added"))
                    {
                        ViewBag.Added = TempData["Added"];
                    }
                    if (TempData.ContainsKey("Updated"))
                    {
                        ViewBag.Updated = TempData["Updated"];
                    }
                    if (TempData.ContainsKey("Deleted"))
                    {
                        ViewBag.Deleted = TempData["Deleted"];
                    }
                    TempData.Clear();
                    var finalList = x.ToPagedList(page.Value, recordsPerPage);
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

        // GET: GradeMaster/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                if (Session["UserID"] != null)
                {

                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Grade_Master_174778 grade_Master_174778 = db.Grade_Master_174778.Find(id);
                    if (grade_Master_174778 == null)
                    {
                        return HttpNotFound();
                    }
                    return View(grade_Master_174778);
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

        // GET: GradeMaster/Create
        public ActionResult Create()
        {
            try
            {
                if (Session["UserID"] != null)
                {

                    string usertype = Session["UserType"].ToString();
                    if (usertype != "R")
                    {
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
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        // POST: GradeMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Grade_Code,Description,Min_Salary,Max_Salary")] Grade_Master_174778 grade_Master_174778)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Grade_Master_174778.Add(grade_Master_174778);
                    db.SaveChanges();
                    
                    TempData["Added"] = true;
                    return RedirectToAction("Index");
                }

                return View(grade_Master_174778);

            }

            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        // GET: GradeMaster/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    string usertype = Session["UserType"].ToString();
                    if (usertype != "R")
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        Grade_Master_174778 grade_Master_174778 = db.Grade_Master_174778.Find(id);
                        if (grade_Master_174778 == null)
                        {
                            return HttpNotFound();
                        }
                        return View(grade_Master_174778);

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
            catch (Exception Ex)
            {

                throw Ex;
            }              
        
        }

        // POST: GradeMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Grade_Code,Description,Min_Salary,Max_Salary")] Grade_Master_174778 grade_Master_174778)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(grade_Master_174778).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Updated"] = true;
                    return RedirectToAction("Index");
                }
                return View(grade_Master_174778);

            }
            catch (Exception Ex)
            {

                throw Ex;
            }
              

        }

        // GET: GradeMaster/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    string usertype = Session["UserType"].ToString();
                    if (usertype != "R")
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        Grade_Master_174778 grade_Master_174778 = db.Grade_Master_174778.Find(id);
                        if (grade_Master_174778 == null)
                        {
                            return HttpNotFound();
                        }
                        return View(grade_Master_174778);
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
            catch (Exception Ex)
            {

                throw Ex;
            }          

        }

        // POST: GradeMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

                Grade_Master_174778 grade_Master_174778 = db.Grade_Master_174778.Find(id);
                db.Grade_Master_174778.Remove(grade_Master_174778);
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
                else
                {

                }
                return RedirectToAction("Login");
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
         
        }


        public ActionResult ExportData()
        {
            var exportData = db.Grade_Master_174778.ToList();
            return View(exportData);
        }
        public ActionResult PrintExportData()
        {
            var print = new ActionAsPdf("ExportData");
            return print;
        }
    }
}
