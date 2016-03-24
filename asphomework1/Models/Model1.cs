namespace asphomework1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Nums> Nums { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Shippers> Shippers { get; set; }
        public virtual DbSet<Scores> Scores { get; set; }
        public virtual DbSet<Tests> Tests { get; set; }
        public virtual DbSet<EmpOrders> EmpOrders { get; set; }
        public virtual DbSet<OrderValues> OrderValues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>()
                .HasMany(e => e.Employees1)
                .WithOptional(e => e.Employees2)
                .HasForeignKey(e => e.MnangerID);

            modelBuilder.Entity<Employees>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Employees)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Categories)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Suppliers>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Suppliers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetails>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderDetails>()
                .Property(e => e.Discount)
                .HasPrecision(4, 3);

            modelBuilder.Entity<Orders>()
                .Property(e => e.Freight)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Orders)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shippers>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Shippers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Scores>()
                .Property(e => e.TestID)
                .IsUnicode(false);

            modelBuilder.Entity<Scores>()
                .Property(e => e.StudentID)
                .IsUnicode(false);

            modelBuilder.Entity<Tests>()
                .Property(e => e.TestID)
                .IsUnicode(false);

            modelBuilder.Entity<Tests>()
                .HasMany(e => e.Scores)
                .WithRequired(e => e.Tests)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmpOrders>()
                .Property(e => e.val)
                .HasPrecision(12, 2);

            modelBuilder.Entity<OrderValues>()
                .Property(e => e.val)
                .HasPrecision(12, 2);
        }
    }
}
