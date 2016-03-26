using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace asphomework1.Models
{
    public class ShippersService
    {
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }

        public List<Shippers> GetShippersName()
        {
            DataTable result = new DataTable();
            string sql = @"SELECT ShipperID,CompanyName FROM Sales.Shippers";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = new SqlCommand(sql, conn);

                sqlAdapter.Fill(result);
                conn.Close();

            }
            return this.MapSipperNameToList(result);
        }

        private List<Shippers> MapSipperNameToList(DataTable orderData)
        {
            List<Shippers> result = new List<Shippers>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Shippers()
                {
                    ShipperID = (int)row["ShipperID"],
                    CompanyName = row["CompanyName"].ToString()
                });
            }
            return result;
        }
    }
}