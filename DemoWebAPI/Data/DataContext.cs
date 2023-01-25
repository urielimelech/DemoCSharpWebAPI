global using DemoWebApi.Data.Objects;
using DemoWebAPI.Data.Objects;

namespace DemoWebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<HashingSalt> HashingSalts { get; set; }
    }
}
