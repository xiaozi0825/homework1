﻿using System;
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
        public ActionResult Index(Models.Orders selectitem)
        {
            EmployeesService EmployeesService = new EmployeesService();
            List<Employees> result = EmployeesService.GetEmployeesName();

            List<SelectListItem> EmployeesData = new List<SelectListItem>();
            foreach (var item in result)
            {
                EmployeesData.Add(new SelectListItem()
                {
                    Text = item.LastName,
                    Value = item.EmployeeID.ToString()
                });
                ViewData["EmpData"] = EmployeesData;
            }

            ShippersService ShippersService = new ShippersService();
            List<Shippers> result1 = ShippersService.GetShippersName();

            List<SelectListItem> ShippersData = new List<SelectListItem>();
            foreach (var item in result1)
            {
                ShippersData.Add(new SelectListItem()
                {
                    Text = item.CompanyName,
                    Value = item.ShipperID.ToString()
                });
                ViewData["ShippersData"] = ShippersData;
            }

            OrdersService OrdersService = new OrdersService();
            ViewBag.SelectData = OrdersService.SelectOrder(selectitem);


            return View();

        }
    }
}
