using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace asphomework1.Models
{
    public class OrdersService
    {
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }

        public List<Orders> SelectOrder(Models.Orders selectitem)
        {
            DataTable selectresult = new DataTable();
            string sql = @"SELECT OrderID,CompanyName,OrderDate,ShippedDate FROM Sales.Orders AS a JOIN Sales.Customers AS b ON a.CustomerID=b.CustomerID";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                //command.Parameters.Add(new SqlParameter("@CustomerID", selectitem.CustomerID == null ? selectitem.CustomerID = null : selectitem.CustomerID));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = new SqlCommand(sql, conn);
                //cmd.Parameters.Add(new SqlParameter("@CustomerID", arg.CustomerID == null ? string.Empty : arg.CustomerID));
                sqlAdapter.Fill(selectresult);
                conn.Close();
            }
            return this.MapSelectOrder(selectresult);
        }

        private List<Orders> MapSelectOrder(DataTable SelectData)
        {
            List<Orders> selectresult = new List<Orders>();


            foreach (DataRow row in SelectData.Rows)
            {
                selectresult.Add(new Orders()
                {
                    OrderID = (int)row["OrderID"],
                    //CustomerID = (int)row["CustomerID"],
                    CompanyName = row["CompanyName"].ToString(),
                    OrderDate = row["OrderDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["OrderDate"],
                    ShippedDate = row["ShippedDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["ShippedDate"]
                });
            }
            return selectresult;
        }
    }
}