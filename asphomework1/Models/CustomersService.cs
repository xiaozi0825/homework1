using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace asphomework1.Models
{
    public class CustomersService
    {
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Customers> GetCustomerName()
        {
            DataTable result = new DataTable();
            string sql = @"SELECT CustomerID,CompanyName FROM Sales.Customers ORDER BY CompanyName";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = new SqlCommand(sql, conn);
                sqlAdapter.Fill(result);
                conn.Close();

            }
            return this.MapCustomerIDList(result);
        }

        private List<Customers> MapCustomerIDList(DataTable orderData)
        {
            List<Customers> result = new List<Customers>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Customers()
                {
                    CustomerID = (int)row["CustomerID"],
                    CompanyName = row["CompanyName"].ToString()
                });
            }
            return result;
        }

        public int InsertOrder(Models.InsertSearch order)
        {
            string sql = @"INSERT INTO Sales.Orders
                           (CustomerID,EmployeeID,OrderDate,RequiredDate,ShippedDate,ShipperID,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry) 
                           VALUES (@CostomerID,@EmployeeID,@OrderDate,@RequiredDate,@ShippedDate,@ShipperID,@Freight,@ShipName,@ShipAddress,@ShipCity,@ShipRegion,@ShipPostalCode,@ShipCountry)
                           Select SCOPE_IDENTITY()";
            int orderid;
            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.Add(new SqlParameter("@CostomerID", order.CostomerID));
                command.Parameters.Add(new SqlParameter("@EmployeeID", order.EmployeeID));
                command.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate ));
                command.Parameters.Add(new SqlParameter("@RequiredDate", order.RequiredDate));
                command.Parameters.Add(new SqlParameter("@ShippedDate", order.ShippedDate));
                command.Parameters.Add(new SqlParameter("@ShipperID", order.ShipperID));
                command.Parameters.Add(new SqlParameter("@Freight", order.Freight));
                command.Parameters.Add(new SqlParameter("@ShipName", order.ShipName));
                command.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddress));
                command.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
                command.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion));
                command.Parameters.Add(new SqlParameter("@ShipPostalCode", order.ShipPostalCode));
                command.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                orderid = (int)command.ExecuteScalar();
                conn.Close();
            }
            return orderid;
        }
    }
}