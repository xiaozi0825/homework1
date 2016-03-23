using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asphomework1.Models
{
    public class Shippers
    {
        /// <summary>
        /// 送貨編號
        /// </summary>
        public int ShipperID { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string Phone { get; set; }
    }
}