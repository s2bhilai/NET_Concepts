using ConsoleApp_WBC_StorageApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_WBC_StorageApp.Data
{
    public class StorageAppDbContext: DbContext
    {
        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<Organization> Organizations => Set<Organization>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("StorageAppDb");
        }
    }
}
