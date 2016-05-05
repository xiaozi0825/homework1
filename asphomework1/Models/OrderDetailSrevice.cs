using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace asphomework1.Models
{
    public class OrderDetailSrevice
    {
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }

        public int InsertOrderDetails(Models.InsertSearch order)
        {
            string sql = @"INSERT INTO Sales.OrderDetails
                           (OrderID,ProductID,UnitPrice,Qty) 
                           VALUES (@OrderID,@ProductID,@UnitPrice,@Qty)
                           Select SCOPE_IDENTITY()";
            int orderid;
            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.Add(new SqlParameter("@OrderID", order.OrderID));
                command.Parameters.Add(new SqlParameter("@ProductID", order.ProductID));
                command.Parameters.Add(new SqlParameter("@UnitPrice", order.UnitPrice));
                command.Parameters.Add(new SqlParameter("@Qty", order.Qty));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                orderid = (int)command.ExecuteScalar();
                conn.Close();
            }
            return orderid;
        }
    }
}