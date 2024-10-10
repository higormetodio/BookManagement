using BookManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Infrastructure.Persistence.Configuration;
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
               .ValueGeneratedOnAdd()
               .UseIdentityColumn();

        builder.Property(b => b.Title)
               .IsRequired()
               .HasColumnType("NVARCHAR")
               .HasMaxLength(200);

        builder.Property(b => b.Author)
               .IsRequired()
               .HasColumnType("NVARCHAR")
               .HasMaxLength(100);

        builder.Property(b => b.ISBN)
               .IsRequired()
               .HasColumnType("NVARCHAR")
               .HasMaxLength(20);

        builder.Property(b => b.PublicationYear)
               .IsRequired()
               .HasColumnType("INT");

        builder.Property(b => b.IsReserved)
               .IsRequired()
               .HasColumnType("BIT");

        builder.Property(b => b.Active)
               .IsRequired()
               .HasColumnType("BIT");

        builder.HasIndex(b => b.ISBN)
               .HasName("IX_Book_ISBN")
               .IsUnique();
    }
}
