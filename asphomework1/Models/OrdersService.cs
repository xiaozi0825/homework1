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
        /// <summary>
        /// 尋找訂單
        /// </summary>
        /// <param name="selectitem"></param>
        /// <returns></returns>
        public List<Orders> SelectOrder(Models.SelectSearch selectitem)
        {
            DataTable selectresult = new DataTable();
            string sql = @"SELECT OrderID,b.CompanyName,CONVERT(varchar(10) ,OrderDate, 111 ) AS OrderDate,CONVERT(varchar(10) ,ShippedDate, 111 ) AS ShippedDate 
                           FROM Sales.Orders AS a JOIN Sales.Customers AS b ON a.CustomerID=b.CustomerID JOIN Sales.Shippers AS c ON a.ShipperID=c.ShipperID
                           WHERE (b.CompanyName LIKE '%'+@CompanyName+'%' OR @CompanyName = '') AND 
                           (a.OrderID LIKE '%'+@OrderID+'%' OR @OrderID = '') AND 
                            (a.EmployeeID LIKE @EmployeeID OR @EmployeeID = '') AND 
                            (a.ShipperID LIKE @ShipperID OR @ShipperID = '') AND 
                            (OrderDate >= @OrderDate AND OrderDate < DATEADD(DAY,1,@OrderDate) OR @OrderDate = '') AND 
                            (ShippedDate >= @ShippedDate AND ShippedDate < DATEADD(DAY,1,@ShippedDate) OR @ShippedDate = '') AND 
                            (RequiredDate >= @RequiredDate AND RequiredDate < DATEADD(DAY,1,@RequiredDate) OR @RequiredDate = '')";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.Add(new SqlParameter("@OrderID", selectitem.OrderID == null ? string.Empty : selectitem.OrderID));
                command.Parameters.Add(new SqlParameter("@CompanyName", selectitem.CompanyName == null ? string.Empty : selectitem.CompanyName));
                command.Parameters.Add(new SqlParameter("@EmployeeID", selectitem.EmployeeID == null ? string.Empty : selectitem.EmployeeID));
                command.Parameters.Add(new SqlParameter("@ShipperID", selectitem.ShipperID == null ? string.Empty : selectitem.ShipperID));
                command.Parameters.Add(new SqlParameter("@OrderDate", selectitem.OrderDate == null ? string.Empty : selectitem.OrderDate));
                command.Parameters.Add(new SqlParameter("@ShippedDate", selectitem.ShippedDate == null ? string.Empty : selectitem.ShippedDate));
                command.Parameters.Add(new SqlParameter("@RequiredDate", selectitem.RequiredDate == null ? string.Empty : selectitem.RequiredDate));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                //sqlAdapter.SelectCommand = new SqlCommand(sql, conn);
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
                    CompanyName = row["CompanyName"].ToString(),
                    OrderDate = row["OrderDate"].ToString(),
                    ShippedDate = row["ShippedDate"].ToString()
                });
            }
            return selectresult;
        }

        public void DeleteOrderByID(string OrderID)
        {
            try
            {
                string sql = "Delete FROM Sales.Orders Where OrderID=@OrderID";
                using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOrderDetailByID(string OrderID)
        {
            try
            {
                string sql = "DELETE FROM Sales.OrderDetails WHERE OrderID=@OrderID";
                using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Orders> SelectOrderByID(string OrderID)
        {
            DataTable selectresult1 = new DataTable();
            string sql = @"SELECT OrderID,b.CompanyName,b.CustomerID,
                            (d.LastName+'-'+d.FirstName) as Lastname,d.EmployeeID,
                            CONVERT(varchar(10) ,OrderDate, 23 ) AS OrderDate,
                            CONVERT(varchar(10) ,RequiredDate, 23 ) AS RequiredDate,
                            CONVERT(varchar(10) ,ShippedDate, 23 ) AS ShippedDate,
                            c.CompanyName as ShipperName,c.ShipperID,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry
                            FROM Sales.Orders AS a JOIN Sales.Customers AS b ON a.CustomerID=b.CustomerID JOIN Sales.Shippers AS c ON a.ShipperID=c.ShipperID 
                            JOIN HR.Employees AS d ON a.EmployeeID=d.EmployeeID 
                            WHERE a.OrderID=@OrderID";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.Add(new SqlParameter("@OrderID", OrderID == null ? string.Empty : OrderID));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                sqlAdapter.Fill(selectresult1);
                conn.Close();
            }
            
            return this.MapSelectOrderByID(selectresult1);
        }
        private List<Orders> MapSelectOrderByID(DataTable SelectData)
        {
            List<Orders> selectresult = new List<Orders>();


            foreach (DataRow row in SelectData.Rows)
            {
                selectresult.Add(new Orders()
                {
                    OrderID = (int)row["OrderID"],
                    CompanyName = row["CompanyName"].ToString(),
                    CustomerID = (int)row["CustomerID"],
                    Lastname = row["Lastname"].ToString(),
                    EmployeeID = (int)row["EmployeeID"],
                    OrderDate = row["OrderDate"].ToString(),
                    RequiredDate = row["RequiredDate"].ToString(),
                    ShippedDate = row["ShippedDate"].ToString(),
                    ShipperName = row["ShipperName"].ToString(),
                    ShipperID = (int)row["ShipperID"],
                    Freight = (decimal)row["Freight"],
                    ShipName = row["ShipName"].ToString(),
                    ShipAddress = row["ShipAddress"].ToString(),
                    ShipCity = row["ShipCity"].ToString(),
                    ShipRegion = row["ShipRegion"].ToString(),
                    ShipPostalCode = row["ShipPostalCode"].ToString(),
                    ShipCountry = row["ShipCountry"].ToString()
                });
            }
            return selectresult;
        }

        public string updateorder(InsertSearch update)
        {
            string sql = "update Sales.Orders set CustomerID=@CustomerID,EmployeeID=@EmployeeID,OrderDate=@OrderDate,RequiredDate=@RequiredDate,ShippedDate=@ShippedDate,ShipperID=@ShipperID, Freight=@Freight,ShipName=@ShipName,ShipAddress=@ShipAddress,ShipCity=@ShipCity,ShipRegion= @ShipRegion,ShipPostalCode= @ShipPostalCode,ShipCountry= @ShipCountry where OrderID =@OrderID";
            string sql2 = @"INSERT INTO Sales.OrderDetails
                           (OrderID,ProductID,UnitPrice,Qty) 
                           VALUES (@OrderID,@ProductID,@UnitPrice,@Qty)";
            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                SqlCommand command2 = new SqlCommand(sql2, conn);
                command.Parameters.Add(new SqlParameter("@OrderID", update.OrderID));
                command.Parameters.Add(new SqlParameter("@CustomerID", update.CustomerID));
                command.Parameters.Add(new SqlParameter("@EmployeeID", update.EmployeeID));
                command.Parameters.Add(new SqlParameter("@OrderDate", update.OrderDate));
                command.Parameters.Add(new SqlParameter("@RequiredDate", update.RequiredDate));
                command.Parameters.Add(new SqlParameter("@ShippedDate", update.ShippedDate));
                command.Parameters.Add(new SqlParameter("@ShipperID", update.ShipperID));
                command.Parameters.Add(new SqlParameter("@Freight", update.Freight));
                command.Parameters.Add(new SqlParameter("@ShipName", update.ShipName));
                command.Parameters.Add(new SqlParameter("@ShipAddress", update.ShipAddress));
                command.Parameters.Add(new SqlParameter("@ShipCity", update.ShipCity));
                command.Parameters.Add(new SqlParameter("@ShipRegion", update.ShipRegion));
                command.Parameters.Add(new SqlParameter("@ShipPostalCode", update.ShipPostalCode));
                command.Parameters.Add(new SqlParameter("@ShipCountry", update.ShipCountry));
                command.ExecuteNonQuery();
                for (int i = 0; i < update.OrderDetails.Count; i++)
                {
                    command2 = new SqlCommand(sql2, conn);
                    command2.Parameters.Add(new SqlParameter("@OrderID", update.OrderID));
                    command2.Parameters.Add(new SqlParameter("@ProductID", update.OrderDetails[i].ProductID));
                    command2.Parameters.Add(new SqlParameter("@UnitPrice", update.OrderDetails[i].UnitPrice));
                    command2.Parameters.Add(new SqlParameter("@Qty", update.OrderDetails[i].Qty));
                    command2.ExecuteNonQuery();
                }
                conn.Close();
            }
            return null;
        }

        public List<OrderDetails> SelectOrderDetailByID(string OrderID)
        {
            DataTable selectresult2 = new DataTable();
            string sql = @"select OrderID,ProductID,UnitPrice,Qty from Sales.OrderDetails where OrderID=@OrderID";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.Add(new SqlParameter("@OrderID", OrderID == null ? string.Empty : OrderID));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                sqlAdapter.Fill(selectresult2);
                conn.Close();
            }
            return this.MapSelectOrderDetailByID(selectresult2);
        }
        private List<OrderDetails> MapSelectOrderDetailByID(DataTable SelectData)
        {
            List<OrderDetails> selectresult2 = new List<OrderDetails>();


            foreach (DataRow row in SelectData.Rows)
            {
                selectresult2.Add(new OrderDetails()
                {
                    OrderID = (int)row["OrderID"],
                    ProductID = (int)row["ProductID"],
                    UnitPrice = (decimal)row["UnitPrice"],
                    Qty = (short)row["Qty"]
                });
            }
            return selectresult2;
        }

    }


}