using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace WebMyPham.Models
{
    class DataContext : DbContext
    {
        public DataContext() : base()
        {
            string databasename = "MyPhamContext";
            string datasource = "GOLAPTOP\\SQLEXPRESS";
            this.Database.Connection.ConnectionString = "Data Source=" + datasource + ";Initial Catalog=" + databasename + ";Trusted_Connection=Yes";
            Database.SetInitializer<DataContext>(new DropCreateDatabaseIfModelChanges<DataContext>());
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Caterogy> Caterogies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PayCheck> PayChecks { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Product_Caterogy> Product_Caterogies { get; set; }
        public DbSet<productImg> ProductImgs { get; set; }
    }
}