using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asphomework1.Models
{
    public class InsertSearch
    {
        public InsertSearch()
        {
            var ods = new List<Models.OrderDetails>();
            ods.Add(new OrderDetails() { ProductID = 58 });
            this.OrderDetails = ods;

        }

        public List<OrderDetails> OrderDetails { get; set; }
        public string CostomerID { get; set; }

        public string EmployeeID { get; set; }

        public string OrderDate { get; set; }

        public string ShippedDate { get; set; }

        public string RequiredDate { get; set; }

        public string ShipperID { get; set; }

        public string Freight { get; set; }

        public string ShipCountry { get; set; }

        public string ShipCity { get; set; }

        public string ShipRegion { get; set; }

        public string ShipPostalCode { get; set; }

        public string ShipAddress { get; set; }

        public string ShipName { get; set; }

        public string ProductID { get; set; }

        public string UnitPrice { get; set; }

        public string Qty { get; set; }

    }
}