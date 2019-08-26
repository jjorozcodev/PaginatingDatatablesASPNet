using System.Web.Mvc;
using PaginatingDatatablesMVC.Models;
using System.Collections.Generic;
using System.Linq;

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
            List<Empleado> empList = new List<Empleado>();
            using(DBModelConnection db = new DBModelConnection())
            {
                empList = db.Empleados.Take(10).ToList();
            }
            return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
        }
    }
}