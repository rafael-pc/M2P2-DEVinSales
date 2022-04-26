using DevInSales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Data.Mappings
{
    public class StateMap : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("States");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Initials)
                .HasMaxLength(2)
                .IsUnicode(false);
        }
    }
}