using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace asphomework1.Models
{
    public class EmployeesService : Controller
    {
        // GET: EmployeesService
        public ActionResult Index()
        {
            return View();
        }
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();
        }

        public DataTable GetEmployeesById()
        {
            DataTable result = new DataTable();
            string sql = @"SELECT LastName+FirstName as Name FROM HR.Employees";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.Fill(result);
                conn.Close();
                
            }
            return result;
        }


    }
}