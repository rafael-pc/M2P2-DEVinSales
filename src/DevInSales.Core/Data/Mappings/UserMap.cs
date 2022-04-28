using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DevInSales.Core.Entities;

namespace DevInSales.Core.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .HasColumnType("varchar(150)")
                .IsUnicode(false)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnType("varchar(50)")
                .IsUnicode(false)
                .IsRequired();
            builder.Property(u => u.BirthDate)
                .HasColumnType("date")
                .IsRequired();

        }
    }

}
