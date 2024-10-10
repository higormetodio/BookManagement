using BookManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Infrastructure.Persistence.Configuration;
public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.ToTable("Loans");

        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id)
               .ValueGeneratedOnAdd()
               .UseIdentityColumn();

        builder.Property(l => l.UserId)
               .IsRequired()
               .HasColumnType("INT");

        builder.Property(l => l.BookId)
               .IsRequired()
               .HasColumnType("INT");

        builder.Property(l => l.LoanDate)
               .IsRequired()
               .HasColumnType("SMALLDATETIME")
               .HasMaxLength(50);

        builder.Property(l => l.ReturnDate)
               .IsRequired()
               .HasColumnType("SMALLDATETIME")
               .HasMaxLength(50);

        builder.Property(l => l.Status)
               .IsRequired()
               .HasColumnType("INT");

        builder.HasOne(l => l.Book)
               .WithMany(b => b.Loans)
               .HasForeignKey(l => l.BookId)
               .HasConstraintName("FK_Loan_Book")
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.User)
               .WithMany(u => u.Loans)
               .HasForeignKey(l => l.UserId)
               .HasConstraintName("FK_Loans_User")
               .OnDelete(DeleteBehavior.Restrict);
    }
}
