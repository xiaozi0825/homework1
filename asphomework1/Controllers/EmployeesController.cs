using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using asphomework1.Models;

namespace asphomework1.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            return View();

        }

        public List<Employees> GetEmployeesById()
        {
            EmployeesService EmployeesService = new EmployeesService();

            List<Employees> result = EmployeesService.GetEmployeesById();
            foreach(var item in result)
            {
                ViewBag.name = item;
            }

            return ViewBag.name;
        }

    }
}