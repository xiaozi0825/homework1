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
    public class OrderUpdateController : Controller
    {
        // GET: OrderUpdate
        [HttpGet]
        public ActionResult UpdateIndex(string OrderID)
        {
            OrdersService OrderService = new OrdersService();
            ViewBag.select = OrderService.SelectOrderByID(OrderID);

            return View();
        }
    }
}