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
    public class OrderUpdateController : Controller
    {
        // GET: OrderUpdate
        [HttpGet]
        public ActionResult UpdateIndex(string OrderID)
        {
            OrdersService OrderService = new OrdersService();
            ViewBag.select = OrderService.SelectOrderByID(OrderID);
            foreach(var item in (List<asphomework1.Models.Orders>)ViewBag.select)
            {
                ViewBag.orderid = item.OrderID;
                ViewBag.customer = item.CompanyName;
                ViewBag.customerid = item.CustomerID.ToString();
                ViewBag.lastname = item.Lastname;
                ViewBag.employeeid = item.EmployeeID.ToString();
                ViewBag.orderdate = item.OrderDate;
                ViewBag.requireddate = item.RequiredDate;
                ViewBag.shippeddate = item.ShippedDate;
                ViewBag.shippername = item.ShipperName;
                ViewBag.shipperid = item.ShipperID.ToString();
                ViewBag.freight = item.Freight;
                ViewBag.shipname = item.ShipName;
                ViewBag.shipaddress = item.ShipAddress;
                ViewBag.shipcity = item.ShipCity;
                ViewBag.shipregion = item.ShipRegion;
                ViewBag.shippostalcode = item.ShipPostalCode;
                ViewBag.shipcountry = item.ShipCountry;
            }

            EmployeesService EmployeesService = new EmployeesService();
            List<Employees> result = EmployeesService.GetEmployeesName();

            List<SelectListItem> EmployeesData = new List<SelectListItem>();
            EmployeesData.Add(new SelectListItem()
            {
                Text = ViewBag.lastname,
                Value = ViewBag.employeeid
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
                Text = ViewBag.shippername,
                Value = ViewBag.shipperid
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
                Text = ViewBag.customer,
                Value = ViewBag.customerid
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

            ViewBag.orderdetail = OrderService.SelectOrderDetailByID(OrderID);

            List<SelectListItem> ProductsData = new List<SelectListItem>();
            List<List<SelectListItem>> getProductList = new List<List<SelectListItem>>();
            ProductsService ProductsService = new ProductsService();
            List<Products> result3 = ProductsService.GetProductName();
            for (int i = 0; i < ViewBag.orderdetail.Count; i++)
            {
                foreach (var item in result3)
                {
                    ProductsData.Add(new SelectListItem()
                    {
                        Text = item.ProductName,
                        Value = item.ProductID.ToString(),
                        Selected = item.ProductID.Equals(ViewBag.orderdetail[i].ProductID)
                    });
                    ViewData["ProductsData"] = ProductsData;
                }
                getProductList.Add(new List<SelectListItem>(ProductsData));
                ProductsData.Clear();
            }

            ViewBag.ProductData = getProductList;

            List<Products> result4 = ProductsService.GetProductUnitPrice();
            List<SelectListItem> PriceData = new List<SelectListItem>();

            ViewBag.PriceData = PriceData;
            foreach (var item in result4)
            {
                PriceData.Add(new SelectListItem()
                {
                    Value = item.UnitPrice.ToString()
                });
                ViewBag.PriceData = PriceData;
            }

            return View();


            
        }


        [HttpPost()]
        public ActionResult UpdateIndex(Models.InsertSearch update)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    OrdersService OrdersService = new OrdersService();
                    OrdersService.DeleteOrderDetailByID(update.OrderID);
                    OrdersService.updateorder(update);
                    return RedirectToAction("../Employees/Index");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return View(update);
        }
    }
}