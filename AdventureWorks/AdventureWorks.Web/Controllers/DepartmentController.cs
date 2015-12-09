using AdventureWorks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdventureWorks.Web.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            IEnumerable<Department> departments;
            using (var context = new AdventureWorksContext())
            {
                departments = context.Departments.ToList();
            }
            return View(departments);
        }

        [HttpGet]
        public ActionResult Edit(short id)
        {
            Department department;
            using (var context = new AdventureWorksContext())
            {
                department = context.Departments.Find(id);
            }
            if (department == null)
                return HttpNotFound();

            return View(department);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(short id)
        {
            Department department;
            using (var context = new AdventureWorksContext())
            {
                department = context.Departments.Find(id);

                if (department == null)
                    return HttpNotFound();
                if (TryUpdateModel(department, new string[] { "Name", "GroupName" }))
                {
                    department.ModifiedDate = DateTime.Now;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(department);               
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}