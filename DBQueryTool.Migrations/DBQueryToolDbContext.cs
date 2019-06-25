using DBQueryTool.DataAccess.Models;
using System.Data.Entity;

namespace DBQueryTool.Migrations
{
    public class DBQueryToolDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<TemplateType> TemplateTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Template>()
                .Property(f => f.CreatedAt)
                .HasColumnType("datetime2")
                .HasPrecision(0);

            modelBuilder.Entity<Report>()
                .Property(f => f.CreatedAt)
                .HasColumnType("datetime2")
                .HasPrecision(0);
        }
    }
}
