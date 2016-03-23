﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asphomework1.Models
{
    public class Employees
    {
        /// <summary>
        /// 員工編號
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 稱呼開頭
        /// </summary>
        public string TitleOfCourtesy { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 聘請時間
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 地區
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 郵政編號
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 國籍
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 部門編號
        /// </summary>
        public int ManagerID { get; set; }

    }
}