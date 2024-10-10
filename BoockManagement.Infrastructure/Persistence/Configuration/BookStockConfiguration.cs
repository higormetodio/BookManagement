using BookManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Infrastructure.Persistence.Configuration;
public class BookStockConfiguration : IEntityTypeConfiguration<BookStock>
{
    public void Configure(EntityTypeBuilder<BookStock> builder)
    {
        builder.ToTable("BookStock");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
               .ValueGeneratedOnAdd()
               .UseIdentityColumn();

        builder.Property(s => s.Quantity)
               .IsRequired()
               .HasColumnType("INT");

        builder.Property(s => s.LoanQuantity)
               .IsRequired()
               .HasColumnType("INT");

        builder.Property(s => s.BookId)
               .IsRequired()
               .HasColumnType("INT");

        builder.HasOne(s => s.Book)
               .WithOne(b => b.Stock)
               .HasForeignKey<BookStock>(s => s.BookId)
               .HasConstraintName("FK_BookStocks_Book")
               .OnDelete(DeleteBehavior.Restrict);
    }
}
