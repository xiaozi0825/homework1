using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace asphomework1.Models
{
    public class ProductsService
    {
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }

        public List<Products> GetProductName()
        {
            DataTable result = new DataTable();
            string sql = @"SELECT ProductID,ProductName FROM Production.Products ORDER BY ProductName";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = new SqlCommand(sql, conn);
                sqlAdapter.Fill(result);
                conn.Close();

            }
            return this.MapProductIDList(result);
        }

        private List<Products> MapProductIDList(DataTable orderData)
        {
            List<Products> result = new List<Products>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Products()
                {
                    ProductID = (int)row["ProductID"],
                    ProductName = row["ProductName"].ToString()
                });
            }
            return result;
        }

        public Products GetProductUnitPrice(string ProductID)
        {
            DataTable result = new DataTable();
            string sql = @"SELECT UnitPrice FROM Production.Products WHERE ProductID = @ProductID";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                sqlAdapter.Fill(result);
                conn.Close();
            }
            return this.MapUnitPriceList(result).FirstOrDefault();
        }

        private List<Products> MapUnitPriceList(DataTable orderData)
        {
            List<Products> result = new List<Products>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Products()
                {
                    UnitPrice = (decimal)row["UnitPrice"]
                });
            }
            return result;
        }
    }
}