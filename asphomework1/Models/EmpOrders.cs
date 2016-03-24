namespace asphomework1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sales.EmpOrders")]
    public partial class EmpOrders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }

        public DateTime? ordermonth { get; set; }

        public int? Qty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? val { get; set; }

        public int? numorders { get; set; }
    }
}
