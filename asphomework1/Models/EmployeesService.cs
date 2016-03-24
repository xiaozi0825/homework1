using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace asphomework1.Models
{
    public class EmployeesService
    {
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }

        public List<Employees> GetEmployeesById()
        {
            DataTable result = new DataTable();
            string sql = @"SELECT LastName+FirstName as LastName FROM HR.Employees";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = new SqlCommand(sql, conn);
                
                sqlAdapter.Fill(result);
                conn.Close();

            }
            return this.MapOrderDataToList(result);
        }

        private List<Employees> MapOrderDataToList(DataTable orderData)
        {
            List<Employees> result = new List<Employees>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Employees()
                {
                    LastName = row["LastName"].ToString()
                });
            }
            return result;
        }
    }
}