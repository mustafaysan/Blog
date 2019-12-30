using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Blog.Data.TemporaryDeveloperTool
{
    public class TemporaryDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=DPP0048\\SQLEXPRESS;Initial Catalog=blog_db;User ID=sa;Password=Mustafa2..;MultipleActiveResultSets=True";
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Blog.Data"));
            return new AppDbContext(builder.Options);
        }
    }
}
