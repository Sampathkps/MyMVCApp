using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BusinessEntities;

namespace DataAccesLayer
{
    public class SalesERPDAL: DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public SalesERPDAL()
            : base("DbConnectString")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("TblEmployee");
            base.OnModelCreating(modelBuilder);
        }
    }
}