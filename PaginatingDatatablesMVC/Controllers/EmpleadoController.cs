using System.Web.Mvc;
using PaginatingDatatablesMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System;

namespace PaginatingDatatablesMVC.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetList()
        {
            // Server side parameter
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<Empleado> empList = new List<Empleado>();
            using(DBModelConnection db = new DBModelConnection())
            {
                empList = db.Empleados.ToList();
                int totalRows = empList.Count();

                //filter
                if (!string.IsNullOrEmpty(searchValue))
                {
                    empList = empList.Where(e => e.Nombre.ToLower().Contains(searchValue.ToLower()) || e.Apellido.ToLower().Contains(searchValue.ToLower()) || e.Documento.ToLower().Contains(searchValue.ToLower())).ToList();
                }
                int totalRowsFiltered = empList.Count();

                //sorting
                empList = empList.OrderBy(sortColumnName + " " + sortDirection).ToList();

                //paging
                empList = empList.Skip(start).Take(length).ToList();

                return Json(new { data = empList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalRowsFiltered }, JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}