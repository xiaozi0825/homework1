namespace asphomework1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sales.OrderValues")]
    public partial class OrderValues
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        public int? CustomerID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ShipperID { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime OrderDate { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int? Qty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? val { get; set; }
    }
}
