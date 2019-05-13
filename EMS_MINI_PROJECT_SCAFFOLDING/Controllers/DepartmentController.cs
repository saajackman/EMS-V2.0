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
    public class DepartmentController : Controller
    {

      
        private EMSContext db = new EMSContext();

        public Department_174778 Department_174778
        {
            get => default(Department_174778);
            set
            {
            }
        }

        // GET: Department
        public ActionResult Index(string pSortOn, string sortOn, string keyword, string orderBy, int? page)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    ViewBag.UserType = Session["UserType"];

                    //sorting
                    ViewBag.OrderBy = orderBy;
                    ViewBag.Keyword = keyword;
                    ViewBag.SortOn = sortOn;

                    var x = db.Department_174778.AsQueryable();

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
                        case "Dept_ID":
                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Dept_ID);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Dept_ID);
                            }
                            break;


                        case "Dept_Name":

                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Dept_Name);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Dept_Name);
                            }
                            break;

                        default:
                            x = x.OrderBy(u => u.Dept_ID);
                            break;
                    }


                    if (!string.IsNullOrWhiteSpace(keyword))
                    {

                        x = x.Where(f => f.Dept_ID.ToString().StartsWith(keyword) || f.Dept_Name.StartsWith(keyword));
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
            catch(Exception Ex)
            {
                throw Ex;
            }
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            try
            {


                if (Session["UserID"] != null)
                {
                    string usertype = Session["UserType"].ToString();
                    if (usertype != "R")  //User is Supervisor
                    {

                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        Department_174778 department_174778 = db.Department_174778.Find(id);
                        if (department_174778 == null)
                        {
                            return HttpNotFound();
                        }


                        return View(department_174778);
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

        // GET: Department/Create
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

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Dept_ID,Dept_Name")] Department_174778 department_174778)
        {
            try
            {

            
            if (ModelState.IsValid)
            {
                db.Department_174778.Add(department_174778);
                db.SaveChanges();
                TempData.Clear();
                TempData["Added"] = "Hello Added Successfully";
                //return View("SuccessMsg");
                return RedirectToAction("Index");
              //  TempData["message"] = "Department Added Successfully";             
            }
            return View(department_174778);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
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
                    Department_174778 department_174778 = db.Department_174778.Find(id);
                    if (department_174778 == null)
                    {
                        return HttpNotFound();
                    }
                    return View(department_174778);
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

        // POST: Department_174778/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Dept_ID,Dept_Name")] Department_174778 department_174778)
        {
            try
            {

            
            if (ModelState.IsValid)
            {
                db.Entry(department_174778).State = EntityState.Modified;
                db.SaveChanges();
                TempData.Clear();
                TempData["Updated"] = "Hello Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(department_174778);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }


        public ActionResult Delete(int id)
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
                    Department_174778 department_174778 = db.Department_174778.Find(id);
                    if (department_174778 == null)
                    {
                        return HttpNotFound();
                    }
                    return View(department_174778);
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

        // POST: Department_174778/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department_174778 department_174778 = db.Department_174778.Find(id);
            db.Department_174778.Remove(department_174778);
            db.SaveChanges();
            TempData.Clear();
            TempData["Deleted"] = "Hello Deleted Successfully";
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
           
            return RedirectToAction("Login", "Login");
            }
            catch (Exception Ex) 
            {

                throw Ex;
            }
        }

        public ActionResult ExportData()
        {
            var exportData = db.Department_174778.ToList();
            return View(exportData);
        }
        public ActionResult PrintExportData()
        {
            var print = new ActionAsPdf("ExportData");
            return print;
        }
    }
}
