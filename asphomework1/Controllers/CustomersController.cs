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
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult InsertIndex()
        {
            EmployeesService EmployeesService = new EmployeesService();
            List<Employees> result = EmployeesService.GetEmployeesName();

            List<SelectListItem> EmployeesData = new List<SelectListItem>();
            EmployeesData.Add(new SelectListItem()
            {
                Text = "",
                Value = null
            });
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
            ShippersData.Add(new SelectListItem()
            {
                Text = "",
                Value = null
            });
            foreach (var item in result1)
            {
                ShippersData.Add(new SelectListItem()
                {
                    Text = item.CompanyName,
                    Value = item.ShipperID.ToString()
                });
                ViewData["ShippersData"] = ShippersData;
            }

            CustomersService CustomersService = new CustomersService();
            List<Customers> result2 = CustomersService.GetCustomerName();

            List<SelectListItem> CustomersData = new List<SelectListItem>();
            CustomersData.Add(new SelectListItem()
            {
                Text = "",
                Value = null
            });
            foreach (var item in result2)
            {
                CustomersData.Add(new SelectListItem()
                {
                    Text = item.CompanyName,
                    Value = item.CustomerID.ToString()
                });
                ViewData["CustomersData"] = CustomersData;
            }

            ProductsService ProductsService = new ProductsService();
            List<Products> result3 = ProductsService.GetProductName();

            List<SelectListItem> ProductsData = new List<SelectListItem>();
            ProductsData.Add(new SelectListItem()
            {
                Text = "",
                Value = null
            });
            foreach(var item in result3)
            {
                ProductsData.Add(new SelectListItem()
                {
                    Text = item.ProductName,
                    Value = item.ProductID.ToString()
                });
                ViewData["ProductsData"] = ProductsData;
            }
            return View(new InsertSearch());
        }

        [HttpPost()]
        public JsonResult GetProdutsUnitPrice(string ProductID)
        {
            try
            {

                ProductsService ProductsService = new ProductsService();
                ProductsService.GetProductUnitPrice(ProductID);

                return this.Json(ProductsService.GetProductUnitPrice(ProductID));
            }
            catch (Exception)
            {
                return this.Json(false);
            }
        }

        [HttpPost()]
        public ActionResult InsertOrder(Models.InsertSearch order)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    CustomersService CustomersService = new CustomersService();
                    CustomersService.InsertOrder(order);
                    return RedirectToAction("InsertIndex");
                }
                catch (Exception ex)
                {
                    return this.Json(ex);
                }

            }
            return View(order);
            //return View();
        }
    }
}