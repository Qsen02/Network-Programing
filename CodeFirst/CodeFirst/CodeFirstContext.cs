using CodeFirst.Entity;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst
{
    public class CodeFirstContext:DbContext
    {
        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Biography> Biographies { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasOne(a => a.Biography)
                .WithOne(b => b.Author)
                .HasForeignKey<Biography>(b => b.AuthorId);

            modelBuilder.Entity<Company>()
              .HasMany(a => a.Employees)
              .WithOne(b => b.Company)
              .HasForeignKey(b => b.CompanyId);

            modelBuilder.Entity<BookCategory>().HasKey(bc => new { bc.BookId, bc.CategoryId });

            modelBuilder.Entity<BookCategory>()
              .HasOne(bc => bc.Book)
              .WithMany(b => b.BookCategories)
              .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<BookCategory>()
              .HasOne(bc => bc.Category)
              .WithMany(b => b.BookCategories)
              .HasForeignKey(b => b.CategoryId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            options.UseLazyLoadingProxies();
        }
    }
}
