using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TaskManager.DataAccess.Data
{
    public class AppDbContextFactory:IDesignTimeDbContextFactory<AppDbContext>

    {
        public AppDbContext CreateDbContext(string[] args) {
        var optionsBuldier =new DbContextOptionsBuilder<AppDbContext>();
            optionsBuldier.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TaskManagerDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new AppDbContext(optionsBuldier.Options);
        }
    }
}
