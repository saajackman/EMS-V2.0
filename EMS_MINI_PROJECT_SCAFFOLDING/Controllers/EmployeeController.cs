using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EMS_MINI_PROJECT_SCAFFOLDING.Models;
using EMS_MINI_PROJECT_SCAFFOLDING.Utilities;
using NLog;
using NLog.Fluent;
using PagedList;
using Rotativa;
using Log = EMS_MINI_PROJECT_SCAFFOLDING.Utilities.Log;

namespace EMS_MINI_PROJECT_SCAFFOLDING.Controllers
{
    public class EmployeeController : Controller
    {
        private EMSContext db = new EMSContext();
        Logger logger = LogManager.GetCurrentClassLogger();

        public Employee_174778 Employee_174778
        {
            get => default(Employee_174778);
            set
            {
            }
        }

        public Department_174778 Department_174778
        {
            get => default(Department_174778);
            set
            {
            }
        }

        public Grade_Master_174778 Grade_Master_174778
        {
            get => default(Grade_Master_174778);
            set
            {
            }
        }

        // GET: Employee
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

                    var x = db.Employee_174778.AsQueryable();

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
                        case "Emp_ID":
                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Emp_ID);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Emp_ID);
                            }
                            break;


                        case "Emp_First_Name":

                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Emp_First_Name);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Emp_First_Name);
                            }
                            break;

                        case "Emp_Last_Name":

                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Emp_Last_Name);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Emp_Last_Name);
                            }
                            break;

                        case "Emp_Date_of_Birth":

                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Emp_Date_of_Birth);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Emp_Date_of_Birth);
                            }
                            break;

                        case "Emp_Date_of_Joining":

                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Emp_Date_of_Joining);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Emp_Date_of_Joining);
                            }
                            break;
                        case "Emp_Dept_ID":

                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Emp_Dept_ID);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Emp_Dept_ID);
                            }
                            break;
                        case "Emp_Grade":

                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Emp_Grade);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Emp_Grade);
                            }
                            break;
                        case "Emp_Designation":

                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Emp_Designation);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Emp_Designation);
                            }
                            break;
                        case "Emp_Basic":

                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Emp_Basic);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Emp_Basic);
                            }
                            break;
                        case "Emp_Gender":

                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Emp_Gender);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Emp_Gender);
                            }
                            break;
                        case "Emp_Marital_Status":

                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Emp_Marital_Status);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Emp_Marital_Status);
                            }
                            break;
                        case "Emp_Home_Address":

                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Emp_Home_Address);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Emp_Home_Address);
                            }
                            break;
                        case "Emp_Contact_Num":

                            if (orderBy.Equals("desc"))
                            {
                                x = x.OrderByDescending(u => u.Emp_Contact_Num);
                            }
                            else
                            {
                                x = x.OrderBy(u => u.Emp_Contact_Num);
                            }
                            break;
                        default:
                            x = x.OrderBy(u => u.Emp_ID);
                            break;
                    }



                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        x = x.Where(f => f.Emp_ID.ToString().StartsWith(keyword) || f.Emp_Last_Name.StartsWith(keyword) || f.Emp_Last_Name.StartsWith(keyword) ||
                       f.Emp_Dept_ID.ToString().StartsWith(keyword) || f.Emp_Grade.ToString().StartsWith(keyword) || f.Emp_Basic.ToString().StartsWith(keyword) ||
                       f.Emp_Designation.StartsWith(keyword) || f.Emp_Marital_Status.StartsWith(keyword) || f.Emp_Home_Address.StartsWith(keyword) || f.Emp_Contact_Num.StartsWith(keyword));
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

    // GET: Employee/Details/5
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
                Employee_174778 employee_174778 = db.Employee_174778.Find(id);
                if (employee_174778 == null)
                {
                    return HttpNotFound();
                }
                return View(employee_174778);
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

        // GET: Employee/Create
        public ActionResult Create()
        {
            try
            {

            
            if (Session["UserID"] != null)
            {
                string usertype = Session["UserType"].ToString();
                if (usertype != "R")
                {
                    ViewBag.Emp_Dept_ID = new SelectList(db.Department_174778, "Dept_ID", "Dept_Name");
                    ViewBag.Emp_Grade = new SelectList(db.Grade_Master_174778, "Grade_Code", "Description");
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

        // POST: Employee/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Emp_ID,Emp_First_Name,Emp_Last_Name,Emp_Date_of_Birth,Emp_Date_of_Joining,Emp_Dept_ID,Emp_Grade,Emp_Designation,Emp_Basic,Emp_Gender,Emp_Marital_Status,Emp_Home_Address,Emp_Contact_Num")] Employee_174778 employee_174778)
        {
            try
            {            
                if (ModelState.IsValid)
                {
                    db.Employee_174778.Add(employee_174778);
                    db.SaveChanges();
                    logger.Error();
                    TempData.Clear();
                    TempData["Added"] = "Hello Added Successfully";
                    return RedirectToAction("Index");                   
                }
                ViewBag.Emp_Dept_ID = new SelectList(db.Department_174778, "Dept_ID", "Dept_Name", employee_174778.Emp_Dept_ID);
                ViewBag.Emp_Grade = new SelectList(db.Grade_Master_174778, "Grade_Code", "Description", employee_174778.Emp_Grade);
                return View(employee_174778);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        // GET: Employee/Edit/5
        // GET: Employee_174778/Edit/5
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
                    Employee_174778 employee_174778 = db.Employee_174778.Find(id);
                    if (employee_174778 == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.Emp_Dept_ID = new SelectList(db.Department_174778, "Dept_ID", "Dept_Name", employee_174778.Emp_Dept_ID);
                    ViewBag.Emp_Grade = new SelectList(db.Grade_Master_174778, "Grade_Code", "Description", employee_174778.Emp_Grade);
                    return View(employee_174778);
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

        // POST: Employee_174778/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Emp_ID,Emp_First_Name,Emp_Last_Name,Emp_Date_of_Birth,Emp_Date_of_Joining,Emp_Dept_ID,Emp_Grade,Emp_Designation,Emp_Basic,Emp_Gender,Emp_Marital_Status,Emp_Home_Address,Emp_Contact_Num")] Employee_174778 employee_174778)
        {
            try
            { 
            if (ModelState.IsValid)
            {
                db.Entry(employee_174778).State = EntityState.Modified;
                db.SaveChanges();
                TempData.Clear();
                TempData["Updated"] = "Hello Updated Successfully";
                return RedirectToAction("Index");
            }
            ViewBag.Emp_Dept_ID = new SelectList(db.Department_174778, "Dept_ID", "Dept_Name", employee_174778.Emp_Dept_ID);
            ViewBag.Emp_Grade = new SelectList(db.Grade_Master_174778, "Grade_Code", "Description", employee_174778.Emp_Grade);
            return View(employee_174778);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        // GET: Employee_174778/Delete/5
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
                    Employee_174778 employee_174778 = db.Employee_174778.Find(id);
                    if (employee_174778 == null)
                    {
                        return HttpNotFound();
                    }
                    return View(employee_174778);
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

        // POST: Employee_174778/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee_174778 employee_174778 = db.Employee_174778.Find(id);
            db.Employee_174778.Remove(employee_174778);
            db.SaveChanges();
            TempData.Clear();
            TempData["Deleted"] = "Deleted";
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
            var exportData = db.Employee_174778.ToList();
            return View(exportData);
        }
        public ActionResult PrintExportData()
        {
            var print = new ActionAsPdf("ExportData");
            return print;
        }
    }
}
