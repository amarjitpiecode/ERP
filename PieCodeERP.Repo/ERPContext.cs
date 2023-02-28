using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PiecodeERP.Data;
using PieCodeErp.Models;

namespace PieCodeERP.Repo
{
    public class ERPContext: DbContext
    {
        public ERPContext(DbContextOptions<ERPContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //var connectionString = configuration.GetConnectionString("myconn");
            //optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<BranchMaster> BranchMasters { get; set; }
        public DbSet<CompanyMaster> CompanyMasters { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }

    }
}
