using HackerRank1.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HackerRank1.Infrastructure.Data;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    { }

    public DbSet<Library> Libraries => Set<Library>();

    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Library>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.Name).IsRequired();
            entity.Property(l => l.Location).IsRequired();
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Name).IsRequired();
            entity.Property(b => b.Category).IsRequired();
            entity.HasOne<Library>()
                .WithMany()
                .HasForeignKey(b => b.LibraryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });
    }
}
