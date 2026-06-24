using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }


public DbSet<Book> Books => Set<Book>();

    public DbSet<Author> Authors => Set<Author>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<PublishingHouse> PublishingHouses => Set<PublishingHouse>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Book>(entity =>
        {
            entity.Property(book => book.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(book => book.Description)
                .IsRequired()
                .HasMaxLength(2000);

            entity.Property(book => book.Price)
                .HasColumnType("decimal(18,2)");

            entity.HasOne(book => book.Author)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(book => book.Category)
                .WithMany(category => category.Books)
                .HasForeignKey(book => book.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(book => book.PublishingHouse)
                .WithMany(publishingHouse => publishingHouse.Books)
                .HasForeignKey(book => book.PublishingHouseId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Author>(entity =>
        {
            entity.Property(author => author.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(author => author.Bio)
                .IsRequired()
                .HasMaxLength(2000);
        });

        builder.Entity<Category>(entity =>
        {
            entity.Property(category => category.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(category => category.Name)
                .IsUnique();
        });

        builder.Entity<PublishingHouse>(entity =>
        {
            entity.Property(publishingHouse => publishingHouse.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(publishingHouse => publishingHouse.Address)
                .IsRequired()
                .HasMaxLength(300);
        });
    }

}
