using DataAccess_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess_Layer
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<AdminModel> Admins { get; set; }

        public DbSet<BookModel> Books { get; set; }

        public DbSet<CartModel> Carts { get; set; }

        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("books");

               

                entity.Property(e => e.AdminUserId)
                    .HasMaxLength(50)
                    .HasColumnName("admin_user_id");

                entity.Property(e => e.Author)
                    .HasMaxLength(50)
                    .HasColumnName("author");

                entity.Property(e => e.BookImage)
                    .HasMaxLength(300)
                    .HasColumnName("bookImage");

                entity.Property(e => e.BookName)
                    .HasMaxLength(50)
                    .HasColumnName("bookName");

                entity.Property(e => e.CreatedAtDate)
                    .HasMaxLength(50)
                    .HasColumnName("createdAt_date");

                entity.Property(e => e.Description)
                    .HasMaxLength(750)
                    .HasColumnName("description");

                entity.Property(e => e.DiscountPrice).HasColumnName("discountPrice");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.UpdatedAtDate)
                    .HasMaxLength(50)
                    .HasColumnName("updatedAt_date");

               
            });
        }



    }
}
