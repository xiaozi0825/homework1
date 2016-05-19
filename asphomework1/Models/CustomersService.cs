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
        /// getCustomersName
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

        

        public string InsertOrder(Models.InsertSearch order)
        {
            string sql = @"INSERT INTO Sales.Orders
                           (CustomerID,EmployeeID,OrderDate,RequiredDate,ShippedDate,ShipperID,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry) 
                           VALUES (@CostomerID,@EmployeeID,@OrderDate,@RequiredDate,@ShippedDate,@ShipperID,@Freight,@ShipName,@ShipAddress,@ShipCity,@ShipRegion,@ShipPostalCode,@ShipCountry)
                           Select SCOPE_IDENTITY()";
            string sql2 = @"INSERT INTO Sales.OrderDetails
                           (OrderID,ProductID,UnitPrice,Qty) 
                           VALUES (@OrderID,@ProductID,@UnitPrice,@Qty)";
            string OrderID;
            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                SqlCommand command2 = new SqlCommand(sql2, conn);
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

                OrderID = command.ExecuteScalar().ToString();

                for(int i = 0; i < order.OrderDetails.Count; i++)
                {
                    command2 = new SqlCommand(sql2, conn);
                    command2.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                    command2.Parameters.Add(new SqlParameter("@ProductID", order.OrderDetails[i].ProductID));
                    command2.Parameters.Add(new SqlParameter("@UnitPrice", order.OrderDetails[i].UnitPrice));
                    command2.Parameters.Add(new SqlParameter("@Qty", order.OrderDetails[i].Qty));
                    command2.ExecuteNonQuery();
                }

               
                conn.Close();
            }
            return OrderID;
        }
    }
}