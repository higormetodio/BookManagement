using BookManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Infrastructure.EntitiesConfiguration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
               .ValueGeneratedOnAdd()
               .UseIdentityColumn();

        builder.Property(u => u.Name)
               .IsRequired()
               .HasColumnType("NVARCHAR")
               .HasMaxLength(100);

        builder.Property(u => u.Email)
               .IsRequired()
               .HasColumnType("NVARCHAR")
               .HasMaxLength(50);

        builder.Property(u => u.BirthDate)
               .IsRequired()
               .HasColumnType("SMALLDATETIME");

        builder.Property(u => u.Active)
               .IsRequired()
               .HasColumnType("BIT");

        builder.Property(u => u.Password)
               .IsRequired()
               .HasColumnType("NVARCHAR(MAX)");

        builder.Property(u => u.Role)
               .IsRequired()
               .HasColumnType("NVARCHAR")
               .HasMaxLength(20);

        builder.HasIndex(u => u.Name)
               .HasDatabaseName("IX_Users_Name")
               .IsUnique();

        builder.HasIndex(u => u.Email)
               .HasDatabaseName("IX_Users_Email")
               .IsUnique();
    }
}
