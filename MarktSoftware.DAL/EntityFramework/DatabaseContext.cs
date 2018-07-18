using System.Data.Entity;
using MarktSoftware.DataAccessLayer.EntityFramework;
using MarktSoftware.Entity;

namespace MarktSoftware.DAL.EntityFramework
{
    public class DatabaseContext:DbContext
    {
        public DbSet<MarktUser> MarktUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}
