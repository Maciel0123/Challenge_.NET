using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MottuData
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseOracle("User Id=RM556480;Password=101004;Data Source=oracle.fiap.com.br:1521/ORCL;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
