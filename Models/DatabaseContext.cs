using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public DatabaseContext() {}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=Libraries.db");
            options.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Library>().ToTable("Libraries");
            builder.Entity<Library>().HasKey(li => li.id);
            builder.Entity<Client>().ToTable("Clients");
            builder.Entity<Client>().HasKey(cl => cl.id);
            builder.Entity<Loan>().ToTable("Loans");
            builder.Entity<Loan>().HasKey(lo => lo.id);
            builder.Entity<Loan>().HasOne(lo => lo.Library)
                .WithMany(li => li.Loans)
                .HasForeignKey(lo => lo.idLibrary)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Loan>().HasOne(lo => lo.Client)
                .WithMany(cl => cl.Loans)
                .HasForeignKey(lo => lo.idClient)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
